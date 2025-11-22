// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.ExtraLoadControl
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
  public class ExtraLoadControl : ImageControl
  {
    private float _min;
    private float _max;
    private float _value;
    private BlinkMode _blinkMode;
    private Vector2 _displayPos;
    private ImageControl _corona;
    private ActionRepeat _actionBlink;
    private ActionRepeat _actionPulse;

    public ExtraLoadControl()
    {
      this._min = 0.0f;
      this._max = 20f;
      this._value = 0.0f;
    }

    public ExtraLoadControl(Texture2D texture)
      : base(texture)
    {
      this._min = 0.0f;
      this._max = 20f;
      this._value = 0.0f;
    }

    public void Init(float min, float max, float start)
    {
      this._min = min;
      this._max = max;
      this._value = start;
      this.Update();
    }

    public override void Create()
    {
      base.Create();
      this._corona = new ImageControl(Res.GameScreen.ExtraCorona);
      this._corona.Position = this.Position + Layout.ExtraCoronaPosOffset;
      this._actionBlink = new ActionRepeat((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionFadeIn((DrawableControl) this._corona, TimeSpan.FromMilliseconds(150.0)),
          (ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(50.0))
        }
      });
      this._actionBlink.ActionManager = this.ActionManager;
      this._actionPulse = new ActionRepeat((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionFadeIn((DrawableControl) this._corona, TimeSpan.FromMilliseconds(150.0), (byte) 0, (byte) 222),
          (ActionBase) new ActionDuration(TimeSpan.FromMilliseconds(50.0)),
          (ActionBase) new ActionFadeOut((DrawableControl) this._corona, TimeSpan.FromMilliseconds(650.0), (byte) 0, (byte) 222)
        }
      });
      this._actionPulse.ActionManager = this.ActionManager;
      this._corona.Opacity = (byte) 0;
    }

    public float Min
    {
      get => this._min;
      set => this._min = value;
    }

    public float Max
    {
      get => this._max;
      set => this._max = value;
    }

    public float Value
    {
      get => this._value;
      set
      {
        if ((double) this._value == (double) value)
          return;
        this._value = value;
        if ((double) this._value < (double) this.Min)
          this._value = this.Min;
        if ((double) this._value > (double) this.Max)
          this._value = this.Max;
        this.Update();
      }
    }

    public BlinkMode BlinkMode
    {
      get => this._blinkMode;
      set
      {
        if (this._blinkMode == value)
          return;
        this._blinkMode = value;
        switch (this.BlinkMode)
        {
          case BlinkMode.None:
            this._actionBlink.Stop();
            this._actionPulse.Stop();
            this._corona.Opacity = (byte) 0;
            break;
          case BlinkMode.Blink:
            this._actionBlink.Start();
            this._actionPulse.Stop();
            break;
          case BlinkMode.Pulse:
            this._actionBlink.Stop();
            this._actionPulse.Start();
            break;
          case BlinkMode.Highlight:
            this._actionBlink.Stop();
            this._actionPulse.Stop();
            this._corona.Opacity = byte.MaxValue;
            break;
        }
      }
    }

    public override Texture2D Texture
    {
      get => base.Texture;
      set
      {
        base.Texture = value;
        this.Update();
      }
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        if (this._corona != null)
          this._corona.Position = this.Position + Layout.ExtraCoronaPosOffset;
        this.Update();
      }
    }

    public override Vector2 Size
    {
      get => base.Size;
      set
      {
        base.Size = value;
        this.Update();
      }
    }

    private void Update()
    {
      if (this.Texture == null)
        return;
      if ((double) this.Value == (double) this.Max)
      {
        this.SourceRectangle = new Rectangle?();
        this._displayPos = this.Position;
      }
      else
      {
        int height = (int) ((double) this.Texture.Height * (double) (this.Value / this.Max));
        int y = this.Texture.Height - height;
        this.SourceRectangle = new Rectangle?(new Rectangle(0, y, this.Texture.Width, height));
        this._displayPos = this.Position + new Vector2(0.0f, (float) y);
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if ((double) this.Value > 0.0 && this.Visible && this.Opacity > (byte) 0 && this.Texture != null)
        spriteBatch.Draw(this.Texture, this._displayPos, this.SourceRectangle, this._opacity, this.Rotation, this._rotationOrigin, this.Scale, this.Effects, 0.0f);
      this._corona.Draw(spriteBatch);
    }
  }
}
