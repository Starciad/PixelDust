﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using PixelDust.Core.Elements;
using PixelDust.Core.Engine;
using PixelDust.Core.Worlding;
using PixelDust.Game.Elements.Solid.Movable;

namespace PixelDust.Game.Elements.Liquid
{
    [PElementRegister(3)]
    internal class Water : PLiquid
    {
        protected override void OnSettings()
        {
            Name = "Water";
            Description = string.Empty;

            Render = new();
            Render.AddFrame(new(2, 0));

            DefaultDispersionRate = 4;
            EnableNeighborsAction = true;
        }

        protected override void OnNeighbors((Vector2, PWorldSlot)[] neighbors, int length)
        {
            foreach ((Vector2, PWorldSlot) neighbor in neighbors)
            {
                if (neighbor.Item2.Element is Dirt)
                {
                    PElementContext.TryDestroy(PElementContext.Position);
                    PElementContext.TryReplace<Mud>(neighbor.Item1);
                    return;
                }

                if (neighbor.Item2.Element is Stone)
                {
                    PElementContext.TryDestroy(PElementContext.Position);
                    PElementContext.TryReplace<Sand>(neighbor.Item1);
                    return;
                }
            }
        }
    }
}