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

        public static async Task PlayAsync(SoundType soundType)
        {
            if (soundPlayers.TryGetValue(soundType, out SoundPlayer soundPlayer))
            {
                await Task.Run(() => soundPlayer.PlaySync());
            }
        }

        public static async Task PlayMainMusicAsync()
        {
            mainMusicPlayer = soundPlayers[SoundType.MainMusic];
            await Task.Run(() => mainMusicPlayer.PlayLooping());
        }

        public static async Task StopMainMusicAsync()
        {
            if (mainMusicPlayer != null)
            {
                await Task.Run(() => mainMusicPlayer.Stop());
            }
        }

        public static async Task PlayMarketSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.Market);
            await Task.Run(() => soundPlayer.PlaySync());
        }



        public static async Task PlayItemPickupSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.Bottle);
            await Task.Run(() => soundPlayer.PlaySync());
        }

        public static async Task PlayGameStartSoundAsync()
        {
            if (soundPlayers.TryGetValue(SoundType.GameStart, out SoundPlayer soundPlayer))
            {
                await Task.Run(() => soundPlayer.PlaySync());
            }
        }

        public static async Task PlayDieSoundAsync()
        {
            if (soundPlayers.TryGetValue(SoundType.Die, out SoundPlayer soundPlayer))
            {
                await Task.Run(() => soundPlayer.PlaySync());
            }

        }

        public static async void PlayScoreSound()
        {
            await Task.Run(() => soundPlayers[SoundType.Score].PlaySync());
        }

        public static async void PlayMissedHitSound()
        {
            await Task.Run(() => soundPlayers[SoundType.MissedHit].PlaySync());
        }

        public static async void PlayVictorySound()
        {
            await Task.Run(() => soundPlayers[SoundType.Victory].PlaySync());
        }

        public static async void PlayDieSound()
        {
            await Task.Run(() => soundPlayers[SoundType.Die].PlaySync());
        }
    }
}
