﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardustSandbox.Core.Interfaces.General;
using StardustSandbox.Core.Mathematics;

namespace StardustSandbox.ContentBundle.GUISystem.Elements.Textual
{
    public class SGUILabelElement(ISGame gameInstance) : SGUITextualElement(gameInstance)
    {
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 origin = this.SpriteFont.GetSpriteFontOriginPoint(this.ContentStringBuilder, this.OriginPivot);

            DrawBorders(spriteBatch, this.ContentStringBuilder, this.Position, this.SpriteFont, this.RotationAngle, origin, this.Scale, this.SpriteEffects);
            spriteBatch.DrawString(this.SpriteFont, this.ContentStringBuilder, this.Position, this.Color, this.RotationAngle, origin, this.Scale, this.SpriteEffects, 0f);
        }
    }
}
