// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.ExtraPointsController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Controller.Extra
{
  public class ExtraPointsController : BaseExtraController
  {
    public ExtraPointsController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen, Layout.ExtraPointsPos)
    {
      this._activeDuration = TimeSpan.FromMilliseconds(20000.0);
      this.Document.PointsChanged += new GameDocument.AddPointHandler(this.Document_PointsChanged);
      this.Document.PointsChanging += new GameDocument.AddPointHandler(this.Document_PointsChanging);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this.LoadControl = new ExtraLoadControl(Res.GameScreen.ExtraX2);
      this._connNorth.Position = Layout.BonusConnector[7];
      this._connNorth.Effects = SpriteEffects.FlipVertically;
      this._connNorthWest.Position = Layout.BonusConnector[6];
      this._connNorthWest.Effects = SpriteEffects.FlipVertically;
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[5][3], this._connNorth, this.ActionManager));
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[4][4], this._connNorthWest, this.ActionManager));
    }

    protected override void Execute()
    {
      base.Execute();
      AudioManager.Play(Res.GameScreen.Sounds.ExtraPointsStart);
    }

    protected override void Stop()
    {
      base.Stop();
      AudioManager.Play(Res.GameScreen.Sounds.ExtraPointsEnd);
    }

    private void Document_PointsChanged(object sender, GameDocument.AddPointArgs e)
    {
      if (!this.Active)
        return;
      AudioManager.Play(Res.GameScreen.Sounds.ExtraPoints);
      MessageService.ShowExtraPoints((object) this, e.ExtraOffset);
    }

    private void Document_PointsChanging(object sender, GameDocument.AddPointArgs e)
    {
      if (!this.Active)
        return;
      e.ExtraOffset += e.Offset;
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      base.Draw(spriteBatch, drawOrder);
    }

    public override void Save(SaveGame savegame)
    {
      ExtraPointSave save = savegame.NewController<ExtraPointSave>((object) this);
      this.Save((ExtraBaseSave) save);
      this.SaveActions((ControllerSave) save, (List<ActionBase>) null);
    }

    public override void Load(SaveGame savegame)
    {
      ExtraPointSave controller = savegame.GetController<ExtraPointSave>((object) this);
      if (controller == null)
        return;
      this.Load((ExtraBaseSave) controller);
      this.LoadActions((ControllerSave) controller);
    }
  }
}
