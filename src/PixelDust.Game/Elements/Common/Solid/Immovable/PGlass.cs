﻿using PixelDust.Game.Attributes.Elements;
using PixelDust.Game.Attributes.GameContent;
using PixelDust.Game.Elements.Rendering.Common;

namespace PixelDust.Game.Elements.Common.Solid.Immovable
{
    [PGameContent]
    [PElementRegister(11)]
    public sealed class PGlass : PImmovableSolid
    {
        protected override void OnSettings()
        {
            this.Name = "Glass";
            this.Description = string.Empty;
            this.Category = string.Empty;
            this.Texture = this.Game.AssetDatabase.GetTexture("element_12");
            this.IconTexture = this.Game.AssetDatabase.GetTexture("icon_element_12");

            this.Rendering.SetRenderingMechanism(new PElementBlobRenderingMechanism());

            this.DefaultTemperature = 25;
        }
    }
}