using System;
using CalamityQOL.Config;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityQOL.Recipes;

public class RecipeChanges : ModSystem {
    private int anyCopperBar;
    private int anySilverBar;
    private int anyGoldBar;
    private int anyIceBlock;

    public override void AddRecipeGroups() {
        anyCopperBar = RecipeGroup.RegisterGroup("AnyCopperBar",
            new RecipeGroup(
                () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperBar),
                ItemID.CopperBar, ItemID.TinBar));
        anySilverBar = RecipeGroup.RegisterGroup("AnySilverBar",
            new RecipeGroup(
                () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.SilverBar),
                ItemID.SilverBar, ItemID.TungstenBar));
        anyGoldBar = RecipeGroup.RegisterGroup("AnyGoldBar",
            new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.GoldBar),
                ItemID.GoldBar, ItemID.PlatinumBar));
        anyIceBlock = RecipeGroup.RegisterGroup("AnyIceBlock",
            new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.IceBlock),
                ItemID.IceBlock, ItemID.PurpleIceBlock,
                ItemID.PinkIceBlock, ItemID.RedIceBlock));
    }


    public override void AddRecipes() {
        if (!QOLConfig.Instance.accessoryRecipes) {
            return;
        }

        Recipe r1 = Recipe.Create(ItemID.CloudinaBottle, 1);
        r1.AddIngredient(ItemID.Bottle, 1);
        r1.AddIngredient(ItemID.Cloud, 25);
        r1.AddIngredient(ItemID.Feather, 2);
        r1.AddTile(TileID.Anvils);
        r1.Register();
        Recipe r2 = Recipe.Create(ItemID.PortableStool, 1);
        r2.AddRecipeGroup(RecipeGroupID.Wood, 15);
        r2.AddTile(TileID.Sawmill);
        r2.Register();
        Recipe r3 = Recipe.Create(ItemID.HermesBoots, 1);
        r3.AddIngredient(ItemID.Silk, 10);
        r3.AddIngredient(ItemID.SwiftnessPotion, 5);
        r3.AddTile(TileID.Loom);
        r3.Register();
        Recipe r4 = Recipe.Create(ItemID.BlizzardinaBottle, 1);
        r4.AddIngredient(ItemID.Bottle, 1);
        r4.AddIngredient(ItemID.SnowBlock, 50);
        r4.AddIngredient(ItemID.Feather, 3);
        r4.AddTile(TileID.Anvils);
        r4.Register();
        Recipe r5 = Recipe.Create(ItemID.SandstorminaBottle, 1);
        r5.AddIngredient(ItemID.Bottle, 1);
        r5.AddIngredient(ItemID.SandBlock, 70);
        r5.AddIngredient(ItemID.Feather, 3);
        r5.AddTile(TileID.Anvils);
        r5.Register();
        Recipe r6 = Recipe.Create(ItemID.FrogLeg, 1);
        r6.AddIngredient(ItemID.Frog, 6);
        r6.AddTile(TileID.Anvils);
        r6.Register();
        Recipe r7 = Recipe.Create(ItemID.Aglet, 1);
        r7.AddRecipeGroup(anyCopperBar, 5);
        r7.AddTile(TileID.Anvils);
        r7.Register();
        Recipe r8 = Recipe.Create(ItemID.AnkletoftheWind, 1);
        r8.AddIngredient(ItemID.JungleSpores, 15);
        r8.AddIngredient(ItemID.Cloud, 15);
        r8.AddIngredient(ItemID.PinkGel, 5);
        r8.AddTile(TileID.Anvils);
        r8.Register();
        Recipe r9 = Recipe.Create(ItemID.WaterWalkingBoots, 1);
        r9.AddIngredient(ItemID.Leather, 5);
        r9.AddIngredient(ItemID.WaterWalkingPotion, 5);
        r9.AddTile(TileID.Anvils);
        r9.Register();
        Recipe r10 = Recipe.Create(ItemID.FlameWakerBoots, 1);
        r10.AddIngredient(ItemID.Silk, 8);
        r10.AddIngredient(ItemID.Obsidian, 2);
        r10.AddTile(TileID.Loom);
        r10.Register();
        Recipe r11 = Recipe.Create(ItemID.IceSkates, 1);
        r11.AddRecipeGroup(anyIceBlock, 20);
        r11.AddIngredient(ItemID.Leather, 5);
        r11.AddRecipeGroup("IronBar", 5);
        r11.AddTile(TileID.IceMachine);
        r11.Register();
        Recipe r12 = Recipe.Create(ItemID.LuckyHorseshoe, 1);
        r12.AddRecipeGroup(anyGoldBar, 8);
        r12.AddTile(TileID.Anvils);
        r12.Register();
        Recipe r13 = Recipe.Create(ItemID.ShinyRedBalloon, 1);
        r13.AddIngredient(ItemID.WhiteString, 1);
        r13.AddIngredient(ItemID.Gel, 60);
        r13.AddIngredient(ItemID.Cloud, 20);
        r13.AddTile(TileID.Solidifier);
        r13.Register();
        Recipe r14 = Recipe.Create(ItemID.LavaCharm, 1);
        r14.AddIngredient(ItemID.LavaBucket, 3);
        r14.AddIngredient(ItemID.Obsidian, 25);
        r14.AddRecipeGroup(anyGoldBar, 5);
        r14.AddTile(TileID.Anvils);
        r14.Register();
        Recipe r15 = Recipe.Create(ItemID.ObsidianRose, 1);
        r15.AddIngredient(ItemID.JungleRose, 1);
        r15.AddIngredient(ItemID.Obsidian, 10);
        r15.AddIngredient(ItemID.Hellstone, 10);
        r15.AddTile(TileID.Anvils);
        r15.Register();
        Recipe r16 = Recipe.Create(ItemID.FeralClaws, 1);
        r16.AddIngredient(ItemID.Leather, 10);
        r16.AddTile(TileID.Anvils);
        r16.Register();
        Recipe r17 = Recipe.Create(ItemID.Radar, 1);
        r17.AddRecipeGroup("IronBar", 5);
        r17.AddTile(TileID.Anvils);
        r17.Register();
        Recipe r18 = Recipe.Create(ItemID.MetalDetector, 1);
        r18.AddIngredient(ItemID.Wire, 10);
        r18.AddIngredient(ItemID.SpelunkerGlowstick, 5);
        r18.AddRecipeGroup(anyCopperBar, 5);
        r18.AddTile(TileID.Anvils);
        r18.Register();
        Recipe r19 = Recipe.Create(ItemID.DPSMeter, 1);
        r19.AddIngredient(ItemID.Wire, 10);
        r19.AddRecipeGroup(anyGoldBar, 5);
        r19.AddTile(TileID.Anvils);
        r19.Register();
        Recipe r20 = Recipe.Create(ItemID.HandWarmer, 1);
        r20.AddIngredient(ItemID.Silk, 5);
        r20.AddIngredient(ItemID.Shiverthorn, 1);
        r20.AddIngredient(ItemID.SnowBlock, 10);
        r20.AddTile(TileID.Loom);
        r20.Register();
        Recipe r21 = Recipe.Create(ItemID.FlowerBoots, 1);
        r21.AddIngredient(ItemID.Silk, 7);
        r21.AddIngredient(ItemID.JungleRose, 1);
        r21.AddIngredient(ItemID.JungleGrassSeeds, 5);
        r21.AddTile(TileID.Loom);
        r21.Register();
        Recipe r22 = Recipe.Create(ItemID.BandofRegeneration, 1);
        r22.AddIngredient(ItemID.Shackle, 1);
        r22.AddIngredient(ItemID.LifeCrystal, 1);
        r22.AddTile(TileID.Anvils);
        r22.Register();
        Recipe r23 = Recipe.Create(ItemID.ShoeSpikes, 1);
        r23.AddRecipeGroup("IronBar", 5);
        r23.AddIngredient(ItemID.Spike, 10);
        r23.AddTile(TileID.Anvils);
        r23.Register();
        Recipe r24 = Recipe.Create(ItemID.FlareGun, 1);
        r24.AddRecipeGroup(anyCopperBar, 5);
        r24.AddIngredient(ItemID.Torch, 10);
        r24.AddTile(TileID.Anvils);
        r24.Register();
    }
}