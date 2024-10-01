using GameCamera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class Enemy : IEnemy
    {
        public int positionX { get; set; }
        public int positionY { get; set; }

        public Vector2 Size;

        public int scale;

        public bool canDamage;
        public float damageCooldown;
        public bool isActivated;
        public bool FacingRight
        {
            get => this.State.Facing == FacingState.Right;
            set
            {
                if (value) this.State.Facing = FacingState.Right; else this.State.Facing = FacingState.Left;
            }
        }

        public Sprite Sprite { get; set; }

        public float deathTimer;
        public float koopaTimer;
        private const float deathDuration = 1f;

        public Cell currentCell;
        public Color CollisionBoxColor { get; set; }

        public EnemyState State  { get; set; }//{ get { return this.State; } set { this.State = value; this.Notify(); } }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float speedControl;
        public float gravityControl;
        public float gravity;
        public float moveSpeed;

        public ISoundEffectManager soundObserver;

        public Enemy(Sprite sprite, EnemyTypeState type, Vector2 position, Camera cam)
        {
            this.Sprite = sprite;
            positionX = (int)position.X;
            positionY = (int)position.Y;
            this.scale = 3;
            Size.X = 16;
            Size.Y = 16;
            this.CollisionBoxColor = Color.Orange;
            this.State = new EnemyState();
            this.State.EnemyType = type;
            this.State.Facing = FacingState.Right;
            this.State.Action = EnemyActionState.Normal;
            this.State.Alive = LivingState.Living;
            updateCollision();
            collisionColor(CollisionBoxColor);
            currentCell = null;
            canDamage = true;
            deathTimer = 0;

            gravity = 20f;
            speedControl = 20f;
            gravityControl = 35f;
            VelocityX = 0f;
            isActivated = false;
        }

        public HashSet<IGameObject> getNearbyObjects()
        {
            HashSet<IGameObject> result = new HashSet<IGameObject>();
            if (currentCell != null)
            {
                foreach (IGameObject gameObject in currentCell.objects)
                {
                    result.Add(gameObject);
                }
            }

            return result;
        }

        public void updateCell(Cell newCell)
        {
            if (newCell != currentCell)
            {
                if (currentCell != null)
                {
                    currentCell.objects.Remove(this);
                    newCell.objects.Add(this);
                }
                currentCell = newCell;
            }
        }

        #region collisions

        public virtual Rectangle getCollisionBox()
        {
            if (State.Alive != LivingState.Dead)
                return new Rectangle(positionX, (positionY + 2) + (24 - (int)Size.Y) * scale, (int)Size.X * scale, (int)Size.Y * scale);
            else
                Sprite.updateCollision(new Rectangle(0, 0, 0, 0));
            return new Rectangle(0, 0, 0, 0);
        }

        public void blockSide(int positionChange)
        {
            collisionColor(Color.Red);
            SetXPosition(positionX - positionChange);
            ChangeDirection();
        }

        public void blockUnder(int positionChange, bool isBumping)
        {
            if (this.State.EnemyType != EnemyTypeState.Piranha)
                VelocityY = 0;
            else
                VelocityY *= -1;
            SetYPosition(positionY - positionChange);
            if (isBumping)
                DieTransition();
        }

        public void playerRight(int positionChange)
        {
            collisionColor(Color.Red);
            this.Notify();
            if (State.Action == EnemyActionState.Shell && VelocityX == 0)
            {
                SetXPosition(positionX - positionChange);
                MoveLeft();
            }

        }

        public void playerLeft(int positionChange)
        {
            collisionColor(Color.Red);
            this.Notify();
            if (State.Action == EnemyActionState.Shell && VelocityX == 0)
            {
                SetXPosition(positionX + positionChange);
                MoveRight();
            }

        }

        public void playerAbove(int positionChange)
        {
            collisionColor(Color.Red);
            DieTransition();
            if (State.Action == EnemyActionState.Shell)
            {
                SetYPosition(positionY + positionChange);
            }

        }

        public void shellHit()
        {
            collisionColor(Color.Red);
            DieTransition();
        }

        public void showCollision()
        {
            Sprite.showCollision();
        }

        public void updateCollision()
        {
            Sprite.updateCollision(getCollisionBox());
        }

        public void collisionColor(Color color)
        {
            Sprite.updateCollisionColor(color);
        }

        #endregion


        public void DieTransition()
        {
            if (this.State.EnemyType == EnemyTypeState.GreenKoopa)
            {
                this.State.Action = EnemyActionState.Shell;
            }
            else
            {
                this.State.Action = EnemyActionState.Die;
                this.State.Alive = LivingState.Dead;
            }
            this.Notify();
            VelocityX = 0;
            VelocityY = 0;
        }

        #region movement
        public void ChangeDirection()
        {
            VelocityX *= -1;
            if (this.State.Facing == FacingState.Right)
            {
                this.State.Facing = FacingState.Left;
            }
            else
            {
                this.State.Facing = FacingState.Right;
            }
        }

        public void MoveLeft()
        {
            VelocityX = -moveSpeed * 3;
        }

        public void MoveRight()
        {
            VelocityX = moveSpeed * 3;
        }

        public void SetXPosition(int x)
        {
            positionX = x;
        }

        public void SetYPosition(int y)
        {
            positionY = y;
        }

        public virtual void EmergeFromShell()
        {

        }

        public void Activate()
        {
            VelocityX = -this.moveSpeed;
            isActivated = false;
        }

        #endregion

        public virtual void Draw(SpriteBatch batch, int scale = 1)
        {
            if (this.State.Alive != LivingState.Dead || deathTimer < deathDuration)
            {
                this.Sprite.drawSprite(batch, new Vector2((int)positionX, (int)positionY), this.FacingRight, scale);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.State.Alive != LivingState.Dead)
            {
                if (VelocityX == 0)
                {
                    if (isActivated)
                    {
                        Activate();
                    }
                }
                this.Sprite.changeCurrentAnimation(this.AnimationName());
                this.Sprite.updateSprite(gameTime, 256);
                updateCollision();
                VelocityY += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (deathTimer == 0)
                    this.Sprite.changeCurrentAnimation(this.AnimationName());
                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            positionX += (int)(VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds * speedControl);
            positionY += (int)(VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds * gravityControl);
        }

        public virtual string AnimationName()
        {
            string result = "enemy";
            result += this.State.Action.ToString();
            return result;
        }

        public void AttachObserver(ISoundEffectManager soundObserver)
        {
            this.soundObserver = soundObserver;
        }

        public void DetachObserver(ISoundEffectManager soundEffectManager)
        {
            this.soundObserver = null;
        }

        public void Notify()
        {
            this.soundObserver.Update(this);
        }
    }
}
