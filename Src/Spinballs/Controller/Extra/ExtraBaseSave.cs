// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.Extra.ExtraBaseSave
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Common.Helper;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Controller.Extra
{
  [DataContract]
  public class ExtraBaseSave : ControllerSave
  {
    [DataMember]
    public ExtraState State;
    [DataMember]
    public float FillStartLoadValue;
    [DataMember]
    public float FillEndLoadValue;
    [DataMember]
    public bool Active;
    [DataMember]
    public TimeSpan ActiveDuration;
    [DataMember]
    public float LoadValue;
    [DataMember]
    public BlinkMode BlinkMode;
  }
}
