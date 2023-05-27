using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace CalamityQOL.RecipeBrowser {
    public class RecipeBrowserUIElement : UIElement {
        private List<int> allIDs = new();
        private List<int> _itemIdsAvailableToShow;
        private CreativeUnlocksTracker _lastTrackerCheckedForEdits;
        private int _lastCheckedVersionForEdits = -1;
        private UISearchBar _searchBar;
        private UIPanel _searchBoxPanel;
        private UIState _parentUIState;
        private string _searchString;
        private UIDynamicItemCollection _itemGrid;
        private EntryFilterer<Item, IItemEntryFilter> _filterer;
        private EntrySorter<int, ICreativeItemSortStep> _sorter;
        private UIElement _containerInfinites;
        private UIElement _containerSacrifice;
        private bool _showSacrificesInsteadOfInfinites;
        public const string SnapPointName_SacrificeSlot = "CreativeSacrificeSlot";
        public const string SnapPointName_SacrificeConfirmButton = "CreativeSacrificeConfirm";
        public const string SnapPointName_InfinitesFilter = "CreativeInfinitesFilter";
        public const string SnapPointName_InfinitesSearch = "CreativeInfinitesSearch";
        public const string SnapPointName_InfinitesItemSlot = "CreativeInfinitesSlot";
        private List<UIImage> _sacrificeCogsSmall = new();
        private List<UIImage> _sacrificeCogsMedium = new();
        private List<UIImage> _sacrificeCogsBig = new();
        private UIImageFramed _sacrificePistons;
        private UIParticleLayer _pistonParticleSystem;
        private Asset<Texture2D> _pistonParticleAsset;
        private int _sacrificeAnimationTimeLeft;
        private bool _researchComplete;
        private bool _hovered;
        private int _lastItemIdSacrificed;
        private int _lastItemAmountWeHad;
        private int _lastItemAmountWeNeededTotal;
        private bool _didClickSomething;
        private bool _didClickSearchBar;
        private bool hasBeenSetUp;

        /// <summary>
        /// Don't set up anything content-based in the constructor! The itemIDs are broken there.
        /// </summary>
        /// <param name="uiStateThatHoldsThis"></param>
        public RecipeBrowserUIElement(UIState uiStateThatHoldsThis) {
            _parentUIState = uiStateThatHoldsThis;
            for (int i = 1; i < TextureAssets.Item.Length; i++) {
                if (!ItemID.Sets.Deprecated[i]) {
                    allIDs.Add(i);
                }
            }

            debugItems();

            _itemIdsAvailableToShow = new List<int>();
            _filterer = new EntryFilterer<Item, IItemEntryFilter>();

            List<IItemEntryFilter> itemEntryFilterList = new List<IItemEntryFilter> {
                new ItemFilters.Weapon(),
                new ItemFilters.Armor(),
                new ItemFilters.Vanity(),
                new ItemFilters.BuildingBlock(),
                new ItemFilters.Furniture(),
                new ItemFilters.Accessories(),
                new ItemFilters.MiscAccessories(),
                new ItemFilters.Consumables(),
                new ItemFilters.Tools(),
                new ItemFilters.Materials()
            };
            List<IItemEntryFilter> filters = new List<IItemEntryFilter>();
            filters.AddRange(itemEntryFilterList);
            filters.Add(new ItemFilters.MiscFallback(itemEntryFilterList));
            _filterer.AddFilters(filters);
            _filterer.SetSearchFilterObject(new ItemFilters.BySearch());
            _sorter = new EntrySorter<int, ICreativeItemSortStep>();
            _sorter.AddSortSteps(new List<ICreativeItemSortStep> {
                new SortingSteps.ByCreativeSortingId(),
                new SortingSteps.Alphabetical()
            });
            BuildPage();
            SetPageTypeToShow(InfiniteItemsDisplayPage.InfiniteItemsPickup);
            hasBeenSetUp = true;
            UpdateContents();
        }

        private void debugItems() {
            foreach (var var1 in ContentSamples.ItemsByType) {
                CalamityQOLMod.i.Logger.Warn($"{var1.Key}, {var1.Value.Name}");
            }
            foreach (var var1 in ContentSamples.ItemCreativeSortingId) {
                CalamityQOLMod.i.Logger.Warn($"{var1.Key}, {var1.Value.Group}");
            }
        }

        private void BuildPage() {
            _lastCheckedVersionForEdits = -1;
            RemoveAllChildren();
            SetPadding(0.0f);
            UIElement totalContainer1 = new UIElement {
                Width = StyleDimension.Fill,
                Height = StyleDimension.Fill
            };
            totalContainer1.SetPadding(0.0f);
            _containerInfinites = totalContainer1;
            UIElement totalContainer2 = new UIElement {
                Width = StyleDimension.Fill,
                Height = StyleDimension.Fill
            };
            totalContainer2.SetPadding(0.0f);
            _containerSacrifice = totalContainer2;
            BuildInfinitesMenuContents(totalContainer1);
            BuildSacrificeMenuContents(totalContainer2);
            OnUpdate += UICreativeInfiniteItemsDisplay_OnUpdate;
        }

        private void Hover_OnUpdate(UIElement affectedElement) {
            if (!_hovered)
                return;
            Main.LocalPlayer.mouseInterface = true;
        }

        private void Hover_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) => _hovered = false;

        private void Hover_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) => _hovered = true;

        private static UIPanel CreateBasicPanel() {
            UIPanel element = new UIPanel();
            SetBasicSizesForCreativeSacrificeOrInfinitesPanel(element);
            element.BackgroundColor *= 0.8f;
            element.BorderColor *= 0.8f;
            return element;
        }

        private static void SetBasicSizesForCreativeSacrificeOrInfinitesPanel(UIElement element) {
            element.Width = new StyleDimension(0.0f, 1f);
            element.Height = new StyleDimension(-38f, 1f);
            element.Top = new StyleDimension(38f, 0.0f);
        }

        private void BuildInfinitesMenuContents(UIElement totalContainer) {
            UIPanel basicPanel = CreateBasicPanel();
            totalContainer.Append(basicPanel);
            basicPanel.OnUpdate += Hover_OnUpdate;
            basicPanel.OnMouseOver += Hover_OnMouseOver;
            basicPanel.OnMouseOut += Hover_OnMouseOut;
            UIDynamicItemCollection dynamicItemCollection = _itemGrid = new UIDynamicItemCollection();
            UIElement uiElement = new UIElement {
                Height = new StyleDimension(24f, 0.0f),
                Width = new StyleDimension(0.0f, 1f)
            };
            uiElement.SetPadding(0.0f);
            basicPanel.Append(uiElement);
            AddSearchBar(uiElement);
            _searchBar.SetContents(null, true);
            UIList uiList = new UIList {
                Width = new StyleDimension(-25f, 1f),
                Height = new StyleDimension(-28f, 1f),
                VAlign = 1f,
                HAlign = 0.0f
            };
            UIList element1 = uiList;
            basicPanel.Append(element1);
            float num = 4f;
            UIScrollbar uiScrollbar1 = new UIScrollbar {
                Height = new StyleDimension((float)(-28.0 - num * 2.0), 1f),
                Top = new StyleDimension(0.0f - num, 0.0f),
                VAlign = 1f,
                HAlign = 1f
            };
            UIScrollbar uiScrollbar2 = uiScrollbar1;
            basicPanel.Append(uiScrollbar2);
            element1.SetScrollbar(uiScrollbar2);
            element1.Add(dynamicItemCollection);
            UICreativeItemsInfiniteFilteringOptions element2 =
                new UICreativeItemsInfiniteFilteringOptions(_filterer, "CreativeInfinitesFilter");
            element2.OnClickingOption += filtersHelper_OnClickingOption;
            element2.Left = new StyleDimension(20f, 0.0f);
            totalContainer.Append(element2);
            element2.OnUpdate += Hover_OnUpdate;
            element2.OnMouseOver += Hover_OnMouseOver;
            element2.OnMouseOut += Hover_OnMouseOut;
        }

        private void BuildSacrificeMenuContents(UIElement totalContainer) {
            UIPanel basicPanel = CreateBasicPanel();
            basicPanel.VAlign = 0.5f;
            basicPanel.Height = new StyleDimension(170f, 0.0f);
            basicPanel.Width = new StyleDimension(170f, 0.0f);
            basicPanel.Top = new StyleDimension();
            totalContainer.Append(basicPanel);
            basicPanel.OnUpdate += Hover_OnUpdate;
            basicPanel.OnMouseOver += Hover_OnMouseOver;
            basicPanel.OnMouseOut += Hover_OnMouseOut;
            AddCogsForSacrificeMenu(basicPanel);
            _pistonParticleAsset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Spark");
            float pixels = 0.0f;
            UIImage uiImage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Slots"));
            uiImage.HAlign = 0.5f;
            uiImage.VAlign = 0.5f;
            uiImage.Top = new StyleDimension(-20f, 0.0f);
            uiImage.Left = new StyleDimension(pixels, 0.0f);
            UIImage element1 = uiImage;
            basicPanel.Append(element1);
            Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_FramedPistons");
            UIImageFramed uiImageFramed = new UIImageFramed(asset, asset.Frame(verticalFrames: 9));
            uiImageFramed.HAlign = 0.5f;
            uiImageFramed.VAlign = 0.5f;
            uiImageFramed.Top = new StyleDimension(-20f, 0.0f);
            uiImageFramed.Left = new StyleDimension(pixels, 0.0f);
            uiImageFramed.IgnoresMouseInteraction = true;
            UIImageFramed element2 = uiImageFramed;
            basicPanel.Append(element2);
            _sacrificePistons = element2;
            UIParticleLayer uiParticleLayer = new UIParticleLayer();
            uiParticleLayer.Width = new StyleDimension(0.0f, 1f);
            uiParticleLayer.Height = new StyleDimension(0.0f, 1f);
            uiParticleLayer.AnchorPositionOffsetByPercents = Vector2.One / 2f;
            uiParticleLayer.AnchorPositionOffsetByPixels = Vector2.Zero;
            _pistonParticleSystem = uiParticleLayer;
            element2.Append(_pistonParticleSystem);
            UIElement element3 = Main.CreativeMenu.ProvideItemSlotElement(0);
            element3.HAlign = 0.5f;
            element3.VAlign = 0.5f;
            element3.Top = new StyleDimension(-15f, 0.0f);
            element3.Left = new StyleDimension(pixels, 0.0f);
            element3.SetSnapPoint("CreativeSacrificeSlot", 0);
            element1.Append(element3);
            UIText uiText1 = new UIText("(0/50)", 0.8f);
            uiText1.Top = new StyleDimension(10f, 0.0f);
            uiText1.Left = new StyleDimension(pixels, 0.0f);
            uiText1.HAlign = 0.5f;
            uiText1.VAlign = 0.5f;
            uiText1.IgnoresMouseInteraction = true;
            UIText element4 = uiText1;
            element4.OnUpdate += descriptionText_OnUpdate;
            basicPanel.Append(element4);
            UIPanel uiPanel = new UIPanel();
            uiPanel.Top = new StyleDimension(0.0f, 0.0f);
            uiPanel.Left = new StyleDimension(pixels, 0.0f);
            uiPanel.HAlign = 0.5f;
            uiPanel.VAlign = 1f;
            uiPanel.Width = new StyleDimension(124f, 0.0f);
            uiPanel.Height = new StyleDimension(30f, 0.0f);
            UIPanel element5 = uiPanel;
            UIText uiText2 = new UIText(Language.GetText("CreativePowers.ConfirmInfiniteItemSacrifice"), 0.8f);
            uiText2.IgnoresMouseInteraction = true;
            uiText2.HAlign = 0.5f;
            uiText2.VAlign = 0.5f;
            UIText element6 = uiText2;
            element5.Append(element6);
            element5.SetSnapPoint("CreativeSacrificeConfirm", 0);
            element5.OnLeftClick += sacrificeButton_OnClick;
            element5.OnMouseOver += FadedMouseOver;
            element5.OnMouseOut += FadedMouseOut;
            element5.OnUpdate += research_OnUpdate;
            basicPanel.Append(element5);
            basicPanel.OnUpdate += sacrificeWindow_OnUpdate;
        }

        private void research_OnUpdate(UIElement affectedElement) {
            if (!affectedElement.IsMouseHovering)
                return;
            Main.instance.MouseText(Language.GetTextValue("CreativePowers.ResearchButtonTooltip"));
        }

        private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
            ((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
            ((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
        }

        private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement) {
            ((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
            ((UIPanel)evt.Target).BorderColor = Color.Black;
        }

        private void AddCogsForSacrificeMenu(UIElement sacrificesContainer) {
            UIElement uiElement = new UIElement();
            uiElement.IgnoresMouseInteraction = true;
            SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uiElement);
            uiElement.VAlign = 0.5f;
            uiElement.Height = new StyleDimension(170f, 0.0f);
            uiElement.Width = new StyleDimension(280f, 0.0f);
            uiElement.Top = new StyleDimension();
            uiElement.SetPadding(0.0f);
            sacrificesContainer.Append(uiElement);
            Vector2 vector2 = new Vector2(-10f, -10f);
            AddSymetricalCogsPair(uiElement, new Vector2(22f, 1f) + vector2, "Images/UI/Creative/Research_GearC",
                _sacrificeCogsSmall);
            AddSymetricalCogsPair(uiElement, new Vector2(1f, 28f) + vector2, "Images/UI/Creative/Research_GearB",
                _sacrificeCogsMedium);
            AddSymetricalCogsPair(uiElement, new Vector2(5f, 5f) + vector2, "Images/UI/Creative/Research_GearA",
                _sacrificeCogsBig);
        }

        private void sacrificeWindow_OnUpdate(UIElement affectedElement) => UpdateVisualFrame();

        private void UpdateVisualFrame() {
            float num1 = 0.05f;
            float animationProgress = GetSacrificeAnimationProgress();
            double lerpValue = Utils.GetLerpValue(1f, 0.7f, animationProgress, true);
            float num2 = 1f + (float)(lerpValue * lerpValue) * 2f;
            float num3 = num1 * num2;
            float num4 = 1f;
            OffsetRotationsForCogs(2f * num3, _sacrificeCogsSmall);
            OffsetRotationsForCogs((float)(1.1428571939468384 * num3), _sacrificeCogsMedium);
            OffsetRotationsForCogs((0.0f - num4) * num3, _sacrificeCogsBig);
            int frameY = 0;
            if (_sacrificeAnimationTimeLeft != 0) {
                float num5 = 0.1f;
                float num6 = 0.06666667f;
                frameY = animationProgress >= 1.0 - num5
                    ? 8
                    : (animationProgress >= 1.0 - num5 * 2.0
                        ? 7
                        : (animationProgress >= 1.0 - num5 * 3.0
                            ? 6
                            : (animationProgress >= num6 * 4.0
                                ? 5
                                : (animationProgress >= num6 * 3.0
                                    ? 4
                                    : (animationProgress >= num6 * 2.0
                                        ? 3
                                        : (animationProgress < (double)num6 ? 1 : 2))))));
                if (_sacrificeAnimationTimeLeft == 56) {
                    SoundEngine.PlaySound(SoundID.Research);
                    Vector2 vector2 = new Vector2(0.0f, 0.16350001f);
                    for (int index = 0; index < 15; ++index) {
                        Vector2 initialVelocity = Main.rand.NextVector2Circular(4f, 3f);
                        if (initialVelocity.Y > 0.0)
                            initialVelocity.Y = 0.0f - initialVelocity.Y;
                        initialVelocity.Y -= 2f;
                        _pistonParticleSystem.AddParticle(
                            new CreativeSacrificeParticle(_pistonParticleAsset, new Rectangle?(), initialVelocity,
                                Vector2.Zero) {
                                AccelerationPerFrame = vector2,
                                ScaleOffsetPerFrame = -0.016666668f
                            });
                    }
                }

                if (_sacrificeAnimationTimeLeft == 40 && _researchComplete) {
                    _researchComplete = false;
                    SoundEngine.PlaySound(SoundID.ResearchComplete);
                }
            }

            _sacrificePistons.SetFrame(1, 9, 0, frameY, 0, 0);
        }

        private static void OffsetRotationsForCogs(float rotationOffset, List<UIImage> cogsList) {
            cogsList[0].Rotation += rotationOffset;
            cogsList[1].Rotation -= rotationOffset;
        }

        private void AddSymetricalCogsPair(
            UIElement sacrificesContainer,
            Vector2 cogOFfsetsInPixels,
            string assetPath,
            List<UIImage> imagesList) {
            Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(assetPath);
            cogOFfsetsInPixels += -asset.Size() / 2f;
            UIImage uiImage1 = new UIImage(asset);
            uiImage1.NormalizedOrigin = Vector2.One / 2f;
            uiImage1.Left = new StyleDimension(cogOFfsetsInPixels.X, 0.0f);
            uiImage1.Top = new StyleDimension(cogOFfsetsInPixels.Y, 0.0f);
            UIImage element1 = uiImage1;
            imagesList.Add(element1);
            sacrificesContainer.Append(element1);
            UIImage uiImage2 = new UIImage(asset);
            uiImage2.NormalizedOrigin = Vector2.One / 2f;
            uiImage2.HAlign = 1f;
            uiImage2.Left = new StyleDimension(0.0f - cogOFfsetsInPixels.X, 0.0f);
            uiImage2.Top = new StyleDimension(cogOFfsetsInPixels.Y, 0.0f);
            UIImage element2 = uiImage2;
            imagesList.Add(element2);
            sacrificesContainer.Append(element2);
        }

        private void descriptionText_OnUpdate(UIElement affectedElement) {
            UIText uiText1 = affectedElement as UIText;
            int itemIdChecked;
            int amountWeHave;
            int amountNeededTotal;
            bool sacrificeNumbers =
                Main.CreativeMenu.GetSacrificeNumbers(out itemIdChecked, out amountWeHave, out amountNeededTotal);
            Main.CreativeMenu.ShouldDrawSacrificeArea();
            if (!Main.mouseItem.IsAir)
                ForgetItemSacrifice();
            if (itemIdChecked == 0) {
                if (_lastItemIdSacrificed != 0 && _lastItemAmountWeNeededTotal != _lastItemAmountWeHad) {
                    UIText uiText2 = uiText1;
                    DefaultInterpolatedStringHandler interpolatedStringHandler =
                        new DefaultInterpolatedStringHandler(3, 2);
                    interpolatedStringHandler.AppendLiteral("(");
                    interpolatedStringHandler.AppendFormatted(_lastItemAmountWeHad);
                    interpolatedStringHandler.AppendLiteral("/");
                    interpolatedStringHandler.AppendFormatted(_lastItemAmountWeNeededTotal);
                    interpolatedStringHandler.AppendLiteral(")");
                    string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                    uiText2.SetText(stringAndClear);
                }
                else
                    uiText1.SetText("???");
            }
            else {
                ForgetItemSacrifice();
                if (!sacrificeNumbers) {
                    uiText1.SetText("X");
                }
                else {
                    UIText uiText3 = uiText1;
                    DefaultInterpolatedStringHandler interpolatedStringHandler =
                        new DefaultInterpolatedStringHandler(3, 2);
                    interpolatedStringHandler.AppendLiteral("(");
                    interpolatedStringHandler.AppendFormatted(amountWeHave);
                    interpolatedStringHandler.AppendLiteral("/");
                    interpolatedStringHandler.AppendFormatted(amountNeededTotal);
                    interpolatedStringHandler.AppendLiteral(")");
                    string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                    uiText3.SetText(stringAndClear);
                }
            }
        }

        private void sacrificeButton_OnClick(UIMouseEvent evt, UIElement listeningElement) => SacrificeWhatYouCan();

        public void SacrificeWhatYouCan() {
            int itemIdChecked;
            int amountWeHave;
            int amountNeededTotal;
            Main.CreativeMenu.GetSacrificeNumbers(out itemIdChecked, out amountWeHave, out amountNeededTotal);
            int amountWeSacrificed;
            switch (Main.CreativeMenu.SacrificeItem(out amountWeSacrificed)) {
                case CreativeUI.ItemSacrificeResult.SacrificedButNotDone:
                    _researchComplete = false;
                    BeginSacrificeAnimation();
                    RememberItemSacrifice(itemIdChecked, amountWeHave + amountWeSacrificed, amountNeededTotal);
                    break;
                case CreativeUI.ItemSacrificeResult.SacrificedAndDone:
                    _researchComplete = true;
                    BeginSacrificeAnimation();
                    RememberItemSacrifice(itemIdChecked, amountWeHave + amountWeSacrificed, amountNeededTotal);
                    break;
            }
        }

        public void StopPlayingAnimation() {
            ForgetItemSacrifice();
            _sacrificeAnimationTimeLeft = 0;
            _pistonParticleSystem.ClearParticles();
            UpdateVisualFrame();
        }

        private void RememberItemSacrifice(int itemId, int amountWeHave, int amountWeNeedTotal) {
            _lastItemIdSacrificed = itemId;
            _lastItemAmountWeHad = amountWeHave;
            _lastItemAmountWeNeededTotal = amountWeNeedTotal;
        }

        private void ForgetItemSacrifice() {
            _lastItemIdSacrificed = 0;
            _lastItemAmountWeHad = 0;
            _lastItemAmountWeNeededTotal = 0;
        }

        private void BeginSacrificeAnimation() => _sacrificeAnimationTimeLeft = 60;

        private void UpdateSacrificeAnimation() {
            if (_sacrificeAnimationTimeLeft <= 0)
                return;
            --_sacrificeAnimationTimeLeft;
        }

        private float GetSacrificeAnimationProgress() =>
            Utils.GetLerpValue(60f, 0.0f, _sacrificeAnimationTimeLeft, true);

        public void SetPageTypeToShow(
            InfiniteItemsDisplayPage page) {
            _showSacrificesInsteadOfInfinites = page == InfiniteItemsDisplayPage.InfiniteItemsResearch;
        }

        private void UICreativeInfiniteItemsDisplay_OnUpdate(UIElement affectedElement) {
            RemoveAllChildren();
            CreativeUnlocksTracker playerCreativeTracker = Main.LocalPlayerCreativeTracker;
            if (_lastTrackerCheckedForEdits != playerCreativeTracker) {
                _lastTrackerCheckedForEdits = playerCreativeTracker;
                _lastCheckedVersionForEdits = -1;
            }

            int lastEditId = playerCreativeTracker.ItemSacrifices.LastEditId;
            if (_lastCheckedVersionForEdits != lastEditId) {
                _lastCheckedVersionForEdits = lastEditId;
                UpdateContents();
            }

            if (_showSacrificesInsteadOfInfinites)
                Append(_containerSacrifice);
            else
                Append(_containerInfinites);
            UpdateSacrificeAnimation();
        }

        private void filtersHelper_OnClickingOption() => UpdateContents();

        private void UpdateContents() {

            _itemIdsAvailableToShow.Clear();
            _itemIdsAvailableToShow.AddRange(allIDs.Where(x => _filterer.FitsFilter(ContentSamples.ItemsByType[x])));
            _itemIdsAvailableToShow.Sort(_sorter);
            _itemGrid.SetContentsToShow(_itemIdsAvailableToShow);
        }

        private void AddSearchBar(UIElement searchArea) {
            UIImageButton uiImageButton1 =
                new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search"));
            uiImageButton1.VAlign = 0.5f;
            uiImageButton1.HAlign = 0.0f;
            UIImageButton element1 = uiImageButton1;
            element1.OnLeftClick += Click_SearchArea;
            element1.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border"));
            element1.SetVisibility(1f, 1f);
            element1.SetSnapPoint("CreativeInfinitesSearch", 0);
            searchArea.Append(element1);
            UIPanel uiPanel1 = new UIPanel();
            uiPanel1.Width = new StyleDimension((float)(0.0 - element1.Width.Pixels - 3.0), 1f);
            uiPanel1.Height = new StyleDimension(0.0f, 1f);
            uiPanel1.VAlign = 0.5f;
            uiPanel1.HAlign = 1f;
            UIPanel uiPanel2 = uiPanel1;
            _searchBoxPanel = uiPanel1;
            UIPanel element2 = uiPanel2;
            element2.BackgroundColor = new Color(35, 40, 83);
            element2.BorderColor = new Color(35, 40, 83);
            element2.SetPadding(0.0f);
            searchArea.Append(element2);
            UISearchBar uiSearchBar1 = new UISearchBar(Language.GetText("UI.PlayerNameSlot"), 0.8f);
            uiSearchBar1.Width = new StyleDimension(0.0f, 1f);
            uiSearchBar1.Height = new StyleDimension(0.0f, 1f);
            uiSearchBar1.HAlign = 0.0f;
            uiSearchBar1.VAlign = 0.5f;
            uiSearchBar1.Left = new StyleDimension(0.0f, 0.0f);
            uiSearchBar1.IgnoresMouseInteraction = true;
            UISearchBar uiSearchBar2 = uiSearchBar1;
            _searchBar = uiSearchBar1;
            UISearchBar element3 = uiSearchBar2;
            element2.OnLeftClick += Click_SearchArea;
            element3.OnContentsChanged += OnSearchContentsChanged;
            element2.Append(element3);
            element3.OnStartTakingInput += OnStartTakingInput;
            element3.OnEndTakingInput += OnEndTakingInput;
            element3.OnNeedingVirtualKeyboard += OpenVirtualKeyboardWhenNeeded;
            element3.OnCanceledTakingInput += OnCanceledInput;
            UIImageButton uiImageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"));
            uiImageButton2.HAlign = 1f;
            uiImageButton2.VAlign = 0.5f;
            uiImageButton2.Left = new StyleDimension(-2f, 0.0f);
            UIImageButton element4 = uiImageButton2;
            element4.OnMouseOver += searchCancelButton_OnMouseOver;
            element4.OnLeftClick += searchCancelButton_OnClick;
            element2.Append(element4);
        }

        private void searchCancelButton_OnClick(UIMouseEvent evt, UIElement listeningElement) {
            if (_searchBar.HasContents) {
                _searchBar.SetContents(null, true);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
            else
                SoundEngine.PlaySound(SoundID.MenuTick);
        }

        private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) =>
            SoundEngine.PlaySound(SoundID.MenuTick);

        private void OnCanceledInput() => Main.LocalPlayer.ToggleInv();

        private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement) {
            if (evt.Target.Parent == _searchBoxPanel)
                return;
            _searchBar.ToggleTakingText();
            _didClickSearchBar = true;
        }

        public override void LeftClick(UIMouseEvent evt) {
            base.LeftClick(evt);
            AttemptStoppingUsingSearchbar(evt);
        }

        public override void RightClick(UIMouseEvent evt) {
            base.RightClick(evt);
            AttemptStoppingUsingSearchbar(evt);
        }

        private void AttemptStoppingUsingSearchbar(UIMouseEvent evt) => _didClickSomething = true;

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (_didClickSomething && !_didClickSearchBar && _searchBar.IsWritingText)
                _searchBar.ToggleTakingText();
            _didClickSomething = false;
            _didClickSearchBar = false;
        }

        private void OnSearchContentsChanged(string contents) {
            _searchString = contents;
            _filterer.SetSearchFilter(contents);
            UpdateContents();
        }

        private void OnStartTakingInput() => _searchBoxPanel.BorderColor = Main.OurFavoriteColor;

        private void OnEndTakingInput() => _searchBoxPanel.BorderColor = new Color(35, 40, 83);

        private void OpenVirtualKeyboardWhenNeeded() {
            int length = 40;
            UIVirtualKeyboard uiVirtualKeyboard = new UIVirtualKeyboard(Language.GetText("UI.PlayerNameSlot").Value,
                _searchString, OnFinishedSettingName, GoBackHere, 3, true);
            uiVirtualKeyboard.SetMaxInputLength(length);
            uiVirtualKeyboard.CustomEscapeAttempt = EscapeVirtualKeyboard;
            IngameFancyUI.OpenUIState(uiVirtualKeyboard);
        }

        private bool EscapeVirtualKeyboard() {
            IngameFancyUI.Close();
            Main.playerInventory = true;
            if (_searchBar.IsWritingText)
                _searchBar.ToggleTakingText();
            Main.CreativeMenu.ToggleMenu();
            return true;
        }

        private static UserInterface GetCurrentInterface() {
            UserInterface activeInstance = UserInterface.ActiveInstance;
            return Main.gameMenu ? Main.MenuUI : Main.InGameUI;
        }

        private void OnFinishedSettingName(string name) {
            _searchBar.SetContents(name.Trim());
            GoBackHere();
        }

        private void GoBackHere() {
            IngameFancyUI.Close();
            Main.CreativeMenu.ToggleMenu();
            _searchBar.ToggleTakingText();
            Main.CreativeMenu.GamepadMoveToSearchButtonHack = true;
        }

        public int GetItemsPerLine() => _itemGrid.GetItemsPerLine();

        public enum InfiniteItemsDisplayPage {
            InfiniteItemsPickup,
            InfiniteItemsResearch,
        }
    }
}