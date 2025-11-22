// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.ChainList
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System.Collections.Generic;

#nullable disable
namespace Spinballs.Document
{
  public class ChainList
  {
    private System.Collections.Generic.List<Chain> _chainList = new System.Collections.Generic.List<Chain>();

    public System.Collections.Generic.List<Chain> List
    {
      get => this._chainList;
      set => this._chainList = value;
    }

    public bool Contains(Ball ball)
    {
      foreach (System.Collections.Generic.List<Ball> chain in this._chainList)
      {
        if (chain.Contains(ball))
          return true;
      }
      return false;
    }

    public override string ToString()
    {
      System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
      int num = 0;
      foreach (Chain chain in this.List)
        stringList.Add(string.Format("Chain {0} ({1}): {2}\n", (object) num++, (object) chain.Count, (object) chain.ToString()));
      return string.Join("", stringList.ToArray());
    }

    public void Sort() => this.List.Sort((IComparer<Chain>) new ChainList.ChainComparer());

    private class ChainComparer : IComparer<Chain>
    {
      public int Compare(Chain x, Chain y) => y.Count - x.Count;
    }
  }
}
