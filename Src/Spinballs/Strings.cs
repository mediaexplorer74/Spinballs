// Decompiled with JetBrains decompiler
// Type: Spinballs.Strings
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Windows.ApplicationModel.Resources;

#nullable disable
namespace Spinballs
{
    internal class Strings
    {
        // Используем UWP ResourceLoader для загрузки строк
        // из ресурсного набора "Strings" (файл Strings.resw и его локализованные варианты).
        private static readonly ResourceLoader resourceLoader =
            ResourceLoader.GetForViewIndependentUse("Resources");

        internal static string Back => resourceLoader.GetString("Back");

        internal static string BestScore => resourceLoader.GetString("BestScore");

        internal static string BuyGame => resourceLoader.GetString("BuyGame");

        internal static string Continue => resourceLoader.GetString("Continue");

        internal static string ContinueGame => resourceLoader.GetString("ContinueGame");

        internal static string Exit => resourceLoader.GetString("Exit");

        internal static string ExitGame => resourceLoader.GetString("ExitGame");

        internal static string Fullversion => resourceLoader.GetString("Fullversion");

        internal static string GameOver => resourceLoader.GetString("GameOver");

        internal static string GamePaused => resourceLoader.GetString("GamePaused");

        internal static string Highscore => resourceLoader.GetString("Highscore");

        internal static string Level => resourceLoader.GetString("Level");

        internal static string MainMenu => resourceLoader.GetString("MainMenu");

        internal static string Menu => resourceLoader.GetString("Menu");

        internal static string Music => resourceLoader.GetString("Music");

        internal static string NewHighScore => resourceLoader.GetString("NewHighScore");

        internal static string NoHighScore => resourceLoader.GetString("NoHighScore");

        internal static string PlayAgain => resourceLoader.GetString("PlayAgain");

        internal static string Points => resourceLoader.GetString("Points");

        internal static string Rank => resourceLoader.GetString("Rank");

        internal static string Register => resourceLoader.GetString("Register");

        internal static string Settings => resourceLoader.GetString("Settings");

        internal static string Sound => resourceLoader.GetString("Sound");

        internal static string Spinballs => resourceLoader.GetString("Spinballs");

        internal static string Start => resourceLoader.GetString("Start");

        internal static string Trial => resourceLoader.GetString("Trial");

        internal static string TrialTitle => resourceLoader.GetString("TrialTitle");

        internal static string Tutorial => resourceLoader.GetString("Tutorial");

        internal static string Tutorial1 => resourceLoader.GetString("Tutorial1");

        internal static string Tutorial2 => resourceLoader.GetString("Tutorial2");

        internal static string Tutorial3 => resourceLoader.GetString("Tutorial3");

        internal static string TutorialTitle => resourceLoader.GetString("TutorialTitle");
    }
}
