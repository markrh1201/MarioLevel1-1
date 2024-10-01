using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sprint4BeanTeam
{
    public class Star : Item
    {

        public Star(Sprite itemSprite, Vector2 position) : base(itemSprite, IState.ItemTypeState.StarMan, position)
        {
            VelocityX = 90f;
            VelocityY = -100f;
        }

        public override void Update(GameTime gameTime)
        {
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 256);
            updateCollision();
            if (isRevealed)
            {
                VelocityY += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                positionX += (int)(VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds);
                positionY += (int)(VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds);
                gravity += 10;
            }
        }
    }
}