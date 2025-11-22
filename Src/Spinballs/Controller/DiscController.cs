// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.DiscController
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
  public class DiscController : GameController
  {
    private ImageControl[] _discHighlights = new ImageControl[Layout.DiscCount * 2];
    private Dictionary<object, int> _rotateDiscIndex = new Dictionary<object, int>();

    public DiscController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      for (int index = 0; index < this._discHighlights.Length; ++index)
        this._discHighlights[index] = new ImageControl();
      this.GameScreen.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      MessageService.Message += new MessageHandler(this.MessageService_Message);
    }

    private void MessageService_Message(object sender, MessageArgs args)
    {
      if (args.Message != Spinballs.Document.Message.HighlightDisc)
        return;
      ValueArgs<int> valueArgs = (ValueArgs<int>) args;
      this.HighlightDisc(valueArgs.Value * 2);
      this.HighlightDisc(valueArgs.Value * 2 + 1);
      args.Handled = true;
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.GameScreen.Document.State != GameState.ClearBalls)
        return;
      foreach (Disc disc in this.GameScreen.Document.BestChain.Discs)
      {
        this.HighlightDisc(disc.DiscIndex * 2);
        this.HighlightDisc(disc.DiscIndex * 2 + 1);
      }
    }

    public ImageControl[] DiscHighlights => this._discHighlights;

    public override void LoadContent()
    {
      base.LoadContent();
      for (int index = 0; index < this._discHighlights.Length; ++index)
      {
        this._discHighlights[index].Texture = Res.GameScreen.DiscHighlight;
        this._discHighlights[index].Position = Layout.DiscsCenter + Layout.DiscOffset[index / 2] + new Vector2(-13f, (float) -(this._discHighlights[index].Texture.Height / 2));
        this._discHighlights[index].Visible = false;
        if (index % 2 == 0)
        {
          this._discHighlights[index].Position = Layout.DiscsCenter + Layout.DiscOffset[index / 2] + Layout.DiscHighlightOffset[1];
        }
        else
        {
          this._discHighlights[index].Position = Layout.DiscsCenter + Layout.DiscOffset[index / 2] + Layout.DiscHighlightOffset[0];
          this._discHighlights[index].Effects = SpriteEffects.FlipHorizontally;
        }
      }
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      for (int discIndex = 0; discIndex < Layout.DiscCount; ++discIndex)
      {
        if (this.DiscContains(discIndex, tapPos))
        {
          if ((double) tapPos.X <= (double) Layout.GetDiscCenter(discIndex).X)
          {
            this.RotateDisc(discIndex, false);
            this.HighlightDisc(discIndex * 2 + 1);
          }
          else
          {
            this.RotateDisc(discIndex, true);
            this.HighlightDisc(discIndex * 2);
          }
        }
      }
    }

    protected override void UpdateCore(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder != DrawOrder.BeforeBalls)
        return;
      for (int index = 0; index < this._discHighlights.Length; ++index)
        this._discHighlights[index].Draw(spriteBatch);
    }

    private bool DiscContains(int discIndex, Vector2 pos)
    {
      return new Circle(Layout.GetDiscCenter(discIndex), (float) Layout.DiscBoundsRadius).Contains(pos);
    }

    private void RotateDisc(int discIndex, bool clockwise)
    {
      Circle circle = new Circle(Layout.GetDiscCenter(discIndex) + new Vector2(-15f, -15f), (float) Layout.DiscRadius);
      BallControl[] balls = this.GameScreen.Balls;
      int index1 = discIndex * Layout.BallsPerDisc;
      for (int index2 = 0; index2 < Layout.BallsPerDisc; ++index2)
      {
        int index3 = index1 + index2;
        int startAngle = Layout.BallAngle[index2];
        int angleOffset = clockwise ? 60 : -60;
        ActionMoveInCircle key = new ActionMoveInCircle((ImageControl) balls[index3], circle, startAngle, angleOffset, TimeSpan.FromMilliseconds(200.0));
        key.ActionManager = this.ActionManager;
        key.ActionFinished += new EventHandler(this.action_ActionFinished);
        this._rotateDiscIndex.Add((object) key, discIndex);
        key.Start();
      }
      if (clockwise)
      {
        BallControl ballControl = balls[index1 + Layout.BallsPerDisc - 1];
        for (int index4 = Layout.BallsPerDisc - 1; index4 > 0; --index4)
          balls[index1 + index4] = balls[index1 + index4 - 1];
        balls[index1] = ballControl;
      }
      else
      {
        BallControl ballControl = balls[index1];
        for (int index5 = 0; index5 < Layout.BallsPerDisc - 1; ++index5)
          balls[index1 + index5] = balls[index1 + index5 + 1];
        balls[index1 + Layout.BallsPerDisc - 1] = ballControl;
      }
      AudioManager.Play(Res.GameScreen.Sounds.DiscTurn);
      this.GameScreen.Document.RotateDisc(discIndex, clockwise);
    }

    private void action_ActionFinished(object sender, EventArgs e)
    {
      int index = this._rotateDiscIndex[sender];
      this._rotateDiscIndex.Remove(sender);
      foreach (Ball ball in this.Document.Discs[index].Balls)
        this.GameScreen.Balls[ball.FlatIndex].Position = Layout.GetBallPosition(ball.FlatIndex);
    }

    private void HighlightDisc(int discIndex)
    {
      this.ActionManager.Add((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionFadeIn((DrawableControl) this.DiscHighlights[discIndex], TimeSpan.FromMilliseconds(100.0)),
          (ActionBase) new ActionFadeOut((DrawableControl) this.DiscHighlights[discIndex], TimeSpan.FromMilliseconds(100.0))
        }
      });
    }
  }
}
