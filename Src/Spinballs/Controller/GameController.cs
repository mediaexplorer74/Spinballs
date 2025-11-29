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
using Spinballs.Core;
using Microsoft.Xna.Framework.Input;

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
            // Обработка сенсорного ввода
            foreach (TouchLocation touchLocation in Res.Input.TouchState)
            {
                if (touchLocation.State == TouchLocationState.Pressed)
                {
                    // TouchLocation.Position приходит в физических координатах экрана,
                    // поэтому преобразуем их в игровые координаты перед обработкой.
                    Vector2 gamePos = Res.ConvertCoordinates(touchLocation.Position);
                    this.HandleTap(gamePos, gameTime);
                }
            }

            // Обработка мышиного ввода
            if (Res.Input.IsNewMouseButtonPress(MouseButtons.Left))
            {
                Vector2 mousePos = Res.GetMousePositionInGameCoords();
                this.HandleTap(mousePos, gameTime);
            }

            // Обработка клавиатурного ввода (например, для отладки или альтернативного управления)
            if (Res.Input.IsNewKeyPress(Keys.Space, new PlayerIndex?(), out PlayerIndex _))
            {
                Vector2 centerPos = new Vector2(240f, 400f); // Центр экрана
                this.HandleTap(centerPos, gameTime);
            }

            // Обработка ввода с геймпада (например, для действий меню)
            if (Res.Input.IsNewButtonPress(Buttons.A, new PlayerIndex?(), out PlayerIndex _))
            {
                Vector2 centerPos = new Vector2(240f, 400f); // Центр экрана
                this.HandleTap(centerPos, gameTime);
            }
        }
        this.UpdateCore(gameTime);
    }

    public GameDocument Document => this.GameScreen.Document;

    protected bool IsLockingView() => this.Document.StateManager.IsLocking((object) this);

    protected void LockView() => this.Document.StateManager.SetViewLock((object) this);

    protected void UnlockView() => this.Document.StateManager.FreeViewLock((object) this);
    
    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
        #if DEBUG
        System.Diagnostics.Debug.WriteLine($"GameController.HandleTap called with position: ({tapPos.X}, {tapPos.Y})");
        #endif
        base.HandleTap(tapPos, gameTime);
    }
}
}
