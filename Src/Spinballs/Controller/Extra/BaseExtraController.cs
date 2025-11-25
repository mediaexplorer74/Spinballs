// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.BaseExtraController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Controller.Extra
{
  public class BaseExtraController : GameController
  {
    protected const int MaxLoad = 20;
    private readonly List<BaseExtraController.ConnectionDescriptor> _connections = new List<BaseExtraController.ConnectionDescriptor>();
    protected ImageControl _connNorth;
    protected ImageControl _connNorthWest;
    private ExtraLoadControl _loadControl;
    private ExtraState _state;
    private Vector2 _position;
    private bool _isPartOfBestChain;
    private float _fillStartLoadValue;
    private float _fillEndLoadValue;
    private bool _active;
    protected TimeSpan _activeDuration;

    public BaseExtraController(Spinballs.View.GameScreen gameScreen, Vector2 position)
      : base(gameScreen)
    {
      this._state = ExtraState.Empty;
      this.Position = position;
      this._activeDuration = TimeSpan.FromMilliseconds(1000.0);
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._connNorth = new ImageControl(Res.GameScreen.BonusConnectorNorth);
      this._connNorthWest = new ImageControl(Res.GameScreen.BonusConnectorNorthWest);
    }

    public override void Init()
    {
      base.Init();
      foreach (BaseExtraController.ConnectionDescriptor connection in this.Connections)
      {
        connection.Active = false;
        connection.Connector.Opacity = (byte) 0;
      }
      this._active = false;
      this._fillStartLoadValue = 0.0f;
      this._fillEndLoadValue = 0.0f;
      if (this._loadControl == null)
        return;
      this._loadControl.Init(0.0f, 20f, 0.0f);
      this._loadControl.BlinkMode = BlinkMode.None;
    }

    protected List<BaseExtraController.ConnectionDescriptor> Connections => this._connections;

    public ExtraLoadControl LoadControl
    {
      get => this._loadControl;
      set
      {
        this._loadControl = value;
        if (this._loadControl == null)
          return;
        this._loadControl.ActionManager = this.ActionManager;
        this._loadControl.Create();
        this._loadControl.Init(0.0f, 20f, 0.0f);
        this._loadControl.Position = this.Position;
      }
    }

    public float LoadValue
    {
      get => this.LoadControl.Value;
      set
      {
        if ((double) this.LoadControl.Value == (double) value || (double) this.LoadControl.Value == 20.0 && (double) value > 20.0)
          return;
        this.LoadControl.Value = value;
        if ((double) this.LoadControl.Value != 20.0 || this.Active)
          return;
        AudioManager.Play(Res.GameScreen.Sounds.ExtraLoaded);
      }
    }

    protected ExtraState State => this._state;

    protected Vector2 Position
    {
      get => this._position;
      set
      {
        this._position = value;
        if (this.LoadControl == null)
          return;
        this.LoadControl.Position = value;
      }
    }

    public bool IsFull => (double) this.LoadValue == 20.0;

    public bool IsPartOfBestChain
    {
      get => this._isPartOfBestChain;
      set => this._isPartOfBestChain = value;
    }

    public float FillStartLoadValue
    {
      get => this._fillStartLoadValue;
      set => this._fillStartLoadValue = value;
    }

    public float FillEndLoadValue
    {
      get => this._fillEndLoadValue;
      set => this._fillEndLoadValue = value;
    }

    public bool Active
    {
      get => this._active;
      set
      {
        this._active = value;
        if (this._active)
          this.LoadControl.BlinkMode = BlinkMode.Highlight;
        else
          this.LoadControl.BlinkMode = BlinkMode.None;
      }
    }

    public void OnBestChainChanged()
    {
      this.IsPartOfBestChain = false;
      foreach (BaseExtraController.ConnectionDescriptor connection in this.Connections)
        connection.Active = false;
      if (this.Document.BestChain == null)
        return;
      foreach (BaseExtraController.ConnectionDescriptor connection in this.Connections)
      {
        connection.Active = this.Document.BestChain.Contains(connection.Ball);
        if (connection.Active)
          this.IsPartOfBestChain = true;
      }
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.ClearBalls && this.Document.State != GameState.BonusExplode 
                && this.Document.State != GameState.BonusSortBalls)
        return;
      foreach (BaseExtraController.ConnectionDescriptor connection in this.Connections)
        connection.Active = false;
    }

    protected virtual void Reset()
    {
      this._state = ExtraState.Empty;
      this.LoadValue = 0.0f;
      this.FillStartLoadValue = 0.0f;
      this.FillEndLoadValue = 0.0f;
      this.IsPartOfBestChain = false;
      this.LoadControl.BlinkMode = BlinkMode.None;
    }

        // RnD: virtual ?
    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      if (!this.LoadControl.Contains(tapPos) || !this.IsFull || this.Active)
        return;
      this.Active = true;
      this.Execute();
    }

    protected virtual void Execute()
    {
    }

    protected virtual void Stop() => this.LoadControl.BlinkMode = BlinkMode.None;

    protected virtual bool UpdateExtraController() => this.Document.State == GameState.Running;

    protected override void UpdateCore(GameTime gameTime)
    {
      if (!this.UpdateExtraController() || !this.Active)
        return;
      this.LoadValue -= 20f * ((float) gameTime.ElapsedGameTime.TotalMilliseconds / (float) this._activeDuration.TotalMilliseconds);
      if ((double) this.LoadValue > 0.0)
        return;
      this.Active = false;
      this.Stop();
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder != DrawOrder.BeforeBalls)
        return;
      this.LoadControl.Draw(spriteBatch);
      foreach (BaseExtraController.ConnectionDescriptor connection in this.Connections)
        connection.Connector.Draw(spriteBatch);
    }

    public override void Save(SaveGame savegame)
    {
      base.Save(savegame);
      ExtraBaseSave save = savegame.NewController<ExtraBaseSave>((object) this);
      this.Save(save);
      this.SaveActions((ControllerSave) save, (List<ActionBase>) null);
    }

    public override void Load(SaveGame savegame)
    {
      base.Load(savegame);
      ExtraBaseSave controller = savegame.GetController<ExtraBaseSave>((object) this);
      if (controller == null)
        return;
      this.Load(controller);
      this.LoadActions((ControllerSave) controller);
    }

    protected void Save(ExtraBaseSave save)
    {
      save.State = this.State;
      save.FillStartLoadValue = this.FillStartLoadValue;
      save.FillEndLoadValue = this.FillEndLoadValue;
      save.Active = this.Active;
      save.ActiveDuration = this._activeDuration;
      save.LoadValue = this.LoadControl.Value;
      save.BlinkMode = this.LoadControl.BlinkMode;
    }

    protected void Load(ExtraBaseSave save)
    {
      this._state = save.State;
      this.FillStartLoadValue = save.FillStartLoadValue;
      this.FillEndLoadValue = save.FillEndLoadValue;
      this.Active = save.Active;
      this._activeDuration = save.ActiveDuration;
      this.LoadControl.Value = save.LoadValue;
      this.LoadControl.BlinkMode = save.BlinkMode;
    }

    protected class ConnectionDescriptor
    {
      public Ball Ball;
      public ImageControl Connector;
      public ActionBase Highlight;
      private bool _active;

      public ConnectionDescriptor(Ball ball, ImageControl connector, ActionManager actionManager)
      {
        this.Ball = ball;
        this.Connector = connector;
        this.Connector.Opacity = (byte) 0;
        this.Highlight = (ActionBase) new ActionRepeat((ActionBase) new ActionSequence()
        {
          Actions = {
            (ActionBase) new ActionFadeIn((DrawableControl) this.Connector, TimeSpan.FromMilliseconds(300.0)),
            (ActionBase) new ActionFadeOut((DrawableControl) this.Connector, TimeSpan.FromMilliseconds(300.0))
          }
        });
        this.Highlight.ActionManager = actionManager;
        this._active = false;
      }

      public bool Active
      {
        get => this._active;
        set
        {
          if (this._active == value)
            return;
          this._active = value;
          if (this._active && !this.Highlight.IsRunning)
          {
            this.Highlight.Start();
          }
          else
          {
            this.Highlight.Stop();
            this.Connector.Opacity = (byte) 0;
          }
        }
      }
    }
  }
}
