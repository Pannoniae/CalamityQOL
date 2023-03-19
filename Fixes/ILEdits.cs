﻿using System;
using CalamityQOL.Config;
using Mono.Cecil;
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
            CalamityQOLMod.i.Logger.Warn("Failed to locate Well Fed");
        }
    }
    
    
    //IL_0685: ldsfld       bool Terraria.Main::dayTime
    //IL_068a: brtrue       IL_0975
    private static void townNPCPatch(ILContext il) {

        // so after the !Main.daytime check, we call UpdateTime_SpawnTownNPCs() anyway
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdsfld<Main>("dayTime"))) {
            // skip brtrue
            ilCursor.Index += 1;
            
            // call         void Terraria.Main::UpdateTime_SpawnTownNPCs()
            ilCursor.Emit<Main>(OpCodes.Call, "UpdateTime_SpawnTownNPCs");
        }
        else {
            CalamityQOLMod.i.Logger.Warn("Failed to locate daytime check");
        }
    }

    public static void load() {
        if (QOLConfig.Instance.wellFedPatch) {
            IL_Player.UpdateLifeRegen += wellFedPatch;
        }

        if (QOLConfig.Instance.townNPCsAtNight) {
            IL_Main.UpdateTime += townNPCPatch;
        }
    }

    public static void unload() {
        IL_Player.UpdateLifeRegen -= wellFedPatch;
        IL_Main.UpdateTime -= townNPCPatch;
    }
}