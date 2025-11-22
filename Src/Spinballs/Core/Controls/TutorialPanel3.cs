// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.TutorialPanel3
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class TutorialPanel3 : ImageControl
  {
    public TutorialPanel3(ActionManager actionManager) => this.ActionManager = actionManager;

    public override void Create()
    {
      base.Create();
      Texture2D panel = Res.Common.Panel;
      RenderTarget2D renderTarget = new RenderTarget2D(Res.Game.GraphicsDevice, panel.Width, panel.Height);
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      spriteBatch.Draw(panel, new Vector2(), Color.White);
      LabelControl labelControl = new LabelControl();
      labelControl.DisplayRect = Layout.PanelHeader;
      labelControl.Orientation = Orientation.Center;
      labelControl.Font = Res.Font.Big2;
      labelControl.Text = Strings.TutorialTitle;
      labelControl.Draw(spriteBatch);
      ImageControl imageControl = new ImageControl(Res.StartScreen.Tutorial3);
      imageControl.Size = new Vector2(152f, 234f);
      imageControl.Position = new Vector2((float) Layout.PanelBody.X, (float) Layout.PanelBody.Y);
      imageControl.Draw(spriteBatch);
      labelControl.DisplayRect = new Rectangle(Layout.PanelBody.X + imageControl.Width + 10, Layout.PanelBody.Y, Layout.PanelBody.Width - imageControl.Width - 15, Layout.PanelBody.Height);
      labelControl.Orientation = Orientation.Left | Orientation.Top;
      labelControl.Font = Res.Font.Small;
      labelControl.Text = Strings.Tutorial3;
      labelControl.WordWrap = true;
      labelControl.Draw(spriteBatch);
      spriteBatch.End();
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.Texture = (Texture2D) renderTarget;
      this.Size = new Vector2(429f, 346f);
    }
  }
}
