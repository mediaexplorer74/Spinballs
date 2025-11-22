// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.PanelDarken
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class PanelDarken : DrawableControl
  {
    private RenderTarget2D _texture;

    public override void Create()
    {
      base.Create();
      this._texture = new RenderTarget2D(Res.Game.GraphicsDevice, (int) this.Size.X, (int) this.Size.Y);
      Res.Game.GraphicsDevice.SetRenderTarget(this._texture);
      Res.Game.GraphicsDevice.Clear(Color.Black);
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.Opacity = (byte) 170;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (!this.Visible || this.Opacity <= (byte) 0 || this._texture == null)
        return;
      spriteBatch.Draw((Texture2D) this._texture, this.Position, this.SourceRectangle, this._opacity, this.Rotation, this._rotationOrigin, this.Scale, this.Effects, 0.0f);
    }
  }
}
