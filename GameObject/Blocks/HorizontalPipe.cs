using System;
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class HorizontalPipe : Block
    {
        private int newScale = 6;
        public HorizontalPipe(Sprite blockSprite, Vector2 position) : base(blockSprite, IState.BlockTypeState.HorizontalPipe, position)
        {

        }
        public override Rectangle getCollisionBox()
        {
            return new Rectangle(positionX, positionY, (int)Size.X * newScale, (int)Size.Y * newScale);
        }

        public override void Draw(SpriteBatch batch, int scale)
        {
            this.Sprite.drawSprite(batch, new Vector2(positionX, positionY), false, newScale);
        }
    }
}
