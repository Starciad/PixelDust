﻿using Microsoft.Xna.Framework;

using StardustSandbox.Core.Constants;
using StardustSandbox.Core.Enums.General;
using StardustSandbox.Core.GUISystem.Elements;
using StardustSandbox.Core.GUISystem.Elements.Graphics;
using StardustSandbox.Core.Interfaces.GUI;
using StardustSandbox.Core.Mathematics.Primitives;

using System;

namespace StardustSandbox.ContentBundle.GUISystem.Menus
{
    public sealed partial class SGUI_MainMenu
    {
        private enum SMainMenuButtonIndex : byte
        {
            Create = 0,
            Quit = 1
        }

        private ISGUILayoutBuilder layout;

        private SGUIImageElement panelBackground;

        private SGUILabelElement[] menuButtons;
        private Action[] menuButtonActions;

        protected override void OnBuild(ISGUILayoutBuilder layout)
        {
            this.layout = layout;

            BuildMainPanel();
            BuildDecorations();
            BuildMainMenu();
            BuildInfos();
        }

        private void BuildMainPanel()
        {
            this.panelBackground = new(this.SGameInstance);
            this.panelBackground.SetTexture(this.particleTexture);
            this.panelBackground.SetScale(new Vector2(487f, SScreenConstants.DEFAULT_SCREEN_HEIGHT));
            this.panelBackground.SetSize(SSize2F.One);
            this.panelBackground.SetColor(new(Color.Black, 180));
            this.panelBackground.PositionRelativeToScreen();

            this.layout.AddElement(this.panelBackground);
        }

        private void BuildDecorations()
        {
            SGUIImageElement prosceniumCurtainElement = new(this.SGameInstance);

            prosceniumCurtainElement.SetTexture(this.prosceniumCurtainTexture);
            prosceniumCurtainElement.SetScale(new Vector2(2));

            this.layout.AddElement(prosceniumCurtainElement);
        }

        private void BuildInfos()
        {
            SGUILabelElement gameVersionLabel = new(this.SGameInstance);
            SGUILabelElement copyrightLabel = new(this.SGameInstance);

            gameVersionLabel.SetTextContent($"Ver. {SGameConstants.VERSION}");
            gameVersionLabel.SetMargin(new Vector2(-32f, -32f));
            gameVersionLabel.SetScale(new Vector2(0.08f));
            gameVersionLabel.SetFontFamily(SFontFamilyConstants.BIG_APPLE_3PM);
            gameVersionLabel.SetColor(Color.White);
            gameVersionLabel.SetPositionAnchor(SCardinalDirection.Southeast);
            gameVersionLabel.SetOriginPivot(SCardinalDirection.West);
            gameVersionLabel.PositionRelativeToScreen();

            copyrightLabel.SetTextContent($"(c) {SGameConstants.YEAR} {SGameConstants.AUTHOR}");
            copyrightLabel.SetMargin(new Vector2(0f, -32));
            copyrightLabel.SetScale(new Vector2(0.08f));
            copyrightLabel.SetFontFamily(SFontFamilyConstants.BIG_APPLE_3PM);
            copyrightLabel.SetColor(Color.White);
            copyrightLabel.SetPositionAnchor(SCardinalDirection.South);
            copyrightLabel.SetOriginPivot(SCardinalDirection.Center);
            copyrightLabel.PositionRelativeToScreen();

            this.layout.AddElement(gameVersionLabel);
            this.layout.AddElement(copyrightLabel);
        }

        private void BuildMainMenu()
        {
            SGUIImageElement gameTitle = new(this.SGameInstance);

            gameTitle.SetTexture(this.gameTitleTexture);
            gameTitle.SetScale(new Vector2(1.5f));
            gameTitle.SetSize(new SSize2(292, 112));
            gameTitle.SetMargin(new Vector2(0, 96));
            gameTitle.SetPositionAnchor(SCardinalDirection.North);
            gameTitle.SetOriginPivot(SCardinalDirection.Center);
            gameTitle.PositionRelativeToElement(this.panelBackground);

            this.menuButtons = [
                new(this.SGameInstance),
                new(this.SGameInstance)
            ];

            this.menuButtonActions = [
                CreateMenuButton,
                QuitMenuButton
            ];

            // BUTTONS
            Vector2 baseMargin = new(0, 0);

            // Labels
            this.menuButtons[(byte)SMainMenuButtonIndex.Create].SetTextContent("Create");
            this.menuButtons[(byte)SMainMenuButtonIndex.Quit].SetTextContent("Quit");

            for (int i = 0; i < this.menuButtons.Length; i++)
            {
                SGUILabelElement labelElement = this.menuButtons[i];

                labelElement.SetScale(new Vector2(0.15f));
                labelElement.SetMargin(baseMargin);
                labelElement.SetColor(new Color(206, 214, 237, 255));
                labelElement.SetFontFamily(SFontFamilyConstants.BIG_APPLE_3PM);
                labelElement.SetBorders(true);
                labelElement.SetBordersColor(Color.Black);
                labelElement.SetBorderOffset(new Vector2(4f));
                labelElement.SetPositionAnchor(SCardinalDirection.Center);
                labelElement.SetOriginPivot(SCardinalDirection.Center);
                labelElement.PositionRelativeToElement(this.panelBackground);

                baseMargin.Y += 96;
            }

            this.layout.AddElement(gameTitle);

            for (int i = 0; i < this.menuButtons.Length; i++)
            {
                this.layout.AddElement(this.menuButtons[i]);
            }
        }
    }
}
