// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.LevelController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller
{
  public class LevelController : GameController
  {
    private LevelUpControl _levelUp;
    private ActionSequence _action;
    private LabelControl _labelLevel;
    private LevelBarControl _levelBar;

    public LevelController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this._levelUp = new LevelUpControl();
      this._labelLevel = new LabelControl();
      this._levelBar = new LevelBarControl();
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      this.Document.PointsChanged += new GameDocument.AddPointHandler(this.Document_PointsChanged);
      this.Document.LevelChanged += new EventHandler(this.Document_LevelChanged);
      this._action = new ActionSequence();
      this._action.ActionFinished += new EventHandler(this._action_ActionFinished);
    }

    public override void Init()
    {
      base.Init();
      this._labelLevel.Text = string.Format("{0} {1}", (object) Strings.Level, (object) (this.Document.CurrentLevel + 1));
      this.UpdateLevelBar();
    }

    private void Document_LevelChanged(object sender, EventArgs e)
    {
      if (this.Document.CurrentLevel > 0)
        this.Document.State = GameState.LevelUp;
      this._labelLevel.Text = string.Format("{0} {1}", (object) Strings.Level, (object) (this.Document.CurrentLevel + 1));
      this.UpdateLevelBar();
    }

    private void Document_PointsChanged(object sender, GameDocument.AddPointArgs e)
    {
      this.UpdateLevelBar();
    }

    private void UpdateLevelBar()
    {
      this._levelBar.ShowCount = (int) ((double) this.Document.CurrentPoints / (double) this.Document.CurrentLevelRequiredPoints * 100.0 / 5.0);
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.LevelUp || this.IsLockingView())
        return;
      this.LockView();
      this._levelUp.Level = this.Document.CurrentLevel + 1;
      this._levelUp.Create();
      AudioManager.Play(Res.GameScreen.Sounds.LevelUp);
      this._action.Start();
    }

    private void _action_ActionFinished(object sender, EventArgs e) => this.UnlockView();

    public override void LoadContent()
    {
      base.LoadContent();
      this._levelUp.Level = this.Document.CurrentLevel;
      this._levelUp.Create();
      this._levelUp.Position = new Vector2(0.0f, this.GameScreen.Size.Y - (float) this._levelUp.Texture.Height);
      this._levelUp.Opacity = (byte) 0;
      this._labelLevel.DisplayRect = Layout.TextLevel;
      this._labelLevel.Orientation = Orientation.Center;
      this._levelBar.Create();
      this._levelBar.Position = Layout.LevelBarPos;
      Vector2 endPos = new Vector2(0.0f, this.GameScreen.Size.Y / 2f - (float) (this._levelUp.Texture.Height / 2));
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._levelUp, TimeSpan.FromMilliseconds(1000.0)));
      actionParallel.Actions.Add((ActionBase) new ActionMoveLinear((ImageControl) this._levelUp, endPos, TimeSpan.FromMilliseconds(1000.0)));
      this._action.ActionManager = this.ActionManager;
      this._action.Actions.Add((ActionBase) actionParallel);
      this._action.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(2000.0)));
      this._action.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._levelUp, TimeSpan.FromMilliseconds(1000.0)));
      this.Document_LevelChanged((object) null, (EventArgs) null);
    }

    protected override void UpdateCore(GameTime gameTime)
    {
      int state = (int) this.Document.State;
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder != DrawOrder.AfterBalls)
        return;
      this._labelLevel.Draw(spriteBatch);
      this._levelBar.Draw(spriteBatch);
      if (this.Document.State != GameState.LevelUp)
        return;
      this._levelUp.Draw(spriteBatch);
    }

    public override void Load(SaveGame savegame)
    {
      base.Load(savegame);
      this._labelLevel.Text = string.Format("{0} {1}", (object) Strings.Level, (object) (this.Document.CurrentLevel + 1));
      this.UpdateLevelBar();
    }
  }
}
