using Microsoft.Xna.Framework;
using static Sprint4BeanTeam.IState;
namespace Sprint4BeanTeam
{
    public interface IBlock : IGameObject
    {
        public bool isBumping { get; set; }
        public bool isBreaking { get; set; }
        void Break();
        void Bump();
        void Hide();
        void storeItem(Item item);
        void Update(GameTime gameTime, PowerState state, FacingState facingState);
    }
}