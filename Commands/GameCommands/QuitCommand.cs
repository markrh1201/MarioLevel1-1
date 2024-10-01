using System;
using Sprint4BeanTeam;

namespace Sprint4BeanTeam
{
    public class QuitCommand : ICommand
    {

        private Game1 thisGame;

        public QuitCommand(Game1 game)
        {
            thisGame = game;
        }
        public void Execute()
        {
            thisGame.Exit();
        }
    }
}

