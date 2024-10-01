using GameCamera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam.GameObject.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class Piranha : Enemy, IObserver
    {
        private bool nearPlayer = false;
        public Piranha(Sprite enemySprite, Vector2 position, Camera cam) : base(enemySprite, IState.EnemyTypeState.Piranha, position, cam)
        {
            this.State.Action = EnemyActionState.Normal;
            VelocityY = 1f;
        }

        public override string AnimationName()
        {
            string result = "enemyPiranha";
            result += this.State.Action.ToString();
            return result;
        }

        public override Rectangle getCollisionBox()
        {
            return new Rectangle(positionX + 26, (positionY - 10) + (24 - (int)Size.Y) * scale, (int)Size.X * scale, (int)Size.Y * 5);
        }

        public override void Draw(SpriteBatch batch, int scale)
        {
            if (this.State.Alive != LivingState.Dead)
            {
                this.Sprite.drawSprite(batch, new Vector2(positionX + 24, positionY + 10), false, scale);
            }
        }

        private const float PeakDuration = 1000f; 
        private float peakTimer = 0f; 
        private bool atPeak = false; 

        public override void Update(GameTime gameTime)
        {

            if (this.State.Alive != LivingState.Dead)
            {
                this.Sprite.changeCurrentAnimation(this.AnimationName());
                this.Sprite.updateSprite(gameTime, 256);
                updateCollision();

                if (!nearPlayer)
                {
                    Move(gameTime);

                    positionY += (int)(VelocityY);
                }
                else
                {
                    Move(gameTime);

                    if (VelocityY > 0 || VelocityY < 0 && positionY <= 280)
                    {
                        positionY += (int)(VelocityY);
                    }

                }


            }
        }

        public void Notify(Player player)
        {
            int offset = 21;

            float distanceToPlayer = Math.Abs(positionX + offset - player.positionX);

            if (distanceToPlayer < 80)
            {
                nearPlayer = true;
            }
            else 
                nearPlayer= false;
        }

        private void Move(GameTime gameTime)
        {
            if (VelocityY < 0 && positionY <= 210)
            {
                VelocityY = 0;
                atPeak = true;
            }

            if (atPeak)
            {
                peakTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (peakTimer > PeakDuration)
                {
                    VelocityY = 1;
                    peakTimer = 0;
                    atPeak = false;
                }
            }
        }
    }
}
