using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public interface IGameObject
    {
        void AttachObserver(ISoundEffectManager soundObserver);
        void DetachObserver(ISoundEffectManager soundObserver);
        void Notify();
        enum ObjectType
        {
            Player, Enemy, Block, Item
        }

        Sprite Sprite { get; set; }

        int positionX { get; set; }
        int positionY { get; set; }

        Color CollisionBoxColor { get; set; }

        HashSet<IGameObject> getNearbyObjects();
        void Draw(SpriteBatch batch, int scale = 1);
        void Update(GameTime gameTime);

        void updateCell(Cell newCell);

        void showCollision();
        void updateCollision();
        void collisionColor(Color color);
        Rectangle getCollisionBox();
    }
}