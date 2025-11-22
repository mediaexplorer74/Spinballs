// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.ChainController
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
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Controller
{
  public class ChainController : GameController
  {
    private ImageControl _executeHighlight;
    private ActionRepeat _actionExecuteHighlightLight;
    private ActionSequence _actionExecuteHighlight;
    private Circle _executeClickArea;
    private LabelControl _labelChainLength;

    public ChainController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this._executeHighlight = new ImageControl();
      this._labelChainLength = new LabelControl();
      this.Document.BestChainChanged += new EventHandler(this.Document_BestChainChanged);
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.State_Changed);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._labelChainLength.DisplayRect = Layout.TextChainLength;
      this._labelChainLength.Orientation = Orientation.Center;
      this._labelChainLength.Font = Res.Font.Big2;
      this._executeHighlight.Texture = Res.GameScreen.ExecuteHighlight;
      this._executeHighlight.Align(Orientation.Bottom | Orientation.HorizontalCenter, this.GameScreen.GetDisplayRect());
      this._executeHighlight.Opacity = (byte) 0;
      this.ActionTargets.Add((object) this._executeHighlight);
      this._executeClickArea = new Circle(new Vector2(240f, 800f), 96f);
      this._actionExecuteHighlightLight = new ActionRepeat((ActionBase) new ActionFadeIn((DrawableControl) this._executeHighlight, TimeSpan.FromMilliseconds(600.0)));
      this._actionExecuteHighlightLight.ActionManager = this.ActionManager;
      this._actionExecuteHighlight = new ActionSequence();
      this._actionExecuteHighlight.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._executeHighlight, TimeSpan.FromMilliseconds(150.0)));
      this._actionExecuteHighlight.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._executeHighlight, TimeSpan.FromMilliseconds(150.0)));
      this._actionExecuteHighlight.ActionManager = this.ActionManager;
      foreach (object ball in this.GameScreen.Balls)
        this.ActionTargets.Add(ball);
    }

    public override void Init()
    {
      base.Init();
      this._executeHighlight.Opacity = (byte) 0;
      this._labelChainLength.SetText((object) 0);
      foreach (BallControl ball in this.GameScreen.Balls)
        ball.Highlight = false;
    }

    private void State_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State == GameState.ClearBalls)
      {
        this.LockView();
        foreach (Ball ball in (List<Ball>) this.Document.BestChain)
        {
          ActionJump action = new ActionJump((ImageControl) this.GameScreen.Balls[ball.FlatIndex]);
          action.ActionRemoved += new EventHandler(this.Action_Removed);
          this.ActionManager.Add((ActionBase) action);
          this.AddDynamicAction((ActionBase) action);
        }
      }
      else
      {
        if (this.Document.State != GameState.BonusExplode && this.Document.State != GameState.BonusSortBalls)
          return;
        this._actionExecuteHighlightLight.Stop();
        this._executeHighlight.Opacity = (byte) 0;
      }
    }

    private void Action_Removed(object sender, EventArgs e)
    {
      bool flag = true;
      foreach (ActionBase action in this.ActionManager.Actions)
      {
        if (action is ActionJump && !action.Finished)
        {
          flag = false;
          break;
        }
      }
      if (!flag)
        return;
      this.UnlockView();
    }

    private void Document_BestChainChanged(object sender, EventArgs e)
    {
      this._labelChainLength.SetText((object) (this.Document.BestChain == null ? 0 : this.Document.BestChain.Count));
      foreach (BallControl ball in this.GameScreen.Balls)
        ball.Highlight = false;
      this._actionExecuteHighlightLight.Stop();
      if (this.Document.BestChain == null)
        return;
      foreach (Ball ball in (List<Ball>) this.Document.BestChain)
        this.GameScreen.Balls[ball.FlatIndex].Highlight = true;
      this._actionExecuteHighlightLight.Start();
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      if (this.Document.BestChain == null || !this._executeClickArea.Contains(tapPos))
        return;
      this._actionExecuteHighlightLight.Stop();
      this._actionExecuteHighlight.Start();
      if (!MessageService.PlayExecuteChainSound((object) this))
        AudioManager.Play(Res.GameScreen.Sounds.ExecuteChain);
      this.Document.ExecuteChain();
    }

    protected override void UpdateCore(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder != DrawOrder.BeforeBalls)
        return;
      this._executeHighlight.Draw(spriteBatch);
      this._labelChainLength.Draw(spriteBatch);
    }

    public override void Save(SaveGame savegame)
    {
      if (this.Document.State != GameState.ClearBalls)
        return;
      base.Save(savegame);
      this.SaveActions((ControllerSave) savegame.NewController<ChainSave>((object) this), (List<ActionBase>) null);
    }

    public override void Load(SaveGame savegame)
    {
      if (this.Document.State != GameState.ClearBalls)
        return;
      base.Load(savegame);
      this._labelChainLength.SetText((object) (this.Document.BestChain == null ? 0 : this.Document.BestChain.Count));
      foreach (Ball ball in (List<Ball>) this.Document.BestChain)
        this.GameScreen.Balls[ball.FlatIndex].Highlight = true;
      ChainSave controller = savegame.GetController<ChainSave>((object) this);
      if (controller == null)
        return;
      this.LoadActions((ControllerSave) controller);
      foreach (ActionBase action in controller.Actions)
      {
        if (action.ActionId < 0)
          action.ActionRemoved += new EventHandler(this.Action_Removed);
      }
      this.LockView();
    }
  }
}
