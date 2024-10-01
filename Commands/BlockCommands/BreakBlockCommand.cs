using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    public class BreakBlockCommand : ICommand
    {
        private IBlock thisBlock;

        public BreakBlockCommand(IBlock block)
        {
            thisBlock = block;
        }
        public void Execute()
        {
            thisBlock.Break();
        }
    }
}
