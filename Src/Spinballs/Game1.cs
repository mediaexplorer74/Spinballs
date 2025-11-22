using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spinballs.Common.Helper;
using Spinballs.Core.ScreenManagement;
using Spinballs.View;

namespace Spinballs
{
    /// <summary>
    /// Игровой класс для UWP приложения
    /// </summary>
    public sealed partial class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ScreenManager ScreenManager { get; private set; }
        public static SaveGame PendingSaveGame { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Инициализируем Res
            //Res.Init(this);
            //RnD
            //Res.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Initialize()
        {
            // Создаем ScreenManager
            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);

            // Добавляем экраны
            ScreenManager.AddScreen(new Spinballs.View.SplashScreen());
            ScreenManager.AddScreen(new StartScreen());
            ScreenManager.AddScreen(new GameScreen());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //RnD
            Res.Init(this);
            
            Res.SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Загружаем начальный экран
            if (PendingSaveGame != null)
            {
                ScreenManager.Load(PendingSaveGame);
                PendingSaveGame = null;
            }
            else
            {
                ScreenManager.ShowScreen(Screens.Splash, TimeSpan.Zero);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}