// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Res
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Spinballs.Core;

#nullable disable
namespace Spinballs.Common.Helper
{
  public sealed class Res
  {
    public static bool IsTrial = false;
    private static ContentManager _content;
    private static Game _game;
    public static SpriteBatch SpriteBatch;
    public static InputState Input = new InputState();
    private static bool _canUseMusic;
    public static object LoadGameContentObj = new object();
    public static Microsoft.Xna.Framework.Vector2 ScaleFactor = Microsoft.Xna.Framework.Vector2.One;
    public static Microsoft.Xna.Framework.Vector2 ScreenOffset = Microsoft.Xna.Framework.Vector2.Zero;

    public static void Init(Game game)
    {
        Res._game = game;
        Res._content = Res._game.Content;
    }

    // Метод для преобразования физических координат в игровые координаты
    public static Microsoft.Xna.Framework.Vector2 ConvertCoordinates(Microsoft.Xna.Framework.Vector2 physicalCoords)
    {
        // Преобразование из физических координат в игровые с учетом масштаба и смещения
        // Только если масштаб не равен единице и смещение не равно нулю
        if (ScaleFactor.X == 1.0f && ScaleFactor.Y == 1.0f && ScreenOffset.X == 0.0f && ScreenOffset.Y == 0.0f)
        {
            // Если масштаб не установлен, возвращаем исходные координаты
            return physicalCoords;
        }
        else
        {
            float gameX = (physicalCoords.X - ScreenOffset.X) / ScaleFactor.X;
            float gameY = (physicalCoords.Y - ScreenOffset.Y) / ScaleFactor.Y;
            return new Microsoft.Xna.Framework.Vector2(gameX, gameY);
        }
    }

    // Метод для получения позиции мыши в игровых координатах
    public static Microsoft.Xna.Framework.Vector2 GetMousePositionInGameCoords()
    {
        var mouseState = Input.CurrentMouseState;
        Microsoft.Xna.Framework.Vector2 mousePos = new Microsoft.Xna.Framework.Vector2(mouseState.X, mouseState.Y);
        return ConvertCoordinates(mousePos);
    }

    public static Game Game => Res._game;

    public static bool CanUseMusic
    {
      get => Res._canUseMusic;
      set => Res._canUseMusic = value;
    }

    public static void LoadStartContent()
    {
      Song musicIntro = Res.StartScreen.MusicIntro;
      Texture2D background = Res.StartScreen.Background;
      Texture2D button = Res.StartScreen.Button;
      Texture2D buttonHighlight = Res.StartScreen.ButtonHighlight;
      Texture2D logoHighlight = Res.StartScreen.LogoHighlight;
      Texture2D tutorial1 = Res.StartScreen.Tutorial1;
      Texture2D tutorial2 = Res.StartScreen.Tutorial2;
      Texture2D tutorial3 = Res.StartScreen.Tutorial3;
      SpriteFont big = Res.Font.Big;
      SpriteFont big2 = Res.Font.Big2;
      SpriteFont big3 = Res.Font.Big3;
      SpriteFont big8 = Res.Font.Big8;
      SpriteFont spriteFont = Res.Font.Default;
      SpriteFont medium = Res.Font.Medium;
      SpriteFont small = Res.Font.Small;
      Texture2D arrowRight = Res.Common.ArrowRight;
      Texture2D arrowRightHighlight = Res.Common.ArrowRightHighlight;
      Texture2D panel = Res.Common.Panel;
      Texture2D slider = Res.Common.Slider;
      Texture2D sliderHighlight = Res.Common.SliderHighlight;
    }

    public static void LoadGameContent()
    {
      lock (Res.LoadGameContentObj)
      {
        Texture2D background = Res.GameScreen.Background;
        Texture2D ballBlue = Res.GameScreen.BallBlue;
        Texture2D ballGreen = Res.GameScreen.BallGreen;
        Texture2D ballHighlight = Res.GameScreen.BallHighlight;
        Texture2D ballRed = Res.GameScreen.BallRed;
        Texture2D ballYellow = Res.GameScreen.BallYellow;
        Texture2D bonusConnectorNorth = Res.GameScreen.BonusConnectorNorth;
        Texture2D bonusConnectorNorth2 = Res.GameScreen.BonusConnectorNorth2;
        Texture2D connectorNorthWest = Res.GameScreen.BonusConnectorNorthWest;
        Texture2D connectorNorthWest2 = Res.GameScreen.BonusConnectorNorthWest2;
        Texture2D buttonMenu = Res.GameScreen.ButtonMenu;
        Texture2D cup = Res.GameScreen.Cup;
        Texture2D discHighlight = Res.GameScreen.DiscHighlight;
        Texture2D executeHighlight = Res.GameScreen.ExecuteHighlight;
        Texture2D extraCorona = Res.GameScreen.ExtraCorona;
        Texture2D extraExplode = Res.GameScreen.ExtraExplode;
        Texture2D extraFloatingX2 = Res.GameScreen.ExtraFloatingX2;
        Texture2D extraSlowMo = Res.GameScreen.ExtraSlowMo;
        Texture2D extraSort = Res.GameScreen.ExtraSort;
        Texture2D extraX2 = Res.GameScreen.ExtraX2;
        Texture2D levelUp1 = Res.GameScreen.LevelUp;
        Texture2D points = Res.GameScreen.Points;
        Texture2D timerBar = Res.GameScreen.TimerBar;
        Texture2D timerBarIced = Res.GameScreen.TimerBarIced;
        for (int i = 0; i < 21; ++i)
          Res.GameScreen.GetLevelBarTexture(i);
        SoundEffect button = Res.GameScreen.Sounds.Button;
        SoundEffect discTurn = Res.GameScreen.Sounds.DiscTurn;
        SoundEffect executeChain = Res.GameScreen.Sounds.ExecuteChain;
        SoundEffect explode = Res.GameScreen.Sounds.Explode;
        SoundEffect extraLoaded = Res.GameScreen.Sounds.ExtraLoaded;
        SoundEffect extraLoading = Res.GameScreen.Sounds.ExtraLoading;
        SoundEffect extraPoints = Res.GameScreen.Sounds.ExtraPoints;
        SoundEffect extraPointsEnd = Res.GameScreen.Sounds.ExtraPointsEnd;
        SoundEffect extraPointsStart = Res.GameScreen.Sounds.ExtraPointsStart;
        SoundEffect gameOverBestScore = Res.GameScreen.Sounds.GameOverBestScore;
        SoundEffect gameOverHighscore = Res.GameScreen.Sounds.GameOverHighscore;
        SoundEffect gameOverNoHighscore = Res.GameScreen.Sounds.GameOverNoHighscore;
        SoundEffect levelUp2 = Res.GameScreen.Sounds.LevelUp;
        SoundEffect sort = Res.GameScreen.Sounds.Sort;
        SoundEffect timeCritical = Res.GameScreen.Sounds.TimeCritical;
        SoundEffect timeFaster = Res.GameScreen.Sounds.TimeFaster;
        SoundEffect timeSlower = Res.GameScreen.Sounds.TimeSlower;
        Song music = Res.GameScreen.Sounds.Music;
      }
    }

    public sealed class Font
    {
      public static SpriteFont Default => Res._content.Load<SpriteFont>("Common/FontDefault");

      public static SpriteFont Medium => Res._content.Load<SpriteFont>("Common/FontMedium");

      public static SpriteFont Small => Res._content.Load<SpriteFont>("Common/FontSmall");

      public static SpriteFont Big => Res._content.Load<SpriteFont>("Common/FontBig");

      public static SpriteFont Big2 => Res._content.Load<SpriteFont>("Common/FontBig2");

      public static SpriteFont Big3 => Res._content.Load<SpriteFont>("Common/FontBig3");

      public static SpriteFont Big4 => Res._content.Load<SpriteFont>("Common/FontBig4");

      public static SpriteFont Big5 => Res._content.Load<SpriteFont>("Common/FontBig5");

      public static SpriteFont Big8 => Res._content.Load<SpriteFont>("Common/FontBig8");
    }

    public sealed class FontSegoe
    {
      public static SpriteFont Default => Res._content.Load<SpriteFont>("Common/Segoe16Bold");

      public static SpriteFont Medium => Res._content.Load<SpriteFont>("Common/Segoe16");

      public static SpriteFont Big => Res._content.Load<SpriteFont>("Common/Segoe20Bold");

      public static SpriteFont VeryBig => Res._content.Load<SpriteFont>("Common/Segoe24Bold");

      public static SpriteFont Biggest => Res._content.Load<SpriteFont>("Common/Segoe30Bold");
    }

    public sealed class Common
    {
      public static Texture2D ArrowRight
      {
        get => Res._content.Load<Texture2D>("Common/Arrow-Right_HighRes");
      }

      public static Texture2D ArrowRightHighlight
      {
        get => Res._content.Load<Texture2D>("Common/Arrow-Right_HighRes_Highlight");
      }

      public static Texture2D Panel => Res._content.Load<Texture2D>("Common/Panel_HighRes");

      public static Texture2D Slider => Res._content.Load<Texture2D>("Common/Slider_HighRes");

      public static Texture2D SliderHighlight
      {
        get => Res._content.Load<Texture2D>("Common/Slider-Highlight_HighRes");
      }

      public static Texture2D LoadIcon
      {
        get => Res._content.Load<Texture2D>("Common/loading-icon-256x256");
      }

      public static Texture2D LoadIconText
      {
        get => Res._content.Load<Texture2D>("Common/loading-icon-text-256x256");
      }
    }

    public sealed class GameScreen
    {
      public static Texture2D Background
      {
        get => Res._content.Load<Texture2D>("GameScreen/Background_480x800");
      }

      public static Texture2D ButtonMenu => Res._content.Load<Texture2D>("GameScreen/ButtonMenu");

      public static Texture2D BallBlue => Res._content.Load<Texture2D>("GameScreen/Ball-Blue");

      public static Texture2D BallGreen => Res._content.Load<Texture2D>("GameScreen/Ball-Green");

      public static Texture2D BallRed => Res._content.Load<Texture2D>("GameScreen/Ball-Red");

      public static Texture2D BallYellow => Res._content.Load<Texture2D>("GameScreen/Ball-Yellow");

      public static Texture2D BallHighlight
      {
        get => Res._content.Load<Texture2D>("GameScreen/Ball-Highlight");
      }

      public static Texture2D DiscHighlight
      {
        get => Res._content.Load<Texture2D>("GameScreen/Disc-Highlight");
      }

      public static Texture2D TimerBar
      {
        get => Res._content.Load<Texture2D>("GameScreen/TimeBar_HighRes");
      }

      public static Texture2D TimerBarIced
      {
        get => Res._content.Load<Texture2D>("GameScreen/TimeBarIced_HighRes");
      }

      public static Texture2D BonusConnectorNorth
      {
        get => Res._content.Load<Texture2D>("GameScreen/Bonus-Connector-North");
      }

      public static Texture2D BonusConnectorNorth2
      {
        get => Res._content.Load<Texture2D>("GameScreen/Bonus-Connector-North2");
      }

      public static Texture2D BonusConnectorNorthWest
      {
        get => Res._content.Load<Texture2D>("GameScreen/Bonus-Connector-North-West");
      }

      public static Texture2D BonusConnectorNorthWest2
      {
        get => Res._content.Load<Texture2D>("GameScreen/Bonus-Connector-North-West2");
      }

      public static Texture2D Points => Res._content.Load<Texture2D>("GameScreen/Points");

      public static Texture2D ExecuteHighlight
      {
        get => Res._content.Load<Texture2D>("GameScreen/Execute-Highlight");
      }

      public static Texture2D ExtraCorona
      {
        get => Res._content.Load<Texture2D>("GameScreen/Extra_Corona_Alpha_128x128_2");
      }

      public static Texture2D ExtraExplode
      {
        get => Res._content.Load<Texture2D>("GameScreen/Extra_Loading_Explode_73x73");
      }

      public static Texture2D ExtraSlowMo
      {
        get => Res._content.Load<Texture2D>("GameScreen/Extra_Loading_SlowMo_73x73");
      }

      public static Texture2D ExtraSort
      {
        get => Res._content.Load<Texture2D>("GameScreen/Extra_Loading_Sort_73x73");
      }

      public static Texture2D ExtraX2
      {
        get => Res._content.Load<Texture2D>("GameScreen/Extra_Loading_x2_73x73");
      }

      public static Texture2D ExtraFloatingX2
      {
        get => Res._content.Load<Texture2D>("GameScreen/Extra_Loading_x2_62x64");
      }

      public static Texture2D Cup => Res._content.Load<Texture2D>("GameScreen/Cup_HighRes");

      public static Texture2D LevelUp => Res._content.Load<Texture2D>("GameScreen/LevelUp");

      public static Texture2D GetLevelBarTexture(int i)
      {
        string assetName = string.Format("GameScreen/LevelBar/LevelBar_HighRes_{0:00}", (object) i);
        return Res._content.Load<Texture2D>(assetName);
      }

      public sealed class Sounds
      {
        public static SoundEffect DiscTurn
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_DiscTurn");
        }

        public static SoundEffect ExecuteChain
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_ExecuteChain");
        }

        public static SoundEffect LevelUp => Res._content.Load<SoundEffect>("Sounds/Sound_LevelUp");

        public static SoundEffect TimeCritical
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_TimeCritical");
        }

        public static SoundEffect TimeFaster
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_TimeFaster");
        }

        public static SoundEffect TimeSlower
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_TimeSlower");
        }

        public static SoundEffect ExtraLoaded
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_ExtraLoaded");
        }

        public static SoundEffect ExtraLoading
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_ExtraLoading");
        }

        public static SoundEffect ExtraPoints
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_ExtraPoints");
        }

        public static SoundEffect ExtraPointsStart
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_MultiplyPointsStart");
        }

        public static SoundEffect ExtraPointsEnd
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_MultiplyPointsEnd");
        }

        public static SoundEffect Explode => Res._content.Load<SoundEffect>("Sounds/Sound_Explode");

        public static SoundEffect Sort => Res._content.Load<SoundEffect>("Sounds/Sound_Sort");

        public static SoundEffect GameOverNoHighscore
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_GameOverNoHighscore");
        }

        public static SoundEffect GameOverBestScore
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_GameOverBestScore");
        }

        public static SoundEffect GameOverHighscore
        {
          get => Res._content.Load<SoundEffect>("Sounds/Sound_GameOverHighscore");
        }

        public static SoundEffect Button => Res._content.Load<SoundEffect>("Sounds/Sound_Button");

        public static Song Music => Res._content.Load<Song>("Sounds/Music_Game");
      }
    }

    public sealed class StartScreen
    {
      public static Song MusicIntro => Res._content.Load<Song>("Sounds/Music_Intro");

      public static Texture2D Background
      {
        get => Res._content.Load<Texture2D>("StartScreen/StartScreen_HighRes");
      }

      public static Texture2D Button
      {
        get => Res._content.Load<Texture2D>("StartScreen/Menu-Button_HighRes");
      }

      public static Texture2D ButtonHighlight
      {
        get => Res._content.Load<Texture2D>("StartScreen/Menu-Button-HL_HighRes");
      }

      public static Texture2D LogoHighlight
      {
        get => Res._content.Load<Texture2D>("StartScreen/Start-Logo-Highlight");
      }

      public static Texture2D Tutorial1 => Res._content.Load<Texture2D>("StartScreen/Tutorial1");

      public static Texture2D Tutorial2 => Res._content.Load<Texture2D>("StartScreen/Tutorial2");

      public static Texture2D Tutorial3 => Res._content.Load<Texture2D>("StartScreen/Tutorial3");
    }
  }
}
