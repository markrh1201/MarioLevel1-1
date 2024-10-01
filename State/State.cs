using Sprint4BeanTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam.State
{
    public class State : IState
    {
        private IGameObject.ObjectType type = IGameObject.ObjectType.Item;
        public IState.ItemTypeState ItemType { get; set; }
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
