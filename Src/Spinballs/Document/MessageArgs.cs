// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.MessageArgs
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System;

#nullable disable
namespace Spinballs.Document
{
  public class MessageArgs : EventArgs
  {
    public bool Handled;
    public Message Message;

    public MessageArgs() => this.Handled = false;

    public MessageArgs(Message msg)
    {
      this.Message = msg;
      this.Handled = false;
    }
  }
}
