using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class RestartCommand : ICommand
    {

        private Game1 thisGame;

        public RestartCommand(Game1 game)
        {
            thisGame = game;
        }
        public void Execute()
        {
            thisGame._level.ReloadLevel();
        }
    }
}

