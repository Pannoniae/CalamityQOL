using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityQOL.RecipeBrowser;

public class MerchantBrowser : GlobalNPC {
    public override void SetDefaults(NPC npc) {
        if (npc.type == NPCID.Merchant) {
            // pass
        }
    }

    public override void OnChatButtonClicked(NPC npc, bool firstButton) {
        // Merchant Recipe button
        if (npc.type == NPCID.Merchant && !firstButton) {
            CalamityQOLMod.i.Logger.Info("Clicked on Merchant Recipe Browser");
            ModContent.GetInstance<RecipeBrowserSystem>().showBrowser();
        }
    }
}