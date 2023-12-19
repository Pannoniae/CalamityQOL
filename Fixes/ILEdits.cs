using System;
using CalamityQOL.Config;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityQOL.Fixes;

public class ILEdits : ModSystem {
    private static void wellFedPatch(ILContext il) {
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("wellFed"))) {
            ilCursor.Emit(OpCodes.Ldc_I4_1);
            ilCursor.Emit(OpCodes.Or);
        }
        else {
            CalamityQOL.i.Logger.Warn("Failed to locate Well Fed");
        }
    }

    //IL_0684: stloc.1      // start
    //IL_0685: ldsfld       bool Terraria.Main::dayTime
    //IL_068a: brtrue       IL_0975
    //IL_068f: ldc.i4.0
    //IL_0690: stsfld       bool Terraria.Main::eclipse
    private static void townNPCPatch(ILContext il) {
        // so after the !Main.daytime check, we call UpdateTime_SpawnTownNPCs() anyway
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchStsfld<Main>("eclipse"))) {
            // call         void Terraria.Main::UpdateTime_SpawnTownNPCs()
            ilCursor.Emit<Main>(OpCodes.Call, "UpdateTime_SpawnTownNPCs");
        }
        else {
            CalamityQOL.i.Logger.Warn("Failed to locate daytime check (Main.eclipse)");
        }
    }

    //IL_14f7: ldloca.s     button
    //IL_14f9: ldloca.s     button2
    //IL_14fb: call         void Terraria.ModLoader.NPCLoader::SetChatButtons(string&, string&)
    private static void NPCButtonPatch(ILContext il) {
        // local indexes for the two buttons
        int button1idx = 11;
        int button2idx = 12;
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchCall(typeof(NPCLoader), "SetChatButtons"))) {
            // call         void Terraria.Main::UpdateTime_SpawnTownNPCs()
            ilCursor.Emit(OpCodes.Ldloca, button1idx);
            ilCursor.Emit(OpCodes.Ldloca, button2idx);
            ilCursor.Emit<QOLHooks>(OpCodes.Call, "SetChatButtons");
        }
        else {
            CalamityQOL.i.Logger.Warn("Failed to locate tModLoader SetChatButtons (in Main.GUIChatDrawInner)");
        }
    }

    private const float VanillaBaseJumpHeight = 5.01f;

    private static void BaseJumpHeightAdjustment(ILContext il) {
        // Increase the base jump height of the player to make early game less of a slog.
        var cursor = new ILCursor(il);

        // The jumpSpeed variable is set to this specific value before anything else occurs.
        if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(VanillaBaseJumpHeight))) {
            CalamityQOL.i.Logger.Warn("[Base Jump Height Buff] Could not locate the jump height variable.");
            return;
        }

        cursor.Remove();

        // Increase by 10% if the higher jump speed is enabled.
        cursor.EmitDelegate<Func<float>>(() =>
            QoLConfig.Instance.HigherJumpHeight
                ? 5.51f
                : VanillaBaseJumpHeight);
    }


    public static void load() {
        if (QoLConfig.Instance.wellFedPatch) {
            IL_Player.UpdateLifeRegen += wellFedPatch;
        }

        IL_Player.Update += BaseJumpHeightAdjustment;

        // don't load if we have the better town NPC patch
        if (QoLConfig.Instance.townNPCsAtNight && CalamityQOL.i.vanillaQoL is null) {
            IL_Main.UpdateTime += townNPCPatch;
        }

        IL_Main.GUIChatDrawInner += NPCButtonPatch;
    }

    public static void unload() {
        IL_Player.UpdateLifeRegen -= wellFedPatch;
        IL_Main.UpdateTime -= townNPCPatch;
        IL_Main.GUIChatDrawInner -= NPCButtonPatch;
        IL_Player.Update -= BaseJumpHeightAdjustment;
    }
}

public class QOLHooks {
    private static void SetChatButtons(ref string button, ref string button2) {
        //SetChatButtons(Main.npc[Main.player[Main.myPlayer].talkNPC], ref button, ref button2);

        // An empty method for now, we aren't using it
    }
}