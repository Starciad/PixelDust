﻿using Microsoft.Xna.Framework;

using PixelDust.Game.Constants;
using PixelDust.Game.Elements;
using PixelDust.Game.Elements.Contexts;
using PixelDust.Game.Mathematics;
using PixelDust.Game.World.Slots;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelDust.Game.World.Components.Threading
{
    public sealed class PWorldThreadingComponent : PWorldComponent
    {
        private int _worldThreadSize;

        private readonly PWorldThread[] _worldThreadsInfos = new PWorldThread[PWorldConstants.TOTAL_WORLD_THREADS];

        private readonly List<Vector2Int> _slotsCapturedForUpdate = [];

        protected override void OnAwake()
        {
            int totalValue = this.World.Infos.Size.Width;
            int remainingValue = totalValue;

            this._worldThreadSize = (int)MathF.Ceiling(totalValue / PWorldConstants.TOTAL_WORLD_THREADS);

            // Setting Ranges
            int rangeStart = 0;
            for (int i = 0; i < PWorldConstants.TOTAL_WORLD_THREADS; i++)
            {
                this._worldThreadsInfos[i] = new(i + 1, rangeStart, rangeStart + this._worldThreadSize - 1);

                rangeStart = this._worldThreadsInfos[i].EndPosition + 1;
                remainingValue -= this._worldThreadSize;
            }

            // Distribute the remaining value to the last object
            if (remainingValue > 0)
            {
                this._worldThreadsInfos[PWorldConstants.TOTAL_WORLD_THREADS - 1].EndPosition += remainingValue;
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            # region Threading Update
            // Odds
            Task odds = Task.Run(() =>
            {
                for (int i = 0; i < PWorldConstants.TOTAL_WORLD_THREADS; i++)
                {
                    if (i % 2 == 0)
                    {
                        ExecuteThreadColumn(gameTime, this._worldThreadsInfos[i]);
                    }
                }

                return Task.CompletedTask;
            });

            odds.Wait();

            // Even
            Task even = Task.Run(() =>
            {
                for (int i = 0; i < PWorldConstants.TOTAL_WORLD_THREADS; i++)
                {
                    if (i % 2 != 0)
                    {
                        ExecuteThreadColumn(gameTime, this._worldThreadsInfos[i]);
                    }
                }

                return Task.CompletedTask;
            });

            even.Wait();
            #endregion
        }

        // THREAD
        private void ExecuteThreadColumn(GameTime gameTime, PWorldThread threadInfo)
        {
            this._slotsCapturedForUpdate.Clear();
            uint totalCapturedElements = 0;

            // Find slots
            for (int x = 0; x < threadInfo.Range + 1; x++)
            {
                for (int y = 0; y < this.World.Infos.Size.Height; y++)
                {
                    Vector2Int pos = new(x + threadInfo.StartPosition, y);
                    bool chunkState = this.World.GetChunkUpdateState(pos);

                    PUpdateElementTarget(gameTime, pos, 1);

                    if (this.World.IsEmptyElementSlot(pos) || !chunkState)
                    {
                        continue;
                    }

                    this._slotsCapturedForUpdate.Add(pos);

                    totalCapturedElements++;
                }
            }

            // Update slots (Steps)
            for (int i = 0; i < totalCapturedElements; i++)
            {
                PUpdateElementTarget(gameTime, this._slotsCapturedForUpdate[i], 2);
            }
        }

        private void PUpdateElementTarget(GameTime gameTime, Vector2Int position, int updateType)
        {
            PWorldElementSlot slot = this.World.GetElementSlot(position);

            if (this.World.TryGetElement(position, out PElement value))
            {
                value.Context = new PElementContext(this.World, this.World.ElementDatabase, slot, position);

                switch (updateType)
                {
                    case 1:
                        value.Update(gameTime);
                        break;

                    case 2:
                        value.Steps();
                        break;

                    default:
                        return;
                }
            }
        }
    }
}
