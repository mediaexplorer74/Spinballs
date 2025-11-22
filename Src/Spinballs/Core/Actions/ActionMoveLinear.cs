// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionMoveLinear
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
  public class ActionMoveLinear : ActionDuration
  {
    private Vector2 _startPos;
    private Vector2 _endPos;

    public ActionMoveLinear(Spinballs.Core.Controls.ImageControl control, Vector2 endPos, TimeSpan duration)
      : base(duration)
    {
      this.ImageControl = (DrawableControl) control;
      this.StartPos = this.ImageControl.Position;
      this.EndPos = endPos;
    }

    [DataMember]
    public Vector2 StartPos
    {
      get => this._startPos;
      set => this._startPos = value;
    }

    [DataMember]
    public Vector2 EndPos
    {
      get => this._endPos;
      set => this._endPos = value;
    }

    public override bool Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this.Finished)
        this.ImageControl.Position = this.EndPos;
      else
        this.ImageControl.Position = Vector2.Lerp(this.StartPos, this.EndPos, (float) (this._elapsed.TotalMilliseconds / this._duration.TotalMilliseconds));
      return this.Finished;
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      if (!(action is ActionMoveLinear actionMoveLinear))
        return;
      this.StartPos = actionMoveLinear.StartPos;
      this.EndPos = actionMoveLinear.EndPos;
    }
  }
}
