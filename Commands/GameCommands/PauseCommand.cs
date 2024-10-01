using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class PauseCommand : ICommand
    {
        private Game1 thisGame;

        public PauseCommand(Game1 game)
        {
            thisGame = game;
        }
        public void Execute()
        {
            //likely need to change this
            thisGame.paused = !thisGame.paused;
            if (thisGame.paused)
            {
                thisGame.SE.PauseMainTheme();
            }
            else thisGame.SE.PlayMainTheme();
        }
    }
}

