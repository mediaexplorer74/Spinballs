// Decompiled with JetBrains decompiler
// Type: Spinballs.View.StartScreen
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Spinballs.Common.Helper;
using Spinballs.Core;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Core.ScreenManagement;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.View
{
  public class StartScreen : BaseScreen
  {
    private MenuButton _buttonStart;
    private MenuButton _buttonContinueGame;
    private MenuButton _buttonTutorial;
    private MenuButton _buttonHighScore;
    private MenuButton _buttonSettings;
    private MenuButton _buttonBuy;
    private MenuButton _buttonExit;
    private MenuButton _buttonBack;
    private MenuButton _buttonContinue;
    private MenuButton _buttonMainMenu;
    private MenuButton _buttonDoBuy;
    private MenuButton _buttonFirstGameStart;
    private ActionParallel _actionButtonsFadeOut;
    private ActionParallel _actionButtonsFadeIn;
    private ActionBase _actionRunningButtonHighlight;
    private SettingsPanel _panelSettings;
    private HighscorePanel _panelHighscore;
    private TutorialPanel1 _panelTutorial1;
    private TutorialPanel2 _panelTutorial2;
    private TutorialPanel3 _panelTutorial3;
    private TrialPanel _panelTrial;
    private ImageControl _logoHighlight;
    private List<DrawableControl> _buttons = new List<DrawableControl>();
    private bool _buttonsEnabled = true;
    private bool _firstStartTutorial;

    public StartScreen()
    {
      this._id = 1;
      this._buttonStart = new MenuButton(this.ActionManager);
      this._buttonContinueGame = new MenuButton(this.ActionManager);
      this._buttonTutorial = new MenuButton(this.ActionManager);
      this._buttonHighScore = new MenuButton(this.ActionManager);
      this._buttonSettings = new MenuButton(this.ActionManager);
      this._buttonBuy = new MenuButton(this.ActionManager);
      this._buttonDoBuy = new MenuButton(this.ActionManager);
      this._buttonExit = new MenuButton(this.ActionManager);
      this._buttonBack = new MenuButton(this.ActionManager);
      this._buttonContinue = new MenuButton(this.ActionManager);
      this._buttonMainMenu = new MenuButton(this.ActionManager);

      this._buttonFirstGameStart = new MenuButton(this.ActionManager);
      this._buttonStart.Clicked += new EventHandler(this._buttonStart_Clicked);
      this._buttonContinueGame.Clicked += new EventHandler(this._buttonContinueGame_Clicked);
      this._buttonTutorial.Clicked += new EventHandler(this._buttonTutorial_Clicked);
      this._buttonHighScore.Clicked += new EventHandler(this._buttonHighScore_Clicked);
      this._buttonSettings.Clicked += new EventHandler(this._buttonSettings_Clicked);
      this._buttonExit.Clicked += new EventHandler(this._buttonExit_Clicked);
      this._buttonBack.Clicked += new EventHandler(this._buttonBack_Clicked);
      this._buttonContinue.Clicked += new EventHandler(this._buttonContinue_Clicked);
      this._buttonMainMenu.Clicked += new EventHandler(this._buttonMainMenu_Clicked);
      this._buttonBuy.Clicked += new EventHandler(this._buttonBuy_Clicked);
      this._buttonDoBuy.Clicked += new EventHandler(this._buttonDoBuy_Clicked);

      this._buttonFirstGameStart.Clicked += new EventHandler(this._buttonFirstGameStart_Clicked);
      
      this._buttons.AddRange((IEnumerable<DrawableControl>) new MenuButton[7]
      {
        this._buttonStart,
        this._buttonContinueGame,
        this._buttonTutorial,
        this._buttonHighScore,
        this._buttonSettings,
        this._buttonBuy,
        this._buttonExit
      });
      this._logoHighlight = new ImageControl();
      this._panelSettings = new SettingsPanel(this.ActionManager);
      this._panelHighscore = new HighscorePanel(this.ActionManager);
      this._panelTutorial1 = new TutorialPanel1(this.ActionManager);
      this._panelTutorial2 = new TutorialPanel2(this.ActionManager);
      this._panelTutorial3 = new TutorialPanel3(this.ActionManager);
      this._panelTrial = new TrialPanel(this.ActionManager);
    }

    public override void Init()
    {
      base.Init();
      this._firstStartTutorial = false;
      this._panelTutorial3.Opacity = (byte) 0;
      AudioManager.StopMusic();
      if (Config.Instance.OrigMusicVolume.HasValue)
      {
        Config.Instance.MusicVolume = Config.Instance.OrigMusicVolume.Value;
        Config.Instance.OrigMusicVolume = new float?();
      }
      this._buttonsEnabled = true;
      this.LayoutButtons();
      this.CreateHighlightActions(true);
    }

    private void CreateHighlightActions(bool start)
    {
      bool flag = false;
      if (this._actionRunningButtonHighlight != null)
      {
        flag = this._actionRunningButtonHighlight.IsRunning;
        this.StopRunningButtonHiglight();
      }
      int delayTime = 300;
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence(this._logoHighlight, 0, 300, 2400));
      actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonStart.Highlight, delayTime, 0, 600));
      if (Config.Instance.LastPlayLevel > 0)
        actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonContinueGame.Highlight, delayTime += 120, 0, 600));
      int num1;
      actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonTutorial.Highlight, num1 = delayTime + 120, 0, 600));
      int num2;
      actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonHighScore.Highlight, num2 = num1 + 120, 0, 600));
      int num3;
      actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonSettings.Highlight, num3 = num2 + 120, 0, 600));
      if (this._buttonBuy.Visible)
        actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonBuy.Highlight, num3 += 120, 0, 600));
      int num4;
      actionParallel.Actions.Add((ActionBase) this.CreateHighlightSequence((ImageControl) this._buttonExit.Highlight, num4 = num3 + 120, 0, 600));
      this._actionRunningButtonHighlight = (ActionBase) new ActionRepeat((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(1000.0)),
          (ActionBase) actionParallel,
          (ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(2000.0))
        }
      });
      this._actionRunningButtonHighlight.ActionManager = this.ActionManager;
      if (!start && !flag)
        return;
      this._actionRunningButtonHighlight.Start();
    }

    private ActionSequence CreateHighlightSequence(
      ImageControl control,
      int delayTime,
      int fadeInTime,
      int fadeOutTime)
    {
      return new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionDuration(TimeSpan.FromMilliseconds((double) delayTime)),
          (ActionBase) new ActionFadeIn((DrawableControl) control, TimeSpan.FromMilliseconds((double) fadeInTime)),
          (ActionBase) new ActionFadeOut((DrawableControl) control, TimeSpan.FromMilliseconds((double) fadeOutTime))
        }
      };
    }

    public override void LoadContent()
    {
      Res.LoadStartContent();
      this._actionButtonsFadeOut = new ActionParallel();
      this._actionButtonsFadeOut.ActionManager = this.ActionManager;
      foreach (DrawableControl button in this._buttons)
        this._actionButtonsFadeOut.Actions.Add((ActionBase) new ActionFadeOut(button, Constants.DialogFadeTime));
      this.Texture = Res.StartScreen.Background;
      this.Size = new Vector2(480f, 800f);
      int num1 = 0;
      this._buttonStart.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Start, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num1, Res.Font.Big);
      int num2 = num1 + 1;
      this._buttonContinueGame.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.ContinueGame, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num2, Res.Font.Big);
      if (Config.Instance.LastPlayLevel > 0)
      {
        ++num2;
      }
      else
      {
        this._buttonContinueGame.Visible = false;
        this._buttonContinueGame.Opacity = (byte) 0;
      }
      this._buttonTutorial.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Tutorial, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num2, Res.Font.Big);
      int num3 = num2 + 1;
      this._buttonHighScore.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Highscore, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num3, Res.Font.Big);
      int num4 = num3 + 1;
      this._buttonSettings.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Settings, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num4, Res.Font.Big);
      int num5 = num4 + 1;
      this._buttonBuy.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Fullversion, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num5, Res.Font.Big);
      int num6 = num5 + 1;
      this._buttonExit.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Exit, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num6, Res.Font.Big);
      this._buttonBack.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Back, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num6, Res.Font.Big);
      this._buttonBack.Opacity = (byte) 0;
      this._buttonContinue.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Continue, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num6, Res.Font.Big);
      this._buttonContinue.Opacity = (byte) 0;
      this._buttonFirstGameStart.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.Start, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num6, Res.Font.Big);
      this._buttonFirstGameStart.Opacity = (byte) 0;
      this._buttonDoBuy.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.BuyGame, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num6, Res.Font.Big);
      this._buttonDoBuy.Opacity = (byte) 0;
      int num7 = num6 + 1;
      this._buttonMainMenu.Create(Res.StartScreen.Button, Res.StartScreen.ButtonHighlight, Strings.MainMenu, Layout.StartFirstButton + Layout.StartButtonOffset * (float) num7, Res.Font.Big);
      this._buttonMainMenu.Opacity = (byte) 0;
      this._buttonBuy.Enabled = this._buttonBuy.Visible = Res.IsTrial;
      if (!Res.IsTrial)
        this._buttonBuy.Opacity = (byte) 0;
      this.CreateHighlightActions(true);
      this._logoHighlight.Texture = Res.StartScreen.LogoHighlight;
      this._logoHighlight.Size = new Vector2(336f, 88f);
      this._logoHighlight.Position = Layout.LogoHighlight;
      this._logoHighlight.Visible = false;
      this._panelSettings.Create();
      this._panelSettings.Opacity = (byte) 0;
      this._panelSettings.Position = Layout.Panel;
      this._panelHighscore.Create();
      this._panelHighscore.Opacity = (byte) 0;
      this._panelHighscore.Position = Layout.Panel;
      this._panelTutorial1.Create();
      this._panelTutorial1.Opacity = (byte) 0;
      this._panelTutorial1.Position = Layout.Panel;
      this._panelTutorial2.Create();
      this._panelTutorial2.Opacity = (byte) 0;
      this._panelTutorial2.Position = Layout.Panel;
      this._panelTutorial3.Create();
      this._panelTutorial3.Opacity = (byte) 0;
      this._panelTutorial3.Position = Layout.Panel;
      this._panelTrial.Create();
      this._panelTrial.Opacity = (byte) 0;
      this._panelTrial.Position = Layout.Panel;
      this._actionButtonsFadeIn = new ActionParallel();
      this._actionButtonsFadeIn.ActionManager = this.ActionManager;
      foreach (DrawableControl button in this._buttons)
      {
        if (button.Visible && button.Opacity > (byte) 0 && button.Enabled)
          this._actionButtonsFadeIn.Actions.Add((ActionBase) new ActionFadeIn(button, Constants.DialogFadeTime));
      }
      base.LoadContent();
    }

    private void LayoutButtons()
    {
      int num1 = 0;
      MenuButton buttonStart = this._buttonStart;
      Vector2 startFirstButton1 = Layout.StartFirstButton;
      Vector2 startButtonOffset1 = Layout.StartButtonOffset;
      int num2 = num1;
      int num3 = num2 + 1;
      double num4 = (double) num2;
      Vector2 vector2_1 = startButtonOffset1 * (float) num4;
      Vector2 vector2_2 = startFirstButton1 + vector2_1;
      buttonStart.Position = vector2_2;
      this._buttonStart.Opacity = byte.MaxValue;
      if (Config.Instance.LastPlayLevel > 0)
      {
        this._buttonContinueGame.Position = Layout.StartFirstButton + Layout.StartButtonOffset * (float) num3++;
        this._buttonContinueGame.Opacity = byte.MaxValue;
        this._buttonContinueGame.Visible = true;
      }
      else
      {
        this._buttonContinueGame.Opacity = (byte) 0;
        this._buttonContinueGame.Visible = false;
      }
      MenuButton buttonTutorial = this._buttonTutorial;
      Vector2 startFirstButton2 = Layout.StartFirstButton;
      Vector2 startButtonOffset2 = Layout.StartButtonOffset;
      int num5 = num3;
      int num6 = num5 + 1;
      double num7 = (double) num5;
      Vector2 vector2_3 = startButtonOffset2 * (float) num7;
      Vector2 vector2_4 = startFirstButton2 + vector2_3;
      buttonTutorial.Position = vector2_4;
      this._buttonTutorial.Opacity = byte.MaxValue;
      MenuButton buttonHighScore = this._buttonHighScore;
      Vector2 startFirstButton3 = Layout.StartFirstButton;
      Vector2 startButtonOffset3 = Layout.StartButtonOffset;
      int num8 = num6;
      int num9 = num8 + 1;
      double num10 = (double) num8;
      Vector2 vector2_5 = startButtonOffset3 * (float) num10;
      Vector2 vector2_6 = startFirstButton3 + vector2_5;
      buttonHighScore.Position = vector2_6;
      this._buttonHighScore.Opacity = byte.MaxValue;
      MenuButton buttonSettings = this._buttonSettings;
      Vector2 startFirstButton4 = Layout.StartFirstButton;
      Vector2 startButtonOffset4 = Layout.StartButtonOffset;
      int num11 = num9;
      int num12 = num11 + 1;
      double num13 = (double) num11;
      Vector2 vector2_7 = startButtonOffset4 * (float) num13;
      Vector2 vector2_8 = startFirstButton4 + vector2_7;
      buttonSettings.Position = vector2_8;
      this._buttonSettings.Opacity = byte.MaxValue;
      MenuButton buttonBuy = this._buttonBuy;
      Vector2 startFirstButton5 = Layout.StartFirstButton;
      Vector2 startButtonOffset5 = Layout.StartButtonOffset;
      int num14 = num12;
      int num15 = num14 + 1;
      double num16 = (double) num14;
      Vector2 vector2_9 = startButtonOffset5 * (float) num16;
      Vector2 vector2_10 = startFirstButton5 + vector2_9;
      buttonBuy.Position = vector2_10;
      this._buttonExit.Position = Layout.StartFirstButton + Layout.StartButtonOffset * (float) num15;
      this._buttonExit.Opacity = byte.MaxValue;
      this._buttonBack.Position = Layout.StartFirstButton + Layout.StartButtonOffset * 5f;
      this._buttonBack.Opacity = (byte) 0;
      this._buttonContinue.Position = Layout.StartFirstButton + Layout.StartButtonOffset * 5f;
      this._buttonContinue.Opacity = (byte) 0;
      this._buttonFirstGameStart.Position = Layout.StartFirstButton + Layout.StartButtonOffset * 5f;
      this._buttonFirstGameStart.Opacity = (byte) 0;
      this._buttonDoBuy.Position = Layout.StartFirstButton + Layout.StartButtonOffset * 5f;
      this._buttonDoBuy.Opacity = (byte) 0;
      this._buttonMainMenu.Position = Layout.StartFirstButton + Layout.StartButtonOffset * 6f;
      this._buttonMainMenu.Opacity = (byte) 0;
      this._buttonBuy.Enabled = this._buttonBuy.Visible = Res.IsTrial;
      this._buttonBuy.Opacity = Res.IsTrial ? byte.MaxValue : (byte) 0;
      this._actionButtonsFadeIn = new ActionParallel();
      this._actionButtonsFadeIn.ActionManager = this.ActionManager;
      foreach (DrawableControl button in this._buttons)
      {
        if (button.Visible && button.Opacity > (byte) 0 && button.Enabled)
          this._actionButtonsFadeIn.Actions.Add((ActionBase) new ActionFadeIn(button, Constants.DialogFadeTime));
      }
    }

    private void StopRunningButtonHiglight()
    {
      this._actionRunningButtonHighlight.Stop();
      foreach (DrawableControl button in this._buttons)
      {
        if (button is MenuButton menuButton)
          menuButton.Highlight.Opacity = (byte) 0;
      }
    }

    private void ShowSettings()
    {
      this._buttonsEnabled = false;
      this.StopRunningButtonHiglight();
      this.ActionManager.Clear();
      this._buttonSettings.StartHighlight();
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonBack, Constants.DialogFadeTime));
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panelSettings, Constants.DialogFadeTime));
      ActionSequence actionSequence = new ActionSequence();
      actionSequence.ActionManager = this.ActionManager;
      actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeOut);
      actionSequence.Actions.Add((ActionBase) actionParallel);
      actionSequence.ActionFinished += new EventHandler(this.PanelFadeIn_ActionFinished);
      actionSequence.Start();
    }

    private void ShowHighscores()
    {
      this._buttonsEnabled = false;
      this.StopRunningButtonHiglight();
      this.ActionManager.Clear();
      this._buttonHighScore.StartHighlight();
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonBack, Constants.DialogFadeTime));
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panelHighscore, Constants.DialogFadeTime));
      ActionSequence actionSequence = new ActionSequence();
      actionSequence.ActionManager = this.ActionManager;
      actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeOut);
      actionSequence.Actions.Add((ActionBase) actionParallel);
      actionSequence.ActionFinished += new EventHandler(this.PanelFadeIn_ActionFinished);
      actionSequence.Start();
    }

    private void ShowTutorial1()
    {
      this._buttonsEnabled = false;
      Config.Instance.FirstGameStart = false;
      this.StopRunningButtonHiglight();
      this.ActionManager.Clear();
      this._buttonTutorial.StartHighlight();
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonContinue, Constants.DialogFadeTime));
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonMainMenu, Constants.DialogFadeTime));
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panelTutorial1, Constants.DialogFadeTime));
      ActionSequence actionSequence = new ActionSequence();
      actionSequence.ActionManager = this.ActionManager;
      actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeOut);
      actionSequence.Actions.Add((ActionBase) actionParallel);
      actionSequence.ActionFinished += new EventHandler(this.PanelFadeIn_ActionFinished);
      actionSequence.Start();
    }

    private void ShowTrial()
    {
      this._buttonsEnabled = false;
      this.StopRunningButtonHiglight();
      this.ActionManager.Clear();
      this._buttonBuy.StartHighlight();
      ActionParallel actionParallel = new ActionParallel();
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonDoBuy, Constants.DialogFadeTime));
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._buttonMainMenu, Constants.DialogFadeTime));
      actionParallel.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panelTrial, Constants.DialogFadeTime));
      ActionSequence actionSequence = new ActionSequence();
      actionSequence.ActionManager = this.ActionManager;
      actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeOut);
      actionSequence.Actions.Add((ActionBase) actionParallel);
      actionSequence.ActionFinished += new EventHandler(this.PanelFadeIn_ActionFinished);
      actionSequence.Start();
    }

    private void PanelFadeIn_ActionFinished(object sender, EventArgs e)
    {
      this._buttonsEnabled = true;
    }

    private void _buttonStart_Clicked(object sender, EventArgs e)
    {
      if (Config.Instance.FirstGameStart)
      {
        this._firstStartTutorial = true;
        this.ShowTutorial1();
      }
      else
      {
        this._buttonsEnabled = false;
        Config.Instance.LastPlayLevel = -1;
        this.Manager.ShowScreen(Screens.Game, Constants.DialogFadeTime, TimeSpan.FromMilliseconds(300.0));
      }
    }

    private void _buttonFirstGameStart_Clicked(object sender, EventArgs e)
    {
      this._buttonsEnabled = false;
      Config.Instance.LastPlayLevel = -1;
      this.Manager.ShowScreen(Screens.Game, Constants.DialogFadeTime, TimeSpan.FromMilliseconds(300.0));
    }

    private void _buttonContinueGame_Clicked(object sender, EventArgs e)
    {
      this._buttonsEnabled = false;
      this.Manager.ShowScreen(Screens.Game, Constants.DialogFadeTime, TimeSpan.FromMilliseconds(300.0));
    }

    private void _buttonExit_Clicked(object sender, EventArgs e)
    {
      this._buttonsEnabled = false;
      // Явно сохраняем конфигурацию перед выходом через меню,
      // так как OnSuspending может не вызываться при Res.Game.Exit()
      Spinballs.Common.Helper.Config.Instance.Save();
      Res.Game.Exit();
    }

    private void _buttonSettings_Clicked(object sender, EventArgs e) => this.ShowSettings();

    private void _buttonHighScore_Clicked(object sender, EventArgs e) => this.ShowHighscores();

    private void _buttonTutorial_Clicked(object sender, EventArgs e) => this.ShowTutorial1();

    private void _buttonBuy_Clicked(object sender, EventArgs e) => this.ShowTrial();

    private void _buttonDoBuy_Clicked(object sender, EventArgs e)
    {
      // В UWP версии MonoGame нет GamerServices, поэтому убираем эту функцию
      // Guide.ShowMarketplace(PlayerIndex.One);
    }

    private void _buttonBack_Clicked(object sender, EventArgs e)
    {
      if (this._panelSettings.Opacity > (byte) 0)
      {
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        ActionParallel actionParallel = new ActionParallel();
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonBack, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelSettings, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(300.0)));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeIn);
        actionSequence.ActionFinished += new EventHandler(this.StartRunningHighlightOnActionFinished);
        actionSequence.Start();
      }
      else
      {
        if (this._panelHighscore.Opacity <= (byte) 0)
          return;
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        ActionParallel actionParallel = new ActionParallel();
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonBack, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelHighscore, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(300.0)));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeIn);
        actionSequence.ActionFinished += new EventHandler(this.StartRunningHighlightOnActionFinished);
        actionSequence.Start();
      }
    }

    private void _buttonMainMenu_Clicked(object sender, EventArgs e)
    {
      if (this._panelTutorial1.Opacity > (byte) 0)
      {
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        ActionParallel actionParallel = new ActionParallel();
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonMainMenu, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonContinue, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelTutorial1, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(300.0)));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeIn);
        actionSequence.ActionFinished += new EventHandler(this.StartRunningHighlightOnActionFinished);
        actionSequence.Start();
      }
      else if (this._panelTutorial2.Opacity > (byte) 0)
      {
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        ActionParallel actionParallel = new ActionParallel();
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonMainMenu, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonContinue, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelTutorial2, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(300.0)));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeIn);
        actionSequence.ActionFinished += new EventHandler(this.StartRunningHighlightOnActionFinished);
        actionSequence.Start();
      }
      else if (this._panelTutorial3.Opacity > (byte) 0)
      {
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        ActionParallel actionParallel = new ActionParallel();
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonMainMenu, Constants.DialogFadeTime));
        if (this._buttonFirstGameStart.Opacity > (byte) 0)
          actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonFirstGameStart, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelTutorial3, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(300.0)));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeIn);
        actionSequence.ActionFinished += new EventHandler(this.StartRunningHighlightOnActionFinished);
        actionSequence.Start();
      }
      else
      {
        if (this._panelTrial.Opacity <= (byte) 0)
          return;
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        ActionParallel actionParallel = new ActionParallel();
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonMainMenu, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._buttonDoBuy, Constants.DialogFadeTime));
        actionParallel.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelTrial, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(300.0)));
        actionSequence.Actions.Add((ActionBase) actionParallel);
        actionSequence.Actions.Add((ActionBase) this._actionButtonsFadeIn);
        actionSequence.ActionFinished += new EventHandler(this.StartRunningHighlightOnActionFinished);
        actionSequence.Start();
      }
    }

    private void StartRunningHighlightOnActionFinished(object sender, EventArgs e)
    {
      this._actionRunningButtonHighlight.Start();
    }

    private void _buttonContinue_Clicked(object sender, EventArgs e)
    {
      if (this._panelTutorial1.Opacity > (byte) 0)
      {
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        actionSequence.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._panelTutorial1, Constants.DialogFadeTime));
        actionSequence.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panelTutorial2, Constants.DialogFadeTime));
        actionSequence.Start();
      }
      else
      {
        if (this._panelTutorial2.Opacity <= (byte) 0)
          return;
        ActionSequence actionSequence = new ActionSequence();
        actionSequence.ActionManager = this.ActionManager;
        actionSequence.Actions.Add((ActionBase) new ActionParallel()
        {
          Actions = {
            (ActionBase) new ActionFadeOut((DrawableControl) this._buttonContinue, Constants.DialogFadeTime),
            (ActionBase) new ActionFadeOut((DrawableControl) this._panelTutorial2, Constants.DialogFadeTime)
          }
        });
        if (this._firstStartTutorial)
          actionSequence.Actions.Add((ActionBase) new ActionParallel()
          {
            Actions = {
              (ActionBase) new ActionFadeIn((DrawableControl) this._buttonFirstGameStart, Constants.DialogFadeTime),
              (ActionBase) new ActionFadeIn((DrawableControl) this._panelTutorial3, Constants.DialogFadeTime)
            }
          });
        else
          actionSequence.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._panelTutorial3, Constants.DialogFadeTime));
        actionSequence.Start();
      }
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      this.Texture = (Texture2D) null;
      this._buttonStart.Destroy();
      AudioManager.StopMusic();
    }

    public override void OnBackButton(GameTime gameTime)
    {
      if (this._panelSettings.Opacity > (byte) 0 || this._panelHighscore.Opacity > (byte) 0)
        this._buttonBack_Clicked((object) null, (EventArgs) null);
      else if (this._panelTutorial1.Opacity > (byte) 0 || this._panelTutorial2.Opacity > (byte) 0 
                || this._panelTutorial3.Opacity > (byte) 0 || this._panelTrial.Opacity > (byte) 0)
        this._buttonMainMenu_Clicked((object) null, (EventArgs) null);
      else
        base.OnBackButton(gameTime);
    }

    public override bool Enabled
    {
      get => base.Enabled;
      set
      {
        if (this.Enabled == value)
          return;
        base.Enabled = value;
        if (!base.Enabled)
          return;
        AudioManager.PlayMusic(Res.StartScreen.MusicIntro);
        this._panelHighscore.Create();
        this._panelSettings.Create();
      }
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (!this.Enabled || !this._buttonsEnabled)
        return;

      foreach (TouchLocation touchLocation in Res.Input.TouchState)
      {
        if (touchLocation.State == TouchLocationState.Pressed)
        {
          // Тач приходит в физических координатах, переводим в игровые и используем общую логику HandleTap
          Vector2 gamePos = Res.ConvertCoordinates(touchLocation.Position);
          this.HandleTap(gamePos, gameTime);
        }
      }

      // Явная обработка мыши: новый клик левой кнопкой → HandleTap в игровых координатах
      if (Res.Input.IsNewMouseButtonPress(MouseButtons.Left))
      {
        Vector2 mousePos = Res.GetMousePositionInGameCoords();
        this.HandleTap(mousePos, gameTime);
      }

      if (!this._panelSettings.Visible || this._panelSettings.Opacity <= (byte) 0)
        return;
      this._panelSettings.HandleInput();
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      if (!this.Enabled || !this._buttonsEnabled)
        return;

      bool handled = false;
      foreach (DrawableControl button in this._buttons)
      {
        if (button.Enabled && button.Opacity > (byte)0 && button.Contains(tapPos))
        {
          button.OnClick((object)this);
          handled = true;
          break;
        }
      }

      if (!handled)
      {
        if (this._buttonBack.Opacity > (byte)0 && this._buttonBack.Contains(tapPos))
          this._buttonBack.OnClick((object)this);
        else if (this._buttonMainMenu.Opacity > (byte)0 && this._buttonMainMenu.Contains(tapPos))
          this._buttonMainMenu.OnClick((object)this);
        else if (this._buttonContinue.Opacity > (byte)0 && this._buttonContinue.Contains(tapPos))
          this._buttonContinue.OnClick((object)this);
        else if (this._buttonDoBuy.Opacity > (byte)0 && this._buttonDoBuy.Contains(tapPos))
          this._buttonDoBuy.OnClick((object)this);
        else if (this._buttonFirstGameStart.Opacity > (byte)0 && this._buttonFirstGameStart.Contains(tapPos))
          this._buttonFirstGameStart.OnClick((object)this);
      }
    }

    protected override void DrawCore(SpriteBatch spriteBatch, GameTime gameTime)
    {
      this._logoHighlight.Draw(Res.SpriteBatch);
      foreach (DrawableControl button in this._buttons)
        button.Draw(spriteBatch);
      this._panelSettings.Draw(spriteBatch);
      this._panelHighscore.Draw(spriteBatch);
      this._panelTutorial1.Draw(spriteBatch);
      this._panelTutorial2.Draw(spriteBatch);
      this._panelTutorial3.Draw(spriteBatch);
      this._panelTrial.Draw(spriteBatch);
      this._buttonBack.Draw(spriteBatch);
      this._buttonMainMenu.Draw(spriteBatch);
      this._buttonContinue.Draw(spriteBatch);
      this._buttonDoBuy.Draw(spriteBatch);
      this._buttonFirstGameStart.Draw(spriteBatch);
    }
  }
}
