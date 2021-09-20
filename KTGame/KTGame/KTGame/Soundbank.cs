using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGame
{
    public class Soundbank
    {
        ContentManager Content;

        private Song BackgroundMusic;
        private string SongName;
        private bool Muted = false;

        SoundEffectInstance SoundEffect;


        private SoundEffect StandardJump;
        private SoundEffect SuperJump;
        private SoundEffect Stomp;
        private SoundEffect Coin;
        private SoundEffect PowerUpAppears;
        private SoundEffect PowerUpCollect;
        private SoundEffect OneUpCollect;
        private SoundEffect Flag;

        private SoundEffect BrickBump;
        private SoundEffect BrickBreak;
        private SoundEffect PipeTravel;
        private SoundEffect TimeWarning;
        private SoundEffect GameOver;
        private SoundEffect Fireball;
        private SoundEffect Die;

        public bool Hurry { get; set; }




        public Soundbank(ContentManager content)
        {
            Content = content;
            LoadContent();
            Hurry = false;
            SongName = "overworld";
        }

        public void PlayBackgroundMusic()
        {
            if (!Hurry || SongName.Equals("victory"))
                BackgroundMusic = Content.Load<Song>("Sounds/" + SongName);
            else
                BackgroundMusic = Content.Load<Song>("Sounds/hurry-" + SongName);

            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
        }

        public void PlayBackgroundMusicNonRepeat()
        {
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.1f;
        }


        public void MuteCommand()
        {
            Muted = !MediaPlayer.IsMuted;
            MediaPlayer.IsMuted = Muted;
        }

        public void LoadContent()
        {
            BackgroundMusic = Content.Load<Song>("Sounds/overworld");

            StandardJump = Content.Load<SoundEffect>("Sounds/smb_jump-small");
            SuperJump = Content.Load<SoundEffect>("Sounds/smb_jump-super");
            Stomp = Content.Load<SoundEffect>("Sounds/smb_stomp");
            Coin = Content.Load<SoundEffect>("Sounds/smb_coin");
            PowerUpAppears = Content.Load<SoundEffect>("Sounds/smb_powerup_appears");
            PowerUpCollect = Content.Load<SoundEffect>("Sounds/smb_powerup");
            OneUpCollect = Content.Load<SoundEffect>("Sounds/smb_1-up");
            Flag = Content.Load<SoundEffect>("Sounds/smb_flagpole");

            BrickBump = Content.Load<SoundEffect>("Sounds/smb_bump");
            BrickBreak = Content.Load<SoundEffect>("Sounds/smb_breakblock");
            PipeTravel = Content.Load<SoundEffect>("Sounds/smb_pipe");
            TimeWarning = Content.Load<SoundEffect>("Sounds/smb_warning");
            GameOver = Content.Load<SoundEffect>("Sounds/smb_gameover");
            Fireball = Content.Load<SoundEffect>("Sounds/smb_fireball");
            Die = Content.Load<SoundEffect>("Sounds/smb_mariodie");
        }

        public void PlaySound(String eventName)
        {
            if (!MediaPlayer.IsMuted)
            {
                switch (eventName)
                {
                    case "StandardJump":
                        SoundEffect = StandardJump.CreateInstance();
                        break;

                    case "SuperJump":
                        SoundEffect = SuperJump.CreateInstance();
                        break;

                    case "Stomp":
                        SoundEffect = Stomp.CreateInstance();
                        break;

                    case "Coin":
                        SoundEffect = Coin.CreateInstance();
                        break;

                    case "PowerUpAppears":
                        SoundEffect = PowerUpAppears.CreateInstance();
                        break;

                    case "PowerUpCollect":
                        SoundEffect = PowerUpCollect.CreateInstance();
                        break;

                    case "OneUpCollect":
                        SoundEffect = OneUpCollect.CreateInstance();
                        break;

                    case "Flag":
                        MediaPlayer.Stop();
                        SoundEffect = Flag.CreateInstance();
                        break;

                    case "BrickBump":
                        SoundEffect = BrickBump.CreateInstance();
                        break;

                    case "BrickBreak":
                        SoundEffect = BrickBreak.CreateInstance();
                        break;

                    case "PipeTravel":
                        SoundEffect = PipeTravel.CreateInstance();
                        break;

                    case "TimeWarning":
                        SoundEffect = TimeWarning.CreateInstance();
                        break;

                    case "GameOver":
                        SoundEffect = GameOver.CreateInstance();
                        break;

                    case "Fireball":
                        SoundEffect = Fireball.CreateInstance();
                        break;

                    case "Die":
                        MediaPlayer.Stop();
                        SoundEffect = Die.CreateInstance();
                        break;
                }
                SoundEffect.Volume = .1f;
                SoundEffect.IsLooped = false;
                SoundEffect.Play();
            }
        }

        public void SwitchSong(string songName)
        {
            SongName = songName;
            if (!Hurry || SongName.Equals("victory"))
                BackgroundMusic = Content.Load<Song>("Sounds/" + SongName);
            else
                BackgroundMusic = Content.Load<Song>("Sounds/hurry-" + SongName);

            switch (songName)
            {
                case "victory":
                    PlayBackgroundMusicNonRepeat();
                    break;

                case "underworld":
                    PlayBackgroundMusic();
                    break;

                case "overworld":
                    PlayBackgroundMusic();
                    break;

                case "starman":
                    PlayBackgroundMusicNonRepeat();
                    break;

            }
        }
    }
}
