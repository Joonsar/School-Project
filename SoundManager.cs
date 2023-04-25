using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class SoundManager
    {
        private static readonly SoundPlayer MainMusicPlayer = new SoundPlayer(@"C:\Users\Omistaja\OneDrive\Työpöytä\Git\School-Project\MainMusic.wav");
        private static readonly SoundPlayer ItemPickupPlayer = new SoundPlayer(@"C:\Users\Omistaja\OneDrive\Työpöytä\Git\School-Project\Item.wav");
        private static readonly SoundPlayer GameStartPlayer = new SoundPlayer(School_Project.Properties.Resources.GameStart);
        private static readonly SoundPlayer DieSoundPlayer = new SoundPlayer(@"C:\Users\Omistaja\OneDrive\Työpöytä\Git\School-Project\Death.wav");
        private static SoundPlayer mainMusicPlayer;

        public static void PlayMainMusic()
        {
            mainMusicPlayer = new SoundPlayer(School_Project.Properties.Resources.MainMusic);
            mainMusicPlayer.PlayLooping();
        }

        public static void StopMainMusic()
        {
            mainMusicPlayer?.Stop();
        }

        public static void PlayItemPickupSound()
        {
            ItemPickupPlayer.Play();
        }

        public static void PlayGameStart()
        {
            GameStartPlayer.Play();
        }

        public static void PlayDieSound()
        {
            DieSoundPlayer.Play();
        }
    }
}
