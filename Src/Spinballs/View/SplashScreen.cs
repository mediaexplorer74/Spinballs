// Decompiled with JetBrains decompiler
// Type: Spinballs.View.SplashScreen
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.Controls;
using Spinballs.Core.ScreenManagement;
using System;

#nullable disable
namespace Spinballs.View
{
  public class SplashScreen : BaseScreen
  {
    private ImageControl _icon;
    private ImageControl _points;
    private TimeSpan _ellapsed = TimeSpan.Zero;
    private TimeSpan _duration = TimeSpan.FromMilliseconds(900.0);
    private bool _transitionStarted;
    private ActionBase _actionHighlight;
    private bool _resourcesLoaded = false;

    public SplashScreen()
    {
      this._id = 0;
      this._icon = new ImageControl();
      this._points = new ImageControl();
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._icon.Texture = Res.Common.LoadIcon;
      this._icon.Position = Layout.SplashLogo;
      this._points.Texture = Res.Common.LoadIconText;
      this._points.Size = new Vector2(256f, 86f);
      this._points.Position = Layout.SplashPoints;
      this._actionHighlight = (ActionBase) new ActionRepeat((ActionBase) new ActionFadeIn((DrawableControl) this._points, TimeSpan.FromMilliseconds(1000.0)));
      this._actionHighlight.ActionManager = this.ActionManager;
      this._actionHighlight.Start();
      // Загружаем ресурсы в текущем потоке, так как Thread не доступен в UWP
      this.LoadResources();
      this._resourcesLoaded = true;
    }

    private void LoadResources()
    {
      Res.LoadStartContent();
      Res.LoadGameContent();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (this._transitionStarted || !this._resourcesLoaded)
        return;
      this.Manager.ShowScreen(Screens.Start, TimeSpan.FromMilliseconds(500.0), TimeSpan.FromMilliseconds(300.0));
      this._transitionStarted = true;
    }

    public override void Draw(GameTime gameTime)
    {
      Res.Game.GraphicsDevice.Clear(Color.Black);
      Res.SpriteBatch.Begin();
      this.DrawCore(Res.SpriteBatch, gameTime);
      Res.SpriteBatch.End();
    }

    protected override void DrawCore(SpriteBatch spriteBatch, GameTime gameTime)
    {
      this._icon.Draw(spriteBatch);
      this._points.Draw(spriteBatch);
    }
  }
}
