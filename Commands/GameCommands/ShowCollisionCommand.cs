using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class ShowCollisionCommand : ICommand
    {
        private IGameObject thisGameObject;

        public ShowCollisionCommand(IGameObject gameObject)
        {
            thisGameObject = gameObject;
        }
        public void Execute()
        {
            thisGameObject.showCollision();
        }
    }
}