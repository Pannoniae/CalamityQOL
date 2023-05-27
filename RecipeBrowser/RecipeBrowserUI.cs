using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace CalamityQOL.RecipeBrowser;

public class RecipeBrowserUI : UIState {
    public RecipeBrowserUIElement window;

    public override void OnActivate() {
        // for some dumbfuck reason this is already attached? why?
        RemoveAllChildren();
        window = new RecipeBrowserUIElement(this) {
            HAlign = 0.0f,
            VAlign = 0.0f,
            Left = new StyleDimension(Main.screenWidth / 2 - TextureAssets.ChatBack.Width() / 2, 0f),
            //Left = new StyleDimension(700f, 0f),
            Top = new StyleDimension(220f, 0f),
            Width = new StyleDimension(TextureAssets.ChatBack.Width(), 0f),
            Height = new StyleDimension(360f, 0f)
        };
        Append(window);
    }

    public override void Update(GameTime gameTime) {
        window.Left = new StyleDimension(Main.screenWidth / 2 - TextureAssets.ChatBack.Width() / 2, 0f);
        base.Update(gameTime);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
    }
}