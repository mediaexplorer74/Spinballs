// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionSound
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Spinballs.Common.Helper;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionSound : ActionBase
  {
    private SoundEffect _sound;

    public ActionSound(SoundEffect sound) => this.Sound = sound;

    public SoundEffect Sound
    {
      get => this._sound;
      set => this._sound = value;
    }

    public override bool Update(GameTime gameTime)
    {
      this.Sound.Play(Config.Instance.SoundVolume, 0.0f, 0.0f);
      this.Finished = true;
      return this.Finished;
    }

    public override void Reset() => this.Finished = false;
  }
}
