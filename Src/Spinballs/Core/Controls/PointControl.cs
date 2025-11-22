// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.PointControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class PointControl : ImageControl
  {
    private int _points;

    public void Init(int points, Vector2 position)
    {
      this.Points = points;
      this.Position = position;
      this.Create();
    }

    public override void Create()
    {
      base.Create();
      LabelControl labelControl = new LabelControl();
      labelControl.Position = this.Position;
      labelControl.DisplayRect = new Rectangle(0, 2, Res.GameScreen.Points.Width, Res.GameScreen.Points.Height - 1);
      labelControl.Orientation = Orientation.Center;
      labelControl.SetText((object) this.Points);
      RenderTarget2D renderTarget = this.Texture == null ? new RenderTarget2D(Res.Game.GraphicsDevice, Res.GameScreen.Points.Width, Res.GameScreen.Points.Height) : (RenderTarget2D) this.Texture;
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      spriteBatch.Draw(Res.GameScreen.Points, new Vector2(), Color.White);
      labelControl.Draw(spriteBatch);
      spriteBatch.End();
      this.Texture = (Texture2D) renderTarget;
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
    }

    public override Vector2 Position
    {
      get => base.Position;
      set => base.Position = value;
    }

    public override Vector2 Size
    {
      get => base.Size;
      set => base.Size = value;
    }

    public override byte Opacity
    {
      get => base.Opacity;
      set => base.Opacity = value;
    }

    public int Points
    {
      get => this._points;
      set => this._points = value;
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
