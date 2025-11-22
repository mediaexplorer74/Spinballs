// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionBlink
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Core.Controls;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionBlink : ActionDuration
  {
    private TimeSpan _blinkDuration;

    public ActionBlink(Spinballs.Core.Controls.ImageControl control, TimeSpan duration, TimeSpan blinkDuration)
      : base(duration)
    {
      this.ImageControl = (DrawableControl) control;
      this._blinkDuration = blinkDuration;
    }

    public TimeSpan BlinkDuration
    {
      get => this._blinkDuration;
      set => this._blinkDuration = value;
    }

    public override bool Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this.Finished)
        this.ImageControl.Opacity = byte.MaxValue;
      else if (this._elapsed.TotalMilliseconds % this.BlinkDuration.TotalMilliseconds < (double) ((int) this.BlinkDuration.TotalMilliseconds >> 1))
        this.ImageControl.Opacity = (byte) 170;
      else
        this.ImageControl.Opacity = byte.MaxValue;
      return this.Finished;
    }

    public override void Reset()
    {
      base.Reset();
      this.ImageControl.Opacity = byte.MaxValue;
    }
  }
}
