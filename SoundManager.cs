using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class SoundManager
    {
        
        private static readonly SoundPlayer ItemPickupPlayer = new SoundPlayer(School_Project.Properties.Resources.Bottle);
        private static readonly SoundPlayer GameStartPlayer = new SoundPlayer(School_Project.Properties.Resources.GameStart);
        private static readonly SoundPlayer DieSoundPlayer = new SoundPlayer(School_Project.Properties.Resources.Death);
        private static readonly SoundPlayer ScoreSoundPlayer = new SoundPlayer(School_Project.Properties.Resources.Score);
        private static readonly SoundPlayer MissedHitSoundPlayer = new SoundPlayer(School_Project.Properties.Resources.Missed);
        private static readonly SoundPlayer VictorySoundPlayer = new SoundPlayer(School_Project.Properties.Resources.Victory);
        private static readonly SoundPlayer MarketSoundPlayer = new SoundPlayer(School_Project.Properties.Resources.Market);
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

        public static void PlayMarketSound()
        {
            MarketSoundPlayer.Play();
        }

        public static void StopMarketSound()
        {
            MarketSoundPlayer.Stop();
        }

        public static void PlayItemPickupSound()
        {
            using (var player = new SoundPlayer(School_Project.Properties.Resources.Bottle))
            {
                player.Load();
                player.Play();
            }
        }

        public static void PlayGameStart()
        {
            GameStartPlayer.Play();
        }

        public static void PlayDieSound()
        {
            DieSoundPlayer.Play();
        }

        public static void PlayScoreSound()
        {
            ScoreSoundPlayer.Play();
        }

        public static void PlayMissedSound()
        {
            MissedHitSoundPlayer.Play();
        }
        
        public static void PlayVictorySound()
        {
            VictorySoundPlayer.Play();
        }
    }
}
