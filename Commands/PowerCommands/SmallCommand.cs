using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class SmallCommand : ICommand
    {
        private IPlayer thisPlayer;

        public SmallCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.Small();
        }
    }
}

