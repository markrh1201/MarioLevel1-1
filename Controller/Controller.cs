using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    /// <summary>
    /// Abstract controller class: implements basic functionality
    /// </summary>
    public abstract class Controller : IController
    {
        /// <summary>
        /// The commands which translated from user input
        /// </summary>
        public List<ICommand> Command;

        /// <summary>
        /// Constructor of controller
        /// </summary>
        public Controller()
        {
            this.Command = new();
        }

        /// <summary>
        /// Execute all commands the user has inputted
        /// </summary>
        public void ExecuteCommand()
        {
            foreach (ICommand x in Command)
            {
                x.Execute();
            }
            this.Command = new();
        }

        /// <summary>
        /// Update command list using command given
        /// </summary>
        /// <param name="command">Command lists to replace with</param>
        public void SetCommand(List<ICommand> command)
        {
            this.Command.AddRange(command);
        }

        public abstract void UpdateState();
    }
}
