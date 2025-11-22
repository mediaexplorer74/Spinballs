// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Controls.BaseControl
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Common.Helper;
using Spinballs.Core.Actions;
using Spinballs.Core.ScreenManagement;
using System;

#nullable disable
namespace Spinballs.Core.Controls
{
  public class BaseControl
  {
    protected Vector2 _position;
    protected Vector2 _size;
    private Vector2 _positionOffset;
    private bool _enabled;
    private ActionManager _actionManager;

    public virtual ActionManager ActionManager
    {
      get
      {
        return this._actionManager == null ? ScreenManager.ActiveScreen.ActionManager : this._actionManager;
      }
      set => this._actionManager = value;
    }

    public BaseControl()
    {
    }

    ~BaseControl() => this.Destroy();

    public BaseControl(Vector2 Position, Vector2 Size)
    {
      this._position = Position;
      this._size = Size;
    }

    public virtual Vector2 Position
    {
      get => this._position;
      set => this._position = value;
    }

    public virtual Vector2 Size
    {
      get => this._size;
      set => this._size = value;
    }

    public Vector2 PositionOffset
    {
      get => this._positionOffset;
      set => this._positionOffset = value;
    }

    public virtual bool Enabled
    {
      get => this._enabled;
      set => this._enabled = value;
    }

    public bool Contains(Vector2 pos)
    {
      return (double) this.Position.X <= (double) pos.X && (double) this.Position.X + (double) this.Size.X >= (double) pos.X && (double) this.Position.Y <= (double) pos.Y && (double) this.Position.Y + (double) this.Size.Y >= (double) pos.Y;
    }

    public event EventHandler Clicked;

    public virtual void OnClick(object sender)
    {
      if (!this.Enabled || this.Clicked == null)
        return;
      this.Clicked(sender, (EventArgs) null);
    }

    public virtual void Create() => this.Enabled = true;

    public virtual void Destroy()
    {
    }

    public Rectangle DisplayRect
    {
      get
      {
        return new Rectangle((int) this.Position.X, (int) this.Position.Y, (int) this.Size.X, (int) this.Size.Y);
      }
      set
      {
        this.Position = new Vector2((float) value.X, (float) value.Y);
        this.Size = new Vector2((float) value.Width, (float) value.Height);
      }
    }

    public void Align(Orientation orientation, Rectangle planeRect)
    {
      this.Position = Layout.GetAlignPos(orientation, planeRect, this.DisplayRect);
    }
  }
}
