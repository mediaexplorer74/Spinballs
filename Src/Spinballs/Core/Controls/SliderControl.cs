// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.SliderControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class SliderControl : ImageControl
  {
    private int _min;
    private int _max;
    private int _value;
    private bool _created;

    public int Min
    {
      get => this._min;
      set => this._min = value;
    }

    public int Max
    {
      get => this._max;
      set => this._max = value;
    }

    public int Value
    {
      get => this._value;
      set
      {
        if (this._value == value)
        {
          if (this._created)
            return;
          this.Create();
        }
        else
        {
          this._value = value;
          this.Create();
        }
      }
    }

    public int GetValueByPos(Vector2 pos)
    {
      int min = this.Min;
      Texture2D sliderHighlight = Res.Common.SliderHighlight;
      return (double) pos.X >= (double) this.Position.X ? ((double) pos.X <= (double) this.Position.X + (double) sliderHighlight.Width ? (int) ((double) (this.Max - this.Min) * (double) ((float) (int) ((double) pos.X - (double) this.Position.X) / (float) sliderHighlight.Width)) : this.Max) : this.Min;
    }

    public void Init(int min, int max, int val)
    {
      this.Min = min;
      this.Max = max;
      this._value = val;
    }

    public override void Create()
    {
      base.Create();
      ImageControl imageControl1 = new ImageControl(Res.Common.Slider);
      ImageControl imageControl2 = new ImageControl(Res.Common.SliderHighlight);
      int num = 0;
      if (this.Value == this.Max)
        num = imageControl1.Width;
      else if (this.Value != this.Min)
        num = (int) ((double) imageControl1.Width * ((double) this.Value / (double) (this.Max - this.Min)));
      imageControl2.SourceRectangle = new Rectangle?();
      imageControl1.SourceRectangle = new Rectangle?();
      imageControl1.PositionOffset = new Vector2();
      imageControl1.Visible = true;
      imageControl2.Visible = true;
      if (num == 0)
        imageControl2.Visible = false;
      else if (num == imageControl1.Width)
      {
        imageControl1.Visible = false;
      }
      else
      {
        imageControl2.SourceRectangle = new Rectangle?(new Rectangle(0, 0, num, imageControl1.Height));
        imageControl1.SourceRectangle = new Rectangle?(new Rectangle(num, 0, imageControl1.Width, imageControl1.Height));
        imageControl1.PositionOffset = new Vector2((float) num, 0.0f);
      }
      if (!(this.Texture is RenderTarget2D renderTarget))
        renderTarget = new RenderTarget2D(Res.Game.GraphicsDevice, imageControl1.Width, imageControl1.Height);
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      imageControl1.Draw(spriteBatch);
      imageControl2.Draw(spriteBatch);
      spriteBatch.End();
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.Texture = (Texture2D) renderTarget;
      this._created = true;
    }

    public override void Destroy()
    {
      if (this.Texture != null)
        this.Texture = (Texture2D) null;
      base.Destroy();
    }
  }
}
