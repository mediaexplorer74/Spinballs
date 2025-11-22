// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.MenuButton
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
  public class MenuButton : ImageTextControl
  {
    private ImageTextControl _highlight;
    private ActionSequence _highlightAction;

    public MenuButton(ActionManager actionManager)
    {
      this._highlight = new ImageTextControl();
      this.ActionManager = actionManager;
    }

    public ImageTextControl Highlight => this._highlight;

    public void Create(
      Texture2D origTexture,
      Texture2D highlightTexture,
      string text,
      Vector2 position,
      SpriteFont font)
    {
      Rectangle padding = new Rectangle(0, 2, 0, 1);
      this.Create(origTexture, text, position, font, Orientation.Center, padding);
      this._highlight.Create(highlightTexture, text, position, font, Orientation.Center, padding);
      this._highlight.Visible = false;
      this._highlightAction = new ActionSequence();
      this._highlightAction.Actions.Add((ActionBase) new ActionFadeIn((DrawableControl) this._highlight, TimeSpan.FromMilliseconds(10.0)));
      this._highlightAction.Actions.Add((ActionBase) new ActionFadeOut((DrawableControl) this._highlight, TimeSpan.FromMilliseconds(290.0)));
      this._highlightAction.ActionManager = this.ActionManager;
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        this._highlight.Position = value;
      }
    }

    public override void Create() => base.Create();

    public override void Destroy()
    {
      if (this.Texture != null)
        this.Texture = (Texture2D) null;
      base.Destroy();
    }

    public override void OnClick(object sender)
    {
      if (this.Enabled)
      {
        this.StartHighlight();
        AudioManager.Play(Res.GameScreen.Sounds.Button);
      }
      base.OnClick(sender);
    }

    public void StartHighlight() => this._highlightAction.Start();

    public void StopHiglight() => this._highlightAction.Stop();

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this._highlight.Draw(spriteBatch);
    }
  }
}
