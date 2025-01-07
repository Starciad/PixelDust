﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardustSandbox.ContentBundle.GUISystem.Helpers.General;
using StardustSandbox.ContentBundle.GUISystem.Helpers.Interactive;
using StardustSandbox.ContentBundle.Localization.GUIs;
using StardustSandbox.ContentBundle.Localization.Statements;
using StardustSandbox.Core.Colors;
using StardustSandbox.Core.Constants.GUISystem.GUIs.Hud;
using StardustSandbox.Core.GUISystem;
using StardustSandbox.Core.GUISystem.Events;
using StardustSandbox.Core.Interfaces;

namespace StardustSandbox.ContentBundle.GUISystem.GUIs.Hud.Complements
{
    internal sealed partial class SGUI_Pause : SGUISystem
    {
        private readonly Texture2D particleTexture;
        private readonly Texture2D panelBackgroundTexture;
        private readonly Texture2D guiButton1Texture;
        private readonly Texture2D[] iconTextures;
        private readonly SpriteFont bigApple3PMSpriteFont;

        private readonly SButton[] menuButtons;

        internal SGUI_Pause(ISGame gameInstance, string identifier, SGUIEvents guiEvents) : base(gameInstance, identifier, guiEvents)
        {
            this.particleTexture = gameInstance.AssetDatabase.GetTexture("particle_1");
            this.panelBackgroundTexture = gameInstance.AssetDatabase.GetTexture("gui_background_10");
            this.guiButton1Texture = gameInstance.AssetDatabase.GetTexture("gui_button_1");
            this.bigApple3PMSpriteFont = gameInstance.AssetDatabase.GetSpriteFont("font_2");

            this.iconTextures = [
                gameInstance.AssetDatabase.GetTexture("icon_gui_16"),
            ];

            this.menuButtons = [
                new(this.iconTextures[0], SLocalization_Statements.Exit, SLocalization_GUIs.Button_Exit_Description, ExitButtonAction),
            ];

            this.menuButtonSlots = new SSlot[this.menuButtons.Length];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateMenuButtons();
        }

        private void UpdateMenuButtons()
        {
            for (int i = 0; i < this.menuButtonSlots.Length; i++)
            {
                SSlot slot = this.menuButtonSlots[i];

                if (this.GUIEvents.OnMouseClick(slot.BackgroundElement.Position, new(SGUI_HUDConstants.SLOT_SIZE)))
                {
                    this.menuButtons[i].ClickAction?.Invoke();
                }

                slot.BackgroundElement.Color = this.GUIEvents.OnMouseOver(slot.BackgroundElement.Position, new(SGUI_HUDConstants.SLOT_SIZE)) ? SColorPalette.HoverColor : SColorPalette.White;
            }
        }
    }
}
