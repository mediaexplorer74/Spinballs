// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionJump
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Core.Controls;
using Spinballs.Core.ScreenManagement;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionJump : ImageAction
  {
    [DataMember]
    public Vector2 _startPosition;
    [DataMember]
    public double _xVelocity;
    [DataMember]
    public double _yVelocity;
    [DataMember]
    public TimeSpan _elapsed;

    public ActionJump(Spinballs.Core.Controls.ImageControl image)
    {
      this.ImageControl = (DrawableControl) image;
      this._startPosition = this.ImageControl.Position;
      Random random = new Random();
      this._xVelocity = (double) Spinballs.Common.Helper.Helper.GetRandom(-50, 50);
      this._yVelocity = 1.0 + (double) random.Next(101) / 500.0;
      this._elapsed = new TimeSpan();
    }

    public override bool Update(GameTime gameTime)
    {
      if (this.Finished)
      {
        this.ImageControl.Visible = false;
      }
      else
      {
        this._elapsed += gameTime.ElapsedGameTime;
        double num1 = -80.0;
        double num2 = 1.2000000476837158;
        double num3 = 1.0 + this._elapsed.TotalMilliseconds / 100.0;
        double num4 = num1 * num3;
        double num5 = num3 * num3 * num3 / num2;
        int num6 = (int) this.ImageControl.Size.X / 2;
        int x = (int) this._startPosition.X + (int) (this._xVelocity * num3 - this._xVelocity);
        int y = (int) this._startPosition.Y + (int) (num4 * this._yVelocity + num5 - num1);
        if (x - num6 < 0)
        {
          x = 2 * num6 - x;
        }
        else
        {
          int num7 = (int) ((double) (x + num6) - (double) ScreenManager.ActiveScreen.Size.X);
          if (num7 > 0)
            x = (int) ((double) ScreenManager.ActiveScreen.Size.X - (double) num7 - (double) num6);
        }
        this.ImageControl.Position = new Vector2((float) x, (float) y);
        this.Finished = (double) y > (double) ScreenManager.ActiveScreen.Size.Y;
      }
      return this.Finished;
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      if (!(action is ActionJump actionJump))
        return;
      this._startPosition = actionJump._startPosition;
      this._xVelocity = actionJump._xVelocity;
      this._yVelocity = actionJump._yVelocity;
      this._elapsed = actionJump._elapsed;
    }

    public override void Reset()
    {
    }
  }
}
