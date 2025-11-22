// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.LabelControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using System.Text;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class LabelControl : DrawableControl
  {
    private string _text;
    private Orientation _orientation;
    private SpriteFont _font;
    private Color _color;
    private Vector2 _realPos;
    private bool _wordWrap;
    private string _origText;

    public LabelControl() => this.Color = Color.White;

    private void WrapText()
    {
      StringBuilder original = new StringBuilder(this._origText);
      StringBuilder target = new StringBuilder();
      WordWrapper.WrapWord(original, target, this.Font, this.DisplayRect, this.Scale);
      this._text = target.ToString();
    }

    public string Text
    {
      get => this._text;
      set
      {
        if (this.WordWrap && this._origText == value || !this.WordWrap && this._text == value)
          return;
        this._text = value;
        if (this.WordWrap)
        {
          this._origText = this._text;
          this.WrapText();
        }
        this.UpdateRealPos();
      }
    }

    public Orientation Orientation
    {
      get => this._orientation;
      set
      {
        this._orientation = value;
        this.UpdateRealPos();
      }
    }

    public SpriteFont Font
    {
      get => this._font == null ? Res.Font.Default : this._font;
      set
      {
        if (this._font == value)
          return;
        this._font = value;
        if (this.WordWrap)
          this.WrapText();
        this.UpdateRealPos();
      }
    }

    public Color Color
    {
      get => this._color;
      set
      {
        this._color = new Color((int) this.Opacity, (int) this.Opacity, (int) this.Opacity, (int) this.Opacity);
      }
    }

    public override byte Opacity
    {
      get => base.Opacity;
      set
      {
        base.Opacity = value;
        this.Color = this.Color;
      }
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        this.UpdateRealPos();
      }
    }

    public override Vector2 Size
    {
      get => base.Size;
      set
      {
        if (this.Size == value)
          return;
        base.Size = value;
        if (this.WordWrap)
          this.WrapText();
        this.UpdateRealPos();
      }
    }

    public bool WordWrap
    {
      get => this._wordWrap;
      set
      {
        if (this._wordWrap == value)
          return;
        this._wordWrap = value;
        if (this._wordWrap)
        {
          this._origText = this._text;
          if (string.IsNullOrEmpty(this._text))
            return;
          this.WrapText();
        }
        else
        {
          this._text = this._origText;
          this._origText = (string) null;
        }
      }
    }

    private void UpdateRealPos()
    {
      if (this.Orientation == Orientation.None || string.IsNullOrEmpty(this.Text))
      {
        this._realPos = this.Position;
      }
      else
      {
        Vector2 vector2 = this.Font.MeasureString(this.Text);
        this._realPos = Layout.GetAlignPos(this.Orientation, this.DisplayRect, new Rectangle(0, 0, (int) vector2.X, (int) vector2.Y));
      }
    }

    public void SetText(object obj) => this.Text = obj.ToString();

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (!this.Visible || this.Opacity <= (byte) 0 || string.IsNullOrEmpty(this.Text))
        return;
      spriteBatch.DrawString(this.Font, this.Text, this._realPos, this.Color, this.Rotation, this.RotationOrigin, this.Scale, this.Effects, 0.0f);
    }
  }
}
