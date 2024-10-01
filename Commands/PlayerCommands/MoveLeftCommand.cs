using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class MoveLeftCommand : ICommand
    {
        private IPlayer thisPlayer;

        public MoveLeftCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.MoveLeft();
        }
    }
}

