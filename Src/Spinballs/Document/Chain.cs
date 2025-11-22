// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.Chain
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Document
{
  public class Chain : List<Ball>
  {
    private List<Ball> _checkedBalls = new List<Ball>();
    private Color _color;

    public List<Ball> CheckedBalls
    {
      get => this._checkedBalls;
      set => this._checkedBalls = value;
    }

    public Color Color
    {
      get => this._color;
      set => this._color = value;
    }

    public override string ToString()
    {
      if (this.Count == 0)
        return string.Empty;
      List<string> stringList = new List<string>();
      stringList.Add(this[0].Color.ToString());
      foreach (Ball ball in (List<Ball>) this)
        stringList.Add(string.Format("{0}-{1}", (object) ball.Disc.DiscIndex, (object) ball.BallIndex));
      return string.Join(", ", stringList.ToArray());
    }

    public List<Disc> Discs
    {
      get
      {
        List<Disc> discs = new List<Disc>();
        foreach (Ball ball in (List<Ball>) this)
        {
          if (!discs.Contains(ball.Disc))
            discs.Add(ball.Disc);
        }
        return discs;
      }
    }
  }
}
