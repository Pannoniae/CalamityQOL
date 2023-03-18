using System.Collections.Generic;
using System.Linq;
using CalamityQOL.Config;
using CalamityQOL.Items;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace CalamityQOL.Fixes;

public class QOLPlayer : ModPlayer {
    public override void UpdateDead() {
        if (!QOLConfig.Instance.respawnTimer) {
            return;
        }
        if (isAnyBossAlive()) {
            return;
        }

        var respawnTimer = isEvent(Player) ? 360 : 180;
        if (Player.respawnTimer > respawnTimer) {
            Player.respawnTimer = respawnTimer;
        }
    }
    
    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        if (QOLConfig.Instance.starterBag) {
            var obj = new Item();
            obj.SetDefaults(ModContent.ItemType<StarterBag>());
            yield return obj;
        }
    }

    public static bool isEvent(Player player) {
        return Main.invasionType > 0 && Main.invasionProgressNearInvasion || player.ZoneTowerStardust ||
               player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula ||
               DD2Event.Ongoing && player.ZoneOldOneArmy ||
               (player.ZoneOverworldHeight || player.ZoneSkyHeight) &&
               (Main.eclipse || Main.pumpkinMoon || Main.snowMoon || Main.bloodMoon);
    }


    public static bool isAnyBossAlive(bool checkForMechs = false) {
        return Main.npc.Any(n => n != null && n.active && n.boss);
    }
}