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
        
        // *********************************************************************
        Vector2 baseScreenSize = new Vector2(480, 800);
        public Microsoft.Xna.Framework.Matrix globalTransformation;
        
        public static bool FirstResize = true;
        public static Vector3 screenScale;
        // *********************************************************************

        public ScreenManager ScreenManager { get; private set; }
        public static SaveGame PendingSaveGame { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Включаем возможность изменения размера окна
            graphics.SupportedOrientations = DisplayOrientation.Default;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = true;//false;

            
        }

        protected override void Initialize()
        {
            // Настройка графики для масштабирования
            graphics.PreferredBackBufferWidth = (int)baseScreenSize.X;  // Ширина оригинального экрана
            graphics.PreferredBackBufferHeight = (int)baseScreenSize.Y; // Высота оригинального экрана
            graphics.ApplyChanges();

            // Подписываемся на события изменения размера экрана
            graphics.DeviceReset += OnDeviceReset;

            // Создаем ScreenManager
            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);

            // Добавляем экраны
            ScreenManager.AddScreen(new Spinballs.View.SplashScreen());
            ScreenManager.AddScreen(new StartScreen());
            ScreenManager.AddScreen(new GameScreen());

            base.Initialize();
        }

        private void OnDeviceReset(object sender, EventArgs e)
        {
            // Обновляем масштаб при изменении размера устройства
            UpdateCoordinateTransform();
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

            // Сохраняем последнее GameTime
            LastGameTime = gameTime;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Вычисляем масштаб для адаптации к различным размерам экрана
            CalculateGlobalScale();

            // Устанавливаем ScaleFactor и ScreenOffset для преобразования координат
            UpdateCoordinateTransform();

            // Рисуем с учетом масштаба
            spriteBatch.Begin(transformMatrix: globalTransformation, samplerState: SamplerState.PointClamp);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        private void UpdateCoordinateTransform()
        {
            // Получаем размеры окна
            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;

            // Размеры оригинального игрового поля
            float originalWidth = baseScreenSize.X;
            float originalHeight = baseScreenSize.Y;

            // Вычисляем масштаб по ширине и высоте
            float scaleX = (float)windowWidth / originalWidth;
            float scaleY = (float)windowHeight / originalHeight;

            // Устанавливаем значения для преобразования координат без сохранения пропорций
            Res.ScaleFactor = new Microsoft.Xna.Framework.Vector2(scaleX, scaleY);
            Res.ScreenOffset = new Microsoft.Xna.Framework.Vector2(0f, 0f);
        }

        public GameTime LastGameTime { get; private set; }

        private void CalculateGlobalScale()
        {
            // Получаем размеры окна
            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;

            // Размеры оригинального игрового поля
            float originalWidth = baseScreenSize.X;
            float originalHeight = baseScreenSize.Y;

            // Вычисляем масштаб по ширине и высоте
            float scaleX = (float)windowWidth / originalWidth;
            float scaleY = (float)windowHeight / originalHeight;

            // Создаем матрицу трансформации без сохранения пропорций и без смещения
            globalTransformation = 
                Microsoft.Xna.Framework.Matrix.CreateScale(scaleX, scaleY, 1f);
        }
    }
}