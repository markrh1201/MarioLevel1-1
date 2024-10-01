using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using Microsoft.Xna.Framework;
using Sprint4BeanTeam;
using Sprint4BeanTeam.GameObject.Enemies;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class EnemyCollisionManager
    {
        public Enemy enemy { get; set; }

        public EnemyCollisionManager(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void StructuralCollision(Block block)
        {
            CollisionDetector.CollisionFrom direction;
            Rectangle enemyRect = enemy.getCollisionBox();
            Rectangle blockRect = block.getCollisionBox();

            direction = CollisionDetector.DetectCollision(enemyRect, blockRect);

            if (!(direction == CollisionDetector.CollisionFrom.None))
            {
                switch (direction)
                {
                    case (CollisionDetector.CollisionFrom.Right):
                        if (enemy.State.EnemyType != EnemyTypeState.Piranha || block.State.BlockType != BlockTypeState.VerticalPipe)
                            enemy.blockSide(Rectangle.Intersect(enemyRect, blockRect).Width);
                        break;
                    case (CollisionDetector.CollisionFrom.Left):
                        if (enemy.State.EnemyType != EnemyTypeState.Piranha || block.State.BlockType != BlockTypeState.VerticalPipe)
                            enemy.blockSide(-Rectangle.Intersect(enemyRect, blockRect).Width);
                        break;
                    case (CollisionDetector.CollisionFrom.Bottom):
                        if (enemy.State.EnemyType != EnemyTypeState.Piranha || block.State.BlockType != BlockTypeState.VerticalPipe)
                        {
                            enemy.blockUnder(Rectangle.Intersect(enemyRect, blockRect).Height, block.isBumping);
                        }
                        break;
                }
            }
            else
                enemy.collisionColor(enemy.CollisionBoxColor);
        }


        public void PlayerCollision(IPlayer player)
        {
            CollisionDetector.CollisionFrom direction;
            Rectangle enemyRect = enemy.getCollisionBox();
            Rectangle playerRect = player.getCollisionBox();
            direction = CollisionDetector.DetectCollision(enemy.getCollisionBox(), player.getCollisionBox());

            if (!(direction == CollisionDetector.CollisionFrom.None))
            {
                enemy.collisionColor(Color.Red);
                switch (direction)
                {
                    case (CollisionDetector.CollisionFrom.Right):
                        enemy.playerRight(Rectangle.Intersect(enemyRect, playerRect).Width);
                        break;
                    case (CollisionDetector.CollisionFrom.Left):
                        enemy.playerLeft(Rectangle.Intersect(enemyRect, playerRect).Width);
                        break;
                    case (CollisionDetector.CollisionFrom.Top):
                        if (enemy.State.EnemyType != EnemyTypeState.Piranha)
                            enemy.playerAbove(Rectangle.Intersect(enemyRect, playerRect).Height);
                        break;
                    case (CollisionDetector.CollisionFrom.Bottom):
                        break;
                }
            }
            else
                enemy.collisionColor(enemy.CollisionBoxColor);
        }

        public void EnemyCollision(Enemy otherEnemy)
        {
            if(enemy.State.Action == IState.EnemyActionState.Shell && enemy.VelocityX > 0)
            {
                CollisionDetector.CollisionFrom direction;
                direction = CollisionDetector.DetectCollision(enemy.getCollisionBox(), otherEnemy.getCollisionBox());

                if (!(direction == CollisionDetector.CollisionFrom.None))
                {
                    otherEnemy.shellHit();
                }
                else
                    enemy.collisionColor(enemy.CollisionBoxColor);
            }
        }
    }
}
