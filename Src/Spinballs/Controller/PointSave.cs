// Decompiled with JetBrains decompiler
// Type: Spinballs.Controller.PointSave
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Common.Helper;

#nullable disable
namespace Spinballs.Controller
{
  public class PointSave : ControllerSave
  {
    public FloatingPoint Point1 = new FloatingPoint();
    public FloatingPoint Point2 = new FloatingPoint();
    public int ShowPointsStepOffset;
    public int CurrentShownPoints;
  }
}
