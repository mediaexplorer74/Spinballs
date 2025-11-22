// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.ControllerBase
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.ScreenManagement;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Controller
{
  public abstract class ControllerBase
  {
    private List<object> _actionTargets = new List<object>();
    private List<ActionBase> _actions = new List<ActionBase>();
    private List<ActionBase> _dynamicActions = new List<ActionBase>();

    public List<object> ActionTargets
    {
      get => this._actionTargets;
      set => this._actionTargets = value;
    }

    public List<ActionBase> Actions
    {
      get => this._actions;
      set => this._actions = value;
    }

    public List<ActionBase> DynamicActions
    {
      get => this._dynamicActions;
      set => this._dynamicActions = value;
    }

    public virtual ActionManager ActionManager => ScreenManager.ActiveScreen.ActionManager;

    protected virtual void AddAction(ActionBase action)
    {
      this.UpdateControlId(action);
      this.Actions.Add(action);
      action.ActionId = this.Actions.Count - 1;
    }

    protected virtual void AddDynamicAction(ActionBase action)
    {
      action.ActionFinished += new EventHandler(this.DynamicAction_ActionFinished);
      this.UpdateControlId(action);
      this.DynamicActions.Add(action);
    }

    private void DynamicAction_ActionFinished(object sender, EventArgs e)
    {
      this.DynamicActions.Remove((ActionBase) sender);
    }

    protected virtual void UpdateControlId(ActionBase action)
    {
      if (action is IActionContainer)
      {
        foreach (ActionBase action1 in ((IActionContainer) action).Actions)
          this.UpdateControlId(action1);
      }
      if (action.Target != null)
      {
        for (int index = 0; index < this.ActionTargets.Count; ++index)
        {
          if (action.Target == this.ActionTargets[index])
          {
            action.ControlId = index;
            return;
          }
        }
      }
      action.ControlId = -1;
    }

    protected virtual void UpdateTargetControl(ActionBase action)
    {
      if (action is IActionContainer)
      {
        foreach (ActionBase action1 in ((IActionContainer) action).Actions)
          this.UpdateTargetControl(action1);
      }
      if (action.ControlId < 0)
        return;
      action.Target = this.ActionTargets[action.ControlId];
    }

    public virtual void Init()
    {
    }

    public virtual void LoadContent()
    {
    }

    public virtual void UnloadContent()
    {
    }

    public virtual void Update(GameTime gameTime)
    {
      foreach (TouchLocation touchLocation in Res.Input.TouchState)
      {
        if (touchLocation.State == TouchLocationState.Pressed)
          this.HandleTap(touchLocation.Position, gameTime);
      }
      this.UpdateCore(gameTime);
    }

    public virtual void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
    }

    public virtual void Save(SaveGame savegame)
    {
    }

    public virtual void Load(SaveGame savegame)
    {
    }

    protected virtual void SaveActions(ControllerSave save, List<ActionBase> toIgnore)
    {
      foreach (ActionBase action in this.Actions)
      {
        if ((toIgnore == null || !toIgnore.Contains(action)) && action.IsRunning)
          save.Actions.Add(action);
      }
      foreach (ActionBase dynamicAction in this.DynamicActions)
      {
        if ((toIgnore == null || !toIgnore.Contains(dynamicAction)) && dynamicAction.IsRunning)
          save.Actions.Add(dynamicAction);
      }
    }

    protected virtual void LoadActions(ControllerSave save)
    {
      foreach (ActionBase action1 in save.Actions)
      {
        if (action1.ActionId >= 0)
        {
          ActionBase action2 = this.Actions[action1.ActionId];
          action2.Init(action1);
          this.UpdateTargetControl(action2);
          this.ActionManager.Add(action2);
        }
        else
        {
          this.UpdateTargetControl(action1);
          action1.Init(action1);
          this.AddDynamicAction(action1);
          this.ActionManager.Add(action1);
        }
      }
    }

    protected abstract void UpdateCore(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder);
  }
}
