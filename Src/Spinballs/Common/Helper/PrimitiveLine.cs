// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.PrimitiveLine
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Common.Helper
{
  public class PrimitiveLine
  {
    private Texture2D pixel;
    private List<Vector2> vectors;
    public Color Colour;
    public Vector2 Position;
    public float Depth;
    private Color _color = Color.White;
    private GraphicsDevice _graphicsDevice;

    public int CountVectors => this.vectors.Count;

    public Color Color
    {
      get => this._color;
      set => this._color = value;
    }

    public PrimitiveLine(GraphicsDevice graphicsDevice)
    {
      this._graphicsDevice = graphicsDevice;
      this.pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
      this.pixel.SetData<Color>(new Color[1]{ this.Color });
      this.Colour = Color.White;
      this.Position = new Vector2(0.0f, 0.0f);
      this.Depth = 0.0f;
      this.vectors = new List<Vector2>();
    }

    public void SetThickness(int height)
    {
      this.pixel = new Texture2D(this._graphicsDevice, 1, height, false, SurfaceFormat.Color);
      Color[] data = new Color[height];
      for (int index = 0; index < height; ++index)
        data[index] = this.Color;
      this.pixel.SetData<Color>(data);
    }

    ~PrimitiveLine()
    {
    }

    public void AddVector(Vector2 vector) => this.vectors.Add(vector);

    public void InsertVector(int index, Vector2 vector) => this.vectors.Insert(index, vector);

    public void RemoveVector(Vector2 vector) => this.vectors.Remove(vector);

    public void RemoveVector(int index) => this.vectors.RemoveAt(index);

    public void ClearVectors() => this.vectors.Clear();

    public void Render(SpriteBatch spriteBatch)
    {
      if (this.vectors.Count < 2)
        return;
      for (int index = 1; index < this.vectors.Count; ++index)
      {
        Vector2 vector1 = this.vectors[index - 1];
        Vector2 vector2 = this.vectors[index];
        float x = Vector2.Distance(vector1, vector2);
        float rotation = (float) Math.Atan2((double) vector2.Y - (double) vector1.Y, (double) vector2.X - (double) vector1.X);
        spriteBatch.Draw(this.pixel, this.Position + vector1, new Rectangle?(), this.Colour, rotation, Vector2.Zero, new Vector2(x, 1f), SpriteEffects.None, this.Depth);
      }
    }

    public void CreateCircle(float radius, int sides)
    {
      this.vectors.Clear();
      float num1 = 6.28318548f;
      float num2 = num1 / (float) sides;
      for (float num3 = 0.0f; (double) num3 < (double) num1; num3 += num2)
        this.vectors.Add(new Vector2(radius * (float) Math.Cos((double) num3), radius * (float) Math.Sin((double) num3)));
      this.vectors.Add(new Vector2(radius * (float) Math.Cos(0.0), radius * (float) Math.Sin(0.0)));
    }
  }
}
