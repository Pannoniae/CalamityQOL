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
            Console.Out.WriteLine("Failed to locate Well Fed");
        }
    }

    public static void load() {
        if (QOLConfig.Instance.wellFedPatch) {
            IL_Player.UpdateLifeRegen += wellFedPatch;
        }
    }

    public static void unload() {
        IL_Player.UpdateLifeRegen -= wellFedPatch;
    }
}