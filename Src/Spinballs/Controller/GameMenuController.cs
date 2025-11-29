// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.GameMenuController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Spinballs.Common.Helper;
using Spinballs.Core;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller
{
  public class GameMenuController : GameController
  {
    private LabelControl _labelPaused;
    private SettingsPanel _panel;
    private MenuButton _buttonContinue;
    private MenuButton _buttonExit;

    public GameMenuController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._panel = new SettingsPanel(this.ActionManager);
      this._panel.Create();
      this._panel.Position = Layout.Panel + new Vector2(0.0f, -100f);
      this._labelPaused = new LabelControl();
      this._labelPaused.Text = Strings.GamePaused;
      this._labelPaused.Font = Res.Font.Big3;
      this._labelPaused.DisplayRect = this.GameScreen.DisplayRect;
      this._labelPaused.Orientation = Orientation.Top | Orientation.HorizontalCenter;
      this._panel.Create();
      this._panel.Opacity = (byte) 0;
      int y = (int) ((double) this._panel.Position.Y + (double) this._panel.Size.Y);
      int x = (int) ((double) this.GameScreen.Size.X - (double) Res.StartScreen.Button.Width) / 2;
      this._buttonContinue = new MenuButton(this.ActionManager);
      this._buttonContinue.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Continue, new Vector2((float) x, (float) y), Res.Font.Big);
      this._buttonExit = new MenuButton(this.ActionManager);
      this._buttonExit.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.MainMenu, new Vector2((float) x, (float) (y + this._buttonContinue.Texture.Height)), Res.Font.Big);
      MessageService.Message += new MessageHandler(this.MessageService_Message);
    }

    private void MessageService_Message(object sender, MessageArgs args)
    {
      if (args.Message != Spinballs.Document.Message.ContinueGame || this.Document.State != GameState.Pause)
        return;
      this.Fade(false, TimeSpan.FromMilliseconds(300.0), true);
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.Pause)
        return;
      this.InitPauseMode();
    }

    protected void InitPauseMode()
    {
      this.LockView();
      this.GameScreen.Darken.Opacity = (byte) 0;
      this._panel.Opacity = (byte) 0;
      this._labelPaused.Opacity = (byte) 0;
      this._buttonContinue.Opacity = (byte) 0;
      this._buttonExit.Opacity = (byte) 0;
      this._panel.MusicValue = Config.Instance.MusicVolume;
      this._panel.SoundValue = Config.Instance.SoundVolume;
      this.Fade(true, TimeSpan.Zero, false);
    }

    private void Fade(bool fadeIn, TimeSpan delay, bool finishedEvent)
    {
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.ActionManager = this.ActionManager;
      TimeSpan dialogFadeTime = Constants.DialogFadeTime;
      if (fadeIn)
      {
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this.GameScreen.Darken, dialogFadeTime, (byte) 0, (byte) 170));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panel, dialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._labelPaused, dialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonContinue, dialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonExit, dialogFadeTime));
      }
      else
      {
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this.GameScreen.Darken, dialogFadeTime, (byte) 0, (byte) 170));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panel, dialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._labelPaused, dialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonContinue, dialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonExit, dialogFadeTime));
      }
      ActionBase actionBase = (ActionBase) actionParallel;
      if (delay != TimeSpan.Zero)
      {
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        actionSequence.Actions.Add((ActionBase) new ActionDuration(delay));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionBase = (ActionBase) actionSequence;
      }
      if (finishedEvent)
        actionBase.ActionFinished += new EventHandler(this.Action_ActionFinished);
      actionBase.Start();
    }

    private void Action_ActionFinished(object sender, EventArgs e) => this.UnlockView();

    public override void Update(GameTime gameTime)
    {
      if (this.GameScreen.Document.State == GameState.Pause)
      {
        foreach (TouchLocation touchLocation in Res.Input.TouchState)
        {
          if (touchLocation.State == TouchLocationState.Pressed)
          {
            // TouchLocation.Position в паузе также приходит в физических координатах,
            // поэтому переводим в игровые координаты.
            Vector2 gamePos = Res.ConvertCoordinates(touchLocation.Position);
            this.HandleTap(gamePos, gameTime);
          }
        }

        // Обработка клика мышью в меню паузы
        if (Res.Input.IsNewMouseButtonPress(MouseButtons.Left))
        {
          Vector2 mousePos = Res.GetMousePositionInGameCoords();
          this.HandleTap(mousePos, gameTime);
        }
      }
      this.UpdateCore(gameTime);
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      if (this._buttonExit.Contains(tapPos))
      {
        this._buttonExit.OnClick((object) this);
        AudioManager.StopMusic();
        Config.Instance.MusicVolume = this._panel.MusicValue;
        this.GameScreen.Manager.ShowScreen(Screens.Start, TimeSpan.FromMilliseconds(500.0));
      }
      else
      {
        if (!this._buttonContinue.Contains(tapPos))
          return;
        this._buttonContinue.OnClick((object) this);
        this.Fade(false, TimeSpan.FromMilliseconds(300.0), true);
      }
    }

    protected override void UpdateCore(GameTime gameTime)
    {
      if (this.GameScreen.Document.State != GameState.Pause)
        return;
      this._panel.HandleInput();
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (this.GameScreen.Document.State != GameState.Pause || drawOrder != DrawOrder.AfterBalls)
        return;
      this.GameScreen.Darken.Draw(spriteBatch);
      this._labelPaused.Draw(spriteBatch);
      this._panel.Draw(spriteBatch);
      this._buttonContinue.Draw(spriteBatch);
      this._buttonExit.Draw(spriteBatch);
    }

    public override void Load(SaveGame savegame)
    {
      if (this.Document.State != GameState.Pause)
        return;
      base.Load(savegame);
      this.InitPauseMode();
    }
  }
}
