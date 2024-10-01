using Microsoft.Xna.Framework;
using Sprint4BeanTeam.State;
using System;
namespace Sprint4BeanTeam
{
    public interface IEnemy : IGameObject
    {
        void ChangeDirection();
        void DieTransition();
        void SetXPosition(int x);
        void SetYPosition(int y);
        void MoveRight();
        void MoveLeft();
    }
}
