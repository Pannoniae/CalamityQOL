﻿using System;
using CalamityQOL.Config;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityQOL;

public class NPCshop : GlobalNPC {
    public override void ModifyShop(NPCShop shop) {
        // 2 gold at start, 4 in hardmode and 8 post-ML
        int cost = 2;
        if (Main.hardMode) {
            cost *= 2;
        }
        Recipe.FindRecipes();

        if (NPC.downedMoonlord) {
            cost *= 2;
        }

        switch (shop.NpcType) {
            case NPCID.Merchant:
                addToShop(shop, ItemID.Bottle,
                    Item.buyPrice(0, 0, 0, 20));
                addToShop(shop, ItemID.WormholePotion,
                    Item.buyPrice(0, 0, 25, 0));
                addToShop(shop, ItemID.ArcheryPotion,
                    Item.buyPrice(0, cost, 0, 0), () => NPC.downedBoss1);
                addToShop(shop, ItemID.HealingPotion, cond: () => NPC.downedBoss2);
                addToShop(shop, ItemID.ManaPotion, cond: () => NPC.downedBoss2);
                addToShop(shop, ItemID.TitanPotion,
                    Item.buyPrice(0, 2, 0, 0), () => NPC.downedBoss3);
                addToShop(shop, ItemID.ApprenticeBait, cond: () => NPC.downedBoss1);
                addToShop(shop, ItemID.JourneymanBait, cond: () => NPC.downedBoss3);
                // haha funny
                addToShop(shop, ItemID.MasterBait, cond: () => NPC.downedPlantBoss);
                addToShop(shop, ItemID.Burger, Item.buyPrice(0, 5, 0, 0),
                    () => NPC.downedBoss3);

                addToShop(shop, ItemID.Hotdog,
                    Item.buyPrice(0, 5, 0, 0), () => Main.hardMode);
                addToShop(shop, ItemID.CoffeeCup, Item.buyPrice(0, 2, 0, 0));
                break;
            case NPCID.Demolitionist:
                addToShop(shop, ItemID.MiningPotion,
                    Item.buyPrice(0, 1, 0, 0), () => NPC.downedBoss1);
                addToShop(shop, ItemID.IronskinPotion,
                    Item.buyPrice(0, 1, 0, 0), () => NPC.downedBoss1);
                addToShop(shop, ItemID.ShinePotion,
                    Item.buyPrice(0, 1, 0, 0), () => NPC.downedBoss1);
                addToShop(shop, ItemID.SpelunkerPotion,
                    Item.buyPrice(0, 2, 0, 0), () => NPC.downedBoss2);
                addToShop(shop, ItemID.ObsidianSkinPotion,
                    Item.buyPrice(0, 2, 0, 0), () => NPC.downedBoss3);
                addToShop(shop, ItemID.EndurancePotion,
                    Item.buyPrice(0, cost, 0, 0), () => NPC.downedBoss3);
                break;
            case NPCID.DyeTrader:
                addToShop(shop, ItemID.DyeTradersScimitar, price: Item.buyPrice(0, 15, 0, 0));
                break;
            case NPCID.ArmsDealer:
                addToShop(shop, ItemID.Boomstick,
                    Item.buyPrice(0, 20, 0, 0), () => NPC.downedQueenBee);
                addToShop(shop, ItemID.AmmoBox,
                    Item.buyPrice(0, 25, 0, 0), () => Main.hardMode);
                addToShop(shop, ItemID.Uzi,
                    Item.buyPrice(0, 45, 0, 0), () => NPC.downedPlantBoss);
                addToShop(shop, ItemID.TacticalShotgun,
                    Item.buyPrice(0, 60, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.SniperRifle,
                    Item.buyPrice(0, 60, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.RifleScope,
                    Item.buyPrice(0, 60, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.AmmoReservationPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.HunterPotion,
                    Item.buyPrice(0, 2, 0, 0));
                addToShop(shop, ItemID.BattlePotion,
                    Item.buyPrice(0, 2, 0, 0), () => NPC.downedBoss2);
                break;
            case NPCID.Stylist:
                addToShop(shop, ItemID.ChocolateChipCookie,
                    Item.buyPrice(0, 3, 0, 0));
                break;
            case NPCID.Cyborg:
                addToShop(shop, ItemID.RocketLauncher,
                    Item.buyPrice(0, 25, 0, 0), () => NPC.downedGolemBoss);
                break;
            case NPCID.Dryad:
                addToShop(shop, ItemID.ThornsPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.FeatherfallPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.RegenerationPotion,
                    Item.buyPrice(0, 2, 0, 0), () => NPC.downedBoss2);
                addToShop(shop, ItemID.SwiftnessPotion,
                    Item.buyPrice(0, cost, 0, 0), () => NPC.downedBoss2);
                addToShop(shop, ItemID.JungleRose, Item.buyPrice(0, 2, 0, 0));
                addToShop(shop, ItemID.NaturesGift, Item.buyPrice(0, 10, 0, 0));
                addToShop(shop, ItemID.Grapes,
                    Item.buyPrice(0, 2, 50, 0), () => NPC.downedBoss3);
                break;
            case NPCID.GoblinTinkerer:
                addToShop(shop, ItemID.StinkPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.Spaghetti,
                    Item.buyPrice(0, 5, 0, 0), () => NPC.downedBoss3);
                break;
            case NPCID.Mechanic:
                addToShop(shop, ItemID.BuilderPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.CombatWrench, Item.buyPrice(0, 10, 0, 0));
                break;
            case NPCID.Clothier:
                addToShop(shop, ItemID.GoldenKey, Item.buyPrice(0, 5, 0, 0), () => Main.hardMode);
                break;
            case NPCID.Painter:
                addToShop(shop, ItemID.PainterPaintballGun, price: Item.buyPrice(0, 15, 0, 0));
                break;
            case NPCID.Steampunker:
                addToShop(shop, ItemID.PurpleSolution,
                    Item.buyPrice(0, 0, 5, 0), () => Main.LocalPlayer.ZoneGraveyard && WorldGen.crimson);
                addToShop(shop, ItemID.RedSolution,
                    Item.buyPrice(0, 0, 5, 0), () => Main.LocalPlayer.ZoneGraveyard && !WorldGen.crimson);
                break;
            case NPCID.Wizard:
                addToShop(shop, ItemID.ManaRegenerationPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(shop, ItemID.MagicPowerPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(shop, ItemID.GravitationPotion,
                    Item.buyPrice(0, 2, 0, 0));
                addToShop(shop, ItemID.PotionOfReturn,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(shop, ItemID.MagicMissile, Item.buyPrice(0, 5, 0, 0));
                addToShop(shop, ItemID.RodofDiscord,
                    Item.buyPrice(10, 0, 0, 0), () => Main.hardMode && Main.LocalPlayer.ZoneHallow);
                addToShop(shop, ItemID.SpectreStaff,
                    Item.buyPrice(0, 25, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.InfernoFork,
                    Item.buyPrice(0, 25, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.ShadowbeamStaff,
                    Item.buyPrice(0, 25, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.MagnetSphere,
                    Item.buyPrice(0, 25, 0, 0), () => NPC.downedGolemBoss);
                break;
            case NPCID.WitchDoctor:
                addToShop(shop, ItemID.SummoningPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(shop, ItemID.CalmingPotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.RagePotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(shop, ItemID.WrathPotion,
                    Item.buyPrice(0, cost, 0, 0));
                addToShop(shop, ItemID.InfernoPotion,
                    Item.buyPrice(0, 2, 0, 0));
                addToShop(shop, ItemID.ButterflyDust,
                    Item.buyPrice(0, 10, 0, 0), () => NPC.downedGolemBoss);
                addToShop(shop, ItemID.FriedEgg, Item.buyPrice(0, 2, 50, 0));
                break;
            case NPCID.PartyGirl:
                addToShop(shop, ItemID.GenderChangePotion,
                    Item.buyPrice(0, 1, 0, 0));
                addToShop(shop, ItemID.Pizza,
                    Item.buyPrice(0, 5, 0, 0), () => NPC.downedBoss3);
                addToShop(shop, ItemID.CreamSoda, Item.buyPrice(0, 2, 50, 0));
                break;
            case NPCID.Princess:
                addToShop(shop, ItemID.PrincessWeapon, Item.buyPrice(0, 50, 0, 0));
                addToShop(shop, ItemID.AppleJuice);
                addToShop(shop, ItemID.FruitJuice);
                addToShop(shop, ItemID.Lemonade);
                addToShop(shop, ItemID.PrismaticPunch);
                addToShop(shop, ItemID.SmoothieofDarkness);
                addToShop(shop, ItemID.TropicalSmoothie);
                break;
            case NPCID.SkeletonMerchant:
                addToShop(shop, ItemID.MilkCarton);
                addToShop(shop, ItemID.Marrow, Item.buyPrice(0, 36, 0, 0),
                    () => Main.hardMode);
                break;
            case NPCID.Golfer:
                addToShop(shop, ItemID.PotatoChips, Item.buyPrice(0, 1, 0, 0));
                break;
            // Zoologist
            case NPCID.BestiaryGirl:
                addToShop(shop, ItemID.Steak, Item.buyPrice(0, 5, 0, 0), () => Main.hardMode);
                break;
        }
    }

    /// <summary>
    /// Compatibilty wrapper for adding items to the shop.
    /// </summary>
    /// <param name="shop">The shop to add to.</param>
    /// <param name="itemID">Which (vanilla) itemID to add.</param>
    /// <param name="price">What the sell price should be.</param>
    /// <param name="cond">The condition to be registered for selling.</param>
    private void addToShop(NPCShop shop, int itemID, int? price = null, Func<bool>? cond = null) {

        if (!QOLConfig.Instance.sellAdditionalItems) {
            return;
        }
        
        // null means always true
        cond ??= () => true;

        var shopItem = new Item(itemID) {
            shopCustomPrice = price
        };
        // TODO: this needs proper conditions for mod support
        shop.Add(shopItem, new Condition(LocalizedText.Empty, cond));
    }
}