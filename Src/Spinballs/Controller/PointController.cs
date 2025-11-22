// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.PointController
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
  public class PointController : GameController
  {
    private PointControl _pointControl1;
    private PointControl _pointControl2;
    private LabelControl _labelPoints;
    private int _currentShownPoints;
    private int _showPointsStepOffset = 10;
    private ImageControl _x2;
    private ActionDuration _actionudPointDuration;
    private ActionRepeat _actionHudPointRepeat;
    private TimeSpan _fadeInDuration = TimeSpan.FromMilliseconds(300.0);
    private TimeSpan _fadeOutDuration = TimeSpan.FromMilliseconds(1000.0);
    private Vector2 _floatingPoint1Pos;

    public PointController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this._pointControl1 = new PointControl();
      this._pointControl2 = new PointControl();
      this._labelPoints = new LabelControl();
      this._currentShownPoints = 0;
      this.Document.PointsChanged += new GameDocument.AddPointHandler(this.Document_PointsChanged);
      MessageService.Message += new MessageHandler(this.MessageService_Message);
    }

    public override void Init()
    {
      base.Init();
      this._labelPoints.SetText((object) this.Document.Points);
      this._currentShownPoints = this.Document.Points;
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._pointControl1.Visible = false;
      this._pointControl2.Visible = false;
      this.ActionTargets.Add((object) this._pointControl1);
      this.ActionTargets.Add((object) this._pointControl2);
      this._x2 = new ImageControl(Res.GameScreen.ExtraFloatingX2);
      this._x2.Opacity = (byte) 0;
      this.ActionTargets.Add((object) this._x2);
      this._actionudPointDuration = new ActionDuration(TimeSpan.FromMilliseconds(1250.0));
      this._actionHudPointRepeat = new ActionRepeat((ActionBase) this._actionudPointDuration, 10);
      this._actionudPointDuration.ActionFinished += new EventHandler(this.ActionDuration_ActionFinished);
      this._actionHudPointRepeat.ActionFinished += new EventHandler(this.ActionRepeat_ActionFinished);
      this._actionHudPointRepeat.ActionManager = this.GameScreen.ActionManager;
      this.AddAction((ActionBase) this._actionHudPointRepeat);
      this._labelPoints.DisplayRect = Layout.TextPoints;
      this._labelPoints.Orientation = Orientation.Right | Orientation.VerticalCenter;
      this.Document_PointsChanged((object) null, (GameDocument.AddPointArgs) null);
    }

    private void Document_PointsChanged(object sender, GameDocument.AddPointArgs e)
    {
      if (this.Document.State == GameState.ClearBalls)
      {
        if (this.Document.Points == this._currentShownPoints)
          return;
        this.LockView();
        int num1 = e.Offset + e.ExtraOffset;
        int num2 = num1 <= 300 ? num1 / 10 : 30;
        this._showPointsStepOffset = num1 / num2;
        this._actionudPointDuration.Duration = TimeSpan.FromMilliseconds((double) (500 / num2));
        this._actionHudPointRepeat.RepeatCount = num2;
        this._actionHudPointRepeat.Start();
        this.ShowFloatingPointInfo(e.Offset, 1, new TimeSpan());
      }
      else
      {
        this._labelPoints.SetText((object) this.Document.Points);
        this._currentShownPoints = this.Document.Points;
      }
    }

    private void MessageService_Message(object sender, MessageArgs args)
    {
      if (args.Message != Spinballs.Document.Message.ShowExtraPoints)
        return;
      ValueArgs<int> valueArgs = (ValueArgs<int>) args;
      int num1 = 150;
      if ((double) (this.GameScreen.GetBestChainCenter() - new Vector2((float) (Res.GameScreen.Points.Width / 2), 0.0f)).X > (double) this.GameScreen.Size.X / 2.0)
      {
        int num2 = num1 * -1;
      }
      this.ShowFloatingPointInfo(valueArgs.Value, 2, TimeSpan.FromMilliseconds(150.0));
      this.ShowX2();
      args.Handled = true;
    }

    private void ShowX2()
    {
      if ((double) this._floatingPoint1Pos.X + (double) (Layout.PointWidth * 2) + 20.0 > (double) this.GameScreen.Size.X)
        this._x2.Position = this._floatingPoint1Pos + Layout.ExtraPointsX2OffsetLeft;
      else
        this._x2.Position = this._floatingPoint1Pos + Layout.ExtraPointsX2Offset;
      ActionMoveLinear actionMoveLinear = new ActionMoveLinear(this._x2, this._x2.Position + new Vector2(0.0f, 35f), this._fadeOutDuration + this._fadeInDuration);
      ActionSequence actionSequence = new ActionSequence();
      actionSequence.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._x2, this._fadeInDuration));
      actionSequence.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._x2, this._fadeOutDuration));
      ActionParallel action = new ActionParallel();
      action.Actions.Add((ActionBase) actionSequence);
      action.Actions.Add((ActionBase) actionMoveLinear);
      this.ActionManager.Add((ActionBase) action);
      this.AddDynamicAction((ActionBase) action);
    }

    public void ShowFloatingPointInfo(int value, int posOffset, TimeSpan delay)
    {
      int pointWidth = Layout.PointWidth;
      PointControl control = posOffset == 1 ? this._pointControl1 : this._pointControl2;
      control.Visible = true;
      control.Points = value;
      control.Create();
      if (posOffset == 1)
      {
        control.Position = this.GameScreen.GetBestChainCenter() - new Vector2((float) (pointWidth / 2), 0.0f);
        if ((double) control.Position.X + (double) pointWidth > (double) this.GameScreen.Size.X)
          control.Position = new Vector2((float) ((double) this.GameScreen.Size.X - (double) pointWidth - 10.0), control.Position.Y);
        if ((double) control.Position.X < 0.0)
          control.Position = new Vector2(10f, control.Position.Y);
        this._floatingPoint1Pos = control.Position;
      }
      else if ((double) this._floatingPoint1Pos.X + (double) (pointWidth * 2) + 20.0 > (double) this.GameScreen.Size.X)
        control.Position = new Vector2((float) ((double) this._floatingPoint1Pos.X - (double) pointWidth - 20.0), this._floatingPoint1Pos.Y);
      else
        control.Position = new Vector2(this._floatingPoint1Pos.X + (float) pointWidth, this._floatingPoint1Pos.Y);
      control.Opacity = (byte) 0;
      ActionMoveLinear actionMoveLinear = new ActionMoveLinear((ImageControl) control, control.Position + new Vector2(0.0f, 35f), this._fadeOutDuration + this._fadeInDuration);
      ActionSequence actionSequence = new ActionSequence();
      if (delay.TotalMilliseconds > 0.0)
        actionSequence.Actions.Add((ActionBase) new ActionDuration(delay));
      actionSequence.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) control, this._fadeInDuration));
      actionSequence.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) control, this._fadeOutDuration));
      ActionParallel action = new ActionParallel();
      action.Actions.Add((ActionBase) actionSequence);
      action.Actions.Add((ActionBase) actionMoveLinear);
      this.ActionManager.Add((ActionBase) action);
      this.AddDynamicAction((ActionBase) action);
    }

    private void ActionRepeat_ActionFinished(object sender, EventArgs e) => this.UnlockView();

    private void ActionDuration_ActionFinished(object sender, EventArgs e)
    {
      this._currentShownPoints += this._showPointsStepOffset;
      this._labelPoints.SetText((object) this._currentShownPoints);
    }

    protected override void UpdateCore(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder == DrawOrder.BeforeBalls)
      {
        this._labelPoints.Draw(spriteBatch);
      }
      else
      {
        if (drawOrder != DrawOrder.AfterBalls)
          return;
        this._pointControl1.Draw(spriteBatch);
        this._pointControl2.Draw(spriteBatch);
        this._x2.Draw(spriteBatch);
      }
    }

    public override void Save(SaveGame savegame) => base.Save(savegame);

    public override void Load(SaveGame savegame)
    {
      base.Load(savegame);
      this._currentShownPoints = this.Document.Points;
      this._labelPoints.SetText((object) this._currentShownPoints);
    }
  }
}
