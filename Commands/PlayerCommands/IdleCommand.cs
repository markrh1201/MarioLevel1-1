using System;
using System.Diagnostics;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class IdleCommand : ICommand
    {
        private IPlayer thisPlayer;

        public IdleCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.Idle();
        }
    }
}