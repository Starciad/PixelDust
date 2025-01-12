﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardustSandbox.ContentBundle.GUISystem.Elements.Graphics;
using StardustSandbox.ContentBundle.GUISystem.Elements.Textual;
using StardustSandbox.Core.Colors;
using StardustSandbox.Core.Constants;
using StardustSandbox.Core.Enums.General;
using StardustSandbox.Core.GUISystem;
using StardustSandbox.Core.GUISystem.Elements;
using StardustSandbox.Core.Interfaces.General;
using StardustSandbox.Core.Mathematics.Primitives;

using System;

namespace StardustSandbox.ContentBundle.GUISystem.Elements.Informational
{
    public sealed class SGUITooltipBoxElement : SGUIElement
    {
        public bool HasContent => this.hasContent;

        public bool IsShowing { get; private set; }
        public SSize2F MinimumSize { get; set; }
        public SSize2F MaximumSize { get; set; }

        public SGUISliceImageElement BackgroundImageElement => this.backgroundImageElement;
        public SGUILabelElement TitleElement => this.titleElement;
        public SGUITextElement DescriptionElement => this.descriptionElement;

        private bool hasContent;

        private readonly SGUISliceImageElement backgroundImageElement;
        private readonly SGUILabelElement titleElement;
        private readonly SGUITextElement descriptionElement;

        private readonly SGUILayout tooltipLayout;

        public SGUITooltipBoxElement(ISGame gameInstance) : base(gameInstance)
        {
            this.IsVisible = true;
            this.ShouldUpdate = true;

            this.Margin = new(60f);

            this.backgroundImageElement = new(this.SGameInstance)
            {
                Texture = this.SGameInstance.AssetDatabase.GetTexture("gui_background_4"),
                Color = SColorPalette.DarkPurple,
                Scale = new(10f, 10f),
                PositionAnchor = SCardinalDirection.Center,
                Size = new(32f),
            };

            this.titleElement = new(this.SGameInstance)
            {
                Scale = new(0.12f),
                SpriteFont = this.SGameInstance.AssetDatabase.GetSpriteFont(SFontFamilyConstants.DIGITAL_DISCO),
                Margin = new(0, -16f),
            };

            this.descriptionElement = new(this.SGameInstance)
            {
                Scale = new(0.078f),
                Margin = new(0, 64f),
                LineHeight = 1.25f,
                SpriteFont = this.SGameInstance.AssetDatabase.GetSpriteFont(SFontFamilyConstants.PIXEL_OPERATOR),
            };

            this.tooltipLayout = new(gameInstance);
            this.tooltipLayout.AddElement(this.backgroundImageElement);
            this.tooltipLayout.AddElement(this.titleElement);
            this.tooltipLayout.AddElement(this.descriptionElement);

            this.MaximumSize = new(SScreenConstants.DEFAULT_SCREEN_WIDTH, SScreenConstants.DEFAULT_SCREEN_HEIGHT);
        }

        public override void Initialize()
        {
            this.tooltipLayout.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.tooltipLayout.Update(gameTime);

            UpdateSize();
            UpdatePosition();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.tooltipLayout.Draw(gameTime, spriteBatch);
        }

        public void RefreshDisplay(string title, string description)
        {
            if (this.IsVisible)
            {
                if (!this.hasContent)
                {
                    this.titleElement.SetTextualContent(title);
                    this.descriptionElement.SetTextualContent(description);

                    UpdateSize();
                    UpdatePosition();

                    this.hasContent = true;
                }

                if (!this.IsShowing)
                {
                    Show();
                }
            }
            else
            {
                Hide();

                if (this.hasContent)
                {
                    this.hasContent = false;

                    this.titleElement.ClearTextualContent();
                    this.descriptionElement.ClearTextualContent();
                }
            }
        }

        public void Show()
        {
            this.IsVisible = true;
            this.IsShowing = true;
        }

        public void Hide()
        {
            this.IsVisible = false;
            this.IsShowing = false;
        }

        public void SetTitle(string value)
        {
            this.titleElement.SetTextualContent(value);

            UpdateSize();
            UpdatePosition();
        }

        public void SetDescription(string value)
        {
            this.descriptionElement.SetTextualContent(value);

            UpdateSize();
            UpdatePosition();
        }

        private void UpdateSize()
        {
            SSize2F titleSize = this.titleElement.GetStringSize();
            SSize2F descriptionSize = this.descriptionElement.GetStringSize();
            SSize2F backgroundBaseSize = new(32f, 32f);

            float finalWidth = Math.Max(this.MinimumSize.Width, titleSize.Width);
            float finalHeight = Math.Max(this.MinimumSize.Height, descriptionSize.Height + titleSize.Height + 10f);

            finalWidth = finalWidth > this.MaximumSize.Width ? this.MaximumSize.Width : finalWidth;
            finalHeight = finalHeight > this.MaximumSize.Height ? this.MaximumSize.Height : finalHeight;

            Vector2 finalScale = new(
                finalWidth / backgroundBaseSize.Width,
                finalHeight / backgroundBaseSize.Height
            );

            SSize2F finalTextAreaSize = new(finalWidth, descriptionSize.Height);

            this.descriptionElement.TextAreaSize = finalTextAreaSize;
            this.backgroundImageElement.Scale = finalScale;
        }

        private void UpdatePosition()
        {
            Vector2 mousePosition = this.SGameInstance.InputManager.GetScaledMousePosition();
            Vector2 newPosition = mousePosition + this.Margin;

            if (newPosition.X + this.backgroundImageElement.Size.Width > SScreenConstants.DEFAULT_SCREEN_WIDTH)
            {
                newPosition.X = mousePosition.X - this.backgroundImageElement.Size.Width - this.Margin.X;
            }

            if (newPosition.Y + this.backgroundImageElement.Size.Height > SScreenConstants.DEFAULT_SCREEN_HEIGHT)
            {
                newPosition.Y = mousePosition.Y - this.backgroundImageElement.Size.Height - this.Margin.Y;
            }

            this.backgroundImageElement.Position = newPosition;

            this.titleElement.PositionRelativeToElement(this.backgroundImageElement);
            this.descriptionElement.PositionRelativeToElement(this.titleElement);
        }
    }
}
