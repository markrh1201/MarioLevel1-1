using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class BigCommand : ICommand
    {
        private IPlayer thisPlayer;

        public BigCommand(IPlayer player)
        {
            thisPlayer = player;
        }

        public void Execute()
        {
            thisPlayer.Big();
        }
    }
}

