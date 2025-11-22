// Decompiled with JetBrains decompiler
// Type: Spinballs.Core.InputState
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

#nullable disable
namespace Spinballs.Core
{
  public class InputState
  {
    public const int MaxInputs = 4;
    public readonly KeyboardState[] CurrentKeyboardStates;
    public readonly GamePadState[] CurrentGamePadStates;
    public readonly KeyboardState[] LastKeyboardStates;
    public readonly GamePadState[] LastGamePadStates;
    public readonly bool[] GamePadWasConnected;
    public TouchCollection TouchState;
    public readonly List<GestureSample> Gestures = new List<GestureSample>();

    public InputState()
    {
      this.CurrentKeyboardStates = new KeyboardState[4];
      this.CurrentGamePadStates = new GamePadState[4];
      this.LastKeyboardStates = new KeyboardState[4];
      this.LastGamePadStates = new GamePadState[4];
      this.GamePadWasConnected = new bool[4];
    }

    public void Update()
    {
      for (int index = 0; index < 4; ++index)
      {
        this.LastKeyboardStates[index] = this.CurrentKeyboardStates[index];
        this.LastGamePadStates[index] = this.CurrentGamePadStates[index];
        this.CurrentKeyboardStates[index] = Keyboard.GetState((PlayerIndex) index);
        this.CurrentGamePadStates[index] = GamePad.GetState((PlayerIndex) index);
        if (this.CurrentGamePadStates[index].IsConnected)
          this.GamePadWasConnected[index] = true;
      }
      this.TouchState = TouchPanel.GetState();
      this.Gestures.Clear();
      while (TouchPanel.IsGestureAvailable)
        this.Gestures.Add(TouchPanel.ReadGesture());
    }

    public bool IsNewKeyPress(
      Keys key,
      PlayerIndex? controllingPlayer,
      out PlayerIndex playerIndex)
    {
      if (controllingPlayer.HasValue)
      {
        playerIndex = controllingPlayer.Value;
        int index = (int) playerIndex;
        return this.CurrentKeyboardStates[index].IsKeyDown(key) && this.LastKeyboardStates[index].IsKeyUp(key);
      }
      return this.IsNewKeyPress(key, new PlayerIndex?(PlayerIndex.One), out playerIndex) || this.IsNewKeyPress(key, new PlayerIndex?(PlayerIndex.Two), out playerIndex) || this.IsNewKeyPress(key, new PlayerIndex?(PlayerIndex.Three), out playerIndex) || this.IsNewKeyPress(key, new PlayerIndex?(PlayerIndex.Four), out playerIndex);
    }

    public bool IsNewButtonPress(
      Buttons button,
      PlayerIndex? controllingPlayer,
      out PlayerIndex playerIndex)
    {
      if (controllingPlayer.HasValue)
      {
        playerIndex = controllingPlayer.Value;
        int index = (int) playerIndex;
        return this.CurrentGamePadStates[index].IsButtonDown(button) && this.LastGamePadStates[index].IsButtonUp(button);
      }
      return this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.One), out playerIndex) || this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.Two), out playerIndex) || this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.Three), out playerIndex) || this.IsNewButtonPress(button, new PlayerIndex?(PlayerIndex.Four), out playerIndex);
    }

    public bool IsMenuSelect(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
    {
      return this.IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) || this.IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
    }

    public bool IsMenuCancel(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
    {
      return this.IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
    }

    public bool IsMenuUp(PlayerIndex? controllingPlayer)
    {
      PlayerIndex playerIndex;
      return this.IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex);
    }

    public bool IsMenuDown(PlayerIndex? controllingPlayer)
    {
      PlayerIndex playerIndex;
      return this.IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex);
    }

    public bool IsPauseGame(PlayerIndex? controllingPlayer)
    {
      PlayerIndex playerIndex;
      return this.IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex) || this.IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
    }
  }
}
