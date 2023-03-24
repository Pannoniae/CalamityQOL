using CalamityQOL.Config;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
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

    public static void load() {
        if (QOLConfig.Instance.wellFedPatch) {
            IL.Terraria.Player.UpdateLifeRegen += wellFedPatch;
        }

        if (QOLConfig.Instance.townNPCsAtNight) {
            IL.Terraria.Main.UpdateTime += townNPCPatch;
        }
    }

    public static void unload() {
        IL.Terraria.Player.UpdateLifeRegen -= wellFedPatch;
        IL.Terraria.Main.UpdateTime -= townNPCPatch;
    }
}