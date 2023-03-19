using CalamityQOL.Config;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityQOL;

public class NPCShop : GlobalNPC {
    public override void SetupShop(int type, Chest shop, ref int nextSlot) {
        // 2 gold at start, 4 in hardmode and 8 post-ML
        int cost = 2;
        if (Main.hardMode) {
            cost *= 2;
        }

        if (NPC.downedMoonlord) {
            cost *= 2;
        }

        switch (type) {
            case NPCID.Merchant:
                addToShop(ref shop, ref nextSlot, ItemID.Bottle,
                    Item.buyPrice(0, 0, 0, 20));
                addToShop(ref shop, ref nextSlot, ItemID.WormholePotion,
                    Item.buyPrice(0, 0, 25, 0));
                addToShop(ref shop, ref nextSlot, ItemID.ArcheryPotion,
                    Item.buyPrice(0, cost, 0, 0), NPC.downedBoss1);
                addToShop(ref shop, ref nextSlot, ItemID.HealingPotion, cond: NPC.downedBoss2);
                addToShop(ref shop, ref nextSlot, ItemID.ManaPotion, cond: NPC.downedBoss2);
                addToShop(ref shop, ref nextSlot, ItemID.TitanPotion,
                    Item.buyPrice(0, 2, 0, 0), NPC.downedBoss3);
                addToShop(ref shop, ref nextSlot, ItemID.ApprenticeBait, cond: NPC.downedBoss1);
                addToShop(ref shop, ref nextSlot, ItemID.JourneymanBait, cond: NPC.downedBoss3);
                // haha funny
                addToShop(ref shop, ref nextSlot, ItemID.MasterBait, cond: NPC.downedPlantBoss);
                addToShop(ref shop, ref nextSlot, ItemID.Burger, Item.buyPrice(0, 5, 0, 0),
                    NPC.downedBoss3);

                addToShop(ref shop, ref nextSlot, ItemID.Hotdog,
                    Item.buyPrice(0, 5, 0, 0), Main.hardMode);
                addToShop(ref shop, ref nextSlot, ItemID.CoffeeCup, Item.buyPrice(0, 2, 0, 0));
                break;
            case NPCID.Demolitionist:
                addToShop(ref shop, ref nextSlot, ItemID.MiningPotion,
                    Item.buyPrice(0, 1, 0, 0), NPC.downedBoss1);
                addToShop(ref shop, ref nextSlot, ItemID.IronskinPotion,
                    Item.buyPrice(0, 1, 0, 0), NPC.downedBoss1);
                addToShop(ref shop, ref nextSlot, ItemID.ShinePotion,
                    Item.buyPrice(0, 1, 0, 0), NPC.downedBoss1);
                addToShop(ref shop, ref nextSlot, ItemID.SpelunkerPotion,
                    Item.buyPrice(0, 2, 0, 0), NPC.downedBoss2);
                addToShop(ref shop, ref nextSlot, ItemID.ObsidianSkinPotion,
                    Item.buyPrice(0, 2, 0, 0), NPC.downedBoss3);
                addToShop(ref shop, ref nextSlot, ItemID.EndurancePotion,
                    Item.buyPrice(0, cost, 0, 0), NPC.downedBoss3);
                break;
            case NPCID.DyeTrader:
                addToShop(ref shop, ref nextSlot, ItemID.DyeTradersScimitar, price: Item.buyPrice(0, 15, 0, 0));
                break;
            case NPCID.ArmsDealer:
                addToShop(ref shop, ref nextSlot, ItemID.Boomstick,
                    Item.buyPrice(0, 20, 0, 0), NPC.downedQueenBee);
                addToShop(ref shop, ref nextSlot, ItemID.AmmoBox,
                    Item.buyPrice(0, 25, 0, 0), Main.hardMode);
                addToShop(ref shop, ref nextSlot, ItemID.Uzi,
                    Item.buyPrice(0, 45, 0, 0), NPC.downedPlantBoss);
                addToShop(ref shop, ref nextSlot, ItemID.TacticalShotgun,
                    Item.buyPrice(0, 60, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.SniperRifle,
                    Item.buyPrice(0, 60, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.RifleScope,
                    Item.buyPrice(0, 60, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.AmmoReservationPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.HunterPotion,
                    Item.buyPrice(0, 2, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.BattlePotion,
                    Item.buyPrice(0, 2, 0, 0), NPC.downedBoss2);
                break;
            case NPCID.Stylist:
                addToShop(ref shop, ref nextSlot, ItemID.ChocolateChipCookie,
                    Item.buyPrice(0, 3, 0, 0));
                break;
            case NPCID.Cyborg:
                addToShop(ref shop, ref nextSlot, ItemID.RocketLauncher,
                    Item.buyPrice(0, 25, 0, 0), NPC.downedGolemBoss);
                break;
            case NPCID.Dryad:
                addToShop(ref shop, ref nextSlot, ItemID.ThornsPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.FeatherfallPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.RegenerationPotion,
                    Item.buyPrice(0, 2, 0, 0), NPC.downedBoss2);
                addToShop(ref shop, ref nextSlot, ItemID.SwiftnessPotion,
                    Item.buyPrice(0, cost, 0, 0), NPC.downedBoss2);
                addToShop(ref shop, ref nextSlot, ItemID.JungleRose, Item.buyPrice(0, 2, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.NaturesGift, Item.buyPrice(0, 10, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.Grapes,
                    Item.buyPrice(0, 2, 50, 0), NPC.downedBoss3);
                break;
            case NPCID.GoblinTinkerer:
                addToShop(ref shop, ref nextSlot, ItemID.StinkPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.Spaghetti,
                    Item.buyPrice(0, 5, 0, 0), NPC.downedBoss3);
                break;
            case NPCID.Mechanic:
                addToShop(ref shop, ref nextSlot, ItemID.BuilderPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.CombatWrench, Item.buyPrice(0, 10, 0, 0));
                break;
            case NPCID.Clothier:
                addToShop(ref shop, ref nextSlot, ItemID.GoldenKey, Item.buyPrice(0, 5, 0, 0), Main.hardMode);
                break;
            case NPCID.Painter:
                addToShop(ref shop, ref nextSlot, ItemID.PainterPaintballGun, price: Item.buyPrice(0, 15, 0, 0));
                break;
            case NPCID.Steampunker:
                addToShop(ref shop, ref nextSlot, ItemID.PurpleSolution,
                    Item.buyPrice(0, 0, 5, 0), Main.LocalPlayer.ZoneGraveyard && WorldGen.crimson);
                addToShop(ref shop, ref nextSlot, ItemID.RedSolution,
                    Item.buyPrice(0, 0, 5, 0), Main.LocalPlayer.ZoneGraveyard && !WorldGen.crimson);
                break;
            case NPCID.Wizard:
                addToShop(ref shop, ref nextSlot, ItemID.ManaRegenerationPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.MagicPowerPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.GravitationPotion,
                    Item.buyPrice(0, 2, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.PotionOfReturn,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.MagicMissile, Item.buyPrice(0, 5, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.RodofDiscord,
                    Item.buyPrice(10, 0, 0, 0), Main.hardMode && Main.LocalPlayer.ZoneHallow);
                addToShop(ref shop, ref nextSlot, ItemID.SpectreStaff,
                    Item.buyPrice(0, 25, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.InfernoFork,
                    Item.buyPrice(0, 25, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.ShadowbeamStaff,
                    Item.buyPrice(0, 25, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.MagnetSphere,
                    Item.buyPrice(0, 25, 0, 0), NPC.downedGolemBoss);
                break;
            case NPCID.WitchDoctor:
                addToShop(ref shop, ref nextSlot, ItemID.SummoningPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.CalmingPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.RagePotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.WrathPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.InfernoPotion,
                    Item.buyPrice(0, 2, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.ButterflyDust,
                    Item.buyPrice(0, 10, 0, 0), NPC.downedGolemBoss);
                addToShop(ref shop, ref nextSlot, ItemID.FriedEgg, Item.buyPrice(0, 2, 50, 0));
                break;
            case NPCID.PartyGirl:
                addToShop(ref shop, ref nextSlot, ItemID.GenderChangePotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.Pizza,
                    Item.buyPrice(0, 5, 0, 0), NPC.downedBoss3);
                addToShop(ref shop, ref nextSlot, ItemID.CreamSoda, Item.buyPrice(0, 2, 50, 0));
                break;
            case NPCID.Princess:
                addToShop(ref shop, ref nextSlot, ItemID.PrincessWeapon, Item.buyPrice(0, 50, 0, 0));
                addToShop(ref shop, ref nextSlot, ItemID.AppleJuice);
                addToShop(ref shop, ref nextSlot, ItemID.FruitJuice);
                addToShop(ref shop, ref nextSlot, ItemID.Lemonade);
                addToShop(ref shop, ref nextSlot, ItemID.PrismaticPunch);
                addToShop(ref shop, ref nextSlot, ItemID.SmoothieofDarkness);
                addToShop(ref shop, ref nextSlot, ItemID.TropicalSmoothie);
                break;
            case NPCID.SkeletonMerchant:
                addToShop(ref shop, ref nextSlot, ItemID.MilkCarton);
                addToShop(ref shop, ref nextSlot, ItemID.Marrow, Item.buyPrice(0, 36, 0, 0),
                    Main.hardMode);
                break;
            case NPCID.Golfer:
                addToShop(ref shop, ref nextSlot, ItemID.PotatoChips, Item.buyPrice(0, 1, 0, 0));
                break;
            // Zoologist
            case NPCID.BestiaryGirl:
                addToShop(ref shop, ref nextSlot, ItemID.Steak, Item.buyPrice(0, 5, 0, 0), Main.hardMode);
                break;
        }
    }

    private void addToShop(ref Chest shop, ref int nextSlot, int itemID, int? price = null, bool cond = true) {
        if (!cond) {
            return;
        }

        if (!QOLConfig.Instance.sellAdditionalItems) {
            return;
        }

        shop.item[nextSlot].SetDefaults(itemID);
        if (price != null) {
            shop.item[nextSlot].shopCustomPrice = price;
        }

        nextSlot++;
    }
}