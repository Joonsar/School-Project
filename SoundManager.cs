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
        private static readonly SoundPlayer ItemPickupPlayer = new SoundPlayer(@"C:\Users\Omistaja\OneDrive\Työpöytä\Git\School-Project\ItemPickup.wav");
        private static readonly SoundPlayer GameStartPlayer = new SoundPlayer(@"C:\Users\Omistaja\OneDrive\Työpöytä\Git\School-Project\GameStart.wav");

        public static void PlayMainMusic()
        {
            MainMusicPlayer.PlayLooping();
        }

        public static void StopMainMusic()
        {
            MainMusicPlayer.Stop();
        }

        public static void PlayItemPickupSound()
        {
            ItemPickupPlayer.Play();
        }

        public static void PlayGameStart()
        {
            GameStartPlayer.Play();
        }
    }
}
