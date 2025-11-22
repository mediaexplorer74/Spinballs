// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.ValueArgs`1
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

#nullable disable
namespace Spinballs.Document
{
  public class ValueArgs<T> : MessageArgs
  {
    public T Value;

    public ValueArgs(T value, Message message)
      : base(message)
    {
      this.Value = value;
    }
  }
}
