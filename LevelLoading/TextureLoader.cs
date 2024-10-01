using GameCamera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;
using Sprint4BeanTeam.GameObject.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    public class TextureLoader
    {
        private Texture2D mario;
        private Texture2D blocks;
        private Texture2D enemies;
        private Texture2D items;
        private Texture2D scenery;
        private Texture2D piranha;
        private Vector2 position;
        private Camera cam;

        private static readonly TextureLoader instance = new TextureLoader();
        public static TextureLoader Instance
        {
            get
            {
                return instance;
            }
        }

        public TextureLoader()
        {

        }

        public void LoadAllTextures(ContentManager content, GraphicsDevice graphics, List<Player> playerList, List<IBlock> blockList, List<IEnemy> enemyList, List<IItem> itemList, Dictionary<string, Sprite> sceneryDict)
        {
            mario = content.Load<Texture2D>("mariosprsht");
            blocks = content.Load<Texture2D>("tileSpritesheet");
            enemies = content.Load<Texture2D>("enemysprsht");
            items = content.Load<Texture2D>("mario_items");
            piranha = content.Load<Texture2D>("PiranhaSprt");
            cam = new Camera(graphics.Viewport);

            Sprite marioSprite = new Sprite(mario, 3, 8);
            Sprite blockSprite = new Sprite(blocks, 6, 3);
            Sprite enemySprite = new Sprite(enemies, 4, 6);
            Sprite itemSprite = new Sprite(items, 1, 17);
            Sprite piranhaSprite = new Sprite(piranha, 1, 2);

            marioSprite.addAnimation("marioRunSmall", 5, 4, 3);
            marioSprite.addAnimation("marioIdleSmall", 6);
            marioSprite.addAnimation("marioJumpSmall", 1);
            marioSprite.addAnimation("marioCrouchSmall", 6);
            marioSprite.addAnimation("marioDashSmall", 2);

            marioSprite.addAnimation("marioRunBig", 13, 12, 11);
            marioSprite.addAnimation("marioIdleBig", 14);
            marioSprite.addAnimation("marioJumpBig", 9);
            marioSprite.addAnimation("marioCrouchBig", 8);
            marioSprite.addAnimation("marioDashBig", 10);

            marioSprite.addAnimation("marioRunFire", 22, 21, 20);
            marioSprite.addAnimation("marioIdleFire", 23);
            marioSprite.addAnimation("marioJumpFire", 17);
            marioSprite.addAnimation("marioCrouchFire", 16);
            marioSprite.addAnimation("marioDashFire", 19);

            marioSprite.addAnimation("marioIdleDie", 0);
            marioSprite.addAnimation("marioRunDie", 0);
            marioSprite.addAnimation("marioJumpDie", 0);
            marioSprite.addAnimation("marioCrouchDie", 0);
            marioSprite.addAnimation("marioDashDie", 0);

            blockSprite.addAnimation("blockGround", 0);
            blockSprite.addAnimation("blockBrick", 1);
            blockSprite.addAnimation("blockEmpty", 3);
            blockSprite.addAnimation("blockBreak", 4);
            blockSprite.addAnimation("blockStair", 5);
            blockSprite.addAnimation("blockQuestion", 7, 8, 9);
            blockSprite.addAnimation("blockHidden", 12);
            blockSprite.addAnimation("blockVerticalPipe", 10);
            blockSprite.addAnimation("blockHorizontalPipe", 11);

            enemySprite.addAnimation("enemyGoombaNormal", 0, 1);
            enemySprite.addAnimation("enemyGoombaDie", 2);
            enemySprite.addAnimation("enemyGreenKoopaNormal", 5, 6);
            enemySprite.addAnimation("enemyGreenKoopaShell", 12);

            piranhaSprite.addAnimation("enemyPiranhaNormal", 0, 1);

            itemSprite.addAnimation("itemSuperMushroom", 0);
            itemSprite.addAnimation("itemFireFlower", 2, 3, 4, 5);
            itemSprite.addAnimation("itemOneUpMushroom", 1);
            itemSprite.addAnimation("itemStarMan", 6, 7, 8, 9);
            itemSprite.addAnimation("itemCoin", 14, 15, 16);
            itemSprite.addAnimation("itemCoinBlock", 10, 11, 12, 13);

            position = new Vector2(100, 100);

            playerList.Add(new Player(marioSprite, position, graphics));

            enemyList.Add(new Enemy(enemySprite, IState.EnemyTypeState.None, position, cam));
            enemyList.Add(new Enemy(piranhaSprite, IState.EnemyTypeState.Piranha, position, cam));

            blockList.Add(new Block(blockSprite, IState.BlockTypeState.Empty, position));

            itemList.Add(new Item(itemSprite, IState.ItemTypeState.None, position));

            scenery = content.Load<Texture2D>("smbcastle");
            Sprite scenerySprite = new Sprite(scenery, 1, 1);
            scenerySprite.addAnimation("default", 0);
            scenerySprite.changeCurrentAnimation("default");
            sceneryDict.Add("castle", scenerySprite);

            scenery = content.Load<Texture2D>("flag");
            scenerySprite = new Sprite(scenery, 1, 1);
            scenerySprite.addAnimation("default", 0);
            scenerySprite.changeCurrentAnimation("default");
            sceneryDict.Add("flag", scenerySprite);


            scenery = content.Load<Texture2D>("coinHUD");
            Sprite coinHUD = new Sprite(scenery, 1, 1);
            coinHUD.addAnimation("coinHUD", 0);
            coinHUD.changeCurrentAnimation("coinHUD");
            sceneryDict.Add("coinHUD", coinHUD);

            scenery = content.Load<Texture2D>("marioHUD");
            Sprite marioHUD = new Sprite(scenery, 1, 1);
            marioHUD.addAnimation("marioHUD", 0);
            marioHUD.changeCurrentAnimation("marioHUD");
            sceneryDict.Add("marioHUD", marioHUD);
        }

    }
}