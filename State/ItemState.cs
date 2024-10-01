using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam.State
{
    public class ItemState : IState
    {
        private IGameObject.ObjectType type = IGameObject.ObjectType.Item;
        public IState.ItemTypeState ItemType { get; set; }

        public IState.LivingState Alive { get; set; }
        public string StateCode
        {
            get
            {
                string result = "";
                result += ((int)this.type);
                result += ((int)this.ItemType);
                return result;
            }
        }
    }
}
