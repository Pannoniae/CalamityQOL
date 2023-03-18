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

    [Label("$Mods.CalamityQOL.Config.respawnTimer")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.respawnTimer")]
    public bool respawnTimer { get; set; }
    
    [Label("$Mods.CalamityQOL.Config.starterBag")]
    [BackgroundColor(192, 64, 128, 192)]
    [DefaultValue(true)]
    [Tooltip("$Mods.CalamityQOL.ConfigTooltip.starterBag")]
    public bool starterBag { get; set; }



    public override ConfigScope Mode => ConfigScope.ServerSide;
}