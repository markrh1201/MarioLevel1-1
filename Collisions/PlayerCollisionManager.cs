using System;
using Sprint4BeanTeam;
using Microsoft.Xna.Framework;
using static Sprint4BeanTeam.IState;
using System.Net.Mail;
using System.Diagnostics;
using Sprint4BeanTeam.GameObject.Enemies;

namespace Sprint4BeanTeam
{
    public class PlayerCollisionManager : ICollisionManager
    {
        public Player player { get; set; }

        public PlayerCollisionManager(Player player)
        {
            this.player = player;
        }

        public void StructuralCollision(Block block)
        {
            CollisionDetector.CollisionFrom direction;
            Rectangle playerRect = player.getCollisionBox();
            Rectangle blockRect = block.getCollisionBox();

            direction = CollisionDetector.DetectCollision(playerRect, blockRect);
            if (!(direction == CollisionDetector.CollisionFrom.None))
            {
                player.collisionColor(Color.Red);
                switch (direction)
                {
                    case (CollisionDetector.CollisionFrom.Right):
                        //player stop right
                        if (block.State.BlockType == BlockTypeState.HorizontalPipe && block.canTeleport == true)
                        {
                            player.warpTimerUnderground = 1f;
                            player.WarpUnderground();
                        }
                        player.blockSide(Rectangle.Intersect(playerRect, blockRect).Width, block.State.BlockType == BlockTypeState.Hidden);
                        break;
                    case (CollisionDetector.CollisionFrom.Left):
                        //player stop left
                        player.blockSide(-Rectangle.Intersect(playerRect, blockRect).Width, block.State.BlockType == BlockTypeState.Hidden);
                        break;
                    case (CollisionDetector.CollisionFrom.Top):
                        if (block.State.BlockType != BlockTypeState.Hidden)
                        {
                            player.SetYPosition(player.positionY + Rectangle.Intersect(playerRect, blockRect).Height);
                            if (player.State.Action == IState.ActionState.Jump)
                                block.Bump();
                            player.VelocityY = -player.VelocityY / 2;
                        }
                        else
                        {
                            if (player.VelocityY > 0)
                            {
                                player.SetYPosition(player.positionY + Rectangle.Intersect(playerRect, blockRect).Height);
                            }
                            else
                            {
                                block.State.BlockType = BlockTypeState.Brick;
                            }
                        }
                        break;
                    case (CollisionDetector.CollisionFrom.Bottom):
                        //player stop fall
                        if (block.State.BlockType != BlockTypeState.VerticalPipe)
                        {
                            player.blockUnder(Rectangle.Intersect(playerRect, blockRect).Height, block.State.BlockType == BlockTypeState.Hidden);
                        }
                        else
                        {
                            player.blockUnder(Rectangle.Intersect(playerRect, blockRect).Height, block.State.BlockType == BlockTypeState.Hidden);
                            if (block.State.BlockType == BlockTypeState.VerticalPipe && block.canTeleport == true && player.State.Action == ActionState.Crouch)
                            {
                                player.warpTimer = 1f;
                                player.Warp();
                            }
                        }
                        break;
                    default:

                        break;
                }
            }
            else
                player.collisionColor(player.CollisionBoxColor);
        }

        public void EnemyCollision(Enemy enemy)
        {
            Rectangle playerRect = player.getCollisionBox();
            Rectangle enemyRect = enemy.getCollisionBox();
            CollisionDetector.CollisionFrom direction;
            direction = CollisionDetector.DetectCollision(player.getCollisionBox(), enemy.getCollisionBox());

            if (!(direction == CollisionDetector.CollisionFrom.None))
            {
                player.collisionColor(Color.Red);
                switch (direction)
                {
                    case (CollisionDetector.CollisionFrom.Right):
                    case (CollisionDetector.CollisionFrom.Left):
                    case (CollisionDetector.CollisionFrom.Top):
                        if (enemy.State.Action == EnemyActionState.Shell && enemyRect.X < playerRect.X)
                            player.enemyHit(true, enemy.canDamage);
                        else
                            player.enemyHit(false, false);
                        break;
                    case (CollisionDetector.CollisionFrom.Bottom):
                        if (enemy.State.EnemyType != EnemyTypeState.Piranha)
                        {
                            enemy.DieTransition();
                            player.enemyBelow(Rectangle.Intersect(playerRect, enemyRect).Height, enemy.State.Action == EnemyActionState.Shell);
                        }
                            break;
                }
            }
            else
                player.collisionColor(player.CollisionBoxColor);
        }

        public void ItemCollision(Item item)
        {
            CollisionDetector.CollisionFrom direction;
            Rectangle playerRect = player.getCollisionBox();
            Rectangle itemRect = item.getCollisionBox();

            direction = CollisionDetector.DetectCollision(playerRect, itemRect);

            if (!(direction == CollisionDetector.CollisionFrom.None))
            {
                player.collisionColor(Color.Red);
                switch (direction)
                {
                    case (CollisionDetector.CollisionFrom.Right):
                    case (CollisionDetector.CollisionFrom.Left):
                    case (CollisionDetector.CollisionFrom.Top):
                    case (CollisionDetector.CollisionFrom.Bottom):
                        player.getItem(item.State.ItemType);
                        item.Consume();
                        break;
                }
            }
            else
                player.collisionColor(player.CollisionBoxColor);
        }
    }
}