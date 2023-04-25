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
        Victory,
        Slots,
        Bottles,
        OpenBottle,
        Win,
        Fail,
        LevelUp
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
            { SoundType.Victory, new SoundPlayer(School_Project.Properties.Resources.Victory) },
            { SoundType.Slots, new SoundPlayer(School_Project.Properties.Resources.Slots) },
            { SoundType.Bottles, new SoundPlayer(School_Project.Properties.Resources.Bottles) },
            { SoundType.OpenBottle, new SoundPlayer(School_Project.Properties.Resources.OpenBottle) },
            { SoundType.Win, new SoundPlayer(School_Project.Properties.Resources.Win) },
            { SoundType.Fail, new SoundPlayer(School_Project.Properties.Resources.Fail) },
            { SoundType.LevelUp, new SoundPlayer(School_Project.Properties.Resources.LevelUp) }
        };

        private static SoundPlayer mainMusicPlayer;
        private static SoundPlayer marketMusicPlayer;
        private static SoundPlayer slotsMusicPlayer;

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
        public static async Task PlaySlotsMusicAsync()
        {
            slotsMusicPlayer = soundPlayers[SoundType.Slots];
            await Task.Run(() => slotsMusicPlayer.PlayLooping());
        }

        public static async Task PlayMarketSoundAsync()
        {
            marketMusicPlayer = soundPlayers[SoundType.Market];
            marketMusicPlayer.PlayLooping();
        }
        public static void StopMarketSound()
        {
            marketMusicPlayer?.Stop();
           
        }

        public static async Task PlayBottlesSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.Bottles);
            await Task.Run(() => soundPlayer.PlaySync());
        }

        public static async Task PlayOpenBottleSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.OpenBottle);
            await Task.Run(() => soundPlayer.PlaySync());
        }

        public static async Task PlayWinSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.Win);
            await Task.Run(() => soundPlayer.PlaySync());
        }


        public static async Task PlayFailSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.Fail);
            await Task.Run(() => soundPlayer.PlaySync());
        }


        public static async Task PlayItemPickupSoundAsync()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.Bottle);
            await Task.Run(() => soundPlayer.PlaySync());
        }
        public static async Task PlayLevelUpSound()
        {
            var soundPlayer = new SoundPlayer(School_Project.Properties.Resources.LevelUp);
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
