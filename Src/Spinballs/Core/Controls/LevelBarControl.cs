// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.LevelBarControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class LevelBarControl : DrawableControl
  {
    private ImageControl[] _levelBarItems;
    private int _showCount;

    public LevelBarControl()
    {
      this._showCount = 0;
      this._levelBarItems = new ImageControl[Layout.LevelBar.Length];
      for (int index = 0; index < this._levelBarItems.Length; ++index)
        this._levelBarItems[index] = new ImageControl();
    }

    public override void Create()
    {
      base.Create();
      for (int i = 0; i < this._levelBarItems.Length; ++i)
      {
        this._levelBarItems[i].Texture = Res.GameScreen.GetLevelBarTexture(i);
        this._levelBarItems[i].Position = this.Position + Layout.LevelBar[i];
      }
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        for (int index = 0; index < this._levelBarItems.Length; ++index)
          this._levelBarItems[index].Position = this.Position + Layout.LevelBar[index];
      }
    }

    public int ShowCount
    {
      get => this._showCount;
      set
      {
        this._showCount = value <= this._levelBarItems.Length ? value : this._levelBarItems.Length;
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      for (int index = 0; index < this.ShowCount; ++index)
        this._levelBarItems[index].Draw(spriteBatch);
    }
  }
}
