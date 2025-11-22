// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionMoveInCircle
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Common.Helper;
using Spinballs.Core.Controls;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionMoveInCircle : ActionDuration
  {
    private Circle _circle;
    private int _startAngle;
    private int _angleOffset;

    public ActionMoveInCircle(
      Spinballs.Core.Controls.ImageControl image,
      Circle circle,
      int startAngle,
      int angleOffset,
      TimeSpan duration)
      : base(duration)
    {
      this.ImageControl = (DrawableControl) image;
      this.Circle = circle;
      this.StartAngle = startAngle;
      this.AngleOffset = angleOffset;
    }

    public Circle Circle
    {
      get => this._circle;
      set => this._circle = value;
    }

    public int StartAngle
    {
      get => this._startAngle;
      set => this._startAngle = value;
    }

    public int AngleOffset
    {
      get => this._angleOffset;
      set => this._angleOffset = value;
    }

    public int StopAngle => this.StartAngle + this.AngleOffset;

    public override bool Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this.Finished)
        this.ImageControl.Position = this.Circle.GetCirclePosition(Circle.ToRadian((double) this.StopAngle));
      else
        this.ImageControl.Position = this.Circle.GetCirclePosition(Circle.ToRadian((double) this.StartAngle + (double) this.AngleOffset / this._duration.TotalMilliseconds * this._elapsed.TotalMilliseconds));
      return this.Finished;
    }

    public override void Reset() => base.Reset();
  }
}
