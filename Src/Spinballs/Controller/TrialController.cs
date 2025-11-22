// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.TrialController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller
{
  public class TrialController : GameController
  {
    private LabelControl _labelTrial;
    private TrialPanel _panel;
    private MenuButton _buttonBuy;
    private MenuButton _buttonExit;

    public TrialController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._panel = new TrialPanel(this.ActionManager);
      this._panel.Create();
      this._panel.Position = Layout.Panel + new Vector2(0.0f, -100f);
      this._panel.Opacity = (byte) 0;
      this._labelTrial = new LabelControl();
      this._labelTrial.Text = Strings.TrialTitle;
      this._labelTrial.Font = Res.Font.Big3;
      this._labelTrial.DisplayRect = this.GameScreen.DisplayRect;
      this._labelTrial.Orientation = Orientation.Top | Orientation.HorizontalCenter;
      int y = (int) ((double) this._panel.Position.Y + (double) this._panel.Size.Y);
      int x = (int) ((double) this.GameScreen.Size.X - (double) Res.StartScreen.Button.Width) / 2;
      this._buttonBuy = new MenuButton(this.ActionManager);
      this._buttonBuy.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.BuyGame, new Vector2((float) x, (float) y), Res.Font.Big);
      this._buttonExit = new MenuButton(this.ActionManager);
      this._buttonExit.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.MainMenu, new Vector2((float) x, (float) (y + this._buttonBuy.Texture.Height)), Res.Font.Big);
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.Trial)
        return;
      this.InitTrialMode();
    }

    protected void InitTrialMode()
    {
      this.LockView();
      this.GameScreen.Darken.Opacity = (byte) 0;
      this._panel.Opacity = (byte) 0;
      this._labelTrial.Opacity = (byte) 0;
      this._buttonBuy.Opacity = (byte) 0;
      this._buttonExit.Opacity = (byte) 0;
      this.Fade(true, TimeSpan.Zero, false);
    }

    private void Fade(bool fadeIn, TimeSpan delay, bool finishedEvent)
    {
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.ActionManager = this.ActionManager;
      int num = 500;
      if (fadeIn)
      {
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this.GameScreen.Darken, TimeSpan.FromMilliseconds((double) num), (byte) 0, (byte) 170));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panel, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._labelTrial, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonBuy, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonExit, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionMusicFade(0.0f, TimeSpan.FromMilliseconds((double) num)));
        Config.Instance.OrigMusicVolume = new float?(Config.Instance.MusicVolume);
      }
      else
      {
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this.GameScreen.Darken, TimeSpan.FromMilliseconds((double) num), (byte) 0, (byte) 170));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panel, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._labelTrial, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonBuy, TimeSpan.FromMilliseconds((double) num)));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonExit, TimeSpan.FromMilliseconds((double) num)));
        if (Config.Instance.OrigMusicVolume.HasValue)
          actionParallel.Actions.Add((ActionBase) new ActionMusicFade(Config.Instance.OrigMusicVolume.Value, TimeSpan.FromMilliseconds((double) num)));
        Config.Instance.OrigMusicVolume = new float?();
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
      if (this.GameScreen.Document.State == GameState.Trial)
      {
        foreach (TouchLocation touchLocation in Res.Input.TouchState)
        {
          if (touchLocation.State == TouchLocationState.Pressed)
            this.HandleTap(touchLocation.Position, gameTime);
        }
      }
      this.UpdateCore(gameTime);
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      if (this._buttonExit.Contains(tapPos))
      {
        this._buttonExit.StartHighlight();
        this.GameScreen.Manager.ShowScreen(Screens.Start, TimeSpan.FromMilliseconds(500.0));
      }
      else
      {
        if (!this._buttonBuy.Contains(tapPos))
          return;
        this._buttonBuy.StartHighlight();
        // В UWP версии MonoGame нет GamerServices, поэтому убираем эту функцию
        // Guide.ShowMarketplace(PlayerIndex.One);
      }
    }

    protected override void UpdateCore(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (this.GameScreen.Document.State != GameState.Trial || drawOrder != DrawOrder.AfterBalls)
        return;
      this.GameScreen.Darken.Draw(spriteBatch);
      this._labelTrial.Draw(spriteBatch);
      this._panel.Draw(spriteBatch);
      this._buttonBuy.Draw(spriteBatch);
      this._buttonExit.Draw(spriteBatch);
    }

    public override void Load(SaveGame savegame)
    {
      if (this.Document.State != GameState.Trial)
        return;
      base.Load(savegame);
      this.InitTrialMode();
    }
  }
}
