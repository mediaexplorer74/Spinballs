// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.DrawableControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Spinballs.Core.Controls
{
  public abstract class DrawableControl : BaseControl
  {
    protected bool _visible;
    protected float _scale;
    protected float _rotation;
    protected Vector2 _rotationOrigin;
    protected Color _opacity;
    protected SpriteEffects _effects;
    protected Rectangle? _sourceRectangle;

    public DrawableControl()
    {
      this._scale = 1f;
      this._rotation = 0.0f;
      this._rotationOrigin = new Vector2(0.0f, 0.0f);
      this._opacity = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this._visible = true;
      this._effects = SpriteEffects.None;
    }

    public virtual bool Visible
    {
      get => this._visible;
      set => this._visible = value;
    }

    public virtual float Scale
    {
      get => this._scale;
      set => this._scale = value;
    }

    public virtual Vector2 RotationOrigin
    {
      get => this._rotationOrigin;
      set => this._rotationOrigin = value;
    }

    public virtual float Rotation
    {
      get => this._rotation;
      set => this._rotation = MathHelper.ToRadians(value);
    }

    public virtual byte Opacity
    {
      get => this._opacity.A;
      set => this._opacity = new Color((int) value, (int) value, (int) value, (int) value);
    }

    public SpriteEffects Effects
    {
      get => this._effects;
      set => this._effects = value;
    }

    public Rectangle? SourceRectangle
    {
      get => this._sourceRectangle;
      set => this._sourceRectangle = value;
    }

    public abstract void Draw(SpriteBatch spriteBatch);
  }
}
