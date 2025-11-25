// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.ScreenManagement.ScreenManager
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.View;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Core.ScreenManagement
{
    public class ScreenManager : DrawableGameComponent
    {
        private Dictionary<int, BaseScreen> _screens = new Dictionary<int, BaseScreen>();
        private static BaseScreen _activeScreen;
        private ActionManager _actionManager;
        private static GameTime _gameTime;
        private TimeSpan _debugTestTime;
        private bool _printMemoryUsage = false; // !
        private BaseScreen _transitionScreen;
        private TimeSpan _transitionDuration;
        private TimeSpan _transitionStart;
        private TimeSpan _transitionDelay;
        private bool _transitionStarted = false;
        
        public static SaveGame PendingSaveGame { get; set; }

        public ScreenManager(Game game)
            : base(game)
        {
            ScreenManager.GameTime = new GameTime();
            this._actionManager = new ActionManager();
            this._debugTestTime = new TimeSpan();
        }

        public Dictionary<int, BaseScreen> Screens
        {
            get => this._screens;
            set => this._screens = value;
        }

        public static BaseScreen ActiveScreen
        {
            get => ScreenManager._activeScreen;
            set
            {
                if (ScreenManager._activeScreen == value)
                    return;
                if (ScreenManager._activeScreen != null)
                {
                    ScreenManager._activeScreen.Visible = false;
                    ScreenManager._activeScreen.Enabled = false;
                }
                ScreenManager._activeScreen = value;
                if (ScreenManager._activeScreen == null)
                    return;
                ScreenManager._activeScreen.Visible = true;
                ScreenManager._activeScreen.Enabled = true;
            }
        }

        public SplashScreen SplashScreen => this.Screens[0] as SplashScreen;

        public Spinballs.View.StartScreen StartScreen => this.Screens[1] as Spinballs.View.StartScreen;

        public Spinballs.View.GameScreen GameScreen => this.Screens[2] as Spinballs.View.GameScreen;

        public static GameTime GameTime
        {
            get => ScreenManager._gameTime;
            set => ScreenManager._gameTime = value;
        }

        public static void DebugOut(string format, params object[] objects)
        {
        }

        public void AddScreen(BaseScreen screen)
        {
            screen.Enabled = false;
            screen.Visible = false;
            this.Screens.Add(screen.Id, screen);
            screen.Manager = this;
        }

        public void Pause()
        {
            if (ScreenManager.ActiveScreen != null)
                ScreenManager.ActiveScreen.Pause();
            if (this._transitionScreen == null)
                return;
            this._transitionScreen.Pause();
        }

        public void ShowScreen(Spinballs.Common.Helper.Screens screen, TimeSpan transitionDuration)
        {
            this.ShowScreen(this.Screens[(int) screen], transitionDuration, TimeSpan.Zero);
        }

        public void ShowScreen(Spinballs.Common.Helper.Screens screen, TimeSpan transitionDuration, TimeSpan delay)
        {
            this.ShowScreen(this.Screens[(int) screen], transitionDuration, delay);
        }

        public void ShowScreen(BaseScreen screen, TimeSpan transitionDuration, TimeSpan delay)
        {
            this._transitionScreen = screen;
            this._transitionDuration = transitionDuration;
            this._transitionDelay = delay;
            this._transitionStart = ScreenManager.GameTime.TotalGameTime;
            if (!this._transitionScreen.ContentLoaded)
            {
                this._transitionStarted = false;
                this._transitionScreen.LoadContent();
                this._transitionScreen.Init();
                this.StartScreenTransition();
            }
            else
            {
                this._transitionScreen.Init();
                this.StartScreenTransition();
            }
        }

        private void LoadTransitionContent(object obj)
        {
            if (this._transitionScreen == null)
                return;
            this._transitionScreen.LoadContent();
        }

        private void StartScreenTransition()
        {
            this._transitionStarted = true;
            if (this._transitionDuration != TimeSpan.Zero)
            {
                this._transitionScreen.Opacity = (byte) 0;
                this._transitionScreen.Visible = false;
                if (!this._transitionScreen.ContentLoaded)
                    this._transitionScreen.LoadContent();
                ActionSequence action = new ActionSequence();

                if (this._transitionDelay != TimeSpan.Zero 
                    && (this._transitionDelay - (ScreenManager.GameTime.TotalGameTime - this._transitionStart)).TotalMilliseconds > 0.0)
                    action.Actions.Add((ActionBase) new ActionDuration(this._transitionDelay));

                action.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._transitionScreen, this._transitionDuration));
                action.ActionFinished += new EventHandler(this._transitionAction_ActionFinished);
                this._actionManager.Add((ActionBase) action);
            }
            else
            {
                //RnD
                ScreenManager.ActiveScreen = this._transitionScreen;
                this._transitionScreen = (BaseScreen) null;
                this._transitionDuration = TimeSpan.Zero;
            }
        }

        private void _transitionAction_ActionFinished(object sender, EventArgs e)
        {
            //RnD
            if (this._transitionScreen != null)
              ScreenManager.ActiveScreen = this._transitionScreen;

            this._transitionScreen = (BaseScreen) null;
            this._transitionDuration = TimeSpan.Zero;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            // Check if there's a pending save game to load (from app suspension)
            if (PendingSaveGame != null)
            {
                this.Load(PendingSaveGame);
                PendingSaveGame = null; // Clear the pending save after loading
            }
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            foreach (KeyValuePair<int, BaseScreen> screen in this._screens)
                screen.Value.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ScreenManager.GameTime = gameTime;
            if (!this._transitionStarted && this._transitionScreen != null && this._transitionScreen.ContentLoaded)
                this.StartScreenTransition();
            if (this._printMemoryUsage && (gameTime.TotalGameTime - this._debugTestTime).TotalMilliseconds > 2000.0)
                this._debugTestTime = gameTime.TotalGameTime;
            Res.Input.Update();
            // RnD
            if (ScreenManager.ActiveScreen != null)
                ScreenManager.ActiveScreen.Update(gameTime);
            if (this._transitionScreen == null || !this._transitionScreen.ContentLoaded)
                return;
            this._transitionScreen.Update(gameTime);
            this._actionManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (ScreenManager.ActiveScreen != null)
                ScreenManager.ActiveScreen.Draw(gameTime);
            if (this._transitionScreen == null || !this._transitionScreen.ContentLoaded || !this._transitionScreen.Visible)
                return;
            this._transitionScreen.Draw(gameTime);
        }

        public bool Save(SaveGame savegame)
        {
            return ScreenManager.ActiveScreen != null && ScreenManager.ActiveScreen.Save(savegame);
        }

        public void Load(SaveGame savegame)
        {
            if (savegame.ActiveScreenId < 0)
                return;
            
            BaseScreen screen = this.Screens[savegame.ActiveScreenId];
            screen.IsNewGame = false;
            if (!screen.ContentLoaded)
                screen.LoadContent();
            ScreenManager.ActiveScreen = screen;
            ScreenManager.ActiveScreen.Load(savegame);
            ScreenManager.ActiveScreen.IsNewGame = true;
        }
    }
}
