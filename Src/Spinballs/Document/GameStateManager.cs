// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.GameStateManager
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Common.Helper;
using System;

#nullable disable
namespace Spinballs.Document
{
  public class GameStateManager
  {
    private GameState _state;
    private StateChangeToken _waitForViews;

    public event GameStateChangeHandler Changing;

    public event GameStateChangeHandler Changed;

    public event EventHandler QueryNextState;

    public GameStateManager()
    {
      this._waitForViews = new StateChangeToken();
      this._waitForViews.TokenFreed += new EventHandler(this.WaitForView_TokenFreed);
      this._state = GameState.None;
    }

    public GameState State
    {
      get => this._state;
      set => this.Change(value);
    }

    public void Reset()
    {
      this._state = GameState.None;
      this._waitForViews.Clear();
    }

    public void SetViewLock(object obj) => this._waitForViews.Lock(obj);

    public void FreeViewLock(object obj) => this._waitForViews.Free(obj);

    public bool IsLocking(object obj) => this._waitForViews.IsLocking(obj);

    public void Change(GameState newState)
    {
      if (!this._waitForViews.IsFree && this._waitForViews.NextState != GameState.None)
      {
        if (this._waitForViews.NextState == GameState.Running && newState == GameState.Pause)
          this._waitForViews.NextState = newState;
      }
      else
        this.ChangeCore(newState);
    }

    private void ChangeCore(GameState newState)
    {
      this._waitForViews.PrevState = this._state;
      this._waitForViews.NextState = newState;
      if (!this._waitForViews.IsFree)
        return;
      if (this.Changing != null)
        this.Changing((object) this, this._waitForViews);
      this._state = newState;
      this._waitForViews.NextState = GameState.None;
      if (this.Changed != null)
        this.Changed((object) this, this._waitForViews);
      if (this._waitForViews.NextState != GameState.None)
        return;
      this.QueryNextState((object) this, (EventArgs) null);
    }

    private void WaitForView_TokenFreed(object sender, EventArgs e)
    {
    }

    public void Update(GameTime gameTime)
    {
      if (!this._waitForViews.IsFree || this._waitForViews.NextState == GameState.None)
        return;
      this.ChangeCore(this._waitForViews.NextState);
    }

    public void Save(SaveGame savegame)
    {
      savegame.State = GameState.Pause;
      savegame.PrevState = GameState.None;
      savegame.NextState = GameState.Running;
    }

    public void Load(SaveGame savegame)
    {
      this._state = savegame.State;
      this._waitForViews.PrevState = savegame.PrevState;
      this._waitForViews.NextState = savegame.NextState;
    }
  }
}
