// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.Disc
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Common.Helper;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Document
{
  public class Disc
  {
    private Ball[] _balls = new Ball[Layout.BallsPerDisc];
    private Dictionary<int, Disc> _connections = new Dictionary<int, Disc>();
    private int _idx;

    public Ball[] Balls => this._balls;

    public int DiscIndex
    {
      get => this._idx;
      set => this._idx = value;
    }

    public Disc(int discIndex)
    {
      this.DiscIndex = discIndex;
      for (int ballIndex = 0; ballIndex < 6; ++ballIndex)
        this._balls[ballIndex] = new Ball(this, ballIndex);
    }

    public Ball this[int index]
    {
      get => this._balls[index];
      set => this._balls[index] = value;
    }

    public void Randomize()
    {
      foreach (Ball ball in this.Balls)
        ball.Color = (BallColors) Spinballs.Common.Helper.Helper.GetRandom(0, 4);
    }
  }
}
