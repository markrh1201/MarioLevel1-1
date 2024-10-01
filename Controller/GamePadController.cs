using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint4BeanTeam
{
    /// <summary>
    /// The GamePad Controller implementation
    /// </summary>
    public class GamePadController : Controller
    {
        /// <summary>
        /// Receiver to take effect on
        /// </summary>
        private Game1 gameReceiver;

        private Player[] playerReceiver;
        private bool[] previousIsMoving;

        /// <summary>
        /// The previous button press
        /// </summary>
        private GamePadState[] previousState;
        /// <summary>
        /// Constructor of Game Pad controller
        /// </summary>
        /// <param name="receiver">The receiver of the commands input to take on</param>
        public GamePadController(Game1 GameReceiver, List<Player> PlayerReceiver)
        {
            this.previousState = new GamePadState[4];
            this.gameReceiver = GameReceiver;
            this.playerReceiver = new Player[4];
            this.previousIsMoving = new bool[4];
            for (int i = 0; i < PlayerReceiver.Count && i < 4; i++)
            {
                this.playerReceiver[i] = PlayerReceiver[i];
                this.previousState[i] = GamePad.GetState(i);
                this.previousIsMoving[i] = false;
            }
        }

        public override void UpdateState()
        {
            List<ICommand> commands = new();
            for (int i = 0; i < this.playerReceiver.Length; i++)
            {
                GamePadState currentState = GamePad.GetState(i);

                if (currentState.IsButtonDown(Buttons.Start))
                {
                    commands.Add(new QuitCommand(this.gameReceiver));
                }

                bool isMoving = false;
                bool keyDirectionConflict = false;
                //Jump
                if (currentState.IsButtonDown(Buttons.DPadUp))
                {
                    keyDirectionConflict = keyDirectionConflict || currentState.IsButtonDown(Buttons.DPadDown);
                    if (!keyDirectionConflict)
                    {
                        commands.Add(new JumpCommand(playerReceiver[0]));
                        isMoving = true;
                    }

                }
                //Crouch
                if (currentState.IsButtonDown(Buttons.DPadDown))
                {
                    keyDirectionConflict = keyDirectionConflict || currentState.IsButtonDown(Buttons.DPadUp);
                    if (!keyDirectionConflict)
                    {
                        commands.Add(new CrouchCommand(playerReceiver[0]));
                        isMoving = true;
                    }
                }
                //Left move
                if (currentState.IsButtonDown(Buttons.DPadLeft) && !gameReceiver.paused)
                {
                    keyDirectionConflict = keyDirectionConflict || currentState.IsButtonDown(Buttons.DPadRight);
                    if (!keyDirectionConflict)
                    {
                        commands.Add(new MoveLeftCommand(playerReceiver[0]));
                        isMoving = true;
                    }
                }
                //Right move
                if (currentState.IsButtonDown(Buttons.DPadRight) && !gameReceiver.paused)
                {
                    keyDirectionConflict = keyDirectionConflict || currentState.IsButtonDown(Buttons.DPadLeft);
                    if (!keyDirectionConflict)
                    {
                        commands.Add(new MoveRightCommand(playerReceiver[0]));
                        isMoving = true;
                    }

                }
                // Pause/Resume
                if (currentState.IsButtonDown(Buttons.Start)) gameReceiver.paused = !gameReceiver.paused;
                if ((previousIsMoving[i] && !isMoving) || keyDirectionConflict)
                {
                    commands.Add(new IdleCommand(playerReceiver[0]));
                }
                previousIsMoving[i] = isMoving;
            }
            this.SetCommand(commands);
            this.ExecuteCommand();
        }
    }
}