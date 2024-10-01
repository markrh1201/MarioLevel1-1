using System;
using System.Net.Mime;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Sprint4BeanTeam.State;

namespace Sprint4BeanTeam
{
    public class SoundEffectManager : ISoundEffectManager
    {
        public enum MainThemeStateEnum { Ready, Playing, Paused };
        public bool IsAccelerated { get; set; }
        public MainThemeStateEnum MainThemeState;
        private Song mainTheme;
        private Song warningSE;
        private Song mainThemeAcc;
        private SoundEffectInstance oneUpInstance;
        private SoundEffectInstance bumpInstance;
        private SoundEffectInstance breakBlockInstance;
        private SoundEffectInstance coinInstance;
        private SoundEffectInstance fireballInstance;
        private SoundEffectInstance fireworksInstance;
        private SoundEffectInstance flagpoleInstance;
        private SoundEffectInstance gameOverInstance;
        private SoundEffectInstance superJumpInstance;
        private SoundEffectInstance jumpInstance;
        private SoundEffectInstance kickInstance;
        private SoundEffectInstance marioDieSE;
        private SoundEffectInstance pauseInstance;
        private SoundEffectInstance pipeInstance;
        private SoundEffectInstance powerUpAppearInstance;
        private SoundEffectInstance powerUpInstance;
        private SoundEffectInstance stageClearInstance;
        private SoundEffectInstance stompInstance;
        private SoundEffectInstance warningInstance;
        public bool Muted{ get; set; }
        public SoundEffectManager(ContentManager content)
        {
            this.mainTheme = content.Load<Song>(@"SoundEffects\mario_sound_track");
            this.mainThemeAcc = content.Load<Song>(@"SoundEffects\mario_sound_track_acc");
            this.warningSE = content.Load<Song>(@"SoundEffects\smb_warning_song");
            this.MainThemeState = MainThemeStateEnum.Ready;
            this.oneUpInstance = content.Load<SoundEffect>(@"SoundEffects\smb_1-up").CreateInstance();
            this.bumpInstance = content.Load<SoundEffect>(@"SoundEffects\smb_bump").CreateInstance();
            this.breakBlockInstance = content.Load<SoundEffect>(@"SoundEffects\smb_breakblock").CreateInstance();
            this.coinInstance = content.Load<SoundEffect>(@"SoundEffects\smb_coin").CreateInstance();
            this.fireballInstance = content.Load<SoundEffect>(@"SoundEffects\smb_fireball").CreateInstance();
            this.fireworksInstance = content.Load<SoundEffect>(@"SoundEffects\smb_fireworks").CreateInstance();
            this.flagpoleInstance = content.Load<SoundEffect>(@"SoundEffects\smb_flagpole").CreateInstance();
            this.gameOverInstance = content.Load<SoundEffect>(@"SoundEffects\smb_gameover").CreateInstance();
            this.superJumpInstance = content.Load<SoundEffect>(@"SoundEffects\smb_jump-super").CreateInstance();
            this.jumpInstance = content.Load<SoundEffect>(@"SoundEffects\smb_jumpsmall").CreateInstance();
            this.kickInstance = content.Load<SoundEffect>(@"SoundEffects\smb_kick").CreateInstance();
            this.marioDieSE = content.Load<SoundEffect>(@"SoundEffects\smb_mariodie").CreateInstance();
            this.pauseInstance = content.Load<SoundEffect>(@"SoundEffects\smb_pause").CreateInstance();
            this.pipeInstance = content.Load<SoundEffect>(@"SoundEffects\smb_pipe").CreateInstance();
            this.powerUpAppearInstance = content.Load<SoundEffect>(@"SoundEffects\smb_powerup_appears").CreateInstance();
            this.powerUpInstance = content.Load<SoundEffect>(@"SoundEffects\smb_powerup").CreateInstance();
            this.stageClearInstance = content.Load<SoundEffect>(@"SoundEffects\smb_stage_clear").CreateInstance();
            this.stompInstance = content.Load<SoundEffect>(@"SoundEffects\smb_stomp").CreateInstance();
            this.warningInstance = content.Load<SoundEffect>(@"SoundEffects\smb_warning").CreateInstance();

            this.Muted = false;
        }

        public void FastReset()
        {
            this.PlayMainTheme();
        }

        public void MuteOrReverse()
        {
            this.Muted = !this.Muted;
            MediaPlayer.IsMuted = this.Muted;
        }

        public void PauseMainTheme()
        {
            if (!Muted) this.pauseInstance.Play();
            MediaPlayer.Pause();
            this.MainThemeState = MainThemeStateEnum.Paused;
        }

        public void PlayBreakBlock()
        {
            if (!Muted) this.breakBlockInstance.Play();
        }

        public void PlayBump()
        {
            if (!Muted) this.bumpInstance.Play();
        }

        public void PlayCoin()
        {
            if (!Muted) this.coinInstance.Play();
        }

        public void PlayDieSE()
        {
            if (!Muted) this.marioDieSE.Play();
        }

        public void PlayFireball()
        {
            if (!Muted) this.fireballInstance.Play();
        }

        public void PlayFireWorks()
        {
            if (!Muted) this.fireworksInstance.Play();
        }

        public void PlayFlagPole()
        {
            if (!Muted) this.flagpoleInstance.Play();
        }

        public void PlayGameOver()
        {
            if (!Muted) this.gameOverInstance.Play();
        }

        public void PlayJump()
        {
            if (!Muted) this.jumpInstance.Play();
        }

        public void PlayKick()
        {
            if (!Muted) this.kickInstance.Play();
        }

        public void PlayMainTheme()
        {
            MediaPlayer.IsRepeating = true;
            if (this.MainThemeState == MainThemeStateEnum.Ready)
                MediaPlayer.Play(this.mainTheme);
            else MediaPlayer.Resume();
        }

        public void PlayMainThemeAcc()
        {
            this.IsAccelerated = true;
            // MediaPlayer.IsRepeating = false;
            // MediaPlayer.Play(this.warningSE);
            // MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;
            if (this.MainThemeState == MainThemeStateEnum.Ready)
                MediaPlayer.Play(this.mainThemeAcc);
            else MediaPlayer.Resume();
        }

        public void PlayOneUp()
        {
            if (!Muted) this.oneUpInstance.Play();
        }

        public void PlayPauseSE()
        {
            if (!Muted) this.pauseInstance.Play();
        }

        public void PlayPipeSE()
        {
            if (!Muted) this.pipeInstance.Play();
        }

        public void PlayPowerUp()
        {
            if (!Muted) this.powerUpInstance.Play();
        }

        public void PlayPowerUpAppear()
        {
            if (!Muted) this.powerUpAppearInstance.Play();
        }

        public void PlayStageClear()
        {
            if (!Muted) this.stageClearInstance.Play();
            MediaPlayer.Pause();
            this.MainThemeState = MainThemeStateEnum.Paused;
        }

        public void PlayStomp()
        {
            if (!Muted) this.stompInstance.Play();
        }

        public void PlaySuperJump()
        {
            if (!Muted) this.superJumpInstance.Play();
        }

        public void PlayWarning()
        {
            if (!Muted) this.warningInstance.Play();
        }

        public void StopMainTheme()
        {
            MediaPlayer.Pause();
            this.MainThemeState = MainThemeStateEnum.Ready;
        }

        public void Update(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case Player:
                    Console.WriteLine("{0} received notification: Subject state is {1}", this.GetType(), ((Player)gameObject).State);
                    this.HandlePlayerSound(((Player)gameObject).State,(gameObject as Player).levelComplete, (gameObject as Player).warp,(gameObject as Player).previousIsWarned!=(gameObject as Player).isWarned);
                    break;
                case Item:
                    Console.WriteLine("{0} received notification: Subject state is {1}", this.GetType(), ((Item)gameObject).State);
                    this.HandleItemSound(((Item)gameObject).State,(gameObject as Item).isRevealed);
                    break;
                case Block:
                    Console.WriteLine("{0} received notification: Subject state is {1}", this.GetType(), ((Block)gameObject).State);
                    this.HandleBlockSound(((Block)gameObject).State, (gameObject as Block).isBreaking,(gameObject as Block).isBumping);
                    break;
                case Enemy:
                    Console.WriteLine("{0} received notification: Subject state is {1}", this.GetType(), ((Enemy)gameObject).State);
                    Console.WriteLine("An enemy state change is received");
                    this.HandleEnemySound((gameObject as Enemy).State);
                    break;
            }
            // Console.WriteLine("{0} received notification: Subject state is {1}", this.GetType(), gameObject.GetType());
        }

        public void HandlePlayerSound(PlayerState state,bool completeLevel, bool warp, bool warnPlayer)
        {
            if (!completeLevel){if (state.Power != IState.PowerState.Die) switch(state.Action)
            {
                case IState.ActionState.Jump:
                    if (state.Power == IState.PowerState.Small)this.PlayJump();
                    else this.PlaySuperJump();
                    break;
                default:
                    if (warp) this.PlayPipeSE();
                    if (warnPlayer) this.PlayMainThemeAcc();
                            break;
            }
            switch(state.Power)
            {
                case IState.PowerState.Die:
                    this.PlayDieSE();
                    this.StopMainTheme();
                    break;
            }}
            else this.PlayStageClear();
        }

        public void HandleItemSound(ItemState state, bool isRevealed)
        {
            switch (state.ItemType)
            {
                case IState.ItemTypeState.Coin:
                if (state.Alive == IState.LivingState.Dead) this.PlayCoin();
                Console.WriteLine("A Coin state change is received");
                    break;
                case IState.ItemTypeState.OneUpMushroom:
                    if (state.Alive == IState.LivingState.Dead) this.PlayOneUp();
                    Console.WriteLine("A OneUp state change is received");
                    break;
                case IState.ItemTypeState.SuperMushroom:
                    if (isRevealed && state.Alive == IState.LivingState.Living) this.PlayPowerUpAppear();
                    else if (state.Alive == IState.LivingState.Dead) this.PlayPowerUp();
                    Console.WriteLine("A OneUp state change is received");
                    break;
            }
        }

        public void HandleBlockSound(BlockState state, bool isBreaking, bool isBumping)
        {
            switch(state.BlockType)
            {
                case IState.BlockTypeState.Question:
                case IState.BlockTypeState.Brick:
                Console.WriteLine("A Block state change is received");
                if (isBumping) this.PlayBump();
                else if (isBreaking) this.PlayBreakBlock();
                    break;
            }
        }

        public void HandleEnemySound(EnemyState state)
        {
            if (state.Action != IState.EnemyActionState.Normal) 
            {
                Console.WriteLine("Stomp instance is played");
                this.PlayStomp();
            }
        }
    }
}