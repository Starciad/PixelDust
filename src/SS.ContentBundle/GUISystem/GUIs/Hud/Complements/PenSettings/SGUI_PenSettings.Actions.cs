﻿using StardustSandbox.Core.Enums.GameInput.Pen;
using StardustSandbox.Core.Enums.World;

namespace StardustSandbox.ContentBundle.GUISystem.GUIs.Hud.Complements
{
    internal sealed partial class SGUI_PenSettings
    {
        // Menu
        private void ExitButtonAction()
        {
            this.SGameInstance.GUIManager.CloseGUI();
        }

        // Tools
        private void SelectVisualizationToolButtonAction()
        {
            this.gameInputController.Pen.Tool = SPenTool.Visualization;
        }

        private void SelectPencilToolButtonAction()
        {
            this.gameInputController.Pen.Tool = SPenTool.Pencil;
        }

        private void SelectEraserToolButtonAction()
        {
            this.gameInputController.Pen.Tool = SPenTool.Eraser;
        }

        private void SelectFillToolButtonAction()
        {
            this.gameInputController.Pen.Tool = SPenTool.Fill;
        }

        private void SelectReplaceToolButtonAction()
        {
            this.gameInputController.Pen.Tool = SPenTool.Replace;
        }

        // Layers
        private void SelectForegroundLayerButtonAction()
        {
            this.gameInputController.Pen.Layer = SWorldLayer.Foreground;
        }

        private void SelectBackgroundLayerButtonAction()
        {
            this.gameInputController.Pen.Layer = SWorldLayer.Background;
        }

        // Shapes
        private void SelectCircleShapeButtonAction()
        {
            this.gameInputController.Pen.Shape = SPenShape.Circle;
        }

        private void SelectSquareShapeButtonAction()
        {
            this.gameInputController.Pen.Shape = SPenShape.Square;
        }

        private void SelectTriangleShapeButtonAction()
        {
            this.gameInputController.Pen.Shape = SPenShape.Triangle;
        }
    }
}
