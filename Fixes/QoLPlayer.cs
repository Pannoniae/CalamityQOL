using System.Collections.Generic;
using System.Linq;
using CalamityQOL.Config;
using CalamityQOL.Items;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace CalamityQOL.Fixes;

public partial class QoLPlayer : ModPlayer {
    public override bool IsLoadingEnabled(Mod mod) {
        // if calamity is loaded, we have zero business here
        return CalamityQoL.i.calamity is null;
    }

    public override void UpdateDead() {
        if (!QoLConfig.Instance.respawnTimer) {
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

    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath) {
        if (QoLConfig.Instance.starterBag) {
            var obj = new Item();
            obj.SetDefaults(ModContent.ItemType<StarterBag>());
            yield return obj;
        }
    }

    public override void UpdateEquips() {
        // If the config is enabled, vastly increase the player's base tile and wall placement speeds
        // This stacks with the Brick Layer and Portable Cement Mixer
        if (QoLConfig.Instance.FasterTilePlacement)
        {
            Player.tileSpeed += 0.5f;
            Player.wallSpeed += 0.5f;
        }
    }

    public override void PostUpdateMiscEffects() {
        // 50% movement speed bonus so that you don't feel like a snail in the early game
        // Disabled while Overhaul is enabled, because Overhaul does very similar things to make movement more snappy
        if (CalamityQoL.i.overhaul is null && QoLConfig.Instance.FasterBaseSpeed) {
            Player.moveSpeed += 0.5f;
        }

        if (QoLConfig.Instance.FasterJumpSpeed) {
            // No category

            // Give the player a 24% jump speed boost while wings are equipped, otherwise grant 4% more jump speed so that players can jump 7 tiles high
            if (Player.wingsLogic > 0)
                Player.jumpSpeedBoost += 1.2f;
            else
                Player.jumpSpeedBoost += 0.2f;
        }

        if (QoLConfig.Instance.FasterFall) {
            // Allow the player to double their gravity (but NOT max fall speed!) by holding the down button while in midair.
            bool holdingDown = Player.controlDown && !Player.controlJump;
            bool controlsEnabled = Player.ControlsEnabled();
            bool notInLiquid = !Player.wet;
            bool notOnRope = !Player.pulley && Player.ropeCount == 0;
            bool notGrappling = Player.grappling[0] == -1;
            bool airborne = Player.velocity.Y != 0;
            if (holdingDown && Player.ControlsEnabled() && notInLiquid && notOnRope && notGrappling && airborne) {
                //Player cannot further increase their ridiculous gravity during a Gravistar Slam
                Player.velocity.Y += Player.gravity * Player.gravDir * (2f - 1f);
                if (Player.velocity.Y * Player.gravDir > Player.maxFallSpeed)
                    Player.velocity.Y = Player.maxFallSpeed * Player.gravDir;
            }
        }
    }

    public static bool isEvent(Player player) {
        return Main.invasionType > 0 && Main.invasionProgressNearInvasion || player.ZoneTowerStardust ||
               player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula ||
               DD2Event.Ongoing && player.ZoneOldOneArmy ||
               (player.ZoneOverworldHeight || player.ZoneSkyHeight) &&
               (Main.eclipse || Main.pumpkinMoon || Main.snowMoon || Main.bloodMoon);
    }


    public static bool isAnyBossAlive() {
        return Main.npc.Any(n => n != null && n.active && n.boss);
    }
}

public static class PlayerExtensions {
    public static bool ControlsEnabled(this Player player, bool allowWoFTongue = false) {
        if (player.CCed) // Covers frozen (player.frozen), webs (player.webbed), and Medusa (player.stoned)
            return false;
        if (player.tongued && !allowWoFTongue)
            return false;
        return true;
    }
}