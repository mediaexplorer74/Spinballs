// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.ImageControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class ImageControl : DrawableControl
  {
    protected Texture2D _texture;

    public ImageControl()
    {
    }

    public ImageControl(Texture2D texture) => this.Texture = texture;

    public virtual Texture2D Texture
    {
      get => this._texture;
      set
      {
        this._texture = value;
        if (this._texture == null)
          return;
        this.Size = new Vector2((float) this._texture.Width, (float) this._texture.Height);
      }
    }

    public virtual int Width => (int) this.Size.X;

    public virtual int Height => (int) this.Size.Y;

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (!this.Visible || this.Opacity <= (byte) 0 || this.Texture == null)
        return;
      spriteBatch.Draw(this.Texture, this.Position + this.PositionOffset, this.SourceRectangle, this._opacity, this.Rotation, this._rotationOrigin, this.Scale, this.Effects, 0.0f);
    }
  }
}
