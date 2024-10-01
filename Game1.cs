using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint4BeanTeam;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System;
using Sprint4BeanTeam.GameObject.Enemies;
using GameCamera;

namespace Sprint4BeanTeam
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public LevelManager _level;
        private GameHUD gameHUD;
        private WinScreen winScreen;

        //private CollisionDistributor collision;
        public List<IController> controllers;
        public bool paused;
        public bool gameOver;

        private Camera _camera;
        private List<Layer> _layers;
        public SoundEffectManager SE;
        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics = new GraphicsDeviceManager(this);
            this.SE = new SoundEffectManager(Content);
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            paused = false;
            gameOver = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            controllers = new List<IController>();

            _level = new LevelManager("../../../tilemap.json", GraphicsDevice, Content, this);

            _camera = new Camera(GraphicsDevice.Viewport);
            _camera.Limits = new Rectangle(0, 0, 4000, 480);

            gameHUD = new GameHUD(this, _camera, _level.Players[0]);
            winScreen = new WinScreen(this, _level.Players[0], _camera, gameHUD, _level);
            _layers = new List<Layer>
            {
                new Layer(_camera) { Parallax = new Vector2(0.7f, 1.0f) },
                new Layer(_camera) { Parallax = new Vector2(0.5f, 1.0f) },
                new Layer(_camera) { Parallax = new Vector2(0.4f, 1.0f) },
                new Layer(_camera) { Parallax = new Vector2(1.0f, 1.0f) }
            };

            _layers[0].Sprites.Add(new Background { Texture = Content.Load<Texture2D>("HillsAndBushes") });
            _layers[1].Sprites.Add(new Background { Texture = Content.Load<Texture2D>("Clouds") });
            _layers[2].Sprites.Add(new Background { Texture = Content.Load<Texture2D>("BiggerClouds") });
            _layers[3].Sprites.Add(new Background { Texture = Content.Load<Texture2D>("SecretBG"), Position = new Vector2(5625, 0)});
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            _level.UpdateLevel(gameTime, _camera);
            foreach (IController controller in controllers)
            {
                controller.UpdateState();
            }
            if (!paused)
            {
                gameHUD.Update(gameTime, _level.Players[0]);
            }
            winScreen.Update(gameTime, _level.Players[0]);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Layer layer in _layers)
                layer.Draw(_spriteBatch);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, transformMatrix: _camera.GetViewMatrix(Vector2.One));
            _level.DrawLevel(_spriteBatch, _camera);
            gameHUD.Draw(_spriteBatch);

            winScreen.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}