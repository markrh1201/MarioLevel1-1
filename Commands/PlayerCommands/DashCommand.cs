using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class DashCommand : ICommand
    {
        private IPlayer thisPlayer;

        public DashCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.Dash();
        }
    }
}