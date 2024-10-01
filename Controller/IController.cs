using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    /// <summary>
    /// This is the interface of controllers that provide general control over different input devices
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// This defines the accepted input type of Controller
        /// </summary>
        public enum ControllerType
        {
            [Description("Undefined")]
            Undefined,
            [Description("Keyboard")]
            Keyboard,
            [Description("GamePad")]
            GamePad,
        }

        /// <summary>
        /// This 
        /// </summary>
        /// <returns></returns>
        public string ToString();

        /// <summary>
        /// After the key is released, but all keys as commands and send for execution
        /// </summary>
        /// <param name="command"></param>
        public void SetCommand(List<ICommand> command);

        /// <summary>
        /// Execute all commands stored
        /// </summary>
        public void ExecuteCommand();

        /// <summary>
        /// Update the states of controller, and translate keypress to commands
        /// </summary>
        public void UpdateState();
    }
}
