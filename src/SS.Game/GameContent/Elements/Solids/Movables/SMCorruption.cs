﻿using Microsoft.Xna.Framework;

using StardustSandbox.Game.Elements.Templates.Solids.Movables;
using StardustSandbox.Game.GameContent.Elements.Rendering;
using StardustSandbox.Game.GameContent.Elements.Utilities;
using StardustSandbox.Game.World.Data;

using System;

namespace StardustSandbox.Game.GameContent.Elements.Solids.Movables
{
    public sealed class SMCorruption : SMovableSolid
    {
        public SMCorruption(SGame gameInstance) : base(gameInstance)
        {
            this.Id = 008;
            this.Texture = gameInstance.AssetDatabase.GetTexture("element_9");
            this.Rendering.SetRenderingMechanism(new SElementBlobRenderingMechanism());
            this.EnableNeighborsAction = true;
        }

        protected override void OnNeighbors(ReadOnlySpan<(Point, SWorldSlot)> neighbors, int length)
        {
            this.Context.InfectNeighboringElements(this.SGameInstance, neighbors, neighbors.Length);
        }
    }
}