// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.ExtraTimeController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller.Extra
{
  public class ExtraTimeController : BaseExtraController
  {
    public ExtraTimeController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen, Layout.ExtraTimePos)
    {
      this._activeDuration = TimeSpan.FromMilliseconds(20000.0);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this.LoadControl = new ExtraLoadControl(Res.GameScreen.ExtraSlowMo);
      this._connNorth.Position = Layout.BonusConnector[3];
      this._connNorthWest.Position = Layout.BonusConnector[2];
      this._connNorthWest.Effects = SpriteEffects.FlipHorizontally;
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[2][0], this._connNorth, this.ActionManager));
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[1][1], this._connNorthWest, this.ActionManager));
    }

    protected override void Execute()
    {
      base.Execute();
      MessageService.SetTimerBarIced((object) this, true);
    }

    protected override void Stop()
    {
      base.Stop();
      MessageService.SetTimerBarIced((object) this, false);
    }

    protected override void Reset()
    {
      base.Reset();
      MessageService.SetTimerBarIced((object) this, false);
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      base.Draw(spriteBatch, drawOrder);
    }
  }
}
