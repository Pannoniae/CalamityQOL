﻿using System.ComponentModel;
using Terraria.ModLoader.Config;
using VanillaQoL.Shared;

namespace CalamityQOL.Config;

[BackgroundColor(64, 2, 32, 128)]
public class QoLConfig : ModConfig {
    // magic tModLoader-managed field, assigned
    // ReSharper disable once UnusedMember.Global
    public static QoLConfig Instance;
    
    
    [Header("gameplay")]

    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    public bool FasterFall { get; set; }

    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool sellAdditionalItems { get; set; }

    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    public bool wellFedPatch { get; set; }

    // Abeemination
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    public bool respawnTimer { get; set; }

    // Void Bag
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    public bool starterBag { get; set; }

    // Anklet of the Wind
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    public bool accessoryRecipes { get; set; }

    // Suspicious Looking Eye
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool nonConsumableSummons { get; set; }
    
    [Header("playerBoosts")]

    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool FasterBaseSpeed { get; set; }

    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool HigherJumpHeight { get; set; }

    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool FasterJumpSpeed { get; set; }

    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool FasterTilePlacement { get; set; }

    public override ConfigScope Mode => ConfigScope.ServerSide;

    public override void OnChanged() {
        if (sellAdditionalItems) {
            GlobalFeatures.enableFeature(Mod, "NPCShops");
        }
        else {
        	GlobalFeatures.disableFeature(Mod, "NPCShops");
        }

        if (nonConsumableSummons) {
            GlobalFeatures.enableFeature(Mod, "nonConsumableSummons");
        }
        else {
            GlobalFeatures.disableFeature(Mod, "nonConsumableSummons");
        }
    }
}