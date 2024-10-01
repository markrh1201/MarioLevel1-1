namespace Sprint4BeanTeam
{
    public class QuestionBlockCommand : ICommand
    {
        private Block thisBlock;

        public QuestionBlockCommand(IBlock block)
        {
            thisBlock = (Block)block;
        }
        public void Execute()
        {
            if (thisBlock.State.BlockType == IState.BlockTypeState.Question || thisBlock.State.BlockType == IState.BlockTypeState.Empty)
            {
                thisBlock.Bump();
            }
        }
    }
}