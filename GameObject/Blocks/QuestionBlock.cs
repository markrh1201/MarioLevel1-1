using System;
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;
using static LevelTilemap.Tilemap;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class QuestionBlock : Block
    {
        public QuestionBlock(Sprite blockSprite, Vector2 position) : base(blockSprite, IState.BlockTypeState.Question, position)
        {
        }

        public override void Bump()
        {
            if (this.State.BlockType == BlockTypeState.Question)
            {
                this.isBumping = true;
                this.isRevealing = true;
                this.Notify();
                this.State.BlockType = BlockTypeState.Empty;
            }
        }

        public override void Update(GameTime gameTime, PowerState marioState, FacingState facingState)
        {
            this.Sprite.changeCurrentAnimation(this.AnimationName());
            this.Sprite.updateSprite(gameTime, 100);
            if (isBumping)
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
            if (isRevealing && items.Count > 0)
            {
                this.currItem = items[0];
                totalTimeSinceLastReveal += gameTime.ElapsedGameTime.Milliseconds;
                totalTimeItem += gameTime.ElapsedGameTime.Milliseconds;
                if (totalTimeSinceLastReveal < 150)
                {
                    this.currItem.positionY -= 2;
                    if (totalTimeSinceLastReveal > 100)
                    {
                        this.currItem.collideOn(true);
                    }
                }
                if (totalTimeItem >= 200)
                {
                    this.currItem.Reveal();
                    this.items.Remove(currItem);
                }
                if (facingState == FacingState.Left)
                {
                    if (currItem.State.ItemType == ItemTypeState.SuperMushroom)
                    {
                        currItem.VelocityX = -150f;
                    }
                    else if (currItem.State.ItemType == ItemTypeState.OneUpMushroom || currItem.State.ItemType == ItemTypeState.StarMan)
                    {
                        currItem.VelocityX = 150f;
                    }
                }
                else
                {
                    if (currItem.State.ItemType == ItemTypeState.SuperMushroom)
                    {
                        currItem.VelocityX = 180f;
                    }
                    else if (currItem.State.ItemType == ItemTypeState.OneUpMushroom || currItem.State.ItemType == ItemTypeState.StarMan)
                    {
                        currItem.VelocityX = -180f;
                    }
                }
            }
        }
    }
}
