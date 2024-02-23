﻿using Microsoft.Xna.Framework;

using PixelDust.Game.Constants;
using PixelDust.Game.Elements;
using PixelDust.Game.Objects;
using PixelDust.Game.World;
using PixelDust.Game.World.Data;

using System;

namespace PixelDust.Game.Managers
{
    public sealed partial class PGameInputManager(PCameraManager cameraManager, PWorld world, PInputManager inputHandler) : PGameObject
    {
        public PElement ElementSelected => this.elementSelected;

        public int PenScale => this.penScale;
        public float CameraMovementSpeed => this.cameraMovementSpeed;
        public bool CanModifyEnvironment
        {
            get => this.canModifyEnvironment;

            set => this.canModifyEnvironment = value;
        }

        // Elements
        private PElement elementSelected;
        private PElement elementOver;
        private PWorldSlot elementOverSlot;

        // Settings
        private int penScale = 1;
        private float cameraMovementSpeed = 10;
        private bool canModifyEnvironment = true;

        protected override void OnAwake()
        {
            BuildKeyboardInputs();
            BuildMouseInputs();
        }
        protected override void OnUpdate(GameTime gameTime)
        {
            // Inputs
            UpdatePlaceAreaSize();
            this._actionHandler.Update(gameTime);

            // External
            ClampCamera();
            GetMouseOverElement();
        }

        // Update
        private void UpdatePlaceAreaSize()
        {
            if (this._inputHandler.GetDeltaScrollWheel() > 0)
            {
                this.penScale -= 1;
            }
            else if (this._inputHandler.GetDeltaScrollWheel() < 0)
            {
                this.penScale += 1;
            }

            this.penScale = Math.Clamp(this.penScale, 0, 10);
        }

        // Settings
        public void SetSelectedElement(PElement value)
        {
            this.elementSelected = value;
        }
        public void SetPenScale(int value)
        {
            this.penScale = value;
        }
        public void SetCameraMovementSpeed(float value)
        {
            this.cameraMovementSpeed = value;
        }

        // Utilities
        private void GetMouseOverElement()
        {
            Vector2 screenPos = cameraManager.ScreenToWorld(this._inputHandler.MouseState.Position.ToVector2());
            Point worldPos = (new Vector2(screenPos.X, screenPos.Y) / PWorldConstants.GRID_SCALE).ToPoint();

            this.elementOverSlot = world.GetElementSlot(worldPos);
            this.elementOver = world.GetElement(worldPos);
        }

        private void ClampCamera()
        {
            int totalX = (world.Infos.Size.Width * PWorldConstants.GRID_SCALE) - PScreenConstants.DEFAULT_SCREEN_WIDTH;
            int totalY = (world.Infos.Size.Height * PWorldConstants.GRID_SCALE) - PScreenConstants.DEFAULT_SCREEN_HEIGHT;

            cameraManager.ClampPosition(new Vector2(0, -totalY), new Vector2(totalX, 0));
        }
    }
}