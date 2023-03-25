using CalamityQOL.Config;
using CalamityQOL.Fixes;
using Terraria.ModLoader;

namespace CalamityQOL;

public class CalamityQOLMod : Mod {

    public static CalamityQOLMod i;

    public override uint ExtraPlayerBuffSlots =>
        (uint)QOLConfig.Instance.moreBuffSlots;

    public override void Load() {
        i = this;
        ILEdits.load();
    }

    public override void Unload() {
        ILEdits.unload();
        i = null!;
    }
}