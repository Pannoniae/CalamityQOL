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
}