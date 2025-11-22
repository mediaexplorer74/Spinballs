// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionMusicFade
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Common.Helper;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionMusicFade : ActionDuration
  {
    private float _startValue;
    private float _endValue;
    private float _offset;

    public ActionMusicFade(float endValue, TimeSpan duration)
      : base(duration)
    {
      this.StartValue = Config.Instance.MusicVolume;
      this.EndValue = endValue;
      this._offset = this.EndValue - this.StartValue;
    }

    public float StartValue
    {
      get => this._startValue;
      set => this._startValue = value;
    }

    public float EndValue
    {
      get => this._endValue;
      set => this._endValue = value;
    }

    public override bool Update(GameTime gameTime)
    {
      base.Update(gameTime);
      Config.Instance.MusicVolume = !this.Finished ? this.StartValue + this._offset * (float) (this._elapsed.TotalMilliseconds / this._duration.TotalMilliseconds) : this.EndValue;
      return this.Finished;
    }
  }
}
