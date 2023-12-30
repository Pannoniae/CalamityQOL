using CalamityQOL.Config;
using CalamityQOL.Fixes;
using CalamityQOL.Recipes;
using Terraria.ModLoader;
using VanillaQoL;
using VanillaQoL.Gameplay;

namespace CalamityQOL;

public class CalamityQOL : Mod {

    public static CalamityQOL i;

    public Mod? vanillaQoL;
    public Mod? overhaul;
    public Mod? calamity;

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

        Utils.completelyWipeClass(typeof(ILEdits));
        Utils.completelyWipeClass(typeof(RecipeChanges));
    }

    // use at load time
    public static bool hasCalamity() {
        return ModLoader.HasMod("CalamityMod");
    }
}