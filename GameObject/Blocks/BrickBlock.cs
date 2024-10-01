using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;
using static LevelTilemap.Tilemap;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class BrickBlock : Block
    {

        public BrickBlock(Sprite blockSprite, BlockTypeState state, Vector2 position) : base(blockSprite, state, position)
        {

        }

        public override void Bump()
        {
            this.isBumping = true;
            this.isRevealing = true;
            this.Notify();
        }

        public override void Draw(SpriteBatch batch, int scale = 1)
        {
            if (isBreaking)
            {
                Vector2 piece0 = new Vector2(),
                piece1 = new Vector2(),
                piece2 = new Vector2(),
                piece3 = new Vector2();
                piece0.X = this.positionX - 15;
                piece1.X = this.positionX + 15;
                piece2.X = this.positionX - 15;
                piece3.X = this.positionX + 15;
                piece0.Y = this.positionY - 15;
                piece1.Y = this.positionY - 15;
                piece2.Y = this.positionY + 15;
                piece3.Y = this.positionY + 15;
                this.Sprite.drawSprite(batch, piece0, false, 2);
                this.Sprite.drawSprite(batch, piece1, false, 2);
                this.Sprite.drawSprite(batch, piece2, false, 2);
                this.Sprite.drawSprite(batch, piece3, false, 2);
            }
            else
            {
                this.Sprite.drawSprite(batch, new Vector2(positionX, positionY), false, scale);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime, PowerState marioState, FacingState facingState)
        {
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 100);
            if (isBumping)
            {
                if (isBumping && (this.State.BlockType == BlockTypeState.Brick || this.State.BlockType == BlockTypeState.Break) && (marioState == PowerState.Big || marioState == PowerState.Fire) && items.Count == 0)
                {
                    this.Break();
                    totalTimeSinceLastBump += gameTime.ElapsedGameTime.Milliseconds;
                    if (totalTimeSinceLastBump > 5)
                    {
                        int velocity = 20;
                        this.currentPosY += velocity;
                        totalTimeSinceLastBump -= 5;
                        bumpHeightSinceLastUpdate += 2;
                        if (totalTimeSinceLastBump > 20f)
                            this.State.Alive = LivingState.Dead;
                    }
                    if (bumpHeightSinceLastUpdate >= targetedDropHeight)
                    {
                        this.isBumping = false;
                        this.bumpHeightSinceLastUpdate = 0;
                        this.totalTimeSinceLastBump = 0;
                    }
                }
                else
                {
                    totalTimeSinceLastBump += gameTime.ElapsedGameTime.Milliseconds;
                    if (totalTimeSinceLastBump > 5)
                    {
                        int posOffset = this.bumpHeightSinceLastUpdate < targetedBumpHeight ? 2 : -2;
                        this.currentPosY -= posOffset;
                        totalTimeSinceLastBump -= 5;
                        bumpHeightSinceLastUpdate += 2;
                    }
                    if (bumpHeightSinceLastUpdate >= targetedBumpHeight * 2)
                    {
                        this.isBumping = false;
                        this.bumpHeightSinceLastUpdate = 0;
                        this.totalTimeSinceLastBump = 0;
                    }
                }
            }
            if (isRevealing && items.Count != 0)
            {
                this.currItem = items[0];
                totalTimeSinceLastReveal += gameTime.ElapsedGameTime.Milliseconds;
                if (totalTimeSinceLastReveal < 120)
                {
                    this.currItem.positionY -= 7;
                    currItem.isRevealed = true;
                }
                else
                {
                    this.items.Remove(currItem);
                    isRevealing = false;
                    currItem.collideOn(true);
                    totalTimeSinceLastReveal = 0;
                }
            }

        }
        public override void Break()
        {
            this.State.BlockType = BlockTypeState.Break;
            this.Sprite.changeCurrentAnimation("blockBreak");
            this.isBreaking = true;
            this.Notify();
        }
    }
}
