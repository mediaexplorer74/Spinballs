// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.MenuController
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Document;
using System;

#nullable disable
namespace Spinballs.Controller
{
  public class MenuController : GameController
  {
    private LabelControl _labelMenu;
    private ImageControl _buttonMenu;

    public MenuController(Spinballs.View.GameScreen gameScreen)
      : base(gameScreen)
    {
      this._labelMenu = new LabelControl();
      this._buttonMenu = new ImageControl();
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._labelMenu.Text = Strings.Menu;
      this._labelMenu.DisplayRect = Layout.TextMenu;
      this._labelMenu.Orientation = Orientation.Center;
      this._buttonMenu.Texture = Res.GameScreen.ButtonMenu;
      this._buttonMenu.Position = Layout.ButtonMenu;
      this._buttonMenu.Opacity = (byte) 0;
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      this._labelMenu.Destroy();
    }

    public override void HandleTap(Vector2 tapPos, GameTime gameTime)
    {
      base.HandleTap(tapPos, gameTime);
      if (!this._labelMenu.Contains(tapPos))
        return;
      this.Document.State = GameState.Pause;
      this.ActionManager.Add((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionFadeIn((DrawableControl) this._buttonMenu, TimeSpan.FromMilliseconds(150.0)),
          (ActionBase) new ActionFadeOut((DrawableControl) this._buttonMenu, TimeSpan.FromMilliseconds(250.0))
        }
      });
      AudioManager.Play(Res.GameScreen.Sounds.Button);
    }

    protected override void UpdateCore(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, DrawOrder drawOrder)
    {
      if (drawOrder != DrawOrder.AfterBalls)
        return;
      this._labelMenu.Draw(spriteBatch);
      this._buttonMenu.Draw(spriteBatch);
    }
  }
}
