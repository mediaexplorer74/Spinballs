// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.ExtraPointSave
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Controller.Extra
{
  [DataContract]
  public class ExtraPointSave : ExtraBaseSave
  {
    [DataMember]
    public Vector2 X2Position;
    [DataMember]
    public byte X2Opacity;
  }
}
