// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.MainExtraController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Controller.Extra
{
  public class MainExtraController : GameController
  {
    private List<BaseExtraController> _controllerList;

    public MainExtraController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this._controllerList = new List<BaseExtraController>();
      this._controllerList.Add((BaseExtraController) new ExtraExplodeController(gameScreen));
      this._controllerList.Add((BaseExtraController) new ExtraPointsController(gameScreen));
      this._controllerList.Add((BaseExtraController) new ExtraSortController(gameScreen));
      this._controllerList.Add((BaseExtraController) new ExtraTimeController(gameScreen));
      this.Document.BestChainChanged += new EventHandler(this.Document_BestChainChanged);
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      MessageService.Message += new MessageHandler(this.MessageService_Message);
    }

    private void MessageService_Message(object sender, MessageArgs args)
    {
      if (args.Message != Spinballs.Document.Message.PlayExecuteChainSound)
        return;
      foreach (BaseExtraController controller in this._controllerList)
      {
        if (controller.IsPartOfBestChain && !controller.IsFull)
        {
          args.Handled = true;
          break;
        }
      }
    }

    private void Document_BestChainChanged(object sender, EventArgs e)
    {
      foreach (BaseExtraController controller in this._controllerList)
        controller.OnBestChainChanged();
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State == GameState.ClearBalls)
      {
        int num1 = 0;
        foreach (BaseExtraController controller in this._controllerList)
        {
          if (!controller.IsFull && controller.IsPartOfBestChain)
          {
            controller.LoadControl.BlinkMode = BlinkMode.Blink;
            ++num1;
          }
        }
        if (num1 <= 0)
          return;
        int num2 = this.Document.BestChain.Count / num1;
        foreach (BaseExtraController controller in this._controllerList)
        {
          if (!controller.IsFull && controller.IsPartOfBestChain)
          {
            controller.FillStartLoadValue = controller.LoadValue;
            controller.FillEndLoadValue = controller.LoadValue + (float) num2;
          }
        }
        this.LockView();
        ActionDuration action = new ActionDuration(TimeSpan.FromMilliseconds(1000.0));
        action.Action += new DurationEventHandler(this.ActionFill_Action);
        action.ActionFinished += new EventHandler(this.ActionFill_ActionFinished);
        this.ActionManager.Add((ActionBase) action);
        AudioManager.Play(Res.GameScreen.Sounds.ExtraLoading);
      }
      else
      {
        if (this.Document.State != GameState.Running)
          return;
        foreach (BaseExtraController controller in this._controllerList)
          controller.LoadControl.BlinkMode = !controller.Active ? (!controller.IsFull ? BlinkMode.None : BlinkMode.Pulse) : BlinkMode.Highlight;
      }
    }

    private void ActionFill_Action(ActionDuration sender, DurationEventArgs args)
    {
      foreach (BaseExtraController controller in this._controllerList)
      {
        if (controller.IsPartOfBestChain)
          controller.LoadValue = controller.FillStartLoadValue + (controller.FillEndLoadValue - controller.FillStartLoadValue) * args.Fraction;
      }
    }

    private void ActionFill_ActionFinished(object sender, EventArgs e)
    {
      foreach (BaseExtraController controller in this._controllerList)
      {
        if (controller.IsPartOfBestChain)
          controller.LoadValue = controller.FillEndLoadValue;
      }
      this.UnlockView();
    }

    public override void LoadContent()
    {
      base.LoadContent();
      foreach (ControllerBase controller in this._controllerList)
        controller.LoadContent();
    }

    public override void Init()
    {
      base.Init();
      foreach (ControllerBase controller in this._controllerList)
        controller.Init();
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      foreach (ControllerBase controller in this._controllerList)
        controller.HandleTap(tapPos, gameTime);
    }

    protected override void UpdateCore(GameTime gameTime)
    {
      foreach (ControllerBase controller in this._controllerList)
        controller.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      foreach (ControllerBase controller in this._controllerList)
        controller.Draw(spriteBatch, drawOrder);
    }

    public override void Save(SaveGame savegame)
    {
      base.Save(savegame);
      foreach (ControllerBase controller in this._controllerList)
        controller.Save(savegame);
    }

    public override void Load(SaveGame savegame)
    {
      foreach (ControllerBase controller in this._controllerList)
        controller.Load(savegame);
    }
  }
}
