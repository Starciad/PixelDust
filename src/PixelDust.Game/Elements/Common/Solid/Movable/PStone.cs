﻿using PixelDust.Game.Attributes.Elements;
using PixelDust.Game.Attributes.GameContent;
using PixelDust.Game.Elements.Common.Liquid;
using PixelDust.Game.Elements.Rendering.Common;

namespace PixelDust.Game.Elements.Common.Solid.Movable
{
    [PGameContent]
    [PElementRegister(3)]
    public sealed class PStone : PMovableSolid
    {
        protected override void OnSettings()
        {
            this.Name = "Stone";
            this.Description = string.Empty;
            this.Category = string.Empty;
            this.Texture = this.Game.AssetDatabase.GetTexture("element_4");
            this.IconTexture = this.Game.AssetDatabase.GetTexture("icon_element_4");

            this.Rendering.SetRenderingMechanism(new PElementBlobRenderingMechanism());

            this.DefaultTemperature = 20;
        }

        protected override void OnTemperatureChanged(short currentValue)
        {
            if (currentValue > 500)
            {
                this.Context.ReplaceElement<PLava>();
                this.Context.SetElementTemperature(600);
            }
        }
    }
}
