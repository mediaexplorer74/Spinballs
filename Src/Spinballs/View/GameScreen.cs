// Decompiled with JetBrains decompiler
// Type: Spinballs.View.GameScreen
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Spinballs.Common.Helper;
using Spinballs.Controller;
using Spinballs.Controller.Extra;
using Spinballs.Core.Controls;
using Spinballs.Core.ScreenManagement;
using Spinballs.Document;
using System;
using System.Collections.Generic;
using Spinballs.Core;

#nullable disable
namespace Spinballs.View
{
  public class GameScreen : BaseScreen
  {
    private GameDocument _document;
    private BallControl[] _balls = new BallControl[Layout.BallCount];
    private List<ControllerBase> _controllerList;
    private Song _music;
    private TimeSpan _trialPlaytime = TimeSpan.Zero;

    public GameScreen()
    {
      this._id = 2;
      this._document = new GameDocument();
      this._document.BallsRearranged += new EventHandler(this.Document_BallsRearranged);
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      for (int index = 0; index < this._balls.Length; ++index)
      {
        this._balls[index] = new BallControl();
        this._balls[index].Visible = false;
        this._balls[index].ActionManager = this.ActionManager;
      }
      this._controllerList = new List<ControllerBase>();
      this._controllerList.Add((ControllerBase) new GameStartController(this));
      this._controllerList.Add((ControllerBase) new TimeController(this));
      this._controllerList.Add((ControllerBase) new DiscController(this));
      this._controllerList.Add((ControllerBase) new ChainController(this));
      this._controllerList.Add((ControllerBase) new PointController(this));
      this._controllerList.Add((ControllerBase) new MainExtraController(this));
      this._controllerList.Add((ControllerBase) new LevelController(this));
      this._controllerList.Add((ControllerBase) new MenuController(this));
      this._controllerList.Add((ControllerBase) new GameMenuController(this));
      this._controllerList.Add((ControllerBase) new GameEndController(this));
      this._controllerList.Add((ControllerBase) new TrialController(this));
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (args.PrevState != GameState.None || this.Document.State != GameState.Starting)
        return;
      AudioManager.PlayMusic(this._music);
    }

    private void Document_BallsRearranged(object sender, EventArgs e)
    {
      this.SyncWithDocument();
      for (int ballIndex = 0; ballIndex < this._balls.Length; ++ballIndex)
        this._balls[ballIndex].Position = Layout.GetBallPosition(ballIndex);
    }

    public BallControl[] Balls => this._balls;

    public GameDocument Document => this._document;

    public override void LoadContent()
    {
      Res.LoadGameContent();
      this._music = Res.GameScreen.Sounds.Music;
      this.ActionManager.Clear();
      this.Document.StateManager.Reset();
      this.Texture = Res.GameScreen.Background;
      this.Size = new Vector2(480f, 800f);
      for (int ballIndex = 0; ballIndex < this._balls.Length; ++ballIndex)
      {
        this._balls[ballIndex].Create();
        this._balls[ballIndex].Position = Layout.GetBallPosition(ballIndex);
      }
      foreach (ControllerBase controller in this._controllerList)
      {
        controller.LoadContent();
        controller.Init();
      }
      base.LoadContent();
    }

    public override bool Enabled
    {
      get => base.Enabled;
      set
      {
        if (base.Enabled == value)
          return;
        base.Enabled = value;
      }
    }

    public override void Init()
    {
      base.Init();
      Config.Instance.FirstGameStart = false;
      AudioManager.StopMusic();
      this.Restart();
    }

    public void Restart()
    {
      this._trialPlaytime = TimeSpan.Zero;
      this.Document.StateManager.Reset();
      this.ActionManager.Clear();
      this.Document.Init();
      for (int ballIndex = 0; ballIndex < this._balls.Length; ++ballIndex)
      {
        this._balls[ballIndex].Position = Layout.GetBallPosition(ballIndex);
        this._balls[ballIndex].Visible = false;
      }
      foreach (ControllerBase controller in this._controllerList)
        controller.Init();
      this.Document.StartGame();
      this.SyncWithDocument();
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      foreach (ControllerBase controller in this._controllerList)
        controller.UnloadContent();
      for (int index = 0; index < this._balls.Length; ++index)
        this._balls[index].Destroy();
      this.Texture = (Texture2D) null;
      this._music = (Song) null;
      AudioManager.StopMusic();
    }

    public void SyncWithDocument()
    {
      for (int index1 = 0; index1 < Layout.DiscCount; ++index1)
      {
        for (int index2 = 0; index2 < Layout.BallsPerDisc; ++index2)
          this._balls[index1 * Layout.BallsPerDisc + index2].Color = this.Document.Discs[index1][index2].Color;
      }
    }

    public Vector2 GetBestChainCenter()
    {
      if (this.Document.BestChain == null)
        return new Vector2();
      Rectangle rectangle = new Rectangle(int.MaxValue, int.MaxValue, 0, 0);
      foreach (Ball ball1 in (List<Ball>) this.Document.BestChain)
      {
        BallControl ball2 = this._balls[ball1.FlatIndex];
        if ((double) rectangle.Left > (double) ball2.Position.X)
          rectangle.X = (int) ball2.Position.X;
        if ((double) rectangle.Right < (double) ball2.Position.X)
          rectangle.Width = (int) ((double) ball2.Position.X - (double) rectangle.X);
        if ((double) rectangle.Top > (double) ball2.Position.Y)
          rectangle.Y = (int) ball2.Position.Y;
        if ((double) rectangle.Bottom < (double) ball2.Position.Y)
          rectangle.Height = (int) ball2.Position.Y - rectangle.Y;
      }
      return new Vector2((float) rectangle.Center.X, (float) rectangle.Center.Y);
    }

    public override void OnBackButton(GameTime gameTime)
    {
      if (this.Document.State == GameState.Pause)
        MessageService.ContinueGame((object) this);
      else
        this.Document.StateManager.Change(GameState.Pause);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Res.IsTrial)
        {
            this._trialPlaytime += gameTime.ElapsedGameTime;
            if (this._trialPlaytime > Constants.TrialDuration && this.Document.State == GameState.Running)
                this.Document.State = GameState.Trial;
        }
        this.Document.Update(gameTime);
        foreach (ControllerBase controller in this._controllerList)
            controller.Update(gameTime);
            
        #if DEBUG
        // Диагностика для отслеживания ввода мыши и клавиатуры
        if (Res.Input.IsNewMouseButtonPress(MouseButtons.Left))
        {
            var mousePos = Res.GetMousePositionInGameCoords();
            System.Diagnostics.Debug.WriteLine(
                $"Mouse tap detected at physical position: ({Res.Input.CurrentMouseState.X}, {Res.Input.CurrentMouseState.Y}), game coordinates: ({mousePos.X}, {mousePos.Y})");
        }
        #endif
        
        // Обработка ввода - теперь включена в Update методе базового класса
        // Но GameScreen может добавить свою специфичную обработку ввода здесь, если нужно
    }

    // Удален метод HandleInput, так как он не переопределяет существующий виртуальный метод

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
        #if DEBUG
        System.Diagnostics.Debug.WriteLine($"GameScreen.HandleTap called with position: ({tapPos.X}, {tapPos.Y})");
        #endif
        
        // Вызов базовой реализации
        base.HandleTap(tapPos, gameTime);
        
        // Обработка нажатия на диски и другие элементы
        foreach (ControllerBase controller in this._controllerList)
        {
            controller.HandleTap(tapPos, gameTime);
        }
    }


    protected override void DrawCore(SpriteBatch spriteBatch, GameTime gameTime)
    {
      foreach (ControllerBase controller in this._controllerList)
        controller.Draw(spriteBatch, DrawOrder.BeforeBalls);
      for (int index = 0; index < this._balls.Length; ++index)
      {
        if (!this._balls[index].Highlight)
          this._balls[index].Draw(spriteBatch);
      }
      for (int index = 0; index < this._balls.Length; ++index)
      {
        if (this._balls[index].Highlight)
          this._balls[index].Draw(spriteBatch);
      }
      foreach (ControllerBase controller in this._controllerList)
        controller.Draw(spriteBatch, DrawOrder.AfterBalls);
    }

    private void SetTimer()
    {
    }

    public override bool Save(SaveGame savegame)
    {
      if (this.Document.State == GameState.None || this.Document.State == GameState.Starting 
                || this.Document.State == GameState.End || this.Document.State == GameState.Trial)
        return false;
      base.Save(savegame);
      for (int index = 0; index < this.Balls.Length; ++index)
      {
        BallControl ball = this.Balls[index];
        savegame.Balls.Insert(index, new BallSave(index, ball.Color, ball.Position, ball.Visible));
      }
      this.Document.Save(savegame);
      foreach (ControllerBase controller in this._controllerList)
        controller.Save(savegame);
      return true;
    }

    public override void Load(SaveGame savegame)
    {
      base.Load(savegame);
      foreach (BallSave ball in savegame.Balls)
      {
        this.Balls[ball.FlatIndex].Color = ball.Color;
        this.Balls[ball.FlatIndex].Visible = true;
        int index1 = ball.FlatIndex / Layout.BallsPerDisc;
        int index2 = ball.FlatIndex - index1 * Layout.BallsPerDisc;
        this.Document.Discs[index1][index2].Color = ball.Color;
      }
      this.Document.Load(savegame);
      foreach (ControllerBase controller in this._controllerList)
        controller.Load(savegame);
      if (this.Document.State == GameState.None)
        return;
      AudioManager.PlayMusic(this._music);
    }

    public override void Pause()
    {
      base.Pause();
      this.Document.State = GameState.Pause;
    }
  }
}
