namespace Sprint4BeanTeam
{
    public interface IState
    {
        enum ActionState
        {
            Crouch, Idle, Run, Jump
        }

        enum PowerState
        {
            Small, Big, Fire, Die
        }

        enum FacingState
        {
            Left, Right
        }
        enum InvincibleState
        {
            Normal, Invincible
        }

        enum BlockTypeState
        {
            Ground, Brick, Hidden, Empty, Stair, Question, Break, VerticalPipe, HorizontalPipe
        }

        enum ItemTypeState
        {
            None, SuperMushroom, Coin, StarMan, OneUpMushroom, FireFlower, CoinBlock, FireBall
        }

        enum EnemyTypeState
        {
            None, Goomba, GreenKoopa, Piranha
        }

        enum EnemyActionState
        {
            Die, Normal, Shell
        }

        enum LivingState
        {
            Dead, Living
        }

        string StateCode { get; }
    }
}