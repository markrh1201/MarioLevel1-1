using System;
using Microsoft.Xna.Framework;

namespace Sprint4BeanTeam
{
    public interface IPlayer : IGameObject
    {
        public bool FacingRight { get; set; }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public bool CanChangeVelocityX { get; set; }
        public bool CanChangeVelocityY { get; set; }
        public bool IsOnGround { get; set; }
        public float AccelerationX { get; set; }
        public float AccelerationY { get; set; }
        public float Gravity { get; set; }
        public float Fraction { get; set; }

        //ISprite?
        void Idle();
        void MoveRight();
        void MoveLeft();
        void Jump();
        void Dash();
        void Crouch();

        void Small();
        void Big();
        void Fire();
        void Damage();

        void SetXPosition(int x);
        void SetYPosition(int y);
    }
}

