using System.Linq;

namespace Sprint4BeanTeam
{
    public class PlayerState : IState
    {
        private IGameObject.ObjectType type = IGameObject.ObjectType.Player;
        public string StateCode
        {
            get
            {
                string result = "";
                result += ((int)this.type);
                result += ((int)this.Facing);
                result += ((int)this.Power);
                result += ((int)this.Invincible);
                return result;
            }
        }

        public IState.ActionState Action { get; set; }
        public IState.FacingState Facing { get; set; }
        public IState.InvincibleState Invincible { get; set; }
        public IState.PowerState Power { get; set; }
        public IState.LivingState Alive { get; set; }

    }
}