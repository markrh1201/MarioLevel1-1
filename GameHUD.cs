using GameCamera;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Sprint4BeanTeam
{
    public class GameHUD
    {
        public Game1 Game;
        private Player player;
        private Camera Cameras;
        private SpriteFont spriteFont;
        private Texture2D coinHUD;
        private Color textColor;
        public Sprite coinSprite;

        private float currentTime;
        private float oneSecond;
        public int points, lives, coins, times, timePoints;
        private String point, life, coin, time;

        private bool decreaseTime;
        private bool playedWarn;
        private bool pointsAdded;

        public GameHUD(Game1 game, Camera camera, Player player)
        {
            Game = game;
            Cameras = camera;

            points = 0;
            coins = 0;
            times = 1400;
            timePoints = 0;
            pointsAdded = false;


            decreaseTime = true;
            playedWarn = false;
            currentTime = 0f;
            oneSecond = 1f;
            spriteFont = Game.Content.Load<SpriteFont>("super-mario-bro");
            textColor = Color.White;
        }

        public void Update(GameTime gameTime, Player player)
        {
            points = player.playerPoints;
            coins = player.playerCoins;
            lives = player.playerLives;
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((currentTime >= oneSecond) && decreaseTime)
            {
                times--;
                currentTime -= 1f;
            }
            if (coins / 100 == 2)
            {
                player.playerCoins -= 100;
                player.playerLives += 1;
            }


            point = "Mario\n" + (points + timePoints).ToString().Substring(1);
            life = "\n    X " + lives.ToString();
            coin = "\n      X " + coins.ToString().Substring(1);
            time = "Time\n" + times.ToString().Substring(1);

            if (times <= 1050 && !playedWarn)
            {
                //Play warning sound
                playedWarn = true;
                player.isWarned = true;
            }
            if (times == 1000)
            {
                player.State.Alive = IState.LivingState.Dead;
                player.State.Power = IState.PowerState.Die;
                player.Notify();
                player.CanNotify = false;
                decreaseTime = false;
            }
            if (player.levelComplete && !pointsAdded)
            {
                pointsAdded = true;
                timePoints += times - 1000;
                player.playerPoints += timePoints;
                decreaseTime = false;
            }
            textColor = Color.White;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //coinSprite.drawSprite(spriteBatch, new Vector2(Cameras.Position.X + 400, Cameras.Position.Y + 40), false, 1);
            spriteBatch.DrawString(spriteFont, point, new Vector2(Cameras.Position.X + 20, Cameras.Position.Y), textColor);
            spriteBatch.DrawString(spriteFont, life, new Vector2(Cameras.Position.X + 200, Cameras.Position.Y - 10), textColor);
            spriteBatch.DrawString(spriteFont, coin, new Vector2(Cameras.Position.X + 300, Cameras.Position.Y - 10), textColor);
            spriteBatch.DrawString(spriteFont, "World\n   1", new Vector2(Cameras.Position.X + 500, Cameras.Position.Y), textColor);
            spriteBatch.DrawString(spriteFont, time, new Vector2(Cameras.Position.X + 700, Cameras.Position.Y), textColor);
            if (lives <= 0)
            {
                Game.paused = true;
                Game.gameOver = true;
                Game.GraphicsDevice.Clear(Color.Black);
                Game.SE.PlayGameOver();
                spriteBatch.DrawString(spriteFont, "GAME OVER!", new Vector2(Cameras.Position.X + (Game.GraphicsDevice.Viewport.Width / 2f) - 100, Cameras.Position.Y + (Game.GraphicsDevice.Viewport.Height / 2f)), Color.White);
                spriteBatch.DrawString(spriteFont, "Press [R] to Retry...\n... or [Q] to Exit.", new Vector2(Cameras.Position.X + (Game.GraphicsDevice.Viewport.Width / 2f) - 100, Cameras.Position.Y + (Game.GraphicsDevice.Viewport.Height / 2f) + 50), Color.White);
            }
        }
    }
}
