// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionRepeat
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionRepeat : ActionBase
  {
    private ActionBase _action;
    private int _repeatCount;
    private int _currentRepeatCount;

    public ActionRepeat(ActionBase action)
    {
      this.Action = action;
      this.RepeatCount = 0;
      this._currentRepeatCount = 0;
    }

    public ActionRepeat(ActionBase action, int repeatCount)
    {
      this.Action = action;
      this.RepeatCount = repeatCount;
      this._currentRepeatCount = 0;
    }

    [DataMember]
    public ActionBase Action
    {
      get => this._action;
      set => this._action = value;
    }

    [DataMember]
    public int RepeatCount
    {
      get => this._repeatCount;
      set => this._repeatCount = value;
    }

    [DataMember]
    public int CurrentRepeatCount
    {
      get => this._currentRepeatCount;
      set => this._currentRepeatCount = value;
    }

    public override bool Update(GameTime gameTime)
    {
      if (this.Action.Finished)
      {
        if (this.RepeatCount > 0)
        {
          if (this._currentRepeatCount >= this.RepeatCount - 1)
          {
            this.Finished = true;
            return this.Finished;
          }
          ++this._currentRepeatCount;
        }
        this.Action.Reset();
      }
      this.Action.Update(gameTime);
      return false;
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      if (!(action is ActionRepeat actionRepeat))
        return;
      if (this.Action != null && actionRepeat.Action != null)
        this.Action.Init(actionRepeat.Action);
      else
        this.Action = actionRepeat.Action;
      this.RepeatCount = actionRepeat.RepeatCount;
      this.CurrentRepeatCount = actionRepeat.CurrentRepeatCount;
    }

    public override void Reset()
    {
      this.Action.Reset();
      this._currentRepeatCount = 0;
      this.Finished = false;
    }
  }
}
