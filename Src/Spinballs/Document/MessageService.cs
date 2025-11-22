// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.MessageService
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

#nullable disable
namespace Spinballs.Document
{
  public class MessageService
  {
    public static event MessageHandler Message;

    public static void SendMessage(object sender, MessageArgs args)
    {
      if (MessageService.Message == null)
        return;
      MessageService.Message(sender, args);
    }

    public static bool PlayExecuteChainSound(object sender)
    {
      if (MessageService.Message == null)
        return false;
      MessageArgs args = new MessageArgs(Spinballs.Document.Message.PlayExecuteChainSound);
      MessageService.Message(sender, args);
      return args.Handled;
    }

    public static bool SetTimerBarIced(object sender, bool iced)
    {
      if (MessageService.Message == null)
        return false;
      MessageArgs args = (MessageArgs) new TimerBarArgs(new bool?(iced), new bool?());
      MessageService.Message(sender, args);
      return args.Handled;
    }

    public static bool ResetTimerBar(object sender)
    {
      if (MessageService.Message == null)
        return false;
      MessageArgs args = (MessageArgs) new TimerBarArgs(new bool?(), new bool?(true));
      MessageService.Message(sender, args);
      return args.Handled;
    }

    public static bool ShowExtraPoints(object sender, int value)
    {
      if (MessageService.Message == null)
        return false;
      MessageArgs args = (MessageArgs) new ValueArgs<int>(value, Spinballs.Document.Message.ShowExtraPoints);
      MessageService.Message(sender, args);
      return args.Handled;
    }

    public static bool ContinueGame(object sender)
    {
      if (MessageService.Message == null)
        return false;
      MessageArgs args = (MessageArgs) new ContinueGameArgs();
      MessageService.Message(sender, args);
      return args.Handled;
    }
  }
}
