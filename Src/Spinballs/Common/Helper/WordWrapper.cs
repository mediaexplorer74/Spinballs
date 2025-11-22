// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.WordWrapper
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

#nullable disable
namespace Spinballs.Common.Helper
{
  public static class WordWrapper
  {
    private static StringBuilder builder = new StringBuilder(" ");
    public static char[] NewLine = new char[2]{ '\r', '\n' };

    private static Vector2 MeasureCharacter(this SpriteFont font, char character)
    {
      WordWrapper.builder[0] = character;
      return font.MeasureString(WordWrapper.builder);
    }

    public static void WrapWord(
      StringBuilder original,
      StringBuilder target,
      SpriteFont font,
      Rectangle bounds,
      float scale)
    {
      int index1 = 0;
      float num1 = 0.0f;
      float num2 = 0.0f;
      int num3 = 0;
      for (int index2 = 0; index2 < original.Length; ++index2)
      {
        char ch = original[index2];
        float num4 = font.MeasureCharacter(ch).X * scale;
        num1 += num4;
        num2 += num4;
        if (ch != '\r' && ch != '\n')
        {
          if ((double) num1 > (double) bounds.Width)
          {
            if (char.IsWhiteSpace(ch))
            {
              target.Insert(index2 + num3, WordWrapper.NewLine);
              ++num3;
              num1 = 0.0f;
              num2 = 0.0f;
              continue;
            }
            target.Insert(index1, WordWrapper.NewLine);
            ++num3;
            target.Remove(index1 + WordWrapper.NewLine.Length, 1);
            num1 = num2;
            num2 = 0.0f;
          }
          else if (char.IsWhiteSpace(ch))
          {
            index1 = target.Length;
            num2 = 0.0f;
          }
        }
        else
        {
          num2 = 0.0f;
          num1 = 0.0f;
        }
        target.Append(ch);
      }
    }
  }
}
