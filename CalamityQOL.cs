using CalamityQOL.Fixes;
using Terraria.ModLoader;

namespace CalamityQOL;

public class CalamityQOL : Mod {

    public static CalamityQOL i;

    public Mod? overhaul;
    public Mod? calamity;

    public override void Load() {
        i = this;
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