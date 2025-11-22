// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.TimerBar
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using System;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class TimerBar : ImageControl
  {
    private int _min;
    private int _max;
    private int _value;
    private bool _iced;
    private ImageControl _icedBar;

    public TimerBar()
    {
      this._min = 0;
      this._max = 100;
      this._value = 100;
      this._iced = false;
      this._icedBar = new ImageControl();
    }

    public void Init(int min, int max, int start)
    {
      this._min = min;
      this._max = max;
      this._value = start;
      this.Update();
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        this._icedBar.Position = this.Position;
      }
    }

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

    public bool Iced
    {
      get => this._iced;
      set
      {
        if (this._iced == value)
          return;
        this._iced = value;
        if (this._iced)
          this.ActionManager.Add((ActionBase) new ActionFadeIn((DrawableControl) this._icedBar, TimeSpan.FromMilliseconds(500.0)));
        else
          this.ActionManager.Add((ActionBase) new ActionFadeOut((DrawableControl) this._icedBar, TimeSpan.FromMilliseconds(500.0)));
      }
    }

    public int Value
    {
      get => this._value;
      set
      {
        if (this._value == value)
          return;
        this._value = value;
        if (this._value < this.Min)
          this._value = this.Min;
        if (this._value > this.Max)
          this._value = this.Max;
        this.Update();
      }
    }

    public override void Create()
    {
      base.Create();
      this.Texture = Res.GameScreen.TimerBar;
      this._icedBar.Texture = Res.GameScreen.TimerBarIced;
      this._icedBar.Position = this.Position;
      this._icedBar.Opacity = (byte) 0;
      this._iced = false;
    }

    private void Update()
    {
      if (this.Value == this.Max)
        this._icedBar.SourceRectangle = this.SourceRectangle = new Rectangle?();
      else
        this._icedBar.SourceRectangle = this.SourceRectangle = new Rectangle?(new Rectangle(0, 0, (int) ((double) this.Texture.Width * (double) ((float) this.Value / (float) this.Max)), this.Texture.Height));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this._icedBar.Draw(spriteBatch);
    }
  }
}
