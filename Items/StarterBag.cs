using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityQOL.Items;

public class StarterBag : ModItem {
    public override void SetStaticDefaults() {
    }

    public override void SetDefaults() {
        Item.consumable = true;
        Item.width = 24;
        Item.height = 24;
        Item.rare = 1;
    }

    public override bool CanRightClick() => true;

    public override void ModifyItemLoot(ItemLoot loot) {
        loot.Add(ItemDropRule.Common(ItemID.CopperBow));
        loot.Add(ItemDropRule.Common(ItemID.CopperBroadsword));
        loot.Add(ItemDropRule.Common(ItemID.AmethystStaff));
        loot.Add(ItemDropRule.Common(ItemID.WoodenArrow, minimumDropped: 100, maximumDropped: 100));
        loot.Add(ItemDropRule.Common(ItemID.ManaCrystal));
        loot.Add(ItemDropRule.Common(ItemID.CopperHammer));
        loot.Add(ItemDropRule.Common(ItemID.Bomb, minimumDropped: 10, maximumDropped: 10));
        loot.Add(ItemDropRule.Common(ItemID.Rope, minimumDropped: 50, maximumDropped: 50));
        loot.Add(ItemDropRule.Common(ItemID.MiningPotion));
        loot.Add(ItemDropRule.Common(ItemID.SpelunkerPotion, minimumDropped: 2, maximumDropped: 2));
        loot.Add(ItemDropRule.Common(ItemID.SwiftnessPotion, minimumDropped: 3, maximumDropped: 3));
        loot.Add(ItemDropRule.Common(ItemID.GillsPotion, minimumDropped: 2, maximumDropped: 2));
        loot.Add(ItemDropRule.Common(ItemID.ShinePotion));
        loot.Add(ItemDropRule.Common(ItemID.RecallPotion, minimumDropped: 3, maximumDropped: 3));
        loot.Add(ItemDropRule.Common(ItemID.Torch, minimumDropped: 25, maximumDropped: 25));
        loot.Add(ItemDropRule.Common(ItemID.Chest, minimumDropped: 3, maximumDropped: 3));
    }
}