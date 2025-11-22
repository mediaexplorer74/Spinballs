// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.ExtraExplodeController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller.Extra
{
  internal class ExtraExplodeController : BaseExtraController
  {
    public ExtraExplodeController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen, Layout.ExtraExplodePos)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      this._activeDuration = TimeSpan.FromMilliseconds(1800.0);
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.BonusExplode)
        return;
      this.LockView();
      this.ExplodeDiscs();
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this.LoadControl = new ExtraLoadControl(Res.GameScreen.ExtraExplode);
      this._connNorth.Position = Layout.BonusConnector[0];
      this._connNorthWest.Position = Layout.BonusConnector[1];
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[6][0], this._connNorth, this.ActionManager));
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[1][5], this._connNorthWest, this.ActionManager));
    }

    protected override void Execute()
    {
      base.Execute();
      this.Document.State = GameState.BonusExplode;
    }

    protected override bool UpdateExtraController()
    {
      return this.Document.State == GameState.BonusExplode;
    }

    protected override void Stop() => base.Stop();

    protected override void Reset() => base.Reset();

    private void ExplodeDiscs()
    {
      foreach (BallControl ball in this.GameScreen.Balls)
        ball.Highlight = false;
      AudioManager.Play(Res.GameScreen.Sounds.Explode);
      ActionParallel action = new ActionParallel();
      foreach (Disc disc in this.Document.Discs)
      {
        if (disc.DiscIndex == 0)
          action.Actions.Add(this.GetExplodeDiscAction(disc.DiscIndex));
        else
          action.Actions.Add((ActionBase) new ActionSequence()
          {
            Actions = {
              (ActionBase) new ActionDuration(TimeSpan.FromMilliseconds((double) (200 * disc.DiscIndex))),
              this.GetExplodeDiscAction(disc.DiscIndex)
            }
          });
      }
      action.ActionFinished += new EventHandler(this.DiscExplode_ActionFinished);
      this.ActionManager.Add((ActionBase) action);
    }

    private void DiscExplode_ActionFinished(object sender, EventArgs e)
    {
      this.LoadControl.BlinkMode = BlinkMode.None;
      this.LoadValue = 0.0f;
      this.Document.Points += 100 * this.Document.CurrentLevel;
      MessageService.ResetTimerBar((object) this);
      this.UnlockView();
    }

    private ActionBase GetExplodeDiscAction(int discIndex)
    {
      ActionParallel explodeDiscAction = new ActionParallel();
      explodeDiscAction.Actions.Add((ActionBase) new ActionMessage((object) this, (MessageArgs) new ValueArgs<int>(discIndex, Message.HighlightDisc)));
      foreach (Ball ball1 in this.Document.Discs[discIndex].Balls)
      {
        BallControl ball2 = this.GameScreen.Balls[ball1.FlatIndex];
        explodeDiscAction.Actions.Add((ActionBase) new ActionJump((ImageControl) ball2));
      }
      return (ActionBase) explodeDiscAction;
    }

    public override void Load(SaveGame savegame)
    {
      base.Load(savegame);
      if (!this.Active)
        return;
      this.Active = false;
      this.FillStartLoadValue = 0.0f;
      this.FillEndLoadValue = 0.0f;
      this.LoadControl.Value = 0.0f;
      this.LoadControl.BlinkMode = BlinkMode.None;
    }
  }
}
