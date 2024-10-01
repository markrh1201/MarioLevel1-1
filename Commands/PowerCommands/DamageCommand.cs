using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class DamageCommand : ICommand
    {
        private IPlayer thisPlayer;

        public DamageCommand(IPlayer player)
        {
            thisPlayer = player;
        }
        public void Execute()
        {
            thisPlayer.Damage();
        }
    }
}

