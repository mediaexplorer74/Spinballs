// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.ExtraSortController
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
  public class ExtraSortController : BaseExtraController
  {
    private ExtraSortController.SortItem[] _sortItems = new ExtraSortController.SortItem[Layout.DiscCount];
    private List<KeyValuePair<Ball, Ball>> _ballSwitches = new List<KeyValuePair<Ball, Ball>>();

    public ExtraSortController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen, Layout.ExtraSortPos)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      for (int index = 0; index < Layout.DiscCount; ++index)
        this._sortItems[index] = new ExtraSortController.SortItem();
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this.LoadControl = new ExtraLoadControl(Res.GameScreen.ExtraSort);
      this._connNorth.Position = Layout.BonusConnector[4];
      this._connNorth.Effects = SpriteEffects.FlipVertically;
      this._connNorthWest.Position = Layout.BonusConnector[5];
      this._connNorthWest.Effects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[3][3], this._connNorth, this.ActionManager));
      this.Connections.Add(new BaseExtraController.ConnectionDescriptor(this.Document.Discs[4][2], this._connNorthWest, this.ActionManager));
    }

    protected override bool UpdateExtraController()
    {
      return this.Document.State == GameState.BonusSortBalls;
    }

    protected override void Execute()
    {
      base.Execute();
      foreach (BallControl ball in this.GameScreen.Balls)
        ball.Highlight = false;
      this.Document.State = GameState.BonusSortBalls;
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.BonusSortBalls)
        return;
      AudioManager.Play(Res.GameScreen.Sounds.Sort);
      if (!this.GetSortBalls())
        return;
      this.LockView();
    }

    private void SwitchBalls(Ball ball1, Ball ball2, TimeSpan delay, ActionParallel par)
    {
      this._ballSwitches.Add(new KeyValuePair<Ball, Ball>(ball1, ball2));
      BallControl ball3 = this.GameScreen.Balls[ball1.FlatIndex];
      BallControl ball4 = this.GameScreen.Balls[ball2.FlatIndex];
      ActionSequence actionSequence = new ActionSequence();
      if (delay.TotalMilliseconds > 0.0)
        actionSequence.Actions.Add((ActionBase) new ActionDuration(delay));
      actionSequence.Actions.Add((ActionBase) new ActionSwitchCircle((ImageControl) ball3, (ImageControl) ball4, TimeSpan.FromMilliseconds(300.0)));
      par.Actions.Add((ActionBase) actionSequence);
    }

    private void Action_ActionFinished(object sender, EventArgs e)
    {
      foreach (KeyValuePair<Ball, Ball> ballSwitch in this._ballSwitches)
      {
        Ball key = ballSwitch.Key;
        Ball ball1 = ballSwitch.Value;
        BallControl ball2 = this.GameScreen.Balls[key.FlatIndex];
        BallControl ball3 = this.GameScreen.Balls[ball1.FlatIndex];
        this.GameScreen.Balls[key.FlatIndex] = ball3;
        this.GameScreen.Balls[ball1.FlatIndex] = ball2;
        BallColors color = ball1.Color;
        ball1.Color = key.Color;
        key.Color = color;
      }
      this._ballSwitches.Clear();
      AudioManager.Play(Res.GameScreen.Sounds.DiscTurn);
      if (this.GetSortBalls())
        return;
      this.LoadControl.BlinkMode = BlinkMode.None;
      this.LoadValue = 0.0f;
      this.UnlockView();
    }

    private bool GetSortBalls()
    {
      ActionParallel actionParallel = new ActionParallel();
      foreach (Disc disc in this.Document.Discs)
      {
        ExtraSortController.SortItem sortItem = this._sortItems[disc.DiscIndex];
        if (sortItem.lSteps > 0)
        {
          --sortItem.lSteps;
          sortItem.pBallA = sortItem.pBallA.Next;
          sortItem.pBallB = sortItem.pBallA.Next;
          this.SwitchBalls(sortItem.pBallA, sortItem.pBallB, new TimeSpan(), actionParallel);
        }
        else
        {
          sortItem.pBallA = this.GetFirstUnsortedBall(disc.DiscIndex);
          if (sortItem.pBallA != null)
          {
            sortItem.pBallB = sortItem.pBallA.Next;
            sortItem.lSteps = this.GetSortStepCount(sortItem.pBallA);
            this.SwitchBalls(sortItem.pBallA, sortItem.pBallB, new TimeSpan(), actionParallel);
          }
        }
      }
      if (actionParallel.Actions.Count == 0)
        return false;
      this.ActionManager.Add((ActionBase) actionParallel);
      actionParallel.ActionFinished += new EventHandler(this.Action_ActionFinished);
      return true;
    }

    private Ball GetFirstUnsortedBall(int discIndex)
    {
      Disc disc = this.Document.Discs[discIndex];
      Dictionary<BallColors, int> dictionary1 = new Dictionary<BallColors, int>();
      foreach (Ball ball in disc.Balls)
      {
        if (dictionary1.ContainsKey(ball.Color))
        {
          Dictionary<BallColors, int> dictionary2;
          BallColors color;
          (dictionary2 = dictionary1)[color = ball.Color] = dictionary2[color] + 1;
        }
        else
          dictionary1[ball.Color] = 1;
      }
      if (dictionary1.Count == 1)
        return (Ball) null;
      foreach (KeyValuePair<BallColors, int> keyValuePair in dictionary1)
      {
        if (keyValuePair.Value > 1)
        {
          for (int index = 0; index < disc.Balls.Length; ++index)
          {
            Ball ball = disc.Balls[index];
          }
          foreach (Ball ball1 in disc.Balls)
          {
            if (ball1.Color == keyValuePair.Key)
            {
              Ball firstUnsortedBall = ball1;
              while (firstUnsortedBall.Color == firstUnsortedBall.Previous.Color)
                firstUnsortedBall = firstUnsortedBall.Previous;
              int num = 0;
              while (firstUnsortedBall.Color == firstUnsortedBall.Next.Color)
              {
                firstUnsortedBall = firstUnsortedBall.Next;
                ++num;
              }
              if (num != keyValuePair.Value - 1)
              {
                if (num == 0)
                  return firstUnsortedBall;
                Ball ball2 = firstUnsortedBall;
                Ball next = firstUnsortedBall.Next;
                while (ball2.Color != next.Color)
                  next = next.Next;
                while (next.Color == next.Next.Color)
                  next = next.Next;
                return next;
              }
              break;
            }
          }
        }
      }
      return (Ball) null;
    }

    private int GetSortStepCount(Ball ball)
    {
      int sortStepCount = 0;
      Ball ball1 = ball;
      while (ball1.Next.Color != ball.Color)
      {
        ball1 = ball1.Next;
        ++sortStepCount;
      }
      return sortStepCount;
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

    private class SortItem
    {
      public Ball pBallA;
      public Ball pBallB;
      public int lSteps;
    }
  }
}
