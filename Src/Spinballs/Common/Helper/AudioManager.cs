// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.AudioManager
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

#nullable disable
namespace Spinballs.Common.Helper
{
  public class AudioManager
  {
    private static SoundEffectInstance _music;

    public static SoundEffectInstance Music
    {
      get => AudioManager._music;
      set => AudioManager._music = value;
    }

    public static void PlayMusic(SoundEffect music)
    {
      AudioManager.Music = music.CreateInstance();
      AudioManager.Music.IsLooped = true;
      AudioManager.Music.Volume = Config.Instance.MusicVolume;
      AudioManager.Music.Play();
    }

    public static void PlayMusic(SoundEffectInstance music)
    {
      if (AudioManager.Music != music)
      {
        if (AudioManager.Music != null && AudioManager.Music.State != SoundState.Playing)
          AudioManager.Music.Stop();
        AudioManager.Music = music;
      }
      if (AudioManager.Music.State == SoundState.Playing)
        return;
      AudioManager.Music.IsLooped = true;
      AudioManager.Music.Volume = Config.Instance.MusicVolume;
      AudioManager.Music.Play();
    }

    public static void PlayMusic(Song song)
    {
      try
      {
        if (!Res.CanUseMusic || !MediaPlayer.GameHasControl)
          return;
        if (MediaPlayer.State != MediaState.Stopped)
          MediaPlayer.Stop();
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(song);
      }
      catch
      {
      }
    }

    public static void StopMusic()
    {
      try
      {
        if (AudioManager.Music != null)
          AudioManager.Music.Stop();
        if (!Res.CanUseMusic)
          return;
        MediaPlayer.Stop();
      }
      catch
      {
      }
    }

    public static void SetMusicVolume(float volume)
    {
      if (!Res.CanUseMusic)
        return;
      try
      {
        if (AudioManager.Music != null)
          AudioManager.Music.Volume = volume;
        if ((double) volume <= 0.0)
        {
          MediaPlayer.IsMuted = true;
        }
        else
        {
          MediaPlayer.IsMuted = false;
          MediaPlayer.Volume = volume;
        }
      }
      catch
      {
      }
    }

    public static void AdminSetMusicVolume(float volume)
    {
      try
      {
        if (AudioManager.Music != null)
          AudioManager.Music.Volume = volume;
        if ((double) volume <= 0.0)
        {
          MediaPlayer.IsMuted = true;
        }
        else
        {
          MediaPlayer.IsMuted = false;
          MediaPlayer.Volume = volume;
        }
      }
      catch
      {
      }
    }

    public static void Play(SoundEffect sound)
    {
      sound.Play(Config.Instance.SoundVolume, 0.0f, 0.0f);
    }
  }
}
