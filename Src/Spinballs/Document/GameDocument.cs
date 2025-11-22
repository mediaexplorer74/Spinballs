// Decompiled with JetBrains decompiler
// Type: Spinballs.Document.GameDocument
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Spinballs.Common.Helper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Document
{
  public class GameDocument
  {
    private Disc[] _discs = new Disc[Layout.DiscCount];
    private Chain _bestChain;
    private int _currentLevel;
    private int _points;
    private GameStateManager _state;
    private static Spinballs.Document.LevelInfo[] _levelInfo;

    public event EventHandler BallsRearranged;

    public event EventHandler BestChainChanged;

    public event EventHandler LevelChanged;

    public event GameDocument.AddPointHandler PointsChanging;

    public event GameDocument.AddPointHandler PointsChanged;

    public GameDocument()
    {
      this._currentLevel = 0;
      this._points = 0;
      this._state = new GameStateManager();
      this._state.Changed += new GameStateChangeHandler(this.State_Changed);
      this._state.QueryNextState += new EventHandler(this.State_QueryNextState);
      GameDocument.InitLevelInfos();
      this.InitStructure();
    }

    private void InitStructure()
    {
      Disc[] discs = this._discs;
      for (int discIndex = 0; discIndex < Layout.DiscCount; ++discIndex)
      {
        discs[discIndex] = new Disc(discIndex);
        for (int index1 = 0; index1 < Layout.BallsPerDisc; ++index1)
        {
          int index2 = index1 - 1 < 0 ? 5 : index1 - 1;
          int index3 = index1 + 1 > 5 ? 0 : index1 + 1;
          discs[discIndex][index1].Add(discs[discIndex][index2], discs[discIndex][index3]);
        }
      }
      discs[0][0].Add(discs[1][3]);
      discs[0][1].Add(discs[2][4]);
      discs[0][2].Add(discs[3][5]);
      discs[0][3].Add(discs[4][0]);
      discs[0][4].Add(discs[5][1]);
      discs[0][5].Add(discs[6][2]);
      discs[1][2].Add(discs[2][5]);
      discs[1][3].Add(discs[0][0]);
      discs[1][4].Add(discs[6][1]);
      discs[2][3].Add(discs[3][0]);
      discs[2][4].Add(discs[0][1]);
      discs[2][5].Add(discs[1][2]);
      discs[3][4].Add(discs[4][1]);
      discs[3][5].Add(discs[0][2]);
      discs[3][0].Add(discs[2][3]);
      discs[4][0].Add(discs[0][3]);
      discs[4][1].Add(discs[3][4]);
      discs[4][5].Add(discs[5][2]);
      discs[4][0].Add(discs[0][3]);
      discs[4][1].Add(discs[3][4]);
      discs[4][5].Add(discs[5][2]);
      discs[5][0].Add(discs[6][3]);
      discs[5][1].Add(discs[0][4]);
      discs[5][2].Add(discs[4][5]);
      discs[6][1].Add(discs[1][4]);
      discs[6][2].Add(discs[0][5]);
      discs[6][3].Add(discs[5][0]);
    }

    private static void InitLevelInfos()
    {
      GameDocument._levelInfo = new Spinballs.Document.LevelInfo[34]
      {
        new Spinballs.Document.LevelInfo(0, 0, 30000),
        new Spinballs.Document.LevelInfo(1, 1000, 28000),
        new Spinballs.Document.LevelInfo(2, 1600, 26000),
        new Spinballs.Document.LevelInfo(3, 2400, 24000),
        new Spinballs.Document.LevelInfo(4, 3400, 22000),
        new Spinballs.Document.LevelInfo(5, 4600, 20000),
        new Spinballs.Document.LevelInfo(6, 6000, 18000),
        new Spinballs.Document.LevelInfo(7, 8000, 16000),
        new Spinballs.Document.LevelInfo(8, 11000, 14000),
        new Spinballs.Document.LevelInfo(9, 15000, 12000),
        new Spinballs.Document.LevelInfo(10, 22000, 10000),
        new Spinballs.Document.LevelInfo(11, 30000, 9000),
        new Spinballs.Document.LevelInfo(12, 40000, 8000),
        new Spinballs.Document.LevelInfo(13, 52000, 7000),
        new Spinballs.Document.LevelInfo(14, 66000, 6000),
        new Spinballs.Document.LevelInfo(15, 82000, 5500),
        new Spinballs.Document.LevelInfo(16, 100000, 5000),
        new Spinballs.Document.LevelInfo(17, 120000, 4500),
        new Spinballs.Document.LevelInfo(18, 150000, 4000),
        new Spinballs.Document.LevelInfo(19, 200000, 3750),
        new Spinballs.Document.LevelInfo(20, 260000, 3500),
        new Spinballs.Document.LevelInfo(21, 330000, 3250),
        new Spinballs.Document.LevelInfo(22, 410000, 3000),
        new Spinballs.Document.LevelInfo(23, 500000, 2750),
        new Spinballs.Document.LevelInfo(24, 600000, 2500),
        new Spinballs.Document.LevelInfo(25, 710000, 2250),
        new Spinballs.Document.LevelInfo(26, 830000, 2000),
        new Spinballs.Document.LevelInfo(27, 960000, 1750),
        new Spinballs.Document.LevelInfo(28, 1100000, 1500),
        new Spinballs.Document.LevelInfo(29, 1250000, 1250),
        new Spinballs.Document.LevelInfo(30, 1410000, 1000),
        new Spinballs.Document.LevelInfo(31, 1580000, 750),
        new Spinballs.Document.LevelInfo(32, 1760000, 500),
        new Spinballs.Document.LevelInfo(33, 1950000, 250)
      };
    }

    public void StartGame()
    {
      this.RandomizeBallColors();
      this.State = GameState.Starting;
    }

    public void Init()
    {
      this._currentLevel = 0;
      this._points = 0;
      if (Config.Instance.LastPlayLevel <= 0)
        return;
      this._currentLevel = Config.Instance.LastPlayLevel;
      this._points = GameDocument.LevelInfo[this._currentLevel].RequiredPoints;
    }

    public void RandomizeBallColors()
    {
      for (int index = 0; index < Layout.DiscCount; ++index)
        this.Discs[index].Randomize();
    }

    public Disc[] Discs => this._discs;

    public Chain BestChain
    {
      get => this._bestChain;
      set
      {
        this._bestChain = value;
        if (this.BestChainChanged == null)
          return;
        this.BestChainChanged((object) this, (EventArgs) null);
      }
    }

    public int CurrentLevel
    {
      get => this._currentLevel;
      set
      {
        if (this._currentLevel == value)
          return;
        this._currentLevel = value;
        Config.Instance.LastPlayLevel = this._currentLevel;
        if (this.LevelChanged == null)
          return;
        this.LevelChanged((object) this, (EventArgs) null);
      }
    }

    public int CurrentPoints
    {
      get
      {
        return this.CurrentLevel > 0 ? this.Points - GameDocument.LevelInfo[this.CurrentLevel].RequiredPoints : this.Points;
      }
    }

    public int CurrentLevelRequiredPoints
    {
      get
      {
        return this.CurrentLevel == GameDocument.LevelInfo.Length - 1 ? int.MaxValue : GameDocument.LevelInfo[this.CurrentLevel + 1].RequiredPoints - GameDocument.LevelInfo[this.CurrentLevel].RequiredPoints;
      }
    }

    public GameState State
    {
      get => this._state.State;
      set => this._state.Change(value);
    }

    public GameStateManager StateManager => this._state;

    public int Points
    {
      get => this._points;
      set
      {
        if (this._points == value)
          return;
        GameDocument.AddPointArgs e = new GameDocument.AddPointArgs(value, value - this._points);
        if (this.PointsChanging != null)
          this.PointsChanging((object) this, e);
        this._points = value + e.ExtraOffset;
        if (this.PointsChanged == null)
          return;
        this.PointsChanged((object) this, e);
      }
    }

    public static Spinballs.Document.LevelInfo[] LevelInfo
    {
      get
      {
        if (GameDocument._levelInfo == null)
          GameDocument.InitLevelInfos();
        return GameDocument._levelInfo;
      }
    }

    private void State_QueryNextState(object sender, EventArgs e)
    {
      switch (this.State)
      {
        case GameState.Starting:
        case GameState.ClearBalls:
        case GameState.LevelUp:
        case GameState.BonusExplode:
        case GameState.BonusSortBalls:
        case GameState.Pause:
          this.State = GameState.Running;
          break;
      }
    }

    private void State_Changed(object sender, StateChangeToken args)
    {
      switch (this.State)
      {
        case GameState.Running:
          if (args.RealPrevState == GameState.ClearBalls || args.RealPrevState == GameState.BonusExplode)
          {
            if (this.BestChain != null)
            {
              foreach (Disc disc in this.BestChain.Discs)
                disc.Randomize();
            }
            else
            {
              foreach (Disc disc in this.Discs)
                disc.Randomize();
            }
            this.UpdateBestChain();
            if (this.BallsRearranged != null)
              this.BallsRearranged((object) this, (EventArgs) null);
          }
          else if (args.RealPrevState == GameState.BonusSortBalls)
            this.UpdateBestChain();
          this.UpdateLevel();
          break;
        case GameState.ClearBalls:
          this.Points += this.CalculatePoints();
          break;
      }
    }

    public void ExecuteChain()
    {
      if (this.BestChain == null)
        return;
      this.State = GameState.ClearBalls;
    }

    public int CalculatePoints()
    {
      int points = 10;
      for (int index = 0; index < this.BestChain.Count - 3; ++index)
        points += (2 + index) * 10;
      return points;
    }

    public void Save(SaveGame savegame)
    {
      savegame.Points = this.Points;
      this._state.Save(savegame);
      if (this.State != GameState.ClearBalls)
        return;
      foreach (Ball ball in (List<Ball>) this.BestChain)
        savegame.ChainBalls.Add(ball.FlatIndex);
    }

    public void Load(SaveGame savegame)
    {
      this._points = savegame.Points;
      this._state.Load(savegame);
      this._currentLevel = this.GetLevel();
      if (this.State == GameState.ClearBalls)
      {
        Chain chain = new Chain();
        foreach (int chainBall in savegame.ChainBalls)
        {
          int index1 = chainBall / Layout.BallsPerDisc;
          int index2 = chainBall - index1 * Layout.BallsPerDisc;
          chain.Add(this.Discs[index1][index2]);
        }
        this._bestChain = chain;
      }
      else
        this.UpdateBestChain();
    }

    public void Update(GameTime gameTime) => this.StateManager.Update(gameTime);

    public static int GetLevel(int points)
    {
      if (points == 0)
        return 0;
      for (int index = 1; index <= GameDocument.LevelInfo.Length; ++index)
      {
        if (points < GameDocument.LevelInfo[index].RequiredPoints)
          return index - 1;
      }
      return GameDocument.LevelInfo.Length;
    }

    private int GetLevel() => GameDocument.GetLevel(this.Points);

    private void UpdateLevel() => this.CurrentLevel = this.GetLevel();

    private void UpdateBestChain() => this.BestChain = ChainHelper.GetBestChain(this.Discs);

    public void RotateDisc(int discIndex, bool clockwise)
    {
      Disc disc = this.Discs[discIndex];
      if (clockwise)
      {
        BallColors color = disc[Layout.BallsPerDisc - 1].Color;
        for (int index = Layout.BallsPerDisc - 1; index > 0; --index)
          disc[index].Color = disc[index - 1].Color;
        disc[0].Color = color;
      }
      else
      {
        BallColors color = disc[0].Color;
        for (int index = 0; index < Layout.BallsPerDisc - 1; ++index)
          disc[index].Color = disc[index + 1].Color;
        disc[Layout.BallsPerDisc - 1].Color = color;
      }
      this.UpdateBestChain();
    }

    public class AddPointArgs : EventArgs
    {
      public int Points;
      public int Offset;
      public int ExtraOffset;

      public AddPointArgs(int points, int offset)
      {
        this.Points = points;
        this.Offset = offset;
        this.ExtraOffset = 0;
      }
    }

    public delegate void AddPointHandler(object sender, GameDocument.AddPointArgs e);
  }
}
