// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionSwitchCircle
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
  public class ActionSwitchCircle : ActionDuration
  {
    private Spinballs.Core.Controls.ImageControl _image1;
    private Spinballs.Core.Controls.ImageControl _image2;
    private Circle _circle;
    private double _startAngle1;
    private double _startAngle2;
    private double _angleOffset1;
    private double _angleOffset2;
    private Vector2 _endPos1;
    private Vector2 _endPos2;

    public ActionSwitchCircle(Spinballs.Core.Controls.ImageControl image1, Spinballs.Core.Controls.ImageControl image2, TimeSpan duration)
      : base(duration)
    {
      this._image1 = image1;
      this._image2 = image2;
      Vector2 center = this._image1.Position + (this._image2.Position - this._image1.Position) / 2f;
      float radius = (float) Circle.GetRadius(center, this._image1.Position);
      this._circle = new Circle(center, radius);
      this._startAngle1 = Circle.GetRadian(center, this._image1.Position);
      this._startAngle2 = Circle.GetRadian(center, this._image2.Position);
      this._angleOffset1 = this._startAngle2 - this._startAngle1;
      this._angleOffset2 = (this._startAngle1 - this._startAngle2) * -1.0;
      this._endPos1 = image2.Position;
      this._endPos2 = this.Image1.Position;
    }

    public Spinballs.Core.Controls.ImageControl Image1
    {
      get => this._image1;
      set => this._image1 = value;
    }

    public Spinballs.Core.Controls.ImageControl Image2
    {
      get => this._image2;
      set => this._image2 = value;
    }

    public override bool Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this.Finished)
      {
        this._image1.Position = this._endPos1;
        this._image2.Position = this._endPos2;
      }
      else
      {
        this._image1.Position = this._circle.GetCirclePosition(this._startAngle1 + this._angleOffset1 / this._duration.TotalMilliseconds * this._elapsed.TotalMilliseconds);
        this._image2.Position = this._circle.GetCirclePosition(this._startAngle2 + this._angleOffset2 / this._duration.TotalMilliseconds * this._elapsed.TotalMilliseconds);
      }
      return this.Finished;
    }

    public override void Reset()
    {
    }
  }
}
