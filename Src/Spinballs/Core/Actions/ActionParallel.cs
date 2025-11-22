// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionParallel
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionParallel : ActionBase, IActionContainer
  {
    private List<ActionBase> _actions = new List<ActionBase>();

    [DataMember]
    public List<ActionBase> Actions
    {
      get => this._actions;
      set => this._actions = value;
    }

    public override bool Update(GameTime gameTime)
    {
      bool flag = false;
      foreach (ActionBase action in this.Actions)
      {
        if (!action.Finished)
        {
          action.Update(gameTime);
          flag = true;
        }
      }
      if (!flag)
        this.Finished = true;
      return this.Finished;
    }

    public override void Reset()
    {
      foreach (ActionBase action in this.Actions)
        action.Reset();
      this.Finished = false;
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      if (!(action is IActionContainer actionContainer) || this.Actions.Count != actionContainer.Actions.Count)
        return;
      for (int index = 0; index < this.Actions.Count; ++index)
        this.Actions[index].Init(actionContainer.Actions[index]);
    }
  }
}
