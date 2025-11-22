// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.HighscorePanel
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Document;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class HighscorePanel : ImageControl
  {
    public HighscorePanel(ActionManager actionManager) => this.ActionManager = actionManager;

    public override void Create()
    {
      base.Create();
      Texture2D panel = Res.Common.Panel;
      this.Size = new Vector2(429f, 346f);
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
      labelControl.Text = Strings.Highscore;
      labelControl.Draw(spriteBatch);
      labelControl.DisplayRect = Layout.HSHeaderCol[0];
      labelControl.Orientation = Orientation.Left | Orientation.Top;
      labelControl.Font = Res.Font.Medium;
      labelControl.Text = Strings.Rank;
      labelControl.Draw(spriteBatch);
      labelControl.DisplayRect = Layout.HSHeaderCol[1];
      labelControl.Orientation = Orientation.Left | Orientation.Top;
      labelControl.Font = Res.Font.Medium;
      labelControl.Text = Strings.Points;
      labelControl.Draw(spriteBatch);
      labelControl.DisplayRect = Layout.HSHeaderCol[2];
      labelControl.Orientation = Orientation.Left | Orientation.Top;
      labelControl.Font = Res.Font.Medium;
      labelControl.Text = Strings.Level;
      labelControl.Draw(spriteBatch);
      List<int> highscores = Highscore.Instance.Highscores;
      Rectangle rectangle1 = new Rectangle(Layout.HSCol[0].X, Layout.HSCol[0].Y, Layout.HSCol[0].Width, Layout.HSCol[0].Height);
      Rectangle rectangle2 = new Rectangle(Layout.HSCol[1].X, Layout.HSCol[1].Y, Layout.HSCol[1].Width, Layout.HSCol[1].Height);
      Rectangle rectangle3 = new Rectangle(Layout.HSCol[2].X, Layout.HSCol[1].Y, Layout.HSCol[2].Width, Layout.HSCol[2].Height);
      for (int index = 0; index < highscores.Count; ++index)
      {
        labelControl.DisplayRect = rectangle1;
        labelControl.Orientation = Orientation.Right | Orientation.Top;
        labelControl.Font = Res.Font.Small;
        labelControl.Text = string.Format("{0}.", (object) (index + 1));
        labelControl.Draw(spriteBatch);
        labelControl.DisplayRect = rectangle2;
        labelControl.Orientation = Orientation.Left | Orientation.Top;
        labelControl.Font = Res.Font.Small;
        labelControl.Text = string.Format("{0}", (object) highscores[index]);
        labelControl.Draw(spriteBatch);
        labelControl.DisplayRect = rectangle3;
        labelControl.Orientation = Orientation.Left | Orientation.Top;
        labelControl.Font = Res.Font.Small;
        labelControl.Text = string.Format("{0}", (object) (GameDocument.GetLevel(highscores[index]) + 1));
        labelControl.Draw(spriteBatch);
        rectangle1.Offset(0, 35);
        rectangle2.Offset(0, 35);
        rectangle3.Offset(0, 35);
      }
      PrimitiveLine primitiveLine = new PrimitiveLine(Res.Game.GraphicsDevice);
      primitiveLine.SetThickness(3);
      primitiveLine.AddVector(Layout.HSHeaderLine[0]);
      primitiveLine.AddVector(Layout.HSHeaderLine[1]);
      primitiveLine.Render(spriteBatch);
      spriteBatch.End();
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.Texture = (Texture2D) renderTarget;
    }
  }
}
