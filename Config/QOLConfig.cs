using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityQOL.Config;

[BackgroundColor(64, 2, 32, 128)]
public class QOLConfig : ModConfig {
    // magic tModLoader-managed field, assigned
    // ReSharper disable once UnusedMember.Global
    public static QOLConfig Instance;
    
    
    [Header("$Mods.CalamityQOL.ConfigHeaders.qol")]
    [Label("$Mods.CalamityQOL.Config.sellAdditionalItems")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.sellAdditionalItems")]
    public bool sellAdditionalItems { get; set; }

    [Label("$Mods.CalamityQOL.Config.wellFedPatch")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.wellFedPatch")]
    public bool wellFedPatch { get; set; }

    // Abeemination
    [Label("$Mods.CalamityQOL.Config.respawnTimer")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.respawnTimer")]
    public bool respawnTimer { get; set; }

    // Void Bag
    [Label("$Mods.CalamityQOL.Config.starterBag")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.starterBag")]
    public bool starterBag { get; set; }

    // Anklet of the Wind
    [Label("$Mods.CalamityQOL.Config.accessoryRecipes")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.accessoryRecipes")]
    public bool accessoryRecipes { get; set; }

    // Universal Pylon
    [Label("$Mods.CalamityQOL.Config.townNPCsAtNight")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.townNPCsAtNight")]
    public bool townNPCsAtNight { get; set; }
    
    // Suspicious Looking Eye
    [Label("$Mods.CalamityQOL.Config.nonConsumableSummons")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.nonConsumableSummons")]
    public bool nonConsumableSummons { get; set; }
    
    // Feral Claws
    [Label("$Mods.CalamityQOL.Config.moreAutoSwing")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.moreAutoSwing")]
    public bool moreAutoSwing { get; set; }
    
    // Ironskin Potion
    [Label("$Mods.CalamityQOL.Config.moreBuffSlots")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(44)]
    [Range(0, 936)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.moreBuffSlots")]
    public int moreBuffSlots { get; set; }
    
    [Header("$Mods.CalamityQOL.ConfigHeaders.buffs")]
    // Wooden Platform
    [Label("$Mods.CalamityQOL.Config.fasterPlacement")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.fasterPlacement")]
    public bool fasterPlacement { get; set; }
    
    // Wooden Platform
    [Label("$Mods.CalamityQOL.Config.fasterJumpSpeed")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.fasterJumpSpeed")]
    public bool fasterJumpSpeed { get; set; }

    // Wooden Platform
    [Label("$Mods.CalamityQOL.Config.fasterSpeed")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(false)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.fasterSpeed")]
    public bool fasterSpeed { get; set; }
    
    // Lucky Horseshoe
    [Label("$Mods.CalamityQOL.Config.fasterFalling")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.fasterFalling")]
    public bool fasterFalling { get; set; }

    public override ConfigScope Mode => ConfigScope.ServerSide;
}