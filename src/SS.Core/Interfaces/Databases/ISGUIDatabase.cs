﻿using StardustSandbox.Core.GUISystem;

namespace StardustSandbox.Core.Interfaces.Databases
{
    public interface ISGUIDatabase
    {
        SGUISystem[] RegisteredGUIs { get; }

        void RegisterGUISystem(string identifier, SGUISystem guiSystem);
        SGUISystem GetGUISystemById(string identifier);
    }
}