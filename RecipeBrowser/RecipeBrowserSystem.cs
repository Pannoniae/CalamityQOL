using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityQOL.RecipeBrowser;

/// <summary>
/// Thank you tModLoader for the ExampleMod to shamelessly copypaste
/// </summary>
[Autoload(Side = ModSide.Client)]
public class RecipeBrowserSystem : ModSystem {
    private static Item recipeItem = new();

    private UserInterface? recipeBrowserUI;
    internal RecipeBrowserUI recipeBrowserUIState;

    public void showBrowser() {
        recipeBrowserUI?.SetState(recipeBrowserUIState);
    }

    public void hideBrowser() {
        recipeBrowserUI?.SetState(null);
    }


    // we have to do this stupid thing because in PostSetupContent the itemSorting things are not initialised yet...
    public override void OnWorldLoad() {
        // Create custom interface which can swap between different UIStates
        recipeBrowserUI = new UserInterface();
        // Creating custom UIState
        recipeBrowserUIState = new RecipeBrowserUI();

        // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
        recipeBrowserUIState.Activate();
    }

    public override void UpdateUI(GameTime gameTime) {
        if (Main.player[Main.myPlayer].talkNPC < 0 && Main.player[Main.myPlayer].sign == -1 ||
            Main.player[Main.myPlayer].chest != -1 || Main.npcShop != 0 ||
            Main.InReforgeMenu) {
            // hide recipes
            hideBrowser();
        }

        // Here we call .Update on our custom UI and propagate it to its state and underlying elements
        if (recipeBrowserUI?.CurrentState != null) {
            recipeBrowserUI?.Update(gameTime);
        }

        base.UpdateUI(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        var mouseItemIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
        if (mouseItemIndex != -1)
            layers.Insert(mouseItemIndex, new LegacyGameInterfaceLayer(
                "CalamityQOL: Recipe Browser",
                () => {
                    if (recipeBrowserUI?.CurrentState != null)
                        recipeBrowserUI.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI)
            );
    }
}