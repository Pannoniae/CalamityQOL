using CalamityQOL.Config;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityQOL.Items;

public class QOLItem : GlobalItem {

    public override bool IsLoadingEnabled(Mod mod) {
        // if calamity is loaded, we have zero business here
        return CalamityQoL.i.calamity is null;
    }
    public override void SetDefaults(Item item) {
        if (QoLConfig.Instance.nonConsumableSummons &&
            item.type is ItemID.SlimeCrown or ItemID.SuspiciousLookingEye or ItemID.BloodMoonStarter
                or ItemID.GoblinBattleStandard or ItemID.WormFood or ItemID.BloodySpine or ItemID.Abeemination
                or ItemID.DeerThing or ItemID.QueenSlimeCrystal or ItemID.PirateMap or ItemID.SnowGlobe
                or ItemID.MechanicalEye or ItemID.MechanicalWorm or ItemID.MechanicalSkull or ItemID.NaughtyPresent
                or ItemID.PumpkinMoonMedallion or ItemID.SolarTablet or ItemID.SolarTablet or ItemID.CelestialSigil) {
            item.consumable = false;
        }
    }
}