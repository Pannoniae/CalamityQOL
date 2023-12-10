using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityQOL.Items;

// drops based on which ore is found in the world
public class HasTinOreGenDropRuleCondition : IItemDropRuleCondition {

    public bool CanDrop(DropAttemptInfo info) {
        return WorldGen.SavedOreTiers.Copper == TileID.Tin;
    }

    public bool CanShowItemDropInUI() {
        return true;
    }

    public string GetConditionDescription() {
        return "Has Tin Ore in the world";
    }
}

public class StarterBag : ModItem {

    public override bool IsLoadingEnabled(Mod mod) {
        // if calamity is loaded, we have zero business here
        return CalamityQoL.i.calamity is null;
    }
    public override void SetDefaults() {
        Item.consumable = true;
        Item.width = 24;
        Item.height = 24;
        Item.rare = ItemRarityID.Blue;
    }

    public override bool CanRightClick() => true;

    public override void ModifyItemLoot(ItemLoot itemLoot) {
        // is tin generated?
        var condition = new HasTinOreGenDropRuleCondition();
        var tinRule = new LeadingConditionRule(condition);
        itemLoot.Add(tinRule);
        tinRule.OnSuccess(ItemDropRule.Common(ItemID.TinBroadsword));
        tinRule.OnSuccess(ItemDropRule.Common(ItemID.TinBow));
        tinRule.OnSuccess(ItemDropRule.Common(ItemID.TopazStaff));
        tinRule.OnFailedConditions(ItemDropRule.Common(ItemID.CopperBow));
        tinRule.OnFailedConditions(ItemDropRule.Common(ItemID.CopperBroadsword));
        tinRule.OnFailedConditions(ItemDropRule.Common(ItemID.AmethystStaff));
        itemLoot.Add(ItemDropRule.Common(ItemID.AbigailsFlower));
        itemLoot.Add(ItemDropRule.Common(ItemID.WoodenArrow, minimumDropped: 100, maximumDropped: 100));
        itemLoot.Add(ItemDropRule.Common(ItemID.ManaCrystal));
        
        tinRule.OnSuccess(ItemDropRule.Common(ItemID.TinHammer));
        tinRule.OnFailedConditions(ItemDropRule.Common(ItemID.CopperHammer));
        
        itemLoot.Add(ItemDropRule.Common(ItemID.Bomb, minimumDropped: 10, maximumDropped: 10));
        itemLoot.Add(ItemDropRule.Common(ItemID.Rope, minimumDropped: 50, maximumDropped: 50));
        itemLoot.Add(ItemDropRule.Common(ItemID.MiningPotion));
        itemLoot.Add(ItemDropRule.Common(ItemID.SpelunkerPotion, minimumDropped: 2, maximumDropped: 2));
        itemLoot.Add(ItemDropRule.Common(ItemID.SwiftnessPotion, minimumDropped: 3, maximumDropped: 3));
        itemLoot.Add(ItemDropRule.Common(ItemID.GillsPotion, minimumDropped: 2, maximumDropped: 2));
        itemLoot.Add(ItemDropRule.Common(ItemID.ShinePotion));
        itemLoot.Add(ItemDropRule.Common(ItemID.RecallPotion, minimumDropped: 3, maximumDropped: 3));
        itemLoot.Add(ItemDropRule.Common(ItemID.Torch, minimumDropped: 25, maximumDropped: 25));
        itemLoot.Add(ItemDropRule.Common(ItemID.Chest, minimumDropped: 3, maximumDropped: 3));
    }
}