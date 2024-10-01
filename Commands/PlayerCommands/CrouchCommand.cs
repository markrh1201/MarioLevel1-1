using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class CrouchCommand : ICommand
    {
        private IPlayer thisPlayer;

        public CrouchCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.Crouch();
        }
    }
}

