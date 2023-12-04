using CalamityQOL.Config;
using CalamityQOL.Fixes;
using Terraria.ModLoader;

namespace CalamityQOL;

public class CalamityQoL : Mod {

    public static CalamityQoL i;

    public Mod? vanillaQoL;
    public Mod? overhaul;

    public override uint ExtraPlayerBuffSlots =>
        vanillaQoL is null ? (uint)QoLConfig.Instance.moreBuffSlots : 0;

    public override void Load() {
        i = this;
        ILEdits.load();
        ModLoader.TryGetMod("VanillaQoL", out vanillaQoL);
        ModLoader.TryGetMod("TerrariaOverhaul", out overhaul);
    }

    public override void Unload() {
        ILEdits.unload();
        i = null!;
    }
}