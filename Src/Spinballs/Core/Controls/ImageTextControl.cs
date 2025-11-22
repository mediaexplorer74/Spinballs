// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.ImageTextControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class ImageTextControl : ImageControl
  {
    private Texture2D _origTexture;
    private LabelControl _label;

    public ImageTextControl() => this._label = new LabelControl();

    public LabelControl Label => this._label;

    public void Create(
      Texture2D origTexture,
      string text,
      Vector2 position,
      SpriteFont font,
      Orientation orientation,
      Rectangle padding)
    {
      this._origTexture = origTexture;
      this.Position = position;
      this.Texture = (Texture2D) null;
      this._label.Text = text;
      this._label.Font = font;
      this._label.Orientation = orientation;
      this._label.DisplayRect = new Rectangle(padding.X, padding.Y, this._origTexture.Width - padding.Width, this._origTexture.Height - padding.Height);
      this.Create();
    }

    public override void Create()
    {
      if (this._origTexture == null)
        return;
      RenderTarget2D renderTarget = this.Texture == null ? new RenderTarget2D(Res.Game.GraphicsDevice, this._origTexture.Width, this._origTexture.Height) : (RenderTarget2D) this.Texture;
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      spriteBatch.Draw(this._origTexture, new Vector2(), Color.White);
      this._label.Draw(spriteBatch);
      spriteBatch.End();
      this.Texture = (Texture2D) renderTarget;
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      base.Create();
    }

    public override void Destroy()
    {
      if (this.Texture != null)
        this.Texture = (Texture2D) null;
      base.Destroy();
    }
  }
}
