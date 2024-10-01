using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class CoinBlock : Item
    {

        public CoinBlock(Sprite itemSprite, Vector2 position) : base(itemSprite, IState.ItemTypeState.CoinBlock, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (this.State.Alive != LivingState.Dead)
            {
                this.Sprite.changeCurrentAnimation(this.AnimationName());
                this.Sprite.updateSprite(gameTime, 256);
                updateCollision();
            }
        }
    }
}