using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sprint4BeanTeam
{
    public class SuperMushroom : Item
    {
        public SuperMushroom(Sprite itemSprite, Vector2 position) : base(itemSprite, IState.ItemTypeState.SuperMushroom, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 256);
            updateCollision();
            if (isRevealed)
            {
                VelocityY += 90f * (float)gameTime.ElapsedGameTime.TotalSeconds * 10;
                positionX += (int)(VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds);
                positionY += (int)(VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}