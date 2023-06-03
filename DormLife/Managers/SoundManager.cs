using DormLife.GameObjects;
using DormLife.Sprites;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public static class SoundManager
    {
        enum TypeSound
        {
            // Добавить
        }

        private static Song basedBackgroundMusic;
        private static Song gameBackgroundMusic;
        private static Song shopBackgroundMusic;

        private static Song pausedMusic;
        private static TimeSpan pausedMusicPosition;

        private static Dictionary<string, SoundEffect> soundEffects = new();

        public static bool isMusicEnabled { get; private set; }

        public static void LoadContent()
        {
            isMusicEnabled = true;

            basedBackgroundMusic = Globals.Content.Load<Song>("Music/menu");
            gameBackgroundMusic = Globals.Content.Load<Song>("Music/game");
            shopBackgroundMusic = Globals.Content.Load<Song>("Music/shop");

            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            SoundEffect.MasterVolume = 0.2f;

            soundEffects.Add("shoot", Globals.Content.Load<SoundEffect>("Sounds/shootstart"));
            soundEffects.Add("nomissed", Globals.Content.Load<SoundEffect>("Sounds/shootend"));
            soundEffects.Add("death", Globals.Content.Load<SoundEffect>("Sounds/death"));
            soundEffects.Add("healbottle", Globals.Content.Load<SoundEffect>("Sounds/healbottle"));
            soundEffects.Add("ultbottle", Globals.Content.Load<SoundEffect>("Sounds/ultbottle"));
            soundEffects.Add("trapspawn", Globals.Content.Load<SoundEffect>("Sounds/settrap"));
            soundEffects.Add("trapexp", Globals.Content.Load<SoundEffect>("Sounds/explodetrap"));
            soundEffects.Add("buff", Globals.Content.Load<SoundEffect>("Sounds/buff"));
            soundEffects.Add("cakedamage", Globals.Content.Load<SoundEffect>("Sounds/cakedamage"));
            soundEffects.Add("gameover", Globals.Content.Load<SoundEffect>("Sounds/gameover"));
            soundEffects.Add("moneyspend", Globals.Content.Load<SoundEffect>("Sounds/moneyspend"));
        }

        public static void PlayBasedBackgroundMusic()
        {
            MediaPlayer.Stop();
            if (isMusicEnabled)
            {
                MediaPlayer.Volume = 0.1f;
            }
            MediaPlayer.Play(basedBackgroundMusic);
        }

        public static void PlayGameBackgroundMusic()
        {
            MediaPlayer.Stop();
            if (isMusicEnabled)
            {
                MediaPlayer.Volume = 0.2f;
            }
            MediaPlayer.Play(gameBackgroundMusic);
        }

        public static void PlayShopBackgroundMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(shopBackgroundMusic);
        }


        public static void PauseGameMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                pausedMusic = MediaPlayer.Queue.ActiveSong;
                pausedMusicPosition = MediaPlayer.PlayPosition;
                MediaPlayer.Pause();
            }
        }

        public static void ResumeGameMusic()
        {
            if (pausedMusic != null)
            {
                if (isMusicEnabled)
                {
                    MediaPlayer.Volume = 0.2f;
                }
                MediaPlayer.Play(pausedMusic, pausedMusicPosition);
            }
        }

        public static void ToggleMusic()
        {
            if (isMusicEnabled)
            {
                isMusicEnabled = !isMusicEnabled;
                MediaPlayer.Volume = 0;

            }
            else
            {
                isMusicEnabled = !isMusicEnabled;
                MediaPlayer.Volume = 0.1f;
            }
        }


        public static void PlaySoundEffect(string effectName)
        {
            if (soundEffects.ContainsKey(effectName))
            {
                soundEffects[effectName].Play();
            }
        }
    }
}
