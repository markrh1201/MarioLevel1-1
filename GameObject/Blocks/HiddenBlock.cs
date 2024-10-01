using System;
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class HiddenBlock : BrickBlock
    {

        public HiddenBlock(Sprite blockSprite, Vector2 position) : base(blockSprite, BlockTypeState.Hidden, position)
        {

        }

        public override void Hide()
        {
            if (this.State.BlockType == IState.BlockTypeState.Hidden)
            {
                this.isVisible = true;
                this.State.BlockType = BlockTypeState.Brick;
            }
            else
            {
                this.State.BlockType = BlockTypeState.Hidden;
            }
        }

    }
}
