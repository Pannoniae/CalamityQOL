using CalamityQOL.Config;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityQOL.Items;

public class QOLItem : GlobalItem {
    public override void SetDefaults(Item item) {
        if (QOLConfig.Instance.nonConsumableSummons &&
            item.type is ItemID.SlimeCrown or ItemID.SuspiciousLookingEye or ItemID.BloodMoonStarter
                or ItemID.GoblinBattleStandard or ItemID.WormFood or ItemID.BloodySpine or ItemID.Abeemination
                or ItemID.DeerThing or ItemID.QueenSlimeCrystal or ItemID.PirateMap or ItemID.SnowGlobe
                or ItemID.MechanicalEye or ItemID.MechanicalWorm or ItemID.MechanicalSkull or ItemID.NaughtyPresent
                or ItemID.PumpkinMoonMedallion or ItemID.SolarTablet or ItemID.SolarTablet or ItemID.CelestialSigil) {
            item.consumable = false;
        }

        if (QOLConfig.Instance.moreAutoSwing &&
            item.type == ItemID.ZombieArm ||
            item.type == ItemID.YellowPhaseblade || 
            item.type == ItemID.XenoStaff ||
            item.type == ItemID.WoodenSword || 
            item.type == ItemID.WhitePhaseblade ||
            item.type == ItemID.WandofSparking ||
            item.type == ItemID.VenusMagnum ||
            item.type == ItemID.VampireFrogStaff || 
            item.type == ItemID.TungstenShortsword ||
            item.type == ItemID.TungstenBroadsword ||
            item.type == ItemID.TrueNightsEdge || 
            item.type == ItemID.TrueExcalibur || 
            item.type == ItemID.Trident ||
            item.type == ItemID.TitaniumTrident ||
            item.type == ItemID.TinShortsword ||
            item.type == ItemID.TinBroadsword || 
            item.type == ItemID.ThunderSpear ||
            item.type == ItemID.TheUndertaker || 
            item.type == ItemID.TheRottedFork ||
            item.type == ItemID.Terrarian ||
            item.type == ItemID.TendonBow || 
            item.type == ItemID.TempestStaff ||
            item.type == ItemID.TaxCollectorsStickOfDoom || 
            item.type == ItemID.Swordfish ||
            item.type == ItemID.StylistKilLaKillScissorsIWish ||
            item.type == ItemID.StormTigerStaff || 
            item.type == ItemID.StardustDragonStaff ||
            item.type == ItemID.StardustCellStaff || 
            item.type == ItemID.SpiderStaff || 
            item.type == ItemID.Spear ||
            item.type == ItemID.Smolstar || 
            item.type == ItemID.SlimeStaff || 
            item.type == ItemID.SilverShortsword ||
            item.type == ItemID.SilverBroadsword || 
            item.type == ItemID.Shotgun ||
            item.type == ItemID.ShadewoodSword ||
            item.type == ItemID.SanguineStaff ||
            item.type == ItemID.SapphireStaff || 
            item.type == ItemID.RichMahoganySword ||
            item.type == ItemID.Revolver ||
            item.type == ItemID.RedPhaseblade || 
            item.type == ItemID.RavenStaff || 
            item.type == ItemID.Rally ||
            item.type == ItemID.PygmyStaff || 
            item.type == ItemID.PurplePhaseblade ||
            item.type == ItemID.PlatinumShortsword || 
            item.type == ItemID.PlatinumBroadsword ||
            item.type == ItemID.PirateStaff || 
            item.type == ItemID.PhoenixBlaster ||
            item.type == ItemID.PearlwoodSword ||
            item.type == ItemID.PearlwoodBow || 
            item.type == ItemID.PalmWoodSword ||
            item.type == ItemID.PalladiumPike || 
            item.type == ItemID.PaladinsHammer ||
            item.type == ItemID.OrichalcumHalberd ||
            item.type == ItemID.OrangePhaseblade || 
            item.type == ItemID.OpticStaff ||
            item.type == ItemID.ObsidianSwordfish || 
            item.type == ItemID.NorthPole || 
            item.type == ItemID.NightsEdge ||
            item.type == ItemID.MythrilHalberd || 
            item.type == ItemID.MushroomSpear ||
            item.type == ItemID.MonkStaffT2 || 
            item.type == ItemID.MoltenFury || 
            item.type == ItemID.Marrow ||
            item.type == ItemID.MagicDagger || 
            item.type == ItemID.LightsBane || 
            item.type == ItemID.LeadShortsword ||
            item.type == ItemID.LeadBroadsword || 
            item.type == ItemID.IronShortsword ||
            item.type == ItemID.IronBroadsword || 
            item.type == ItemID.ImpStaff ||
            item.type == ItemID.IceSickle ||
            item.type == ItemID.IceBow || 
            item.type == ItemID.HornetStaff ||
            item.type == ItemID.FlowerofFrost || 
            item.type == ItemID.FlowerofFire || 
            item.type == ItemID.FlinxStaff ||
            item.type == ItemID.FieryGreatsword || 
            item.type == ItemID.EmpressBlade ||
            item.type == ItemID.EbonwoodSword || 
            item.type == ItemID.DyeTradersScimitar ||
            item.type == ItemID.DemonScythe || 
            item.type == ItemID.DemonBow || 
            item.type == ItemID.DeadlySphereStaff ||
            item.type == ItemID.DD2LightningAuraT3Popper || 
            item.type == ItemID.DD2LightningAuraT2Popper ||
            item.type == ItemID.DD2FlameburstTowerT3Popper || 
            item.type == ItemID.DD2FlameburstTowerT2Popper ||
            item.type == ItemID.DD2ExplosiveTrapT3Popper ||
            item.type == ItemID.DD2ExplosiveTrapT2Popper || 
            item.type == ItemID.DD2BallistraTowerT3Popper ||
            item.type == ItemID.DD2BallistraTowerT2Popper ||
            item.type == ItemID.DarkLance ||
            item.type == ItemID.CrimsonYoyo ||
            item.type == ItemID.CorruptYoyo ||
            item.type == ItemID.CopperShortsword ||
            item.type == ItemID.CopperBroadsword ||
            item.type == ItemID.Code1 || 
            item.type == ItemID.CobaltNaginata ||
            item.type == ItemID.ChristmasTreeSword || 
            item.type == ItemID.ChlorophytePartisan ||
            item.type == ItemID.ChainKnife || 
            item.type == ItemID.Cascade || 
            item.type == ItemID.CandyCaneSword ||
            item.type == ItemID.CactusSword ||
            item.type == ItemID.BreakerBlade ||
            item.type == ItemID.BorealWoodSword || 
            item.type == ItemID.BoneSword ||
            item.type == ItemID.BluePhaseblade ||
            item.type == ItemID.BloodyMachete || 
            item.type == ItemID.BloodButcherer ||
            item.type == ItemID.BladeofGrass || 
            item.type == ItemID.BeamSword || 
            item.type == ItemID.BabyBirdStaff ||
            item.type == ItemID.AdamantiteGlaive ||
            item.type == ItemID.ZapinatorOrange ||
            item.type == ItemID.ZapinatorGray || 
            item.type == ItemID.Yelets || 
            item.type == ItemID.WoodYoyo ||
            item.type == ItemID.WeatherPain || 
            item.type == ItemID.ValkyrieYoyo ||
            item.type == ItemID.Umbrella ||
            item.type == ItemID.TragicUmbrella ||
            item.type == ItemID.ThornWhip ||
            item.type == ItemID.TheEyeOfCthulhu ||
            item.type == ItemID.TentacleSpike || 
            item.type == ItemID.SwordWhip ||
            item.type == ItemID.Starfury || 
            item.type == ItemID.ScytheWhip ||
            item.type == ItemID.RedsYoyo || 
            item.type == ItemID.RainbowWhip || 
            item.type == ItemID.PaperAirplaneB ||
            item.type == ItemID.PaperAirplaneA ||
            item.type == ItemID.ManaCrystal ||
            item.type == ItemID.MaceWhip || 
            item.type == ItemID.LifeFruit || 
            item.type == ItemID.LifeCrystal ||
            item.type == ItemID.BlandWhip ||
            item.type == ItemID.Kraken ||
            item.type == ItemID.JungleYoyo || 
            item.type == ItemID.HelFire || 
            item.type == ItemID.Gradient ||
            item.type == ItemID.FormatC || 
            item.type == ItemID.FireWhip ||
            item.type == ItemID.Code2 || 
            item.type == ItemID.Chik || 
            item.type == ItemID.BoneWhip ||
            item.type == ItemID.BatBat || 
            item.type == ItemID.Amarok ||
            item.type == ItemID.AbigailsFlower) {
            item.autoReuse = true;
        }
    }
}