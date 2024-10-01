using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    public class HiddenBlockCommand : ICommand
    {
        private Block thisBlock;

        public HiddenBlockCommand(IBlock block)
        {
            thisBlock = (Block)block;
        }
        public void Execute()
        {
            if (thisBlock.State.BlockType == IState.BlockTypeState.Hidden)
            {
                thisBlock.Hide();
            }
        }
    }
}
