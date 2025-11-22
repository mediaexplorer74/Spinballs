// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionMessage
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Document;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionMessage : ActionBase
  {
    private MessageArgs _messageArgs;
    private object _sender;

    public ActionMessage(object sender, MessageArgs messageArgs)
    {
      this.MessageArgs = messageArgs;
      this.Sender = sender;
    }

    public MessageArgs MessageArgs
    {
      get => this._messageArgs;
      set => this._messageArgs = value;
    }

    public object Sender
    {
      get => this._sender;
      set => this._sender = value;
    }

    public override bool Update(GameTime gameTime)
    {
      MessageService.SendMessage(this.Sender, this.MessageArgs);
      this.Finished = true;
      return this.Finished;
    }

    public override void Reset() => this.Finished = false;
  }
}
