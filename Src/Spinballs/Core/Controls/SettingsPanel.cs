// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.SettingsPanel
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using System;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class SettingsPanel : ImageControl
  {
    private const float _slideOffset = 0.2f;
    private SliderControl _musicSlider;
    private SliderControl _soundSlider;
    private ImageControl _musicLeft;
    private ImageControl _musicRight;
    private ImageControl _soundLeft;
    private ImageControl _soundRight;
    private float _soundValue;
    private float _musicValue;

    public SettingsPanel(ActionManager actionManager)
    {
      this.ActionManager = actionManager;
      this._musicSlider = new SliderControl();
      this._soundSlider = new SliderControl();
      this._musicLeft = new ImageControl();
      this._musicRight = new ImageControl();
      this._soundLeft = new ImageControl();
      this._soundRight = new ImageControl();
    }

    public override void Create()
    {
      base.Create();
      Texture2D panel = Res.Common.Panel;
      RenderTarget2D renderTarget = new RenderTarget2D(Res.Game.GraphicsDevice, panel.Width, panel.Height);
      this._musicSlider.Init(0, 100, 0);
      this._musicSlider.Position = Layout.SliderMusic + this.Position;
      this._soundSlider.Init(0, 100, 0);
      this._soundSlider.Position = Layout.SliderSound + this.Position;
      Res.Game.GraphicsDevice.SetRenderTarget(renderTarget);
      Res.Game.GraphicsDevice.Clear(Color.Transparent);
      SpriteBatch spriteBatch = new SpriteBatch(Res.Game.GraphicsDevice);
      spriteBatch.Begin();
      spriteBatch.Draw(panel, new Vector2(), Color.White);
      LabelControl labelControl = new LabelControl();
      labelControl.DisplayRect = Layout.PanelHeader;
      labelControl.Orientation = Orientation.Center;
      labelControl.Font = Res.Font.Big2;
      labelControl.Text = Strings.Settings;
      labelControl.Draw(spriteBatch);
      labelControl.Text = Strings.Music;
      labelControl.DisplayRect = Layout.TextMusic;
      labelControl.Orientation = Orientation.Top | Orientation.HorizontalCenter;
      labelControl.Draw(spriteBatch);
      labelControl.Text = Strings.Sound;
      labelControl.DisplayRect = Layout.TextSound;
      labelControl.Draw(spriteBatch);
      ImageControl imageControl = new ImageControl();
      imageControl.Texture = Res.Common.ArrowRight;
      imageControl.Position = Layout.MusicLeft;
      imageControl.Effects = SpriteEffects.FlipHorizontally;
      imageControl.Draw(spriteBatch);
      imageControl.Texture = Res.Common.ArrowRight;
      imageControl.Position = Layout.MusicRight;
      imageControl.Effects = SpriteEffects.None;
      imageControl.Draw(spriteBatch);
      imageControl.Texture = Res.Common.ArrowRight;
      imageControl.Position = Layout.SoundLeft;
      imageControl.Effects = SpriteEffects.FlipHorizontally;
      imageControl.Draw(spriteBatch);
      imageControl.Texture = Res.Common.ArrowRight;
      imageControl.Position = Layout.SoundRight;
      imageControl.Effects = SpriteEffects.None;
      imageControl.Draw(spriteBatch);
      spriteBatch.End();
      Res.Game.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.Texture = (Texture2D) renderTarget;
      this.Size = new Vector2(429f, 346f);
      this._musicLeft.Texture = Res.Common.ArrowRightHighlight;
      this._musicLeft.Position = Layout.MusicLeft + this.Position;
      this._musicLeft.Effects = SpriteEffects.FlipHorizontally;
      this._musicRight.Texture = Res.Common.ArrowRightHighlight;
      this._musicRight.Position = Layout.MusicRight + this.Position;
      this._soundLeft.Texture = Res.Common.ArrowRightHighlight;
      this._soundLeft.Position = Layout.SoundLeft + this.Position;
      this._soundLeft.Effects = SpriteEffects.FlipHorizontally;
      this._soundRight.Texture = Res.Common.ArrowRightHighlight;
      this._soundRight.Position = Layout.SoundRight + this.Position;
      this._musicLeft.Opacity = (byte) 0;
      this._musicRight.Opacity = (byte) 0;
      this._soundLeft.Opacity = (byte) 0;
      this._soundRight.Opacity = (byte) 0;
      this.SoundValue = Config.Instance.SoundVolume;
      this.MusicValue = Config.Instance.MusicVolume;
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        this._musicSlider.Position = Layout.SliderMusic + this.Position;
        this._soundSlider.Position = Layout.SliderSound + this.Position;
        this._musicLeft.Position = Layout.MusicLeft + this.Position;
        this._musicRight.Position = Layout.MusicRight + this.Position;
        this._soundLeft.Position = Layout.SoundLeft + this.Position;
        this._soundRight.Position = Layout.SoundRight + this.Position;
      }
    }

    public override byte Opacity
    {
      get => base.Opacity;
      set
      {
        base.Opacity = value;
        this._musicSlider.Opacity = value;
        this._soundSlider.Opacity = value;
      }
    }

    public float SoundValue
    {
      get => this._soundValue;
      set
      {
        this._soundValue = value;
        if ((double) this._soundValue > 1.0)
          this._soundValue = 1f;
        if ((double) this._soundValue < 0.0)
          this._soundValue = 0.0f;
        Config.Instance.SoundVolume = this._soundValue;
        this._soundSlider.Value = (int) ((double) this._soundValue * 100.0);
      }
    }

    public float MusicValue
    {
      get => this._musicValue;
      set
      {
        this._musicValue = value;
        if ((double) this._musicValue > 1.0)
          this._musicValue = 1f;
        if ((double) this._musicValue < 0.0)
          this._musicValue = 0.0f;
        Config.Instance.MusicVolume = this._musicValue;
        this._musicSlider.Value = (int) ((double) this._musicValue * 100.0);
      }
    }

    public float AdminMusicValue
    {
      get => this._musicValue;
      set
      {
        if ((double) this._musicValue == (double) value)
          return;
        this._musicValue = value;
        if ((double) this._musicValue > 1.0)
          this._musicValue = 1f;
        if ((double) this._musicValue < 0.0)
          this._musicValue = 0.0f;
        Config.Instance.AdminMusicVolume = this._musicValue;
        this._musicSlider.Value = (int) ((double) this._musicValue * 100.0);
      }
    }

    public bool HandleInput()
    {
        bool flag = false;
        
        // Обработка сенсорного ввода
        foreach (TouchLocation touchLocation in Res.Input.TouchState)
        {
            if (touchLocation.State == TouchLocationState.Pressed)
            {
                Vector2 touchPos = new Vector2(touchLocation.Position.X, touchLocation.Position.Y);
                if (this.Contains(touchPos)) // Проверяем, что касание происходит внутри панели
                {
                    flag = this.HandleTap(touchPos);
                    if (flag)
                        break;
                }
            }
            else if (touchLocation.State != TouchLocationState.Invalid)
            {
                if (this._musicSlider.Contains(touchLocation.Position))
                {
                    this.AdminMusicValue = (float) this._musicSlider.GetValueByPos(touchLocation.Position) / 100f;
                    Config.Instance.OrigMusicVolume = new float?();
                }
                else if (this._soundSlider.Contains(touchLocation.Position))
                {
                    this.SoundValue = (float) this._soundSlider.GetValueByPos(touchLocation.Position) / 100f;
                    Config.Instance.OrigSoundVolume = new float?();
                    if (touchLocation.State == TouchLocationState.Released)
                        AudioManager.Play(Res.GameScreen.Sounds.Button);
                }
            }
        }
        
        // Обработка мышиного ввода
        if (Res.Input.IsNewMouseButtonPress(MouseButtons.Left))
        {
            Vector2 mousePos = Res.GetMousePositionInGameCoords();
            if (this.Contains(mousePos)) // Проверяем, что клик происходит внутри панели
            {
                flag = this.HandleTap(mousePos) || flag;
            }
        }
        
        return flag;
    }

    public bool HandleTap(Vector2 pos)
    {
        // Проверяем, что точка находится внутри панели
        if (!this.Contains(pos))
            return false;
            
        // Преобразуем позицию в игровые координаты если нужно
        Vector2 gamePos = Res.ConvertCoordinates(pos);
        
        if (this._musicLeft.Contains(gamePos))
        {
            this.StartHighlight(this._musicLeft);
            this.AdminMusicValue -= 0.2f;
            Config.Instance.OrigMusicVolume = new float?();
            return true;
        }
        if (this._musicRight.Contains(gamePos))
        {
            this.StartHighlight(this._musicRight);
            this.AdminMusicValue += 0.2f;
            Config.Instance.OrigMusicVolume = new float?();
            return true;
        }
        if (this._soundLeft.Contains(gamePos))
        {
            this.StartHighlight(this._soundLeft);
            this.SoundValue -= 0.2f;
            Config.Instance.OrigSoundVolume = new float?();
            AudioManager.Play(Res.GameScreen.Sounds.Button);
            return true;
        }
        if (this._soundRight.Contains(gamePos))
        {
            this.StartHighlight(this._soundRight);
            this.SoundValue += 0.2f;
            Config.Instance.OrigSoundVolume = new float?();
            AudioManager.Play(Res.GameScreen.Sounds.Button);
            return true;
        }
        // Проверяем слайдеры
        if (this._musicSlider.Contains(gamePos))
        {
            this.AdminMusicValue = (float) this._musicSlider.GetValueByPos(gamePos) / 100f;
            Config.Instance.OrigMusicVolume = new float?();
            return true;
        }
        if (this._soundSlider.Contains(gamePos))
        {
            this.SoundValue = (float) this._soundSlider.GetValueByPos(gamePos) / 100f;
            Config.Instance.OrigSoundVolume = new float?();
            AudioManager.Play(Res.GameScreen.Sounds.Button);
            return true;
        }
        return false; // Ни один из элементов не был затронут
    }

    private void StartHighlight(ImageControl img)
    {
      this.ActionManager.Add((ActionBase) new ActionSequence()
      {
        Actions = {
          (ActionBase) new ActionFadeIn((DrawableControl) img, TimeSpan.FromMilliseconds(250.0)),
          (ActionBase) new ActionFadeOut((DrawableControl) img, TimeSpan.FromMilliseconds(250.0))
        }
      });
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this._musicSlider.Draw(spriteBatch);
      this._soundSlider.Draw(spriteBatch);
      this._musicLeft.Draw(spriteBatch);
      this._musicRight.Draw(spriteBatch);
      this._soundLeft.Draw(spriteBatch);
      this._soundRight.Draw(spriteBatch);
    }
  }
}
