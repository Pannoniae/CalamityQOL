using CalamityQOL.Fixes;
using Terraria;
using Terraria.ModLoader;

namespace CalamityQOL {
    public class CalamityQOL : Mod {
        public override void Load() {
            ILEdits.load();
        }

        public override void Unload() {
            ILEdits.unload();
        }
    }
}