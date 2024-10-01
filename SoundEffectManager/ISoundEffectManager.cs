using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Sprint4BeanTeam
{
    public interface ISoundEffectManager
    {
        public void FastReset();
        public void PlayMainTheme();
        public void PlayMainThemeAcc();
        public void PauseMainTheme();
        public void StopMainTheme();
        public void PlayOneUp();
        public void PlayBreakBlock();
        public void PlayBump();
        public void PlayCoin();
        public void PlayFireball();
        public void PlayFireWorks();
        public void PlayFlagPole();
        public void PlayGameOver();
        public void PlaySuperJump();
        public void PlayJump();
        public void PlayKick();
        public void PlayDieSE();
        public void PlayPauseSE();
        public void PlayPipeSE();
        public void PlayPowerUpAppear();
        public void PlayPowerUp();
        public void PlayStageClear();
        public void PlayStomp();
        public void PlayWarning();

        /// <summary>
        /// This method follows Observer Pattern
        /// </summary>
        /// <param name="gameObject">The in take game object to react</param>
        public void Update(IGameObject gameObject);
    }
}
