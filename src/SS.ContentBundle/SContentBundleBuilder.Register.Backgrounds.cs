﻿using Microsoft.Xna.Framework;

using StardustSandbox.Core.Backgrounds;
using StardustSandbox.Core.Databases;
using StardustSandbox.Core.Interfaces.General;

using System;

namespace StardustSandbox.ContentBundle
{
    public sealed partial class SContentBundleBuilder
    {
        protected override void OnRegisterBackgrounds(ISGame game, SBackgroundDatabase backgroundDatabase)
        {
            backgroundDatabase.RegisterBackground("ocean_1", game.AssetDatabase.GetTexture("background_1"), new Action<SBackground>((background) =>
            {
                background.AddLayer(new Point(0, 0), new Vector2(2f, 0f), Vector2.Zero, false, true);
            }));
        }
    }
}
