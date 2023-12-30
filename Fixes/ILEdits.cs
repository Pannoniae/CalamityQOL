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
    }

    public static void unload() {
        IL_Player.UpdateLifeRegen -= wellFedPatch;
        IL_Player.Update -= BaseJumpHeightAdjustment;
    }
}