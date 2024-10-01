using System;
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class GroundBlock : Block
    {

        public GroundBlock(Sprite blockSprite, Vector2 position) : base(blockSprite, IState.BlockTypeState.Ground, position)
        {

        }
    }
}
