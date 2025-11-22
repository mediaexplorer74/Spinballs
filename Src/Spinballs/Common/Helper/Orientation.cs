// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Orientation
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System;

#nullable disable
namespace Spinballs.Common.Helper
{
  [Flags]
  public enum Orientation
  {
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8,
    VerticalCenter = 16, // 0x00000010
    HorizontalCenter = 32, // 0x00000020
    Center = HorizontalCenter | VerticalCenter, // 0x00000030
  }
}
