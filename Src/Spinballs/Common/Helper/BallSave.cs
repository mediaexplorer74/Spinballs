// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.BallSave
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Common.Helper
{
  [DataContract]
  public class BallSave
  {
    [DataMember]
    public int FlatIndex;
    [DataMember]
    public BallColors Color;
    [DataMember]
    public Vector2 Position;
    [DataMember]
    public bool Visible;

    public BallSave()
    {
    }

    public BallSave(int index, BallColors color, Vector2 position, bool visible)
    {
      this.FlatIndex = index;
      this.Color = color;
      this.Position = position;
      this.Visible = visible;
    }
  }
}
