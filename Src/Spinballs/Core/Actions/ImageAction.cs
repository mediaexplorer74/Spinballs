// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ImageAction
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Core.Controls;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ImageAction : ActionBase
  {
    [DataMember]
    public int Dummy = 2;
    private DrawableControl _imageControl;

    public DrawableControl ImageControl
    {
      get => this._imageControl;
      set => this.Target = (object) (this._imageControl = value);
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      this._imageControl = (DrawableControl) (action.Target as Spinballs.Core.Controls.ImageControl);
    }
  }
}
