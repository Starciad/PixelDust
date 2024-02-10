﻿using PixelDust.Game.Elements.Attributes;
using PixelDust.Game.Elements.Templates.Solid;

namespace PixelDust.Game.Elements.Common.Solid.Immovable
{
    [PElementRegister(12)]
    public sealed class PMetal : PImmovableSolid
    {
        protected override void OnSettings()
        {
            this.Name = "Metal";
            this.Description = string.Empty;

            this.Render.AddFrame(new(2, 1));

            this.DefaultTemperature = 30;
        }
    }
}