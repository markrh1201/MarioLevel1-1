using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameCamera;
using LevelTilemap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;
using Sprint4BeanTeam.GameObject.Enemies;
using Sprint4BeanTeam.State;
using System.Linq;
using System.Collections.ObjectModel;

public class LevelManager
{
    Tilemap level;
    GraphicsDevice Graphics;
    Game1 Game;

    List<IEnemy> enemyList;
    List<IBlock> blockList;
    List<Player> playerList;
    List<IItem> itemList;
    Dictionary<string, Sprite> sceneryDict;

    public CollisionDistributor collision;

    SoundEffectManager soundObserver;

    public List<Player> Players;
    public List<IBlock> Tiles;
    public List<IEnemy> Enemies;
    public List<IItem> Items;
    private List<KeyValuePair<Sprite, Vector2>> Scenery;
    private List<int> checkpointPositions;
    private int _previousPlayerLives;
    private int _previousXPosition;
    private bool _loading;

    public LevelManager(string filename, GraphicsDevice graphics, ContentManager content, Game1 game)
    {
        level = Tilemap.ReadJsonFile(filename);
        Game = game;
        _loading = false;
        enemyList = new List<IEnemy>();
        blockList = new List<IBlock>();
        playerList = new List<Player>();
        itemList = new List<IItem>();
        sceneryDict = new Dictionary<string, Sprite>();
        checkpointPositions = new List<int>();

        TextureLoader.Instance.LoadAllTextures(content, graphics, playerList, blockList, enemyList, itemList, sceneryDict);

        collision = new CollisionDistributor(80, 80, 13);
        Players = new List<Player>();
        Tiles = new List<IBlock>();
        Enemies = new List<IEnemy>();
        Items = new List<IItem>();
        Scenery = new List<KeyValuePair<Sprite, Vector2>>();

        Graphics = graphics;
        this.soundObserver = game.SE;
        Console.WriteLine("The level manager have sound observer {0}", this.soundObserver is not null);
        this.LoadObjects();
        Game.controllers.Add(new KeyboardController(Game, Players, Tiles, Enemies, Items));
        Game.controllers.Add(new GamePadController(Game, Players));
        this.soundObserver.PlayMainTheme();
        
    }

    public void LoadObjects()
    {
        // if (this.soundObserver is null) throw new NullReferenceException("SOUND OBSERVER IS NULL!");
        int width = level.tileWidth * level.tileScale;
        int height = level.tileHeight * level.tileScale;
        for (int i = 0; i < level.checkpointXPositions.Length; i++)
        {
            checkpointPositions.Add(level.checkpointXPositions[i] * width);
        }
        foreach (Tilemap.Entity entity in level.entities)
        {
            switch (entity.entityType)
            {
                case ("avatar"):
                    for (int i = 0; i < entity.positions.Length; i++)
                    {
                        Player avatar = new Player(playerList[0].Sprite, Vector2.Zero, Graphics);
                        Console.WriteLine("The sound observer is {0} at player creation", this.soundObserver is not null);
                        avatar.AttachObserver(this.soundObserver);
                        avatar.SetXPosition(entity.positions[i][0] * width);
                        avatar.SetYPosition(entity.positions[i][1] * height);
                        switch (entity.stateAtIndex[i])
                        {
                            case ("small"):
                                avatar.State.Power = IState.PowerState.Small;
                                break;
                            case ("big"):
                                avatar.State.Power = IState.PowerState.Big;
                                break;
                            case ("fire"):
                                avatar.State.Power = IState.PowerState.Fire;
                                break;
                            case ("die"):
                                avatar.State.Power = IState.PowerState.Die;
                                break;
                            default: break;
                        }
                        Debug.Write(avatar.State.Power.ToString());
                        if (!entity.facingRightAtIndex[i])
                        {
                            avatar.State.Facing = IState.FacingState.Left;
                        }
                        Players.Add(avatar);
                    }
                    break;
                case ("goomba"):
                    for (int i = 0; i < entity.positions.Length; i++)
                    {
                        Enemy enemy = new Goomba(enemyList[0].Sprite.Duplicate(), Vector2.Zero, new Camera(Graphics.Viewport));
                        enemy.AttachObserver(this.soundObserver);
                        enemy.SetXPosition(entity.positions[i][0] * width);
                        enemy.SetYPosition(entity.positions[i][1] * height);
                        if (!entity.facingRightAtIndex[i])
                        {
                            enemy.ChangeDirection();
                        }
                        Enemies.Add(enemy);
                    }
                    break;

                case ("koopa"):
                    for (int i = 0; i < entity.positions.Length; i++)
                    {
                        Enemy enemy = new GreenKoopa(enemyList[0].Sprite.Duplicate(), Vector2.Zero, new Camera(Graphics.Viewport));
                        enemy.AttachObserver(this.soundObserver);
                        enemy.positionX = entity.positions[i][0] * width;
                        enemy.positionY = entity.positions[i][1] * height;
                        if (!entity.facingRightAtIndex[i])
                        {
                            enemy.ChangeDirection();
                        }
                        Enemies.Add(enemy);
                    }
                    break;
                case ("piranha"):
                    for (int i = 0; i < entity.positions.Length; i++)
                    {
                        Enemy enemy = new Piranha(enemyList[1].Sprite.Duplicate(), Vector2.Zero, new Camera(Graphics.Viewport));
                        enemy.AttachObserver(this.soundObserver);
                        enemy.positionX = entity.positions[i][0] * width;
                        enemy.positionY = entity.positions[i][1] * height;
                        if (!entity.facingRightAtIndex[i])
                        {
                            enemy.ChangeDirection();
                        }
                        Enemies.Add(enemy);
                    }
                    break;
                default: break;
            }
        }
        foreach (Tilemap.Tile tile in level.tiles)
        {
            switch (tile.tileType)
            {
                case ("question"):
                    for (int i = 0; i < tile.positions.Length; i++)
                    {
                        Block currTile = new QuestionBlock(blockList[0].Sprite.Duplicate(), new Vector2(tile.positions[i][0] * width, tile.positions[i][1] * height));
                        currTile.AttachObserver(this.soundObserver);
                        currTile.State.BlockType = IState.BlockTypeState.Question;
                        switch (tile.itemAtIndex[i])
                        {
                            // ADD STORED ITEMS TO BLOCK HERE.
                            case ("flower"):
                                Item flower = new FireFlower(itemList[0].Sprite.Duplicate(), new Vector2(currTile.positionX, currTile.positionY));
                                flower.AttachObserver(this.soundObserver);
                                currTile.storeItem(flower);
                                Items.Add(flower);
                                break;
                            case ("mushroom"):
                                Item mushroom = new SuperMushroom(itemList[0].Sprite.Duplicate(), new Vector2(currTile.positionX, currTile.positionY));
                                mushroom.AttachObserver(this.soundObserver);
                                currTile.storeItem(mushroom);
                                Items.Add(mushroom);
                                break;
                            case ("1up"):
                                Item OneUp = new OneUpMushroom(itemList[0].Sprite.Duplicate(), new Vector2(currTile.positionX, currTile.positionY));
                                OneUp.AttachObserver(this.soundObserver);
                                currTile.storeItem(OneUp);
                                Items.Add(OneUp);
                                break;
                            case ("star"):
                                Item star = new Star(itemList[0].Sprite.Duplicate(), new Vector2(currTile.positionX, currTile.positionY));
                                star.AttachObserver(this.soundObserver);
                                currTile.storeItem(star);
                                Items.Add(star);
                                break;
                            case ("coin"):
                                Item coin = new Coin(itemList[0].Sprite.Duplicate(), new Vector2(currTile.positionX, currTile.positionY));
                                coin.AttachObserver(this.soundObserver);
                                currTile.storeItem(coin);
                                Items.Add(coin);
                                break;
                            default: break;
                        }
                        //v this can be removed, goin to wait till after tilemap is refactored
                        if (tile.hiddenAtIndex[i])
                        {
                            // ADD HIDDEN STATE HERE
                        }
                        //^
                        Tiles.Add(currTile);
                    }
                    break;
                case ("brick"):
                    for (int i = 0; i < tile.positions.Length; i++)
                    {
                        Block currTile = new BrickBlock(blockList[0].Sprite.Duplicate(), IState.BlockTypeState.Brick, new Vector2(tile.positions[i][0] * width, tile.positions[i][1] * height));
                        currTile.AttachObserver(this.soundObserver);
                        int numItems = tile.coinsAtIndex[i];
                        switch (tile.itemAtIndex[i])
                        {
                            // ADD STORED ITEMS TO BLOCK HERE.
                            case ("coin"):
                                for (int x = 0; x < numItems; x++)
                                {
                                    Item coin = new Coin(itemList[0].Sprite.Duplicate(), new Vector2(currTile.positionX, currTile.positionY));
                                    coin.AttachObserver(this.soundObserver);
                                    currTile.storeItem(coin);
                                    Items.Add(coin);
                                }
                                break;
                            default: break;
                        }
                        if (tile.hiddenAtIndex[i])
                        {
                            // ADD INVISIBILITY CODE HERE
                            currTile.State.BlockType = IState.BlockTypeState.Hidden;
                        }
                        // STORED COINS = tile.coinsAtIndex[i];
                        Tiles.Add(currTile);
                    }
                    break;
                case ("stair"):
                    for (int i = 0; i < tile.positions.Length; i++)
                    {
                        Block currTile = new StairBlock(blockList[0].Sprite.Duplicate(), new Vector2(tile.positions[i][0] * width, tile.positions[i][1] * height));
                        Tiles.Add(currTile);
                    }
                    break;
                case ("ground"):
                    for (int i = 0; i < tile.positions.Length; i++)
                    {
                        Block currTile = new GroundBlock(blockList[0].Sprite.Duplicate(), new Vector2(tile.positions[i][0] * width, tile.positions[i][1] * height));
                        Tiles.Add(currTile);
                    }
                    break;
                case ("verticalpipe"):
                    for (int i = 0; i < tile.positions.Length; i++)
                    {
                        Block currTile = new VerticalPipe(blockList[0].Sprite.Duplicate(), new Vector2(tile.positions[i][0] * width, tile.positions[i][1] * height));
                        currTile.AttachObserver(this.soundObserver);
                        if (tile.canTeleportAtIndex[i])
                        {
                            currTile.canTeleport = true;
                        }
                        Tiles.Add(currTile);
                    }
                    break;
                case ("horizontalpipe"):
                    for (int i = 0; i < tile.positions.Length; i++)
                    {
                        Block currTile = new HorizontalPipe(blockList[0].Sprite.Duplicate(), new Vector2(tile.positions[i][0] * width, tile.positions[i][1] * height));
                        currTile.AttachObserver(this.soundObserver);
                        if (tile.canTeleportAtIndex[i])
                        {
                            checkpointPositions.Add(tile.positions[i][0] * width);
                            currTile.canTeleport = true;
                        }
                        Tiles.Add(currTile);
                    }
                    break;
                default: break;
            }
        }
        foreach (Tilemap.Item item in level.items)
        {
            switch (item.itemType)
            {
                case ("mushroom"):
                    for (int i = 0; i < item.positions.Length; i++)
                    {
                        Item currItem = new SuperMushroom(itemList[0].Sprite.Duplicate(), Vector2.Zero);
                        currItem.AttachObserver(this.soundObserver);
                        currItem.positionX = item.positions[i][0] * width;
                        currItem.positionY = item.positions[i][1] * height;
                        Items.Add(currItem);
                    }
                    break;
                case ("flower"):
                    for (int i = 0; i < item.positions.Length; i++)
                    {
                        Item currItem = new FireFlower(itemList[0].Sprite.Duplicate(), Vector2.Zero);
                        currItem.AttachObserver(this.soundObserver);
                        currItem.positionX = item.positions[i][0] * width;
                        currItem.positionY = item.positions[i][1] * height;
                        Items.Add(currItem);
                    }
                    break;
                case ("1up"):
                    for (int i = 0; i < item.positions.Length; i++)
                    {
                        Item currItem = new OneUpMushroom(itemList[0].Sprite.Duplicate(), Vector2.Zero);
                        currItem.AttachObserver(this.soundObserver);
                        currItem.positionX = item.positions[i][0] * width;
                        currItem.positionY = item.positions[i][1] * height;
                        Items.Add(currItem);
                    }
                    break;
                case ("star"):
                    for (int i = 0; i < item.positions.Length; i++)
                    {
                        Item currItem = new Star(itemList[0].Sprite.Duplicate(), Vector2.Zero);
                        currItem.AttachObserver(this.soundObserver);
                        currItem.positionX = item.positions[i][0] * width;
                        currItem.positionY = item.positions[i][1] * height;
                        Items.Add(currItem);
                    }
                    break;
                case ("coin"):
                    for (int i = 0; i < item.positions.Length; i++)
                    {
                        Item currItem = new Coin(itemList[0].Sprite.Duplicate(), Vector2.Zero);
                        currItem.AttachObserver(this.soundObserver);
                        currItem.positionX = item.positions[i][0] * width;
                        currItem.positionY = item.positions[i][1] * height;
                        Items.Add(currItem);
                    }
                    break;
                case ("coinBlock"):
                    for (int i = 0; i < item.positions.Length; i++)
                    {
                        Item currItem = new CoinBlock(itemList[0].Sprite.Duplicate(), Vector2.Zero);
                        currItem.AttachObserver(this.soundObserver);
                        currItem.positionX = item.positions[i][0] * width;
                        currItem.positionY = item.positions[i][1] * height;
                        Items.Add(currItem);
                    }
                    break;
                default: break;
            }
        }
        foreach (Tilemap.Scenery scenery in level.scenery)
        {
            for (int i = 0; i < scenery.positions.Length; i++)
            {
                Sprite scenerySprite;
                sceneryDict.TryGetValue(scenery.sceneryType, out scenerySprite);
                Scenery.Add(new KeyValuePair<Sprite, Vector2>(scenerySprite, new Vector2(scenery.positions[i][0] * width, scenery.positions[i][1] * height)));
            }
        }
        collision.DistributeObjects(Players, Tiles, Enemies, Items);
    }

    public void ReloadLevel()
    {
        foreach (Player player in Players)
        {
            _previousXPosition = player.positionX;
            _previousPlayerLives = player.playerLives;
        }
        _loading = true;

        collision = new CollisionDistributor(80, 80, 13);
        collision.ClearObjects(Players, Tiles, Enemies, Items);
        Players.Clear();
        Items.Clear();
        Enemies.Clear();
        Tiles.Clear();
        checkpointPositions.Clear();
        this.soundObserver.FastReset();
        LoadObjects();
        Game.controllers = new List<IController>();
        Game.controllers.Add(new KeyboardController(Game, Players, Tiles, Enemies, Items));
        Game.controllers.Add(new GamePadController(Game, Players));
        foreach (Player player in Players)
        {
            if (player.playerLives > 0 && !Game.gameOver)
            {
                foreach (int cp in checkpointPositions)
                {
                    if (_previousXPosition >= cp)
                    {
                        player.SetXPosition(cp);
                        player.SetYPosition(3 * level.tileScale * level.tileHeight);
                    }
                }
            }
            player.playerLives = _previousPlayerLives;
        }
        if (Game.gameOver) {
            Game.gameOver = false;
            Game.paused = false;
            foreach(Player player in Players) {
                player.playerLives = 3;
            }
        }
        _loading = false;
    }

    public void UpdateLevel(GameTime gameTime, Camera camera)
    {
        if (!Game.paused && !_loading)
        {
            
            foreach (Player player in Players)
            {
                if (player.State.Equals(IState.PowerState.Die))
                {
                    ReloadLevel();
                    break;
                }
                player.Update(gameTime);

                camera.LookAt(new Vector2(player.positionX, player.positionY));
                if (player.positionX > 5625)
                {
                    camera.Limits = new Rectangle(5625, 0, 800, 480);
                }
                else
                    camera.Limits = new Rectangle(0, 0, 5600, 480);
            }
            foreach (Block tile in Tiles)
            {
                if (tile.positionX > camera.Position.X - 50 && tile.positionX < camera.Position.X + Game.GraphicsDevice.Viewport.Width)
                {
                    tile.Update(gameTime, Players[0].State.Power, Players[0].State.Facing);
                }
                    
            }
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.positionX < camera.Position.X + 800)
                    enemy.isActivated = true;
                
                if (enemy is IObserver)
                    foreach (Player player in Players) { ((IObserver)enemy).Notify(player); }

                enemy.Update(gameTime);
            }
            foreach (Item item in Items)
            {
                item.Update(gameTime);
            }
            foreach (KeyValuePair<Sprite, Vector2> pair in Scenery)
            {
                pair.Key.updateSprite(gameTime, 100);
            }
            collision.Update();
        }
    }

    public void DrawLevel(SpriteBatch spriteBatch, Camera camera)
    {
        if (!_loading && Players[0].playerLives > 0)
        {
            foreach (KeyValuePair<Sprite, Vector2> pair in Scenery)
            {
                if (pair.Key.spriteAnimation.Keys.Contains<String>("coinHUD"))
                    pair.Key.drawSprite(spriteBatch, new Vector2(camera.Position.X + 325, camera.Position.Y + 20), false, 1);

                if (pair.Key.spriteAnimation.Keys.Contains<String>("marioHUD"))
                    pair.Key.drawSprite(spriteBatch, new Vector2(camera.Position.X + 190, camera.Position.Y + 20), false, 2);
                else
                    pair.Key.drawSprite(spriteBatch, pair.Value, true, level.tileScale);
            }
            foreach (Player player in Players)
            {
                player.Draw(spriteBatch, level.tileScale);
            }
            foreach (Block tile in Tiles)
            {
                tile.Draw(spriteBatch, level.tileScale);
            }
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.positionX > camera.Position.X - 50 && enemy.positionX < camera.Position.X + Game.GraphicsDevice.Viewport.Width)
                {
                    enemy.Draw(spriteBatch, level.tileScale);
                }
                    
            }
            foreach (Item item in Items)
            {
                item.Draw(spriteBatch, level.tileScale);
            }

        }
    }

}