// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionManager
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionManager
  {
    private bool _actionListLocked;
    private List<ActionBase> _actions = new List<ActionBase>();

    public ReadOnlyCollection<ActionBase> Actions => this._actions.AsReadOnly();

    public void Update(GameTime gameTime)
    {
      int index = 0;
      this._actionListLocked = true;
      while (index < this._actions.Count)
      {
        ActionBase action = this._actions[index];
        if (action.Update(gameTime))
        {
          this._actions.RemoveAt(index);
          action.OnActionRemoved();
        }
        else
          ++index;
      }
      this._actionListLocked = false;
    }

    public ActionBase Add(ActionBase action)
    {
      this._actions.Add(action);
      action.ActionManager = this;
      return action;
    }

    public bool Remove(ActionBase action)
    {
      return !this._actionListLocked && this._actions.Remove(action);
    }

    public void Clear() => this._actions.Clear();

    public bool Contains(ActionBase action) => this._actions.Contains(action);
  }
}
