using System;
using System.Collections.Generic;
using GameCamera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4BeanTeam;
using Sprint4BeanTeam.GameObject.Enemies;
using Sprint4BeanTeam.State;
using static Sprint4BeanTeam.IState;

namespace Sprint4BeanTeam
{
    public class Goomba : Enemy
    {

        public Goomba(Sprite enemySprite, Vector2 position, Camera cam) : base(enemySprite, IState.EnemyTypeState.Goomba, position, cam)
        {
            moveSpeed = 5f;
        }

        public override string AnimationName()
        {
            string result = "enemyGoomba";
            result += this.State.Action.ToString();
            return result;
        }

    }
}

