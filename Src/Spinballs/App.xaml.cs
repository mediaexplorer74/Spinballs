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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Windows.UI.Xaml.Navigation;
using Spinballs.Core.ScreenManagement;
using Spinballs.Common.Helper;

namespace Spinballs
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Load state from previously suspended application
                    var savegame = Spinballs.Common.Helper.SaveGame.Load();
                    if (savegame != null)
                    {
                        // The actual loading will be handled by the game when it's initialized
                        // We'll pass the information through a static variable or property
                        Spinballs.Core.ScreenManagement.ScreenManager.PendingSaveGame = savegame;
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    // rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    // Instead of navigating to MainPage, we will set the content directly
                    // to a new MainPage instance which will handle the MonoGame initialization.
                    Window.Current.Content = new MainPage();
                }

                // Enable the media features
                Res.CanUseMusic = true;

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        // Метод для получения экземпляра игры
        public static Game1 GetGameInstance()
        {
            // В разных моментах жизненного цикла Window.Current.Content может быть
            // либо непосредственно MainPage, либо Frame, внутри которого лежит MainPage.
            var content = Windows.UI.Xaml.Window.Current.Content;

            // Пытаемся сначала получить MainPage напрямую
            var mainPage = content as MainPage;

            // Если содержимое окна — Frame, ищем MainPage внутри него
            if (mainPage == null && content is Windows.UI.Xaml.Controls.Frame frame)
            {
                mainPage = frame.Content as MainPage;
            }

            return mainPage != null ? mainPage._game : null;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            // Save application state and stop any background activity

            try
            {
                var game = GetGameInstance();
                if (game != null && game.ScreenManager != null)
                {
                    var savegame = new Spinballs.Common.Helper.SaveGame();
                    if (game.ScreenManager.Save(savegame))
                    {
                        savegame.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[ex] App.xaml.cs - OnSuspending - SaveGame handling error: " + ex.Message);
            }

            Spinballs.Common.Helper.Config.Instance.Save();
            deferral.Complete();
        }
    }
}
