using System;
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class Block : IBlock
    {
        public int currentPosX;
        public int currentPosY;

        public int targetedBumpHeight = 10;
        public int targetedDropHeight = 100;
        public int totalTimeSinceLastBump;
        public int bumpHeightSinceLastUpdate;
        public int totalTimeSinceLastReveal;

        public int totalTimeItem;
        public Sprite Sprite { get; set; }

        public int positionX
        {
            get => this.currentPosX;
            set { this.currentPosX = value; }
        }

        public int positionY
        {
            get => this.currentPosY;
            set { this.currentPosY = value; }
        }

        public Color CollisionBoxColor { get; set; }
        private Cell currentCell;

        public Item currItem;
        public List<Item> items;

        public Player currentPlayer;
        public List<Player> players;

        public Vector2 Size;

        public BlockState State { get; set; } //{ get { return this.State; } set { this.State = value; this.Notify(); } }

        public bool isBumping { get; set; }

        public bool isBreaking { get; set; }

        public bool isRevealing;

        public bool isVisible;

        public int scale;

        public bool canTeleport;

        public ISoundEffectManager soundObserver;

        public Block(Sprite blockSprite, BlockTypeState type, Vector2 position)
        {
            this.items = new List<Item>();
            players = new List<Player>();
            this.Sprite = blockSprite;
            this.positionX = (int)position.X;
            this.positionY = (int)position.Y;
            this.State = new BlockState();
            this.State.BlockType = type;
            this.State.ItemType = ItemTypeState.None;
            this.totalTimeSinceLastBump = 0;
            this.totalTimeSinceLastReveal = 0;
            this.totalTimeItem = 0;
            this.State.Alive = LivingState.Living;
            this.targetedBumpHeight = 10;
            this.bumpHeightSinceLastUpdate = 0;
            this.scale = 3;
            Size.X = 16;
            Size.Y = 16;
            this.isVisible = type == BlockTypeState.Hidden ? false : true;
            this.isBreaking = false;
            this.isRevealing = false;
            CollisionBoxColor = Color.GreenYellow;
            updateCollision();
            collisionColor(CollisionBoxColor);
            currentCell = null;
            canTeleport = false;
        }

        public virtual void Bump()
        {

        }

        public void storeItem(Item item)
        {
            item.collideOn(false);
            items.Add(item);
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

        public virtual Rectangle getCollisionBox()
        {
            if (State.BlockType != BlockTypeState.Break)
                return new Rectangle(positionX, positionY, (int)Size.X * scale, (int)Size.Y * scale);
            else
                Sprite.updateCollision(new Rectangle(0, 0, 0, 0));
            return new Rectangle(0, 0, 0, 0);
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

        public virtual void Draw(SpriteBatch batch, int scale = 1)
        {
            this.Sprite.drawSprite(batch, new Vector2(positionX, positionY), false, scale);
        }

        public virtual void Hide()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Update(GameTime gameTime, PowerState marioState, FacingState facingState)
        {
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 100);
        }

        public string AnimationName()
        {
            string result = "block";
            result += State.BlockType.ToString();
            return result;
        }

        public virtual void Break()
        {

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