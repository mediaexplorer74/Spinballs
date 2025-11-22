// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.Actions.ActionDuration
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Spinballs.Core.Actions
{
  [DataContract]
  public class ActionDuration : ImageAction
  {
    [DataMember]
    public TimeSpan _elapsed;
    protected TimeSpan _duration;

    public event DurationEventHandler Action;

    public ActionDuration(TimeSpan duration)
    {
      this._duration = duration;
      this._elapsed = new TimeSpan();
    }

    [DataMember]
    public TimeSpan Duration
    {
      get => this._duration;
      set => this._duration = value;
    }

    public override bool Update(GameTime gameTime)
    {
      this._elapsed += gameTime.ElapsedGameTime;
      if (this._duration <= this._elapsed)
        this.Finished = true;
      else if (this.Action != null)
        this.Action(this, new DurationEventArgs(this._elapsed, this._duration, (float) this._elapsed.TotalMilliseconds / (float) this._duration.TotalMilliseconds));
      return this.Finished;
    }

    public override void Reset()
    {
      this._elapsed = new TimeSpan();
      this.Finished = false;
    }

    public override void Init(ActionBase action)
    {
      base.Init(action);
      if (!(action is ActionDuration actionDuration))
        return;
      this.Duration = actionDuration.Duration;
      this._elapsed = actionDuration._elapsed;
    }
  }
}
