// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionFadeIn
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
  public class ActionFadeIn : ActionDuration
  {
    public byte _minOpacity;
    private byte _maxOpacity;
    private byte _opacityRange;

    public ActionFadeIn(DrawableControl control, TimeSpan duration)
      : base(duration)
    {
      this.ImageControl = control;
      this.MinOpacity = (byte) 0;
      this.MaxOpacity = byte.MaxValue;
    }

    public ActionFadeIn(
      DrawableControl control,
      TimeSpan duration,
      byte minOpacity,
      byte maxOpacity)
      : base(duration)
    {
      this.ImageControl = control;
      this.MinOpacity = minOpacity;
      this.MaxOpacity = maxOpacity;
    }

    [DataMember]
    public byte MinOpacity
    {
      get => this._minOpacity;
      set
      {
        this._minOpacity = value;
        this._opacityRange = (byte) ((uint) this.MaxOpacity - (uint) this.MinOpacity);
      }
    }

    [DataMember]
    public byte MaxOpacity
    {
      get => this._maxOpacity;
      set
      {
        this._maxOpacity = value;
        this._opacityRange = (byte) ((uint) this.MaxOpacity - (uint) this.MinOpacity);
      }
    }

    public override bool Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this.Finished)
      {
        this.ImageControl.Opacity = this.MaxOpacity;
        this.ImageControl.Visible = true;
      }
      else
      {
        this.ImageControl.Opacity = (byte) ((double) this.MinOpacity + (double) this._opacityRange * (this._elapsed.TotalMilliseconds / this._duration.TotalMilliseconds));
        this.ImageControl.Visible = true;
      }
      return this.Finished;
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      if (!(action is ActionFadeIn actionFadeIn))
        return;
      this.MinOpacity = actionFadeIn.MinOpacity;
      this.MaxOpacity = actionFadeIn.MaxOpacity;
    }

    public override void Reset() => base.Reset();
  }
}
