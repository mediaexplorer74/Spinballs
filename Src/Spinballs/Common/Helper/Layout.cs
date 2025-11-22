// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Layout
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Spinballs.Common.Helper
{
  public class Layout
  {
    public static readonly Vector2 DiscsCenter = new Vector2(240f, 365f);
    public static readonly int DiscCount = 7;
    public static readonly int BallsPerDisc = 6;
    public static readonly int BallCount = Layout.DiscCount * Layout.BallsPerDisc;
    public static readonly int DiscRadius = 58;
    public static readonly int DiscBoundsRadius = 82;
    public static readonly int[] BallAngle = new int[6]
    {
      270,
      330,
      30,
      90,
      150,
      210
    };
    public static readonly int[] BallAngleCW = new int[6]
    {
      Layout.BallAngle[1],
      Layout.BallAngle[2],
      Layout.BallAngle[3],
      Layout.BallAngle[4],
      Layout.BallAngle[5],
      Layout.BallAngle[0]
    };
    public static readonly int[] BallAngleCCW = new int[6]
    {
      Layout.BallAngle[5],
      Layout.BallAngle[0],
      Layout.BallAngle[1],
      Layout.BallAngle[2],
      Layout.BallAngle[3],
      Layout.BallAngle[4]
    };
    public static readonly Vector2[] DiscOffset = new Vector2[7]
    {
      new Vector2(0.0f, 0.0f),
      new Vector2(0.0f, -171f),
      new Vector2(148f, -85f),
      new Vector2(148f, 85f),
      new Vector2(0.0f, 171f),
      new Vector2(-148f, 85f),
      new Vector2(-148f, -85f)
    };
    public static readonly Vector2[] BallOffset = new Vector2[6]
    {
      new Vector2(-15f, -73f),
      new Vector2(35f, -44f),
      new Vector2(35f, 14f),
      new Vector2(-15f, 43f),
      new Vector2(-65f, 14f),
      new Vector2(-65f, -44f)
    };
    public static readonly Vector2[] DiscHighlightOffset = new Vector2[2]
    {
      new Vector2(-112f, -106f),
      new Vector2(-15f, -106f)
    };
    public static readonly Vector2 TimeBarPositon = new Vector2(21f, 10f);
    public static readonly Vector2[] BonusConnector = new Vector2[8]
    {
      new Vector2(71f, 148f),
      new Vector2(124f, 129f),
      new Vector2(301f, 129f),
      new Vector2(367f, 148f),
      new Vector2(367f, 528f),
      new Vector2(301f, 558f),
      new Vector2(124f, 558f),
      new Vector2(71f, 528f)
    };
    public static readonly Rectangle TextMenu = new Rectangle(350, 772, 130, 28);
    public static readonly Rectangle TextLevel = new Rectangle(190, 780, 107, 22);
    public static readonly Rectangle TextPoints = new Rectangle(10, 772, 104, 28);
    public static readonly Rectangle TextChainLength = new Rectangle(202, 726, 83, 46);
    public static readonly Vector2 LevelBarPos = new Vector2(136f, 703f);
    public static readonly Vector2[] LevelBar = new Vector2[21]
    {
      new Vector2(0.0f, 83f),
      new Vector2(5f, 72f),
      new Vector2(9f, 61f),
      new Vector2(14f, 49f),
      new Vector2(22f, 38f),
      new Vector2(30f, 27f),
      new Vector2(40f, 17f),
      new Vector2(51f, 8f),
      new Vector2(66f, 3f),
      new Vector2(80f, 1f),
      new Vector2(96f, 0.0f),
      new Vector2(115f, 1f),
      new Vector2(129f, 4f),
      new Vector2(140f, 8f),
      new Vector2(153f, 17f),
      new Vector2(163f, 28f),
      new Vector2(172f, 39f),
      new Vector2(179f, 50f),
      new Vector2(184f, 60f),
      new Vector2(190f, 72f),
      new Vector2(194f, 83f)
    };
    public static readonly Vector2 ExtraExplodePos = new Vector2(55f, 75f);
    public static readonly Vector2 ExtraTimePos = new Vector2(351f, 75f);
    public static readonly Vector2 ExtraSortPos = new Vector2(351f, 580f);
    public static readonly Vector2 ExtraPointsPos = new Vector2(55f, 580f);
    public static readonly int PointWidth = 130;
    public static readonly Vector2 ExtraPointsX2Offset = new Vector2((float) (Layout.PointWidth + 20), -30f);
    public static readonly Vector2 ExtraPointsX2OffsetLeft = new Vector2((float) -(Layout.PointWidth / 6), -30f);
    public static readonly Vector2 ExtraCoronaPosOffset = new Vector2(-27f, -27f);
    public static readonly Vector2 LevelDisplayOffset = new Vector2(133f, 115f);
    public static readonly Vector2 LevelDisplaySize = new Vector2(212f, 150f);
    public static readonly Vector2 Panel = new Vector2(20f, 310f);
    public static readonly Rectangle PanelHeader = new Rectangle(60, 20, 315, 55);
    public static readonly Rectangle PanelBody = new Rectangle(20, 90, 390, 235);
    public static readonly Rectangle PanelBodyText = new Rectangle(30, 100, 370, 225);
    public static readonly Rectangle TextMusic = new Rectangle(20, 100, 390, 330);
    public static readonly Rectangle TextSound = new Rectangle(20, 190, 390, 330);
    public static readonly Vector2 MusicLeft = new Vector2(22f, 118f);
    public static readonly Vector2 MusicRight = new Vector2(328f, 118f);
    public static readonly Vector2 SoundLeft = new Vector2(22f, 210f);
    public static readonly Vector2 SoundRight = new Vector2(328f, 210f);
    public static readonly Vector2 SliderMusic = new Vector2(92f, 132f);
    public static readonly Vector2 SliderSound = new Vector2(92f, 224f);
    public static readonly Vector2 ButtonMenu = new Vector2(340f, 752f);
    public static readonly Vector2 LogoHighlight = new Vector2(72f, 180f);
    public static readonly Vector2 StartFirstButton = new Vector2(90f, 338f);
    public static readonly Vector2 StartButtonOffset = new Vector2(0.0f, 64f);
    public static readonly Rectangle[] HSHeaderCol = new Rectangle[3]
    {
      new Rectangle(45, 95, 55, 25),
      new Rectangle(135, 95, 160, 25),
      new Rectangle(310, 95, 70, 25)
    };
    public static readonly Vector2[] HSHeaderLine = new Vector2[2]
    {
      new Vector2(40f, 125f),
      new Vector2(380f, 125f)
    };
    public static readonly Rectangle[] HSCol = new Rectangle[3]
    {
      new Rectangle(45, 140, 45, 30),
      new Rectangle(135, 140, 160, 30),
      new Rectangle(330, 140, 60, 30)
    };
    public static readonly Vector2 SplashLogo = new Vector2(112f, 272f);
    public static readonly Vector2 SplashPoints = new Vector2(112f, 548f);

    public static Vector2 GetBallPosition(int ballIndex)
    {
      int index1 = ballIndex / Layout.BallsPerDisc;
      int index2 = ballIndex - index1 * Layout.BallsPerDisc;
      return Layout.DiscsCenter + Layout.DiscOffset[index1] + Layout.BallOffset[index2];
    }

    public static Vector2 GetDiscCenter(int discIndex)
    {
      return Layout.DiscsCenter + Layout.DiscOffset[discIndex];
    }

    public static Vector2 GetAlignPos(
      Orientation orientation,
      Rectangle planeRect,
      Rectangle objRect)
    {
      float x = (float) objRect.X;
      float y = (float) objRect.Y;
      if ((orientation & Orientation.Left) == Orientation.Left)
        x = (float) planeRect.X;
      else if ((orientation & Orientation.Right) == Orientation.Right)
        x = (float) (planeRect.X + planeRect.Width - objRect.Width);
      else if ((orientation & Orientation.HorizontalCenter) == Orientation.HorizontalCenter)
        x = (float) (planeRect.X + (planeRect.Width - objRect.Width) / 2);
      if ((orientation & Orientation.Top) == Orientation.Top)
        y = (float) planeRect.Y;
      else if ((orientation & Orientation.Bottom) == Orientation.Bottom)
        y = (float) (planeRect.Y + planeRect.Height - objRect.Height);
      else if ((orientation & Orientation.VerticalCenter) == Orientation.VerticalCenter)
        y = (float) (planeRect.Y + (planeRect.Height - objRect.Height) / 2);
      return new Vector2((float) (int) x, (float) (int) y);
    }
  }
}
