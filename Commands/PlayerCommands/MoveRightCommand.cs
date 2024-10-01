using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class MoveRightCommand : ICommand
    {
        private IPlayer thisPlayer;

        public MoveRightCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.MoveRight();
        }
    }
}

