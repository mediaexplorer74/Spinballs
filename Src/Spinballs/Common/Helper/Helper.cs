// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Helper
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System;

#nullable disable
namespace Spinballs.Common.Helper
{
  public class Helper
  {
    private static readonly Random rand = new Random();

    public static int GetRandom(int min, int max) => Spinballs.Common.Helper.Helper.rand.Next(min, max);
  }
}
