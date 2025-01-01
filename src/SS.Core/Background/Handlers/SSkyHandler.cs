﻿using Microsoft.Xna.Framework.Graphics;

using StardustSandbox.Core.Colors;
using StardustSandbox.Core.Interfaces;
using StardustSandbox.Core.Interfaces.Background.Handlers;
using StardustSandbox.Core.Objects;

using System;

namespace StardustSandbox.Core.Background.Handlers
{
    internal sealed class SSkyHandler(ISGame gameInstance) : SGameObject(gameInstance), ISSkyHandler
    {
        public bool IsActive { get; set; } = true;
        public Texture2D Texture => this.texture;
        public Effect Effect => this.effect;
        public SSkyGradientColorMap[] GradientColorMap => this.gradientColorMap;

        private Texture2D texture;
        private Effect effect;
        private readonly SSkyGradientColorMap[] gradientColorMap = [
            new()
            {
                StartTime = new(0, 0, 0), // Midnight
                EndTime = new(3, 0, 0),  // Late Night
                Color1 = (SColorPalette.DarkPurple, SColorPalette.NavyBlue),
                Color2 = (SColorPalette.NavyBlue, SColorPalette.DarkTeal),
            },

            new()
            {
                StartTime = new(3, 0, 0), // Late Night
                EndTime = new(6, 0, 0),  // Dawn
                Color1 = (SColorPalette.NavyBlue, SColorPalette.DarkTeal),
                Color2 = (SColorPalette.DarkTeal, SColorPalette.OrangeRed),
            },

            new()
            {
                StartTime = new(6, 0, 0), // Dawn
                EndTime = new(8, 0, 0),  // Early Morning
                Color1 = (SColorPalette.DarkTeal, SColorPalette.OrangeRed),
                Color2 = (SColorPalette.SkyBlue, SColorPalette.Orange),
            },

            new()
            {
                StartTime = new(8, 0, 0), // Early Morning
                EndTime = new(12, 0, 0), // Noon
                Color1 = (SColorPalette.SkyBlue, SColorPalette.Orange),
                Color2 = (SColorPalette.SkyBlue, SColorPalette.LemonYellow),
            },

            new()
            {
                StartTime = new(12, 0, 0), // Noon
                EndTime = new(15, 0, 0),  // Early Afternoon
                Color1 = (SColorPalette.SkyBlue, SColorPalette.LemonYellow),
                Color2 = (SColorPalette.SkyBlue, SColorPalette.Gold),
            },

            new()
            {
                StartTime = new(15, 0, 0), // Early Afternoon
                EndTime = new(18, 0, 0),  // Dusk
                Color1 = (SColorPalette.SkyBlue, SColorPalette.Gold),
                Color2 = (SColorPalette.OrangeRed, SColorPalette.DarkTeal),
            },

            new()
            {
                StartTime = new(18, 0, 0), // Dusk
                EndTime = new(20, 0, 0), // Evening
                Color1 = (SColorPalette.OrangeRed, SColorPalette.DarkTeal),
                Color2 = (SColorPalette.DarkTeal, SColorPalette.NavyBlue),
            },

            new()
            {
                StartTime = new(20, 0, 0), // Evening
                EndTime = new(23, 59, 59), // Midnight
                Color1 = (SColorPalette.DarkTeal, SColorPalette.NavyBlue),
                Color2 = (SColorPalette.DarkPurple, SColorPalette.NavyBlue),
            },
        ];

        public override void Initialize()
        {
            this.texture = this.SGameInstance.AssetDatabase.GetTexture("background_4");
            this.effect = this.SGameInstance.AssetDatabase.GetEffect("effect_1");
        }

        public SSkyGradientColorMap GetGradientByTime(TimeSpan currentTime)
        {
            return Array.Find(this.gradientColorMap, x =>
            {
                return currentTime >= x.StartTime && currentTime < x.EndTime;
            });
        }
    }
}
