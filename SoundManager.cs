using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public enum SoundType
    {
        MainMusic,
        Market,
        ItemPickup,
        GameStart,
        Die,
        Score,
        MissedHit,
        Victory
    }

    public static class SoundManager
    {
        private static readonly Dictionary<SoundType, SoundPlayer> soundPlayers = new Dictionary<SoundType, SoundPlayer>
        {
            { SoundType.MainMusic, new SoundPlayer(School_Project.Properties.Resources.MainMusic) },
            { SoundType.Market, new SoundPlayer(School_Project.Properties.Resources.Market) },
            { SoundType.ItemPickup, new SoundPlayer(School_Project.Properties.Resources.Bottle) },
            { SoundType.GameStart, new SoundPlayer(School_Project.Properties.Resources.GameStart) },
            { SoundType.Die, new SoundPlayer(School_Project.Properties.Resources.Dead) },
            { SoundType.Score, new SoundPlayer(School_Project.Properties.Resources.Score) },
            { SoundType.MissedHit, new SoundPlayer(School_Project.Properties.Resources.Missed) },
            { SoundType.Victory, new SoundPlayer(School_Project.Properties.Resources.Victory) }
        };

        private static SoundPlayer mainMusicPlayer;

        public static void Play(SoundType soundType)
        {
            if (soundPlayers.TryGetValue(soundType, out SoundPlayer soundPlayer))
            {
                soundPlayer.Play();
            }
        }

        public static void PlayMainMusic()
        {
            mainMusicPlayer = soundPlayers[SoundType.MainMusic];
            mainMusicPlayer.PlayLooping();
        }

        public static void StopMainMusic()
        {
            mainMusicPlayer?.Stop();
        }

        public static void PlayMarketSound()
        {
            soundPlayers[SoundType.Market].Play();
        }

        public static void StopMarketSound()
        {
            soundPlayers[SoundType.Market].Stop();
        }

        public static void PlayItemPickupSound()
        {
            using (var player = new SoundPlayer(School_Project.Properties.Resources.Bottle))
            {
                player.Load();
                player.Play();
            }
        }

        public static void PlayGameStartSound()
        {
            soundPlayers[SoundType.GameStart].Play();
        }

        public static void PlayDieSound()
        {
            soundPlayers[SoundType.Die].Play();
        }

        public static void PlayScoreSound()
        {
            soundPlayers[SoundType.Score].Play();
        }

        public static void PlayMissedHitSound()
        {
            soundPlayers[SoundType.MissedHit].Play();
        }

        public static void PlayVictorySound()
        {
            soundPlayers[SoundType.Victory].Play();
        }
    }
}
