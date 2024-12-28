﻿using StardustSandbox.Core.Controllers.GameInput;
using StardustSandbox.Core.Interfaces.Databases;
using StardustSandbox.Core.Interfaces.Managers;
using StardustSandbox.Core.Interfaces.World;

namespace StardustSandbox.Core.Interfaces
{
    public interface ISGame
    {
        // Databases
        ISAssetDatabase AssetDatabase { get; }
        ISElementDatabase ElementDatabase { get; }
        ISGUIDatabase GUIDatabase { get; }
        ISCatalogDatabase CatalogDatabase { get; }
        ISBackgroundDatabase BackgroundDatabase { get; }
        ISEntityDatabase EntityDatabase { get; }

        // Managers
        ISGameManager GameManager { get; }
        ISInputManager InputManager { get; }
        ISCameraManager CameraManager { get; }
        ISGraphicsManager GraphicsManager { get; }
        ISGUIManager GUIManager { get; }
        ISBackgroundManager BackgroundManager { get; }
        ISCursorManager CursorManager { get; }

        // Core
        ISWorld World { get; }
        SGameInputController GameInputController { get; }

        void Quit();
    }
}
