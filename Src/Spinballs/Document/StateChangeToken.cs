// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.StateChangeToken
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Document
{
  public class StateChangeToken : EventArgs
  {
    private List<object> _registered = new List<object>();
    private GameState _prevState;
    private GameState _nextState;
    private GameState _realPrevState;

    public StateChangeToken()
    {
    }

    public StateChangeToken(GameState prevState, GameState nextState)
    {
      this.NextState = nextState;
      this.PrevState = prevState;
    }

    public List<object> Registered
    {
      get => this._registered;
      set => this._registered = value;
    }

    public bool IsFree => this.Registered.Count == 0;

    public GameState PrevState
    {
      get => this._prevState;
      set
      {
        this._prevState = value;
        if (this._prevState == GameState.Pause)
          return;
        this.RealPrevState = this._prevState;
      }
    }

    public GameState NextState
    {
      get => this._nextState;
      set => this._nextState = value;
    }

    public GameState RealPrevState
    {
      get => this._realPrevState;
      set => this._realPrevState = value;
    }

    public bool IsLocking(object obj) => this.Registered.Contains(obj);

    public void Lock(object obj)
    {
      if (this.Registered.Contains(obj))
        return;
      this.Registered.Add(obj);
    }

    public void Free(object obj)
    {
      this.Registered.Remove(obj);
      if (this.Registered.Count != 0 || this.TokenFreed == null)
        return;
      this.TokenFreed((object) this, (EventArgs) null);
    }

    public void Clear()
    {
      this._registered.Clear();
      this._prevState = GameState.None;
      this._nextState = GameState.None;
      this._realPrevState = GameState.None;
    }

    public event EventHandler TokenFreed;
  }
}
