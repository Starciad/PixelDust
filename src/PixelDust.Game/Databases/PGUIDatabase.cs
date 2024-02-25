﻿using PixelDust.Game.GUI;
using PixelDust.Game.GUI.Events;
using PixelDust.Game.Objects;

using System.Collections.Generic;
using System.Linq;

namespace PixelDust.Game.Databases
{
    public sealed class PGUIDatabase : PGameObject
    {
        public IReadOnlyList<PGUISystem> RegisteredGUIs => this._registeredGUIs;

        private List<PGUISystem> _registeredGUIs = [];

        public void Build()
        {
            this._registeredGUIs = [.. this._registeredGUIs.OrderBy(x => x.ZIndex)];
        }

        public void RegisterGUISystem(PGUISystem guiSystem, PGUIEvents guiEvents, PGUILayoutPool layoutPool)
        {
            guiSystem.Configure(guiEvents, layoutPool);
            guiSystem.Initialize(this.Game);

            this._registeredGUIs.Add(guiSystem);
        }

        public PGUISystem Find(string name)
        {
            return this._registeredGUIs.Find(x => x.Name == name);
        }
    }
}
