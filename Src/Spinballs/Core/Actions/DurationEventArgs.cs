// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.DurationEventArgs
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System;

#nullable disable
namespace Spinballs.Core.Actions
{
  public class DurationEventArgs : EventArgs
  {
    public TimeSpan Ellapsed;
    public TimeSpan Duration;
    public float Fraction;

    public DurationEventArgs(TimeSpan ellapsed, TimeSpan duration, float fraction)
    {
      this.Ellapsed = ellapsed;
      this.Duration = duration;
      this.Fraction = fraction;
    }
  }
}
