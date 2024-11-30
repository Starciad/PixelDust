﻿using Microsoft.Xna.Framework;

using StardustSandbox.Core.Constants;
using StardustSandbox.Core.Constants.GUI.Common;
using StardustSandbox.Core.Enums.General;
using StardustSandbox.Core.GUISystem.Elements;
using StardustSandbox.Core.GUISystem.Elements.Graphics;
using StardustSandbox.Core.Interfaces.GUI;
using StardustSandbox.Core.Items;
using StardustSandbox.Core.Mathematics.Primitives;

namespace StardustSandbox.ContentBundle.GUISystem.Hud
{
    public partial class SGUI_HUD
    {
        private ISGUILayoutBuilder layout;

        private SGUIElement topToolbarContainer;
        private SGUIElement leftToolbarContainer;
        private SGUIElement rightToolbarContainer;

        private SGUIImageElement toolbarElementSearchButton;
        private readonly (SGUIImageElement background, SGUIImageElement icon)[] toolbarElementSlots = new (SGUIImageElement, SGUIImageElement)[SHUDConstants.HEADER_ELEMENT_SELECTION_SLOTS_LENGTH];

        protected override void OnBuild(ISGUILayoutBuilder layout)
        {
            this.layout = layout;

            // Containers
            this.topToolbarContainer = new SGUIContainerElement(this.SGameInstance);
            this.leftToolbarContainer = new SGUIContainerElement(this.SGameInstance);
            this.rightToolbarContainer = new SGUIContainerElement(this.SGameInstance);

            this.topToolbarContainer.PositionRelativeToScreen();
            this.leftToolbarContainer.PositionRelativeToScreen();
            this.rightToolbarContainer.PositionRelativeToScreen();

            layout.AddElement(this.topToolbarContainer);
            layout.AddElement(this.leftToolbarContainer);
            layout.AddElement(this.rightToolbarContainer);

            // Styles
            this.topToolbarContainer.SetSize(new SSize2F(SScreenConstants.DEFAULT_SCREEN_WIDTH, 96));

            // Process
            BuildTopToolbar(this.topToolbarContainer);
            BuildLeftToolbar(this.leftToolbarContainer);
            BuildRightToolbar(this.rightToolbarContainer);
        }

        private void BuildTopToolbar(SGUIElement container)
        {
            SGUIImageElement slotAreaBackground = new(this.SGameInstance);

            this.layout.AddElement(slotAreaBackground);

            // Background
            slotAreaBackground.SetTexture(this.particleTexture);
            slotAreaBackground.SetScale(container.Size.ToVector2());
            slotAreaBackground.SetColor(new Color(Color.White, 32));
            slotAreaBackground.SetSize(SSize2F.One);

            // Append
            slotAreaBackground.PositionRelativeToElement(container);

            // ================================= //

            CreateSlots();
            CreateSearchSlot();

            // ================================= //

            void CreateSlots()
            {
                int slotSize = SHUDConstants.HEADER_ELEMENT_SELECTION_SLOTS_SIZE;
                int slotScale = SHUDConstants.SLOT_SCALE;
                int slotSpacing = slotSize * 2;

                Vector2 slotMargin = new(slotSpacing, 0);

                for (int i = 0; i < SHUDConstants.HEADER_ELEMENT_SELECTION_SLOTS_LENGTH; i++)
                {
                    // Creation
                    SGUIImageElement slotBackground = new(this.SGameInstance);
                    SGUIImageElement slotIcon = new(this.SGameInstance);

                    // Background
                    slotBackground.SetTexture(this.squareShapeTexture);
                    slotBackground.SetOriginPivot(SCardinalDirection.Center);
                    slotBackground.SetScale(new Vector2(slotScale));
                    slotBackground.SetPositionAnchor(SCardinalDirection.West);
                    slotBackground.SetSize(new SSize2(slotSize));
                    slotBackground.SetMargin(slotMargin);

                    SItem selectedItem = this.SGameInstance.ItemDatabase.Items[i];
                    if (!slotBackground.ContainsData(SHUDConstants.DATA_FILED_ELEMENT_ID))
                    {
                        slotBackground.AddData(SHUDConstants.DATA_FILED_ELEMENT_ID, selectedItem.Identifier);
                    }

                    // Icon
                    slotIcon.SetTexture(selectedItem.IconTexture);
                    slotIcon.SetOriginPivot(SCardinalDirection.Center);
                    slotIcon.SetScale(new Vector2(1.5f));
                    slotIcon.SetSize(new SSize2(slotSize));

                    // Update
                    slotBackground.PositionRelativeToElement(slotAreaBackground);
                    slotIcon.PositionRelativeToElement(slotBackground);

                    // Save
                    this.toolbarElementSlots[i] = (slotBackground, slotIcon);

                    // Spacing
                    slotMargin.X += slotSpacing + (slotSize / 2);

                    this.layout.AddElement(slotBackground);
                    this.layout.AddElement(slotIcon);
                }
            }

            void CreateSearchSlot()
            {
                SGUIImageElement slotSearchBackground = new(this.SGameInstance);
                SGUIImageElement slotIcon = new(this.SGameInstance);

                // Background
                slotSearchBackground.SetTexture(this.squareShapeTexture);
                slotSearchBackground.SetOriginPivot(SCardinalDirection.Center);
                slotSearchBackground.SetScale(new Vector2(SHUDConstants.SLOT_SCALE + 0.45f));
                slotSearchBackground.SetPositionAnchor(SCardinalDirection.East);
                slotSearchBackground.SetSize(new SSize2(SHUDConstants.HEADER_ELEMENT_SELECTION_SLOTS_SIZE));
                slotSearchBackground.SetMargin(new Vector2(SHUDConstants.HEADER_ELEMENT_SELECTION_SLOTS_SIZE * 2 * -1, 0));
                slotSearchBackground.PositionRelativeToElement(slotAreaBackground);

                // Icon
                slotIcon.SetTexture(this.SGameInstance.AssetDatabase.GetTexture("icon_gui_1"));
                slotIcon.SetOriginPivot(SCardinalDirection.Center);
                slotIcon.SetScale(new Vector2(2f));
                slotIcon.SetSize(new SSize2(1));
                slotIcon.PositionRelativeToElement(slotSearchBackground);

                this.layout.AddElement(slotSearchBackground);
                this.layout.AddElement(slotIcon);

                this.toolbarElementSearchButton = slotSearchBackground;
            }
        }
        private void BuildLeftToolbar(SGUIElement container) { return; }
        private void BuildRightToolbar(SGUIElement container) { return; }
    }
}
