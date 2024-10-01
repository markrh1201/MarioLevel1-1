using static Sprint4BeanTeam.IState;
namespace Sprint4BeanTeam
{
    public class BlockState : IState
    {
        private IGameObject.ObjectType type = IGameObject.ObjectType.Block;

        public IState.BlockTypeState BlockType { get; set; }
        public IState.ItemTypeState ItemType { get; set; }
        public IState.LivingState Alive { get; set; }
        public string StateCode
        {
            get
            {
                string result = "";
                result += ((int)this.type);
                result += ((int)this.BlockType);
                //result += ((int)this.ItemType);
                return result;
            }
        }
    }

}