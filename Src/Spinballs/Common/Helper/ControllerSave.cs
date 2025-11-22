// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.ControllerSave
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Controller;
using Spinballs.Controller.Extra;
using Spinballs.Core.Actions;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Common.Helper
{
  [KnownType(typeof (TimerSave))]
  [KnownType(typeof (ExtraBaseSave))]
  [KnownType(typeof (PointSave))]
  [DataContract]
  [KnownType(typeof (ExtraPointSave))]
  [KnownType(typeof (GameStartSave))]
  [KnownType(typeof (ChainSave))]
  public class ControllerSave
  {
    [DataMember]
    public List<ActionBase> Actions = new List<ActionBase>();
  }
}
