// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.BallControl
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
  public class BallControl : ImageControl
  {
    private BallColors _color;
    private ImageControl _highlightImage;
    private ActionRepeat _actionHighlight;
    private Vector2 _centerOffset;
    private Vector2 _centerOffsetHighlight;
    private bool _highlight;

    public BallColors Color
    {
      get => this._color;
      set
      {
        this._color = value;
        switch (this._color)
        {
          case BallColors.Blue:
            this.Texture = Res.GameScreen.BallBlue;
            break;
          case BallColors.Green:
            this.Texture = Res.GameScreen.BallGreen;
            break;
          case BallColors.Red:
            this.Texture = Res.GameScreen.BallRed;
            break;
          case BallColors.Yellow:
            this.Texture = Res.GameScreen.BallYellow;
            break;
        }
      }
    }

    public override Vector2 Position
    {
      get => base.Position - this._centerOffset;
      set
      {
        base.Position = value + this._centerOffset;
        this._highlightImage.Position = value + this._centerOffset + new Vector2(4f, 4f);
      }
    }

    public override bool Visible
    {
      get => base.Visible;
      set
      {
        if (this.Visible == value)
          return;
        base.Visible = value;
        this.UpdateHighlightingAction();
      }
    }

    public override byte Opacity
    {
      get => base.Opacity;
      set
      {
        if ((int) this.Opacity == (int) value)
          return;
        base.Opacity = value;
        this.UpdateHighlightingAction();
      }
    }

    public bool Highlight
    {
      get => this._highlight;
      set
      {
        if (this._highlight == value)
          return;
        this._highlight = value;
        this.UpdateHighlightingAction();
      }
    }

    private void UpdateHighlightingAction()
    {
      if (this.Highlight && this.Visible && this.Opacity > (byte) 0)
      {
        this._actionHighlight.Start();
      }
      else
      {
        if (this._actionHighlight == null)
          return;
        this._actionHighlight.Stop();
      }
    }

    public void ResetHighlightingAction() => this._actionHighlight.Reset();

    public void Create(BallColors color)
    {
      this.Color = color;
      this._centerOffset = new Vector2((float) -(this.Texture.Width / 2), (float) -(this.Texture.Height / 2));
      this._highlightImage = new ImageControl(Res.GameScreen.BallHighlight);
      this._centerOffsetHighlight = new Vector2((float) -(this._highlightImage.Texture.Width / 2), (float) -(this._highlightImage.Texture.Height / 2));
      this._actionHighlight = new ActionRepeat((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionFadeIn((DrawableControl) this._highlightImage, TimeSpan.FromMilliseconds(300.0)),
          (ActionBase) new ActionFadeOut((DrawableControl) this._highlightImage, TimeSpan.FromMilliseconds(300.0))
        }
      });
      this._actionHighlight.ActionManager = this.ActionManager;
    }

    public override void Create()
    {
      base.Create();
      this.Create(BallColors.Blue);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if (!this.Visible || !this.Highlight)
        return;
      this._highlightImage.Draw(spriteBatch);
    }
  }
}
