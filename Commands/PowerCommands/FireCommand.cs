using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class FireCommand : ICommand
    {
        private IPlayer thisPlayer;

        public FireCommand(IPlayer player)
        {
            thisPlayer = player;
        }

        public void Execute()
        {
            thisPlayer.Fire();
        }
    }
}

