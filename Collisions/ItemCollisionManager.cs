using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.Xna.Framework;
using Sprint4BeanTeam;
using Sprint4BeanTeam.GameObject.Enemies;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class ItemCollisionManager
    {
        public Item item { get; set; }

        public ItemCollisionManager(Item item)
        {
            this.item = item;
        }

        public void StructuralCollision(Block block)
        {
            CollisionDetector.CollisionFrom direction;
            Rectangle itemRect = item.getCollisionBox();
            Rectangle blockRect = block.getCollisionBox();

            direction = CollisionDetector.DetectCollision(itemRect, blockRect);

            if (!(direction == CollisionDetector.CollisionFrom.None))
            {
                item.collisionColor(Color.Red);
                if (block.State.BlockType != BlockTypeState.Hidden)
                {
                    switch (direction)
                    {
                        case (CollisionDetector.CollisionFrom.Right):
                            item.blockSide(Rectangle.Intersect(itemRect, blockRect).Width);
                            break;
                        case (CollisionDetector.CollisionFrom.Left):
                            item.blockSide(-Rectangle.Intersect(itemRect, blockRect).Width);
                            break;
                        case (CollisionDetector.CollisionFrom.Top):
                            item.blockAbove(Rectangle.Intersect(itemRect, blockRect).Height);
                            break;
                        case (CollisionDetector.CollisionFrom.Bottom):
                            item.blockBelow(Rectangle.Intersect(itemRect, blockRect).Height);
                            break;
                    }
                }
            }
            else
                item.collisionColor(item.CollisionBoxColor);
        }
    }
}
