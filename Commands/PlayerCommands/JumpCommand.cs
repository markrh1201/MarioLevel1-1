using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class JumpCommand : ICommand
    {
        private IPlayer thisPlayer;

        public JumpCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.Jump();
        }
    }
}

