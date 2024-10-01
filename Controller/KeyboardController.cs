using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint4BeanTeam
{
    /// <summary>
    /// The Keyboard class implementation of controller
    /// </summary>
    public class KeyboardController : Controller
    {
        /// <summary>
        /// The receiver of commands to take on
        /// </summary>
        private Game1 gameReceiver;

        private List<Player> playerReceiver;
        private List<IBlock> blockReceiver;
        private List<IEnemy> enemyReceiver;
        private List<IItem> itemReceiver;

        private bool previousIsMoving;

        /// <summary>
        /// The previous key state
        /// </summary>
        private KeyboardState previousState;

        /// <summary>
        /// The constructor of keyboard controller
        /// </summary>
        /// <param name="receiver">Receiver this controller to work on</param>
        public KeyboardController(Game1 GameReceiver, List<Player> PlayerReceiver, List<IBlock> blockReceiver, List<IEnemy> enemyReceiver, List<IItem> itemReceiver)
        {
            this.previousState = Keyboard.GetState();
            this.gameReceiver = GameReceiver;
            this.playerReceiver = new();
            this.blockReceiver = new();
            this.enemyReceiver = new();
            this.itemReceiver = new();
            this.playerReceiver.AddRange(PlayerReceiver);
            this.blockReceiver.AddRange(blockReceiver);
            this.enemyReceiver.AddRange(enemyReceiver);
            this.itemReceiver.AddRange(itemReceiver);
            this.previousIsMoving = false;
        }

        public override void UpdateState()
        {
            KeyboardState currentState = Keyboard.GetState();
            List<ICommand> commands = new();
            //This block is used to manually control long press. Long press should not interfere
            //Assume at least 1 player - the key of player1 is determined.
            bool isMoving = false;
            bool keyDirectionConflict = false;
            //Jump
            if (currentState.IsKeyDown(Keys.W) || currentState.IsKeyDown(Keys.Up))
            {
                keyDirectionConflict = keyDirectionConflict || (currentState.IsKeyDown(Keys.S) || currentState.IsKeyDown(Keys.Down));
                if (!keyDirectionConflict)
                {
                    commands.Add(new JumpCommand(playerReceiver[0]));
                    isMoving = true;
                }
            }
            //Crouch
            if (currentState.IsKeyDown(Keys.S) || currentState.IsKeyDown(Keys.Down))
            {
                keyDirectionConflict = keyDirectionConflict || (currentState.IsKeyDown(Keys.W) || currentState.IsKeyDown(Keys.Up));
                if (!keyDirectionConflict)
                {
                    commands.Add(new CrouchCommand(playerReceiver[0]));
                    isMoving = true;
                }
            }
            //Left move
            if ((currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Left)) && !gameReceiver.paused)
            {
                keyDirectionConflict = keyDirectionConflict || (currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Right));
                if (!keyDirectionConflict)
                {
                    commands.Add(new MoveLeftCommand(playerReceiver[0]));
                    isMoving = true;
                }
            }
            //Right move
            if ((currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Right)) && !gameReceiver.paused)
            {
                keyDirectionConflict = keyDirectionConflict || (currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Left));
                if (!keyDirectionConflict)
                {
                    commands.Add(new MoveRightCommand(playerReceiver[0]));
                    isMoving = true;
                }
            }
            // Pause/Resume
            if (currentState.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P) && !gameReceiver.gameOver) commands.Add(new PauseCommand(gameReceiver));
            // Force Restart
            if (currentState.IsKeyDown(Keys.R) && !previousState.IsKeyDown(Keys.R) && (!gameReceiver.paused || gameReceiver.gameOver)) commands.Add(new RestartCommand(gameReceiver));

            if ((previousIsMoving && !isMoving) || keyDirectionConflict)
            {
                commands.Add(new IdleCommand(playerReceiver[0]));
            }
            previousIsMoving = isMoving;

            Keys[] pressedKeys = currentState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (!this.previousState.IsKeyDown(key))
                {
                    if (this.playerReceiver == null || key == Keys.Q)
                    {
                        commands.Add(new QuitCommand(this.gameReceiver));
                    }
                    else if (key == Keys.M)
                    {
                        commands.Add(new MuteSoundCommand(this.gameReceiver.SE));
                    }
                    else if (!gameReceiver.paused)
                    {
                        foreach (IPlayer player in this.playerReceiver) switch (key)
                            {
                                case Keys.Y:
                                    commands.Add(new SmallCommand(player));
                                    break;
                                case Keys.U:
                                    commands.Add(new BigCommand(player));
                                    break;
                                case Keys.I:
                                    commands.Add(new FireCommand(player));
                                    break;
                                case Keys.O:
                                    commands.Add(new DamageCommand(player));
                                    break;
                                case Keys.C:
                                    commands.Add(new ShowCollisionCommand(player));
                                    break;
                            }
                    }
                    foreach (IBlock block in this.blockReceiver) switch (key)
                        {
                            case Keys.H:
                                commands.Add(new HiddenBlockCommand(block));
                                break;
                            case Keys.B:
                                commands.Add(new BumpBlockCommand(block));
                                break;
                            case Keys.OemQuestion:
                                commands.Add(new QuestionBlockCommand(block));
                                break;
                            case Keys.C:
                                commands.Add(new ShowCollisionCommand(block));
                                break;
                        }
                    foreach (IEnemy enemy in this.enemyReceiver) switch (key)
                        {
                            case Keys.C:
                                commands.Add(new ShowCollisionCommand(enemy));
                                break;
                        }
                    foreach (IItem item in this.itemReceiver) switch (key)
                        {
                            case Keys.C:
                                commands.Add(new ShowCollisionCommand(item));
                                break;
                        }
                }
            }
            this.SetCommand(commands);
            this.previousState = currentState;
            this.ExecuteCommand();
        }
    }
}