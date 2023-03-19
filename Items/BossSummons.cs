﻿using CalamityQOL.Config;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityQOL.Items;

public class BossSummons : GlobalItem {
    public override void SetDefaults(Item item) {
        if (QOLConfig.Instance.nonConsumableSummons &&
            item.type is ItemID.SlimeCrown or ItemID.SuspiciousLookingEye or ItemID.BloodMoonStarter
                or ItemID.GoblinBattleStandard or ItemID.WormFood or ItemID.BloodySpine or ItemID.Abeemination
                or ItemID.DeerThing or ItemID.QueenSlimeCrystal or ItemID.PirateMap or ItemID.SnowGlobe
                or ItemID.MechanicalEye or ItemID.MechanicalWorm or ItemID.MechanicalSkull or ItemID.NaughtyPresent
                or ItemID.PumpkinMoonMedallion or ItemID.SolarTablet or ItemID.SolarTablet or ItemID.CelestialSigil) {
            item.consumable = false;
        }
    }
}