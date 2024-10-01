using Microsoft.Xna.Framework;
using Sprint4BeanTeam.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public interface IItem : IGameObject
    {
        void ChangeDirection();
        void Consume();
        void SetXPosition(int x);
        void SetYPosition(int y);
        void collideOn(bool on);
    }
}
