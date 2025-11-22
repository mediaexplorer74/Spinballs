// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.LevelInfo
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

#nullable disable
namespace Spinballs.Document
{
  public struct LevelInfo(int level, int requiredPoints, int timeLoad)
  {
    public int Level = level;
    public int RequiredPoints = requiredPoints;
    public int TimeLoad = timeLoad;
  }
}
