using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint4BeanTeam.IState;
using GameCamera;

namespace Sprint4BeanTeam.GameObject.Enemies
{
    public class GreenKoopa : Enemy
    {
        public GreenKoopa(Sprite greenKoopaSprite, Vector2 position, Camera cam) : base(greenKoopaSprite, IState.EnemyTypeState.GreenKoopa, position, cam)
        {
            this.State.Facing = FacingState.Right;
            this.koopaTimer = 0;
            moveSpeed = 5f;
        }

        public override Rectangle getCollisionBox()
        {
            return new Rectangle(positionX, positionY + (24 - (int)Size.Y) * scale, (int)Size.X * scale, (int)Size.Y * scale);
        }

        public override void Draw(SpriteBatch batch, int scale = 1)
        {
            this.Sprite.drawSprite(batch, new Vector2((int)positionX, (int)positionY), this.FacingRight, scale);
        }

        public override void EmergeFromShell()
        {
            this.State.Action = EnemyActionState.Normal;
            this.State.Facing = FacingState.Right;
            VelocityX = 5f;
        }

        public override void Update(GameTime gameTime)
        {
            if (State.Action != EnemyActionState.Shell)
            {
                if (VelocityX == 0)
                {
                    if (isActivated)
                        Activate();
                }
            }
            if (this.State.Action == EnemyActionState.Shell && this.VelocityX == 0)
            {
                if (koopaTimer > 10)
                {
                    EmergeFromShell();
                    this.Sprite.changeCurrentAnimation(this.AnimationName());
                    koopaTimer = 0;
                }
                koopaTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (this.State.Alive != LivingState.Dead)
            {
                this.Sprite.changeCurrentAnimation(this.AnimationName());
                this.Sprite.updateSprite(gameTime, 256);
                updateCollision();
                VelocityY += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            positionX += (int)(VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds * speedControl);
            positionY += (int)(VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds * gravityControl);
        }

        public override string AnimationName()
        {
            string result = "enemyGreenKoopa";
            result += this.State.Action.ToString();
            return result;
        }
    }
}
