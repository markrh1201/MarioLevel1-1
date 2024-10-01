using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class Player : IPlayer
    {
        public PlayerState State
        {
            get; set;
            // get
            // { return this.State; }
            // set { this.State = value; }
            // { bool stateChanged = this.State != value; this.State = value; if (stateChanged) this.Notify(); }
        }
        public int positionX { get; set; }
        public int positionY { get; set; }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public bool CanChangeVelocityX { get; set; }
        public bool CanChangeVelocityY { get; set; }
        public bool IsOnGround { get; set; }
        public float AccelerationX { get; set; }
        public float AccelerationY { get; set; }
        public float Gravity { get; set; }
        public float Fraction { get; set; }
        public float warpTimer;
        public bool warp;
        public bool levelComplete;
        public bool previousIsWarned { get; set; }
        public bool isWarned { get; set; }

        public const float MarioMaxVelocityX = 10f;
        public const float MarioMaxVelocityY = 20f;

        public Vector2 Size;

        public int scale;

        private List<Cell> currentCells;

        private bool canDamage;
        public bool CanNotify;
        private float damageCooldown;

        public bool FacingRight
        {
            get => this.State.Facing == FacingState.Right;
            set
            {
                if (value) this.State.Facing = FacingState.Right; else this.State.Facing = FacingState.Left;
            }
        }
        public Sprite Sprite { get; set; }

        public Color CollisionBoxColor { get; set; }

        public ISoundEffectManager soundObserver;
        public int playerLives;
        public int playerCoins;
        public int playerPoints;

        public bool isCollidable;
        public bool teleport;
        public bool isVertical;
        public float warpTimerUnderground;

        public Player(Sprite playerAvatar, Vector2 position, GraphicsDevice graphics)
        {
            this.Sprite = playerAvatar;
            positionX = (int)position.X;
            positionY = (int)position.Y;
            this.scale = 3;
            Size.X = 16;
            Size.Y = 16;
            this.State = new PlayerState();
            this.State.Action = ActionState.Idle;
            this.State.Power = PowerState.Small;
            this.State.Facing = FacingState.Right;
            this.State.Alive = LivingState.Living;
            this.CollisionBoxColor = Color.HotPink;
            updateCollision();
            collisionColor(CollisionBoxColor);
            currentCells = new List<Cell>();
            canDamage = true;
            CanNotify = true;
            this.isWarned = false;
            this.previousIsWarned = false;

            isCollidable = true;
            teleport = false;
            this.warp = false;
            this.Gravity = 1f;
            this.Fraction = 1f;
            this.VelocityX = 0f;
            this.VelocityY = 0f;
            this.AccelerationX = 0f;
            this.AccelerationY = 0f;
            levelComplete = false;
            playerLives = 3;
            playerCoins = 100;
            playerPoints = 1000000;
        }



        #region collision
        public Rectangle getCollisionBox()
        {
            if (isCollidable)
                return new Rectangle(positionX + 1 * scale, positionY + (32 - (int)Size.Y) * scale, (int)Size.X * scale, (int)Size.Y * scale);
            else
                Sprite.updateCollision(new Rectangle(0, 0, 0, 0));
            return new Rectangle(0, 0, 0, 0);
        }

        public void blockSide(int positionChange, bool isHidden)
        {
            if (!isHidden)
                SetXPosition(positionX - positionChange);
        }

        public void blockUnder(int positionChange, bool isHidden)
        {
            if (!isHidden)
            {
                SetYPosition(positionY - positionChange);
                VelocityY = 0;
                AccelerationY = 0;
                CanChangeVelocityY = true;
            }
        }

        public void enemyHit(bool isShell, bool damageShell)
        {
            if (!isShell)
            {
                /*
                if (State.Power == PowerState.Small)
                {
                   // playerLives--;
                }
                */
                Damage();
            }
            else if (damageShell)
                Damage();
        }

        public void enemyBelow(int positionChange, bool isShell)
        {
            CanChangeVelocityY = true;
            Jump();
            IsOnGround = false;
            CanChangeVelocityY = false;
            Idle();
            if (isShell)
            {
                SetYPosition(positionY - positionChange);
                /*
                if (playerRect.Location.X <= enemyRect.Location.X)
                {
                    //enemy.MoveRight();
                }
                else
                {
                    //enemy.MoveLeft();
                }
                */
            }
            else
            {
                playerPoints += 100;
            }
        }

        public void getItem(ItemTypeState itemType)
        {
            switch (itemType)
            {
                case (ItemTypeState.SuperMushroom):
                    if (State.Power != PowerState.Fire)
                        Big();
                    playerPoints += 1000;
                    break;
                case (ItemTypeState.FireFlower):
                    Fire();
                    playerPoints += 1000;
                    break;
                case (ItemTypeState.OneUpMushroom):
                    playerLives++;
                    break;
                case (ItemTypeState.Coin):
                    playerCoins++;
                    playerPoints += 200;
                    break;
                case (ItemTypeState.StarMan):
                    playerPoints += 1000;
                    break;
            }
        }
        public HashSet<IGameObject> getNearbyObjects()
        {
            HashSet<IGameObject> result = new HashSet<IGameObject>();
            foreach (Cell cell in currentCells)
            {
                foreach (IGameObject gameObject in cell.objects)
                {
                    if (gameObject != this)
                        result.Add(gameObject);
                }
            }
            return result;
        }

        public void updateCell(Cell newCell)
        {
            if (!currentCells.Contains(newCell))
            {
                foreach (Cell cell in currentCells.ToArray())
                {
                    if (!cell.objects.Contains(this))
                    {
                        currentCells.Remove(cell);
                    }
                }
                currentCells.Add(newCell);
                newCell.objects.Add(this);
            }
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

        public void Big()
        {
            this.State.Power = PowerState.Big;
            if (this.State.Action == ActionState.Crouch)
                Size.Y = 22;
            else
                Size.Y = 32;
            Sprite.updateCollision(getCollisionBox());
            this.Notify();
        }

        public void Crouch()
        {
            if (this.State.Action != ActionState.Crouch)
            {
                // this.downSpeed = 5;
                if (this.State.Power != PowerState.Die)
                {
                    this.State.Action = ActionState.Crouch;
                    Size.Y = 22;
                }
                else this.Idle();
            }
        }

        public void DieTransition()
        {
            this.State.Power = PowerState.Small;
            this.State.Power = PowerState.Die;
            this.State.Action = ActionState.Crouch;
            playerLives--;
        }

        public void Damage()
        {
            if (canDamage)
            {
                if (this.State.Power == PowerState.Fire)
                {
                    this.Big();
                    this.Notify();
                }
                else if (this.State.Power == PowerState.Big)
                {
                    this.Small();
                    this.Notify();
                }
                else if (this.State.Power == PowerState.Small)
                {
                    DieTransition();
                    this.Notify();
                    this.CanNotify = false;
                }
                canDamage = false;
                damageCooldown = 0;
            }
        }

        public void Dash()
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch batch, int scale = 1)
        {
            this.Sprite.drawSprite(batch, new Vector2(positionX, positionY), this.FacingRight, scale);
        }

        public void Fire()
        {
            Big();
            this.State.Power = PowerState.Fire;
        }

        public void Idle()
        {
            this.State.Action = ActionState.Idle;
            // moveSpeed = 0;
            // downSpeed = 0;
            if (this.State.Power != PowerState.Small && this.State.Power != PowerState.Die)
            {
                Size.Y = 32;
            }
            this.AccelerationX = 0f;
            this.AccelerationY = 0f;
            // if (this.VelocityY<0) this.VelocityY = 0;
        }

        public void Jump()
        {
            if (this.State.Action == ActionState.Crouch)
            {
                this.State.Action = ActionState.Idle;
                if (this.State.Power != PowerState.Small && this.State.Power != PowerState.Die)
                {
                    Size.Y = 32;
                }
            }
            else if (this.State.Action != ActionState.Jump)
            {
                this.State.Action = ActionState.Jump;
                this.Notify();
                this.VelocityY = this.CanChangeVelocityY ? -20f : 0;
                this.AccelerationY = this.CanChangeVelocityY ? -8f : 0;
                this.CanChangeVelocityY = false;
                // this.downSpeed = -5;
            }
            else if (this.State.Action == ActionState.Jump)
            {
                this.AccelerationY = 0f;
            }
            this.AccelerationX = 0f;
        }

        public void MoveLeft()
        {
            if (this.FacingRight)
            {
                if (this.State.Action == ActionState.Run)
                {
                    Idle();
                }
                else this.FacingRight = false;
            }
            else if (this.State.Action == ActionState.Idle)
            {
                this.State.Action = ActionState.Run;
                this.AccelerationX = -5f;
            }
            else if (this.State.Action == ActionState.Jump)
            {
                this.AccelerationX = -5f;
            }
        }

        public void MoveRight()
        {
            if (!this.FacingRight)
            {
                if (this.State.Action == ActionState.Run)
                {
                    Idle();
                }
                else this.FacingRight = true;
            }
            else if (this.State.Action == ActionState.Idle)
            {
                this.State.Action = ActionState.Run;
                this.AccelerationX = 5f;
            }
            else if (this.State.Action == ActionState.Jump)
            {
                this.AccelerationX = 5f;
            }
        }

        public void Small()
        {
            this.State.Power = PowerState.Small;
            this.Notify();
            Size.Y = 16;
        }

        public void SetXPosition(int x)
        {
            this.positionX = x;
        }

        public void SetYPosition(int y)
        {
            this.positionY = y;
        }

        public void CompleteLevel()
        {
            levelComplete = true;
            VelocityX = 0;
            VelocityY = 0;
            playerPoints += (int)((289 - positionY) * 16.8f);
        }

        public void Warp()
        {
            isCollidable = false;
            isVertical = true;
            VelocityX = 0;
            VelocityY = 0;
        }

        public void WarpUnderground()
        {
            isCollidable = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!levelComplete)
            {
                if (this.State.Power != PowerState.Die)
                {
                    if (this.previousIsWarned != this.isWarned) this.Notify();
                    // Console.WriteLine(this.State.Action);
                    // Console.WriteLine("Acc X: " + this.AccelerationX + ", Acc Y: " + this.AccelerationY);
                    float localFraction = 0f;
                    if (this.VelocityX != 0) localFraction = this.VelocityX > 0 ? this.Fraction : -this.Fraction;
                    this.VelocityX = Math.Clamp(this.VelocityX + this.AccelerationX - localFraction, -MarioMaxVelocityX, MarioMaxVelocityX);
                    this.VelocityY = Math.Clamp(this.VelocityY + this.AccelerationY + this.Gravity, -MarioMaxVelocityY, MarioMaxVelocityY);
                    // Console.WriteLine("Velocity X: " + this.VelocityX + ", Velocity Y: " + this.VelocityY);
                    this.positionX += Convert.ToInt32(this.VelocityX);
                    this.positionY += Convert.ToInt32(this.VelocityY);
                    // Console.WriteLine("POS X: " + this.VelocityX + ", POS Y: " + this.VelocityY);
                    // Console.WriteLine("POS YY: " + this.positionY);
                    SetXPosition(positionX);
                    SetYPosition(positionY);

                    updateCollision();
                }
                if (playerLives <=0)
                {
                    this.Notify();
                }

                if (!canDamage)
                {
                    damageCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (damageCooldown > 2f)
                        canDamage = true;
                }
                if (this.positionY > 400)
                {
                    this.State.Power = PowerState.Die;
                    this.Notify();
                    this.CanNotify = false;
                }
                if (this.positionX > 5157 && this.positionX < 5300)
                {
                    CompleteLevel();
                    this.Notify();
                }
                if (warpTimer > 0)
                {
                    warp = true;
                    if (warp)
                    {
                        State.Action = ActionState.Idle;
                        Warp();
                    }
                    warpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (warpTimer > 2f)
                    {
                        warpTimer = 0;
                        this.warp = false;
                        teleport = true;
                        if (teleport && isVertical)
                        {
                            isCollidable = true;
                            positionX = 5700;
                            positionY = 10;
                        }
                    }
                }
                if (warpTimerUnderground > 0)
                {
                    warp = true;
                    if (warp)
                    {
                        WarpUnderground();
                        State.Action = ActionState.Idle;
                        VelocityX = 2f;
                        VelocityY = -1f;
                    }
                    warpTimerUnderground += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (warpTimerUnderground > 2f)
                    {
                        warpTimerUnderground = 0;
                        this.warp = false;
                        teleport = true;
                        if (teleport)
                        {
                            isCollidable = true;
                            positionX = 4500;
                            positionY = 100;
                        }
                    }
                }
            }
            else
            {
                if (positionY < 284)
                {
                    VelocityY = 2f;
                    VelocityX = 0f;
                }
                else if (positionX < 5345)
                {
                    this.State.Action = ActionState.Run;
                    VelocityX = 1f;
                    VelocityY = 0f;
                }
                else
                {
                    this.State.Action = ActionState.Idle;
                    VelocityX = 0f;
                }

                this.positionX += Convert.ToInt32(this.VelocityX);
                this.positionY += Convert.ToInt32(this.VelocityY);
            }
            this.previousIsWarned = this.isWarned;
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 256);
        }

        public string AnimationName()
        {
            string result = "mario";
            result += this.State.Action.ToString();
            result += this.State.Power.ToString();
            return result;
        }

        public void AttachObserver(ISoundEffectManager soundObserver)
        {
            Console.WriteLine("Mario should attach observer");
            this.soundObserver = soundObserver;
            Console.WriteLine("{0},{1}",soundObserver is null,this.soundObserver is null);
        }

        public void DetachObserver(ISoundEffectManager soundEffectManager)
        {
            this.soundObserver = null;
        }

        public void Notify()
        {
            if (this.CanNotify) this.soundObserver.Update(this);
        }

    }
}