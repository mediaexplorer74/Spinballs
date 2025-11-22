// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.GameController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Document;

#nullable disable
namespace Spinballs.Controller
{
  public abstract class GameController : ControllerBase
  {
    protected Spinballs.View.GameScreen _gameScreen;

    public GameController(Spinballs.View.GameScreen gameScreen) => this._gameScreen = gameScreen;

    public Spinballs.View.GameScreen GameScreen
    {
      get => this._gameScreen;
      set => this._gameScreen = value;
    }

    public override ActionManager ActionManager => this.GameScreen.ActionManager;

    public override void Update(GameTime gameTime)
    {
      if (this.GameScreen.Document.State == GameState.Running)
      {
        foreach (TouchLocation touchLocation in Res.Input.TouchState)
        {
          if (touchLocation.State == TouchLocationState.Pressed)
            this.HandleTap(touchLocation.Position, gameTime);
        }
      }
      this.UpdateCore(gameTime);
    }

    public GameDocument Document => this.GameScreen.Document;

    protected bool IsLockingView() => this.Document.StateManager.IsLocking((object) this);

    protected void LockView() => this.Document.StateManager.SetViewLock((object) this);

    protected void UnlockView() => this.Document.StateManager.FreeViewLock((object) this);
  }
}
