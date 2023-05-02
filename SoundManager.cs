using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace School_Project
{
    public enum SoundType
    {
        None,
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
        LevelUp,
        Viewer,
        Bg2,
        Door,
        HitEnemy,
        Alcohol,
        NonAlcohol

    }

    public static class SoundManager
    {
        private static readonly Dictionary<SoundType, AudioFileReader> soundPlayers = new();

        private static SoundType currentMusicType;

        static SoundManager()
        {
            LoadSound(SoundType.MainMusic, "MainMusic.wav");
            LoadSound(SoundType.Market, "Market.wav");
            LoadSound(SoundType.ItemPickup, "Bottle.wav");
            LoadSound(SoundType.GameStart, "GameStart.wav");
            LoadSound(SoundType.Die, "Dead.wav");
            LoadSound(SoundType.Score, "Score.wav");
            //LoadSound(SoundType.MissedHit, "Missed.wav");
            LoadSound(SoundType.MissedHit, "MissHit.mp3");
            LoadSound(SoundType.Victory, "Victory.wav");
            LoadSound(SoundType.Slots, "Slots.wav");
            LoadSound(SoundType.Bottles, "Bottles.wav");
            LoadSound(SoundType.OpenBottle, "OpenBottle.wav");
            LoadSound(SoundType.Win, "Win.wav");
            //LoadSound(SoundType.Fail, "Fail.wav");
            LoadSound(SoundType.Fail, "Ooh.mp3");
            LoadSound(SoundType.HitEnemy, "Ough.mp3");
            LoadSound(SoundType.LevelUp, "LevelUp.wav");
            LoadSound(SoundType.Viewer, "Viewer.mp3");
            LoadSound(SoundType.Bg2, "bg2.mp3");
            LoadSound(SoundType.Door, "door.mp3");
            LoadSound(SoundType.Alcohol, "Alcohol.mp3");
            LoadSound(SoundType.NonAlcohol, "NonAlcohol.mp3");
        }

        private static void LoadSound(SoundType soundType, string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds", fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var soundPlayer = new AudioFileReader(filePath);
            soundPlayers.Add(soundType, soundPlayer);
        }

        public static void Play(SoundType soundType)
        {
            if (soundPlayers.TryGetValue(soundType, out AudioFileReader soundPlayer))
            {
                var waveOut = new WaveOutEvent();
                waveOut.Init(soundPlayer);
                soundPlayer.Seek(0, SeekOrigin.Begin);
                waveOut.Play();
            }
        }

        //tällä funktiolla soitetaan looppaavaa musiikkia.
        public static void PlayMusic(SoundType soundType)
        {
            if (currentMusicType != SoundType.None)
            {
                StopMusic();
            }

            if (soundPlayers.TryGetValue(soundType, out AudioFileReader soundPlayer))
            {
                var waveOut = new WaveOutEvent();
                waveOut.Init(soundPlayer);
                soundPlayer.Seek(0, SeekOrigin.Begin);
                waveOut.PlaybackStopped += (sender, args) =>
                {
                    //jos on päästy äänen loppuun alotetaan se uudestaan.
                    if (args.Exception == null && waveOut.PlaybackState == PlaybackState.Stopped)
                    {
                        soundPlayer.Seek(0, SeekOrigin.Begin);
                        waveOut.Play();
                    }
                };
                waveOut.Play();
                currentMusicType = soundType;
            }
        }

        //pysäyttää tällähetkellä soivan taustamusiikin
        public static void StopMusic()
        {
            if (currentMusicType != SoundType.None && soundPlayers.TryGetValue(currentMusicType, out AudioFileReader soundPlayer))
            {
                soundPlayer.Seek(0, SeekOrigin.Begin);
                soundPlayers[currentMusicType] = new AudioFileReader(soundPlayer.FileName);

                soundPlayer.Dispose();
            }
            currentMusicType = SoundType.None;
        }

        //vaihdetaan taustamusiiki
        public static void ChangeMusic(SoundType? soundType)
        {
            StopMusic();

            if (soundType.HasValue)
            {
                PlayMusic(soundType.Value);
                currentMusicType = soundType.Value;
            }
        }
    }
}