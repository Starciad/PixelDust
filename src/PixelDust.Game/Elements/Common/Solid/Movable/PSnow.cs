﻿using PixelDust.Game.Attributes.Elements;
using PixelDust.Game.Elements.Rendering.Common;

namespace PixelDust.Game.Elements.Common.Solid.Movable
{
    [PElementRegister(7)]
    public sealed class PSnow : PMovableSolid
    {
        protected override void OnSettings()
        {
            this.Name = "Snow";
            this.Description = string.Empty;
            this.Category = string.Empty;
            this.Texture = this.Game.AssetDatabase.GetTexture("element_8");
            this.IconTexture = this.Game.AssetDatabase.GetTexture("icon_element_8");

            this.Rendering.SetRenderingMechanism(new PElementBlobRenderingMechanism());

            this.DefaultTemperature = -5;
        }
    }
}
