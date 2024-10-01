using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class MuteCommand : ICommand
    {

        private Game1 thisGame;

        public MuteCommand(Game1 game)
        {
            thisGame = game;
        }
        public void Execute()
        {
            //likely need to establish later with sound
        }
    }
}
