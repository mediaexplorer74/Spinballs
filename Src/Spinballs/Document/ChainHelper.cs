// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.ChainHelper
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

#nullable disable
namespace Spinballs.Document
{
  public class ChainHelper
  {
    public static Chain GetBestChain(Disc[] discs)
    {
      ChainList chainList = ChainHelper.BuildChain(discs);
      if (chainList.List.Count == 0)
        return (Chain) null;
      chainList.Sort();
      return chainList.List[0].Count < 3 ? (Chain) null : chainList.List[0];
    }

    public static ChainList BuildChain(Disc[] discs)
    {
      ChainList chainList = new ChainList();
      foreach (Disc disc in discs)
      {
        foreach (Ball ball in disc.Balls)
          ChainHelper.AddChainBall(chainList, ball);
      }
      return chainList;
    }

    public static void AddChainBall(ChainList chainList, Ball ball)
    {
      if (chainList.Contains(ball))
        return;
      Chain chain = new Chain();
      chainList.List.Add(chain);
      ChainHelper.AddBall(chain, ball);
    }

    public static void AddBall(Chain chain, Ball ball)
    {
      if (ball == null || chain.CheckedBalls.Contains(ball))
        return;
      chain.CheckedBalls.Add(ball);
      chain.Add(ball);
      foreach (Ball connectedBall in ball.ConnectedBalls)
      {
        if (connectedBall.Color == ball.Color)
          ChainHelper.AddBall(chain, connectedBall);
      }
    }
  }
}
