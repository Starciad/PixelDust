﻿using StardustSandbox.ContentBundle.Elements.Liquids;
using StardustSandbox.Core.Elements.Rendering;
using StardustSandbox.Core.Elements.Templates.Solids.Immovables;
using StardustSandbox.Core.Interfaces.General;

namespace StardustSandbox.ContentBundle.Elements.Solids.Immovables
{
    public sealed class SMetal : SImmovableSolid
    {
        public SMetal(ISGame gameInstance) : base(gameInstance)
        {
            this.id = 012;
            this.texture = gameInstance.AssetDatabase.GetTexture("element_13");
            this.rendering.SetRenderingMechanism(new SElementBlobRenderingMechanism());
            this.defaultTemperature = 30;
        }

        protected override void OnTemperatureChanged(short currentValue)
        {
            if (currentValue > 1200)
            {
                this.Context.ReplaceElement<SLava>();
            }
        }
    }
}