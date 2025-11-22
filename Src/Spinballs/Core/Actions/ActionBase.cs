// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionBase
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [KnownType(typeof (ActionRepeat))]
  [KnownType(typeof (ActionJump))]
  [KnownType(typeof (ActionParallel))]
  [KnownType(typeof (ActionSequence))]
  [KnownType(typeof (ActionSound))]
  [KnownType(typeof (ActionSwitchCircle))]
  [KnownType(typeof (ImageAction))]
  [DataContract]
  [KnownType(typeof (ActionMessage))]
  [KnownType(typeof (ActionMoveInCircle))]
  [KnownType(typeof (ActionMoveLinear))]
  [KnownType(typeof (ActionMusicFade))]
  [KnownType(typeof (ActionBlink))]
  [KnownType(typeof (ActionDuration))]
  [KnownType(typeof (ActionFadeIn))]
  [KnownType(typeof (ActionFadeOut))]
  public class ActionBase
  {
    private int _controlId = -1;
    private int _actionId = -1;
    private object _target;
    private bool _finished;
    private ActionManager _actionManager;

    public event EventHandler ActionFinished;

    public event EventHandler ActionRemoved;

    public void OnActionRemoved()
    {
      if (this.ActionRemoved == null)
        return;
      this.ActionRemoved((object) this, (EventArgs) null);
    }

    public ActionManager ActionManager
    {
      get => this._actionManager;
      set => this._actionManager = value;
    }

    [DataMember]
    public bool Finished
    {
      get => this._finished;
      set
      {
        if (this._finished == value)
          return;
        this._finished = value;
        if (!this._finished || this.ActionFinished == null)
          return;
        this.ActionFinished((object) this, (EventArgs) null);
      }
    }

    [DataMember]
    public int ControlId
    {
      get => this._controlId;
      set => this._controlId = value;
    }

    [DataMember]
    public int ActionId
    {
      get => this._actionId;
      set => this._actionId = value;
    }

    public object Target
    {
      get => this._target;
      set => this._target = value;
    }

    public virtual void Init(ActionBase action)
    {
      this.Target = action.Target;
      this.Finished = action.Finished;
    }

    public bool IsRunning => this.ActionManager != null && this.ActionManager.Contains(this);

    public void Start()
    {
      if (this.ActionManager == null)
        return;
      this.Reset();
      if (this.IsRunning)
        return;
      this.ActionManager.Add(this);
    }

    public void Stop()
    {
      if (this.ActionManager == null)
        return;
      this.ActionManager.Remove(this);
      this.Reset();
    }

    public void Pause()
    {
      if (this.ActionManager == null)
        return;
      if (this.IsRunning)
        this.ActionManager.Remove(this);
      else
        this.ActionManager.Add(this);
    }

    public virtual bool Update(GameTime gameTime) => this.Finished;

    public virtual void Reset()
    {
    }
  }
}
