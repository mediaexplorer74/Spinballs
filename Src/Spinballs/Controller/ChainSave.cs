// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.ChainSave
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Common.Helper;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Controller
{
  [DataContract]
  public class ChainSave : ControllerSave
  {
    [DataMember]
    public int Dummy = 1;
  }
}
