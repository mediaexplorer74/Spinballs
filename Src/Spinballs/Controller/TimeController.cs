// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.TimeController
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

#nullable disable
namespace Spinballs.Controller
{
  public class TimeController : GameController
  {
    private const int _criticalMilliseconds = 5000;
    private TimerBar _timeBar;
    private readonly TimeSpan _blinkActionDuration = TimeSpan.FromMilliseconds(5000.0);
    private readonly TimeSpan _blinkDuration = TimeSpan.FromMilliseconds(250.0);
    private bool _startCritical = true;
    private ActionBlink _actionBlink;

    public TimeController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      this.Document.LevelChanged += new EventHandler(this.Document_LevelChanged);
      this._timeBar = new TimerBar();
      MessageService.Message += new MessageHandler(this.MessageService_Message);
    }

    private void MessageService_Message(object sender, MessageArgs args)
    {
      if (args.Message != Spinballs.Document.Message.TimerBar)
        return;
      TimerBarArgs timerBarArgs = (TimerBarArgs) args;
      timerBarArgs.Handled = true;
      bool iced1 = this._timeBar.Iced;
      bool? iced2 = timerBarArgs.Iced;
      if ((iced1 != iced2.GetValueOrDefault() ? 0 : (iced2.HasValue ? 1 : 0)) != 0)
        return;
      if (timerBarArgs.Iced.HasValue)
      {
        this._timeBar.Iced = timerBarArgs.Iced.Value;
        if (this._timeBar.Iced)
          AudioManager.Play(Res.GameScreen.Sounds.TimeSlower);
        else
          AudioManager.Play(Res.GameScreen.Sounds.TimeFaster);
      }
      if (!timerBarArgs.ResetTime.HasValue || !timerBarArgs.ResetTime.Value)
        return;
      this.TimerBar.Value = this.TimerBar.Max;
    }

    private void Document_LevelChanged(object sender, EventArgs e) => this.Init();

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.ClearBalls)
        return;
      this.TimerBar.Value += this.CalculateTimeBonus();
      if (this.TimerBar.Value <= 5000)
        return;
      this._startCritical = true;
      this._actionBlink.Stop();
    }

    public TimerBar TimerBar => this._timeBar;

    private int CalculateTimeBonus()
    {
      return this.Document.BestChain.Count * (this.Document.BestChain.Count - 2) * 300;
    }

    public override void Init()
    {
      base.Init();
      int timeLoad = GameDocument.LevelInfo[this.Document.CurrentLevel].TimeLoad;
      this.TimerBar.Init(0, timeLoad, timeLoad);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this.TimerBar.Create();
      this.TimerBar.Position = Layout.TimeBarPositon;
      this._actionBlink = new ActionBlink((ImageControl) this.TimerBar, this._blinkActionDuration, this._blinkDuration);
      this._actionBlink.ActionManager = this.ActionManager;
    }

    protected override void UpdateCore(GameTime gameTime)
    {
      if (this.Document.State != GameState.Running || gameTime.ElapsedGameTime.TotalMilliseconds < 0.0)
        return;
      if (this.TimerBar.Iced)
        this.TimerBar.Value -= (int) (gameTime.ElapsedGameTime.TotalMilliseconds / 2.0);
      else
        this.TimerBar.Value -= (int) gameTime.ElapsedGameTime.TotalMilliseconds;
      if (this._startCritical && this.TimerBar.Value < 5000)
      {
        AudioManager.Play(Res.GameScreen.Sounds.TimeCritical);
        this._actionBlink.Start();
        this._startCritical = false;
      }
      else
      {
        if (this.TimerBar.Value > 0)
          return;
        this.Document.State = GameState.End;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder != DrawOrder.BeforeBalls)
        return;
      this._timeBar.Draw(spriteBatch);
    }

    public override void Save(SaveGame savegame)
    {
      base.Save(savegame);
      TimerSave timerSave = savegame.NewController<TimerSave>((object) this);
      timerSave.Time = this._timeBar.Value;
      timerSave.Iced = this._timeBar.Iced;
      timerSave.StartCritical = this._startCritical;
    }

    public override void Load(SaveGame savegame)
    {
      base.Load(savegame);
      TimerSave controller = savegame.GetController<TimerSave>((object) this);
      if (controller == null)
        return;
      this._timeBar.Iced = controller.Iced;
      this._timeBar.Value = controller.Time;
      this._startCritical = true;
      this._timeBar.Max = GameDocument.LevelInfo[this.Document.CurrentLevel].TimeLoad;
    }
  }
}
