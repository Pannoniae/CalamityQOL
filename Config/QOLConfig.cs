using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityQOL.Config;

public class QOLConfig : ModConfig {
    // magic tModLoader-managed field, assigned
    // ReSharper disable once UnusedMember.Global
    public static QOLConfig Instance;

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
    
    // Wooden Platform
    [Label("$Mods.CalamityQOL.Config.fasterPlacement")]
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.fasterPlacement")]
    public bool fasterPlacement { get; set; }
    
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


    public override ConfigScope Mode => ConfigScope.ServerSide;
}