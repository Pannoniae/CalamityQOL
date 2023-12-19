using System.ComponentModel;
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

    // Universal Pylon
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool townNPCsAtNight { get; set; }
    
    // Suspicious Looking Eye
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(true)]
    public bool nonConsumableSummons { get; set; }
    
    // Ironskin Potion
    [BackgroundColor(192, 54, 128, 192)]
    [DefaultValue(44)]
    [Range(0, 936)]
    public int moreBuffSlots { get; set; }
    
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
    }
}