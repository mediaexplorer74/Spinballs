// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.Ball
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Common.Helper;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Document
{
  public class Ball
  {
    private BallColors _color;
    private Disc _disc;
    private List<Ball> _connectedBalls = new List<Ball>();
    private int _ballIndex;

    public int BallIndex
    {
      get => this._ballIndex;
      set => this._ballIndex = value;
    }

    public int NextBallIndex => this.BallIndex + 1 <= 5 ? this.BallIndex + 1 : 0;

    public int PrevBallIndex => this.BallIndex - 1 >= 0 ? this.BallIndex - 1 : 5;

    public Ball Next => this.Disc.Balls[this.NextBallIndex];

    public Ball Previous => this.Disc.Balls[this.PrevBallIndex];

    public Ball(Disc disc, int ballIndex)
    {
      this.Disc = disc;
      this.BallIndex = ballIndex;
    }

    public List<Ball> ConnectedBalls => this._connectedBalls;

    public Disc Disc
    {
      get => this._disc;
      set => this._disc = value;
    }

    public BallColors Color
    {
      get => this._color;
      set => this._color = value;
    }

    public int FlatIndex => this.Disc.DiscIndex * Layout.BallsPerDisc + this.BallIndex;

    public void Add(params Ball[] balls)
    {
      this._connectedBalls.AddRange((IEnumerable<Ball>) balls);
    }
  }
}
