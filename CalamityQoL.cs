using CalamityQOL.Config;
using CalamityQOL.Fixes;
using Terraria.ModLoader;

namespace CalamityQOL;

public class CalamityQoL : Mod {

    public static CalamityQoL i;

    public Mod? vanillaQoL;
    public Mod? overhaul;
    public Mod? calamity;

    public override uint ExtraPlayerBuffSlots =>
        vanillaQoL is null ? (uint)QoLConfig.Instance.moreBuffSlots : 0;

    public override void Load() {
        i = this;
        ModLoader.TryGetMod("VanillaQoL", out vanillaQoL);
        ModLoader.TryGetMod("TerrariaOverhaul", out overhaul);
        ModLoader.TryGetMod("CalamityMod", out calamity);

        // if calamity is loaded, we have zero business here
        if (calamity is not null) {
            return;
        }

        ILEdits.load();
    }

    public override void Unload() {
        ILEdits.unload();
        i = null!;
    }

    // use at load time
    public static bool hasCalamity() {
        return ModLoader.HasMod("CalamityMod");
    }
}