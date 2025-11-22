// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.GameStartController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller
{
  public class GameStartController : GameController
  {
    private readonly TimeSpan _duration = TimeSpan.FromMilliseconds(3000.0);
    private TimeSpan _ellapsed = new TimeSpan();
    private float _fraction;
    private LabelControl labelTime;

    public GameStartController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      this.labelTime = new LabelControl();
    }

    public override void Init()
    {
      base.Init();
      this._ellapsed = new TimeSpan();
      this._fraction = 0.0f;
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this.labelTime.Text = "3";
      this.labelTime.Position = Layout.GetDiscCenter(0);
      this.labelTime.Orientation = Orientation.Center;
      this.labelTime.Font = Res.Font.Big4;
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.Starting)
        return;
      foreach (DrawableControl ball in this.GameScreen.Balls)
        ball.Visible = false;
      this.LockView();
      this._fraction = (float) this._duration.TotalMilliseconds / (float) this.GameScreen.Balls.Length;
      this._ellapsed = new TimeSpan();
    }

    protected override void UpdateCore(GameTime gameTime)
    {
      if (this.Document.State != GameState.Starting)
        return;
      this._ellapsed += gameTime.ElapsedGameTime;
      this.labelTime.Text = string.Format("{0}", (object) (3 - (int) this._ellapsed.TotalSeconds));
      int num = (int) (this._ellapsed.TotalMilliseconds / (double) this._fraction);
      for (int index = 0; index < num && index < this.GameScreen.Balls.Length; ++index)
        this.GameScreen.Balls[index].Visible = true;
      if (num < this.GameScreen.Balls.Length)
        return;
      this.UnlockView();
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (this.Document.State != GameState.Starting || drawOrder != DrawOrder.BeforeBalls)
        return;
      this.labelTime.Draw(spriteBatch);
    }

    public override void Save(SaveGame savegame)
    {
      if (this.Document.State != GameState.Starting)
        return;
      base.Save(savegame);
      GameStartSave gameStartSave = savegame.NewController<GameStartSave>((object) this);
      gameStartSave.Ellapsed = this._ellapsed.TotalMilliseconds;
      gameStartSave.Fraction = this._fraction;
    }

    public override void Load(SaveGame savegame)
    {
      if (this.Document.State != GameState.Starting)
        return;
      base.Load(savegame);
      GameStartSave controller = savegame.GetController<GameStartSave>((object) this);
      if (controller == null)
        return;
      this._ellapsed = TimeSpan.FromMilliseconds(controller.Ellapsed);
      this._fraction = controller.Fraction;
      this.labelTime.Text = string.Format("{0}", (object) (3 - (int) this._ellapsed.TotalSeconds));
      this.LockView();
    }
  }
}
