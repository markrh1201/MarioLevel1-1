using Sprint4BeanTeam;
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint4BeanTeam
{
    public class EmptyBlock : Block
    {

        public EmptyBlock(Sprite blockSprite, Vector2 position) : base(blockSprite, IState.BlockTypeState.Empty, position)
        {

        }
    }
}
