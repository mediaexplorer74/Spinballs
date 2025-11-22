// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.GameEndController
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
  public class GameEndController : GameController
  {
    private GameEndPanel _panel;
    private MenuButton _buttonPlay;
    private MenuButton _buttonMenu;

    public GameEndController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this.Document.StateManager.Changed += new GameStateChangeHandler(this.StateManager_Changed);
      this._panel = new GameEndPanel();
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._panel.Position = Layout.Panel + new Vector2(0.0f, -180f);
      this._buttonPlay = new MenuButton(this.ActionManager);
      this._buttonMenu = new MenuButton(this.ActionManager);
    }

    private void StateManager_Changed(object sender, StateChangeToken args)
    {
      if (this.Document.State != GameState.End)
        return;
      this.LockView();
      this.GameScreen.Darken.Opacity = (byte) 0;
      int highscoreIndex = -1;
      if (this.Document.Points > 0)
        highscoreIndex = Highscore.Instance.Add(this.Document.Points);
      this._panel.PointsText = string.Format("{0} {1} ({2} {3})", (object) this.Document.Points, (object) Strings.Points, (object) Strings.Level, (object) (this.Document.CurrentLevel + 1));
      this._panel.HighscoreText = Strings.NoHighScore;
      this._panel.HeaderText = highscoreIndex != 0 ? (highscoreIndex <= 0 ? Strings.GameOver : Strings.NewHighScore) : Strings.BestScore;
      this._panel.Create(highscoreIndex);
      this._panel.Opacity = (byte) 0;
      int y = (int) ((double) this._panel.Position.Y + (double) this._panel.Size.Y + 50.0);
      int x = (int) ((double) this.GameScreen.Size.X - (double) Res.StartScreen.Button.Width) / 2;
      this._buttonPlay.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.PlayAgain, new Vector2((float) x, (float) y), Res.Font.Big);
      this._buttonMenu.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.MainMenu, new Vector2((float) x, (float) (y + this._buttonPlay.Texture.Height)), Res.Font.Big);
      this.ActionManager.Add((ActionBase) new ActionFadeIn((DrawableControl) this.GameScreen.Darken, Constants.DialogFadeTime, (byte) 0, (byte) 170));
      this.ActionManager.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panel, Constants.DialogFadeTime));
      this.ActionManager.Add((ActionBase) new ActionMusicFade(0.0f, Constants.DialogFadeTime));
      Config.Instance.OrigMusicVolume = new float?(Config.Instance.MusicVolume);
      if (highscoreIndex == 0)
        AudioManager.Play(Res.GameScreen.Sounds.GameOverBestScore);
      else if (highscoreIndex > 0)
        AudioManager.Play(Res.GameScreen.Sounds.GameOverHighscore);
      else
        AudioManager.Play(Res.GameScreen.Sounds.GameOverNoHighscore);
    }

    public override void Update(GameTime gameTime)
    {
      if (this.GameScreen.Document.State == GameState.End)
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
      if (this._buttonMenu.Contains(tapPos))
      {
        this._buttonMenu.StartHighlight();
        this.GameScreen.Manager.ShowScreen(Screens.Start, TimeSpan.FromMilliseconds(500.0));
      }
      else
      {
        if (!this._buttonPlay.Contains(tapPos))
          return;
        this._buttonPlay.StartHighlight();
        Config.Instance.LastPlayLevel = -1;
        if (Config.Instance.OrigMusicVolume.HasValue)
        {
          Config.Instance.MusicVolume = Config.Instance.OrigMusicVolume.Value;
          Config.Instance.OrigMusicVolume = new float?();
        }
        this.GameScreen.Restart();
      }
    }

    protected override void UpdateCore(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (this.Document.State != GameState.End || drawOrder != DrawOrder.AfterBalls)
        return;
      this.GameScreen.Darken.Draw(spriteBatch);
      this._panel.Draw(spriteBatch);
      this._buttonPlay.Draw(spriteBatch);
      this._buttonMenu.Draw(spriteBatch);
    }
  }
}
