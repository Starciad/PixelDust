﻿using Microsoft.Xna.Framework;

using StardustSandbox.ContentBundle.Enums.Elements;
using StardustSandbox.Core.Components.Common.Entities;
using StardustSandbox.Core.Components.Templates;
using StardustSandbox.Core.Constants;
using StardustSandbox.Core.Entities;
using StardustSandbox.Core.Extensions;
using StardustSandbox.Core.Interfaces.General;
using StardustSandbox.Core.Mathematics;
using StardustSandbox.Core.Mathematics.Primitives;
using StardustSandbox.Core.World;

namespace StardustSandbox.ContentBundle.Components.Entities.AI
{
    internal sealed class SMagicCursorEntityAIComponent : SEntityComponent
    {
        private enum MoveState
        {
            Static,
            Moving
        }

        private enum BuildingState
        {
            Constructing,
            Removing
        }

        private MoveState currentMoveState;
        private BuildingState currentBuildingState;

        private SElementId selectedElement;
        private Vector2 targetPosition;
        private readonly SWorld world;
        private readonly SSize2 worldSize;
        private readonly SEntityTransformComponent transformComponent;

        private static readonly SElementId[] AllowedElements =
        {
            SElementId.Dirt, SElementId.Mud, SElementId.Water, SElementId.Stone,
            SElementId.Grass, SElementId.Sand, SElementId.Lava, SElementId.Acid,
            SElementId.Wood, SElementId.TreeLeaf
        };

        private int moveStateTimer = 0;
        private int buildingStateTimer = 0;
        private int elementChangeTimer = 0;

        public SMagicCursorEntityAIComponent(ISGame gameInstance, SEntity entityInstance, SEntityTransformComponent transformComponent) : base(gameInstance, entityInstance)
        {
            this.world = gameInstance.World;
            this.transformComponent = transformComponent;
            this.worldSize = this.world.Infos.Size * SWorldConstants.GRID_SCALE;
            this.selectedElement = AllowedElements.GetRandomItem();

            SelectRandomPosition();

            this.currentMoveState = MoveState.Moving;
            this.currentBuildingState = BuildingState.Constructing;
        }

        public override void Update(GameTime gameTime)
        {
            HandleStateTransition();
            UpdateElementSelection();
            ExecuteStateActions();
            UpdateSmoothMovement();

            base.Update(gameTime);
        }

        private void HandleStateTransition()
        {
            this.moveStateTimer++;
            this.buildingStateTimer++;

            if (this.moveStateTimer > 10)
            {
                this.moveStateTimer = 0;
                this.currentMoveState = (MoveState)SRandomMath.Range(0, 3);

                // If moving, select a new target
                if (this.currentMoveState == MoveState.Moving)
                {
                    SelectRandomPosition();
                }
            }

            if (this.buildingStateTimer > 96)
            {
                this.buildingStateTimer = 0;
                this.currentBuildingState = (BuildingState)SRandomMath.Range(0, 3);
            }
        }

        private void UpdateElementSelection()
        {
            this.elementChangeTimer++;
            if (this.elementChangeTimer > 350)
            {
                this.elementChangeTimer = 0;
                this.selectedElement = AllowedElements.GetRandomItem();
            }
        }

        private void ExecuteStateActions()
        {
            Point gridPosition = (this.transformComponent.Position / SWorldConstants.GRID_SCALE).ToPoint();

            switch (this.currentMoveState)
            {
                case MoveState.Static:
                    break;

                case MoveState.Moving:
                    SelectRandomPosition();
                    break;

                default:
                    break;
            }

            switch (this.currentBuildingState)
            {
                case BuildingState.Constructing:
                    if (this.world.IsEmptyElementSlot(gridPosition))
                    {
                        this.world.InstantiateElement(gridPosition, (uint)this.selectedElement);
                    }

                    break;

                case BuildingState.Removing:
                    if (!this.world.IsEmptyElementSlot(gridPosition))
                    {
                        this.world.DestroyElement(gridPosition);
                    }

                    break;

                default:
                    break;
            }
        }

        private void UpdateSmoothMovement()
        {
            this.transformComponent.Position = Vector2.Lerp(this.transformComponent.Position, this.targetPosition, 0.1f);
        }

        private void SelectRandomPosition()
        {
            this.targetPosition = new Vector2(SRandomMath.Range(0, this.worldSize.Width), SRandomMath.Range(0, this.worldSize.Height));
        }
    }
}
