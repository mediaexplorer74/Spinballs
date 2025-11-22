// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.IActionContainer
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System.Collections.Generic;

#nullable disable
namespace Spinballs.Core.Actions
{
  public interface IActionContainer
  {
    List<ActionBase> Actions { get; }
  }
}
