﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using PixelDust.Core.Elements;
using PixelDust.Core.Engine;

using System;
using System.Collections.Generic;

namespace PixelDust.Core.Worlding
{
    public static class PWorld
    {
        public const int Scale = 32;

        public static PWorldStates States { get; private set; } = new();
        public static PWorldInfos Infos { get; private set; } = new();

        internal static PWorldSlot[,] Slots { get; private set; }

        private static readonly PWorldComponent[] _components = new PWorldComponent[]
        {
            new PWorldChunkingComponent(),
            new PWorldThreadingComponent(),
        };

        // Engine
        internal static void Initialize()
        {
            Restart();

            foreach (PWorldComponent component in _components)
                component.Initialize();

            States.IsActive = true;
        }
        internal static void Unload()
        {
            States.IsUnloaded = true;
            Clear();
        }

        // Components
        internal static T GetComponent<T>() where T : PWorldComponent
        {
            return (T)Array.Find(_components, x => x.GetType() == typeof(T));
        }

        // Update
        internal static void Update()
        {
            if (!States.IsActive || States.IsPaused || States.IsUnloaded) return;
            foreach (PWorldComponent component in _components)
            {
                component.Update();
            }
        }

        // Draw
        internal static void Draw()
        {
            PGraphics.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, PWorldCamera.Camera.GetViewMatrix());
            if (!States.IsActive)
            {
                PGraphics.SpriteBatch.End();
                return;
            }

            // System
            DrawSlots();

            // Components
            foreach (PWorldComponent component in _components)
            {
                component.Draw();
            }

            PGraphics.SpriteBatch.End();
        }
        private static void DrawSlots()
        {
            for (int x = 0; x < Infos.Width; x++)
            {
                for (int y = 0; y < Infos.Height; y++)
                {
                    PElementContext.Update(Slots[x, y], new(x, y));

                    if (!IsEmpty(new(x, y)))
                    {
                        Slots[x, y].Element.Draw();
                    }
                    else
                    {
                        PGraphics.SpriteBatch.Draw(PTextures.Pixel, new Vector2(x * Scale, y * Scale), null, Color.Black, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        // Chunking
        internal static int GetActiveChunksCount()
        {
            return GetComponent<PWorldChunkingComponent>().GetActiveChunksCount();
        }
        internal static bool TryGetChunkUpdateState(Vector2 pos, out bool result)
        {
            return GetComponent<PWorldChunkingComponent>().TryGetChunkUpdateState(pos, out result);
        }
        internal static bool TryNotifyChunk(Vector2 pos)
        {
            return GetComponent<PWorldChunkingComponent>().TryNotifyChunk(pos);
        }

        // Slots
        public static bool InsideTheWorldDimensions(Vector2 pos)
        {
            return (int)pos.X >= 0 && (int)pos.X < Infos.Width &&
                   (int)pos.Y >= 0 && (int)pos.Y < Infos.Height;
        }
        public static bool TryInstantiate<T>(Vector2 pos) where T : PElement
        {
            return TryInstantiate(pos, (uint)PElementsHandler.GetIdOfElementType<T>());
        }
        public static bool TryInstantiate(Vector2 pos, uint id)
        {
            return TryInstantiate(pos, PElementsHandler.GetElementById(id));
        }
        public static bool TryInstantiate(Vector2 pos, PElement value)
        {
            if (!InsideTheWorldDimensions(pos) || !IsEmpty(pos))
                return false;

            TryNotifyChunk(pos);

            Slots[(int)pos.X, (int)pos.Y].Instantiate(value);
            return true;
        }
        public static bool TryUpdatePosition(Vector2 oldPos, Vector2 newPos)
        {
            if (!InsideTheWorldDimensions(oldPos) ||
                !InsideTheWorldDimensions(newPos) ||
                IsEmpty(oldPos) ||
                !IsEmpty(newPos))
                return false;

            TryNotifyChunk(oldPos);
            TryNotifyChunk(newPos);

            Slots[(int)newPos.X, (int)newPos.Y].Copy(Slots[(int)oldPos.X, (int)oldPos.Y]);
            Slots[(int)oldPos.X, (int)oldPos.Y].Destroy();
            return true;
        }
        public static bool TrySwitchPosition(Vector2 oldPos, Vector2 newPos)
        {
            if (!InsideTheWorldDimensions(oldPos) ||
                !InsideTheWorldDimensions(newPos) ||
                IsEmpty(oldPos) ||
                IsEmpty(newPos))
                return false;

            TryNotifyChunk(oldPos);
            TryNotifyChunk(newPos);

            PWorldSlot oldValue = Slots[(int)oldPos.X, (int)oldPos.Y];
            PWorldSlot newValue = Slots[(int)newPos.X, (int)newPos.Y];

            Slots[(int)oldPos.X, (int)oldPos.Y].Copy(newValue);
            Slots[(int)newPos.X, (int)newPos.Y].Copy(oldValue);

            return true;
        }
        public static bool TryDestroy(Vector2 pos)
        {
            if (!InsideTheWorldDimensions(pos) ||
                IsEmpty(pos))
                return false;

            TryNotifyChunk(pos);
            Slots[(int)pos.X, (int)pos.Y].Destroy();

            return true;
        }
        public static bool TryReplace<T>(Vector2 pos) where T : PElement
        {
            if (!TryDestroy(pos)) return false;
            if (!TryInstantiate<T>(pos)) return false;

            return true;
        }
        public static bool TryGetElement(Vector2 pos, out PElement value)
        {
            if (!InsideTheWorldDimensions(pos) ||
                IsEmpty(pos))
            {
                value = null;
                return false;
            }

            value = Slots[(int)pos.X, (int)pos.Y].Element;
            return true;
        }
        public static bool TryGetSlot(Vector2 pos, out PWorldSlot value)
        {
            value = default;
            if (!InsideTheWorldDimensions(pos))
                return false;

            value = Slots[(int)pos.X, (int)pos.Y];
            return !value.IsEmpty();
        }
        public static bool TryGetNeighbors(Vector2 pos, out (Vector2, PWorldSlot)[] neighbors)
        {
            List<(Vector2, PWorldSlot)> slotsFound = new();
            neighbors = Array.Empty<(Vector2, PWorldSlot)>();

            if (!InsideTheWorldDimensions(pos))
                return false;

            Vector2[] neighborsPositions = new Vector2[]
            {
                // Top
                new(pos.X, pos.Y - 1),
                new(pos.X + 1, pos.Y - 1),
                new(pos.X - 1, pos.Y - 1),

                // Center
                new(pos.X + 1, pos.Y),
                new(pos.X - 1, pos.Y),

                // Down
                new(pos.X, pos.Y + 1),
                new(pos.X + 1, pos.Y + 1),
                new(pos.X - 1, pos.Y + 1),
            };

            foreach (Vector2 neighborPos in neighborsPositions)
            {
                if (TryGetSlot(neighborPos, out PWorldSlot value))
                    slotsFound.Add((neighborPos, value));
            }

            if (slotsFound.Count > 0)
            {
                neighbors = slotsFound.ToArray();
                return true;
            }

            return false;
        }
        public static bool IsEmpty(Vector2 pos)
        {
            if (!InsideTheWorldDimensions(pos) || Slots[(int)pos.X, (int)pos.Y].IsEmpty())
                return true;

            return false;
        }

        // Engine
        public static void Resize(Vector2 size)
        {
            Pause();
            Clear();

            Infos.SetWidth((uint)size.X);
            Infos.SetWidth((uint)size.Y);

            Restart();
        }

        // States
        public static void Restart()
        {
            uint width = (uint)Infos.Width;
            uint height = (uint)Infos.Height;

            Infos.SetWidth(width);
            Infos.SetHeight(height);

            Slots = new PWorldSlot[width, height];
        }
        public static void Pause()
        {
            States.IsPaused = true;
        }
        public static void Resume()
        {
            States.IsPaused = false;
        }

        // Tools
        public static void Clear()
        {
            if (Slots == null)
                return;

            for (int x = 0; x < Infos.Width; x++)
            {
                for (int y = 0; y < Infos.Height; y++)
                {
                    if (IsEmpty(new(x, y)))
                        continue;

                    TryDestroy(new(x, y));
                }
            }
        }
    }
}