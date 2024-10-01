using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    public class EnemyState : IState
    {
        private IGameObject.ObjectType type = IGameObject.ObjectType.Enemy;
        public IState.EnemyTypeState EnemyType { get; set; }
        public string StateCode
        {
            get
            {
                string result = "";
                result += ((int)this.type);
                result += ((int)this.Facing);
                result += ((int)this.EnemyType);
                return result;
            }
        }

        public IState.FacingState Facing { get; set; }
        public IState.EnemyActionState Action { get; set; }
        public IState.LivingState Alive { get; set; }

    }
}
