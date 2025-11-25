// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.ScreenManagement.BaseScreen
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;

#nullable disable
namespace Spinballs.Core.ScreenManagement
{
  public abstract class BaseScreen : ImageControl
  {
    protected int _id;
    private bool _contentLoaded;
    protected ActionManager _actionManager;
    private PanelDarken _panelDarken;
    private ScreenManager _manager;
    private bool _isNewgame = true;

    public bool IsNewGame
    {
      get => this._isNewgame;
      set => this._isNewgame = value;
    }

    public BaseScreen()
    {
      this._id = 0;
      this._actionManager = new ActionManager();
      this._contentLoaded = false;
    }

    public int Id => this._id;

    public override ActionManager ActionManager
    {
      get => this._actionManager;
      set
      {
      }
    }

    public bool ContentLoaded
    {
      get
      {
        lock (this)
          return this._contentLoaded;
      }
      set
      {
        lock (this)
          this._contentLoaded = value;
      }
    }

    public PanelDarken Darken
    {
      get => this._panelDarken;
      set => this._panelDarken = value;
    }

    public ScreenManager Manager
    {
      get => this._manager;
      set => this._manager = value;
    }

    public virtual void LoadContent()
    {
      this._panelDarken = new PanelDarken();
      this._panelDarken.Size = new Vector2((float) Res.Game.GraphicsDevice.Viewport.Width, (float) Res.Game.GraphicsDevice.Viewport.Height);
      this._panelDarken.Create();
      this._panelDarken.Opacity = (byte) 0;
      this.ContentLoaded = true;
    }

    public virtual void UnloadContent()
    {
      this.ActionManager.Clear();
      this.ContentLoaded = false;
    }

    public virtual void Update(GameTime gameTime)
    {
        if (this.Enabled && Res.Input.IsNewButtonPress(Buttons.Back, new PlayerIndex?(), out PlayerIndex _))
            this.OnBackButton(gameTime);
        this._actionManager.Update(gameTime);
        
        // Обработка мышиных событий
        if (this.Enabled && Res.Input.IsNewMouseButtonPress(MouseButtons.Left))
        {
            Vector2 mousePos = Res.GetMousePositionInGameCoords();
            #if DEBUG
            System.Diagnostics.Debug.WriteLine($"BaseScreen processing mouse tap at game coords: ({mousePos.X}, {mousePos.Y})");
            #endif
            this.HandleTap(mousePos, gameTime);
        }
        
        // Обработка клавиатурных событий
        if (this.Enabled)
        {
            PlayerIndex playerIndex;
            if (Res.Input.IsNewKeyPress(Keys.Space, new PlayerIndex?(), out playerIndex))
            {
                #if DEBUG
                System.Diagnostics.Debug.WriteLine("Space key pressed");
                #endif
                // Обработка нажатия пробела
                this.HandleTap(new Vector2(240, 400), gameTime); // Центр экрана
            }
            else if (Res.Input.IsNewKeyPress(Keys.Enter, new PlayerIndex?(), out playerIndex))
            {
                #if DEBUG
                System.Diagnostics.Debug.WriteLine("Enter key pressed");
                #endif
                // Обработка нажатия Enter
                this.HandleTap(new Vector2(240, 400), gameTime); // Центр экрана
            }
            else if (Res.Input.IsNewKeyPress(Keys.Escape, new PlayerIndex?(), out playerIndex))
            {
                #if DEBUG
                System.Diagnostics.Debug.WriteLine("Escape key pressed");
                #endif
                // Обработка нажатия Escape
                this.OnBackButton(gameTime);
            }
        }
    }

    public virtual void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
        #if DEBUG
        System.Diagnostics.Debug.WriteLine($"BaseScreen.HandleTap called with position: ({tapPos.X}, {tapPos.Y})");
        #endif
    }

    public virtual void OnBackButton(GameTime gameTime) => Res.Game.Exit();

    public virtual void Draw(GameTime gameTime)
    {
      Res.SpriteBatch.Begin();
      this.Draw(Res.SpriteBatch);
      this.DrawCore(Res.SpriteBatch, gameTime);
      Res.SpriteBatch.End();
    }

    public virtual Rectangle GetDisplayRect()
    {
      return new Rectangle(0, 0, (int) this.Size.X, (int) this.Size.Y);
    }

    protected abstract void DrawCore(SpriteBatch spriteBatch, GameTime gameTime);

    public virtual bool Save(SaveGame savegame)
    {
        savegame.ActiveScreenId = this.Id;
        return true;
    }

    public virtual void Load(SaveGame savegame)
    {
    }

    public virtual void Init() => this.ActionManager.Clear();

    public virtual void Pause()
    {
    }

  }
}
