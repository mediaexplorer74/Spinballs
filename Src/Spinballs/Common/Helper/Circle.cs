// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Circle
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Spinballs.Common.Helper
{
  public class Circle
  {
    private Vector2 _center;
    private float _radius;

    public Circle(Vector2 center, float radius)
    {
      this.Center = center;
      this.Radius = radius;
    }

    public Vector2 Center
    {
      get => this._center;
      set => this._center = value;
    }

    public float Radius
    {
      get => this._radius;
      set => this._radius = value;
    }

    public bool Contains(Vector2 pos) => Circle.GetRadius(this.Center, pos) <= (double) this.Radius;

    public Vector2 GetCirclePosition(double radian)
    {
      return new Vector2((float) (int) ((double) this.Center.X + (double) this.Radius * Math.Cos(radian)), (float) (int) ((double) this.Center.Y + (double) this.Radius * Math.Sin(radian)));
    }

    public static double GetRadius(Vector2 center, Vector2 pos)
    {
      Vector2 vector2 = center - pos;
      if ((double) vector2.X < 0.0)
        vector2.X *= -1f;
      if ((double) vector2.Y < 0.0)
        vector2.Y *= -1f;
      return Math.Sqrt((double) vector2.X * (double) vector2.X + (double) vector2.Y * (double) vector2.Y);
    }

    public static double GetRadian(Vector2 center, Vector2 pos)
    {
      Vector2 vector2 = pos - center;
      double num = Math.Atan2((double) vector2.Y * -1.0, (double) vector2.X);
      return num > 0.0 ? 2.0 * Math.PI - num : Math.Abs(num);
    }

    public static double ToRadian(double degree) => degree * Math.PI / 180.0;

    public static double ToDegree(double radian) => radian / Math.PI * 180.0;
  }
}
