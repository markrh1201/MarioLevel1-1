using GameCamera;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;


namespace Sprint4BeanTeam
{
    public class WinScreen
    {
        public Game1 Game;
        public bool gameOver;

        private Player Player;
        private Camera camera;
        private SpriteFont spriteFont;
        
        private GameHUD gameHUD;
        private Texture2D winnerScreen;

        public WinScreen(Game1 game, Player player, Camera camera, GameHUD gameHUD, LevelManager level)
        {
            Game = game;
            this.camera = camera;
            Player = level.Players[0];

            gameOver = player.levelComplete;

            spriteFont = Game.Content.Load<SpriteFont>("super-mario-bro");
            winnerScreen = Game.Content.Load<Texture2D>("WinnerScreen");

            this.gameHUD = gameHUD;

        }

        public void Update(GameTime gameTime, Player player)
        {
            gameOver = player.levelComplete;
            Player = player;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (gameOver && Player.positionX == 5345)
            {
                spriteBatch.Draw(winnerScreen, camera.Position, Color.White);
                spriteBatch.DrawString(spriteFont, (gameHUD.points + gameHUD.timePoints).ToString().Substring(1), camera.Position + new Vector2(140, 130),Color.White);
                spriteBatch.DrawString(spriteFont, gameHUD.coins.ToString().Substring(1), camera.Position + new Vector2(450, 130), Color.White);
                spriteBatch.DrawString(spriteFont, gameHUD.timePoints.ToString(), camera.Position + new Vector2(725, 130), Color.White);
            }


        }
    }
}
