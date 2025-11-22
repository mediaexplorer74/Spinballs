// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.LevelUpControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class LevelUpControl : ImageControl
  {
    private int _level;

    public int Level
    {
      get => this._level;
      set => this._level = value;
    }

    public override void Create()
    {
      base.Create();
      Texture2D levelUp = Res.GameScreen.LevelUp;
      this.Size = new Vector2(480f, 392f);
      RenderTarget2D renderTarget = this.Texture == null ? new RenderTarget2D(Res.Game.GraphicsDevice, levelUp.Width, levelUp.Height) : (RenderTarget2D) this.Texture;
      LabelControl labelControl1 = new LabelControl();
      labelControl1.Text = Strings.Level;
      labelControl1.Position = Layout.LevelDisplayOffset;
      labelControl1.Size = Layout.LevelDisplaySize;
      labelControl1.Orientation = Orientation.Top | Orientation.HorizontalCenter;
      labelControl1.Font = Res.Font.Big3;
      LabelControl labelControl2 = new LabelControl();
      labelControl2.SetText((object) this.Level);
      labelControl2.Position = Layout.LevelDisplayOffset;
      labelControl2.Size = Layout.LevelDisplaySize;
      labelControl2.Orientation = Orientation.Bottom | Orientation.HorizontalCenter;
      labelControl2.Font = Res.Font.Big5;
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      spriteBatch.Draw(levelUp, new Vector2(), Color.White);
      labelControl1.Draw(spriteBatch);
      labelControl2.Draw(spriteBatch);
      spriteBatch.End();
      this.Texture = (Texture2D) renderTarget;
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
    }
  }
}
