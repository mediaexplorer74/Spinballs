// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.GameEndPanel
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class GameEndPanel : ImageControl
  {
    private LabelControl _labelHeader;
    private LabelControl _labelPoints;
    private LabelControl _labelHighscore;

    public GameEndPanel()
    {
      this._labelHeader = new LabelControl();
      this._labelPoints = new LabelControl();
      this._labelHighscore = new LabelControl();
    }

    public string HeaderText
    {
      get => this._labelHeader.Text;
      set => this._labelHeader.Text = value;
    }

    public string PointsText
    {
      get => this._labelPoints.Text;
      set => this._labelPoints.Text = value;
    }

    public string HighscoreText
    {
      get => this._labelHighscore.Text;
      set => this._labelHighscore.Text = value;
    }

    public void Create(int highscoreIndex)
    {
      this.Create();
      Texture2D panel = Res.Common.Panel;
      this.Size = new Vector2(429f, 346f);
      this._labelHeader.DisplayRect = Layout.PanelHeader;
      this._labelHeader.Orientation = Orientation.Center;
      this._labelHeader.Font = Res.Font.Big2;
      RenderTarget2D renderTarget = new RenderTarget2D(Res.Game.GraphicsDevice, panel.Width, panel.Height);
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      spriteBatch.Draw(panel, new Vector2(), Color.White);
      if (highscoreIndex < 0)
      {
        this._labelPoints.DisplayRect = Layout.PanelBody;
        this._labelPoints.Font = Res.Font.Medium;
        Vector2 vector2_1 = this._labelPoints.Font.MeasureString(this._labelPoints.Text);
        int num = (int) ((double) Layout.PanelBody.Height - (double) vector2_1.Y * 2.0) / 4;
        this._labelPoints.Position = new Vector2((float) (((double) Layout.PanelBody.Width - (double) vector2_1.X) / 2.0 + 20.0), (float) (Layout.PanelBody.Y + num));
        this._labelHighscore.DisplayRect = Layout.PanelBody;
        this._labelHighscore.Font = Res.Font.Medium;
        Vector2 vector2_2 = this._labelHighscore.Font.MeasureString(this._labelHighscore.Text);
        this._labelHighscore.Position = new Vector2((float) (((double) Layout.PanelBody.Width - (double) vector2_2.X) / 2.0 + 20.0), (float) (Layout.PanelBody.Y + num * 2) + vector2_2.Y);
        this._labelHeader.Draw(spriteBatch);
        this._labelPoints.Draw(spriteBatch);
        this._labelHighscore.Draw(spriteBatch);
      }
      else if (highscoreIndex >= 0)
      {
        this._labelPoints.DisplayRect = Layout.PanelBody;
        this._labelPoints.Font = Res.Font.Medium;
        this._labelPoints.Orientation = Orientation.Bottom | Orientation.HorizontalCenter;
        LabelControl labelControl1 = new LabelControl();
        labelControl1.Text = Strings.Rank;
        labelControl1.DisplayRect = Layout.PanelBody;
        labelControl1.Font = Res.Font.Big2;
        labelControl1.Orientation = Orientation.Top | Orientation.HorizontalCenter;
        LabelControl labelControl2 = new LabelControl();
        labelControl2.SetText((object) (highscoreIndex + 1));
        labelControl2.DisplayRect = Layout.PanelBody;
        labelControl2.Font = Res.Font.Big8;
        labelControl2.Orientation = Orientation.Center;
        this._labelHeader.Draw(spriteBatch);
        this._labelPoints.Draw(spriteBatch);
        labelControl1.Draw(spriteBatch);
        labelControl2.Draw(spriteBatch);
        ImageControl imageControl = new ImageControl(Res.GameScreen.Cup);
        imageControl.Position = new Vector2(30f, (float) (Layout.PanelBody.Y + 10));
        imageControl.Draw(spriteBatch);
        imageControl.Position = new Vector2((float) (Layout.PanelBody.X + Layout.PanelBody.Width - imageControl.Texture.Width - 10), (float) (Layout.PanelBody.Y + 10));
        imageControl.Draw(spriteBatch);
      }
      spriteBatch.End();
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.Texture = (Texture2D) renderTarget;
      this.Size = new Vector2(429f, 346f);
    }
  }
}
