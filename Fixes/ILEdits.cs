using CalamityQOL.Config;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityQOL.Fixes;

public class ILEdits : ModSystem {
    private static void wellFedPatch(ILContext il) {
        int bullshitVariable;
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => {
                bullshitVariable = 0;
                return i.MatchLdfld<Player>("wellFed");
            })) {
            ilCursor.Emit(OpCodes.Ldc_I4_1);
            ilCursor.Emit(OpCodes.Or);
        }
        else {
            CalamityQOLMod.i.Logger.Warn("Failed to locate Well Fed");
        }
    }

    //IL_0684: stloc.1      // start
    //IL_0685: ldsfld       bool Terraria.Main::dayTime
    //IL_068a: brtrue       IL_0975
    //IL_068f: ldc.i4.0
    //IL_0690: stsfld       bool Terraria.Main::eclipse
    private static void townNPCPatch(ILContext il) {
        // so after the !Main.daytime check, we call UpdateTime_SpawnTownNPCs() anyway
        int bullshitVariable;
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => {
                bullshitVariable = 0;
                return i.MatchStsfld<Main>("eclipse");
            })) {
            // call         void Terraria.Main::UpdateTime_SpawnTownNPCs()
            ilCursor.Emit<Main>(OpCodes.Call, "UpdateTime_SpawnTownNPCs");
        }
        else {
            CalamityQOLMod.i.Logger.Warn("Failed to locate daytime check (Main.eclipse)");
        }
    }
    
    //IL_14f7: ldloca.s     button
    //IL_14f9: ldloca.s     button2
    //IL_14fb: call         void Terraria.ModLoader.NPCLoader::SetChatButtons(string&, string&)
    private static void NPCButtonPatch(ILContext il) {
        // local indexes for the two buttons
        int button1idx = 11;
        int button2idx = 12;
        int bullshitVariable;
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => {
                bullshitVariable = 0;
                return i.MatchCall(typeof(NPCLoader), "SetChatButtons");
            })) {
            // call         void Terraria.Main::UpdateTime_SpawnTownNPCs()
            ilCursor.Emit(OpCodes.Ldloca, button1idx);
            ilCursor.Emit(OpCodes.Ldloca, button2idx);
            ilCursor.Emit<QOLHooks>(OpCodes.Call, "SetChatButtons");
        }
        else {
            CalamityQOLMod.i.Logger.Warn("Failed to locate tModLoader SetChatButtons (in Main.GUIChatDrawInner)");
        }
    }
    

    public static void load() {
        if (QOLConfig.Instance.wellFedPatch) {
            IL_Player.UpdateLifeRegen += wellFedPatch;
        }

        if (QOLConfig.Instance.townNPCsAtNight) {
            IL_Main.UpdateTime += townNPCPatch;
        }
        
        IL_Main.GUIChatDrawInner += NPCButtonPatch;
    }

    public static void unload() {
        IL_Player.UpdateLifeRegen -= wellFedPatch;
        IL_Main.UpdateTime -= townNPCPatch;
        IL_Main.GUIChatDrawInner -= NPCButtonPatch;
    }
}

public class QOLHooks {
    private static void SetChatButtons(ref string button, ref string button2) {
        //SetChatButtons(Main.npc[Main.player[Main.myPlayer].talkNPC], ref button, ref button2);
        
        // An empty method for now, we aren't using it
    }

}