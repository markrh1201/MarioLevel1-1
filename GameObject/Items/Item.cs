using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LevelTilemap.Tilemap;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class Item : IItem
    {
        public ItemState State { get; set; } //{ get { return this.State; } set { this.State = value; this.Notify(); } }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public Vector2 Size;

        public int scale;
        public bool FacingRight;
        public bool isCollidable;
        public bool isRevealed;
        public Sprite Sprite { get; set; }
        public Color CollisionBoxColor { get; set; }
        private Cell currentCell;

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public float gravity;

        public ISoundEffectManager soundObserver;

        public Item(Sprite itemSprite, ItemTypeState type, Vector2 position)
        {
            this.Sprite = itemSprite;
            positionX = (int)position.X;
            positionY = (int)position.Y;
            this.State = new ItemState();
            this.State.ItemType = type;
            this.State.Alive = LivingState.Living;
            this.scale = 3;
            this.isCollidable = true;
            isRevealed = false;
            Size.X = 16;
            Size.Y = 16;
            this.CollisionBoxColor = Color.Purple;
            updateCollision();
            collisionColor(CollisionBoxColor);
            currentCell = null;

            gravity = 0f;
            VelocityX = 0f;
            VelocityY = 0f;
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
        public void ChangeDirection()
        {
            VelocityX *= -1;
        }

        public void Consume()
        {
            this.State.Alive = LivingState.Dead;
            this.Notify();
        }

        public void Reveal()
        {
            isRevealed = true;
            this.VelocityX = 100f;
            this.Notify();
        }

        public Rectangle getCollisionBox()
        {
            if (State.Alive != LivingState.Dead && isCollidable)
                return new Rectangle(positionX, positionY, (int)Size.X * scale, (int)Size.Y * scale);
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

        public void blockAbove(int positionChange)
        {
            collisionColor(Color.Red);
            if (State.ItemType == IState.ItemTypeState.StarMan)
            {
                VelocityY *= -1;
            }
            SetYPosition(positionY + positionChange);
        }

        public void blockBelow(int positionChange)
        {
            collisionColor(Color.Green);
            if (State.ItemType == IState.ItemTypeState.StarMan)
            {
                gravity = -450f;
            }
            VelocityY = 0;
            SetYPosition(positionY - positionChange);
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

        public void collideOn(bool on)
        {
            isCollidable = on;
        }

        public void SetXPosition(int x)
        {
            positionX = x;
        }

        public void SetYPosition(int y)
        {
            positionY = y;
        }

        public void Draw(SpriteBatch batch, int scale = 1)
        {
            if (this.State.Alive != LivingState.Dead && isCollidable)
                this.Sprite.drawSprite(batch, new Vector2((int)positionX, (int)positionY), this.FacingRight, scale);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 256);
            updateCollision();
            this.Notify();

            if (isRevealed)
            {
                VelocityY += 90f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                positionX += (int)(VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds);
                positionY += (int)(VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
        public string AnimationName()
        {
            string result = "item";
            result += this.State.ItemType.ToString();
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
            Console.WriteLine(this.soundObserver is null);
            this.soundObserver.Update(this);
        }
    }
}
