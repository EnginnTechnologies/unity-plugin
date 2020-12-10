using System;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class ProjectColor : Model
  {
    public int id;

    public string name;
    public int r;
    public int g;
    public int b;

    public Color GetColor(float a = 1f)
    {
      return new Color(r / 255f, g / 255f, b / 255f, a);
    }

    public Color GetTextColor()
    {
      Color color = GetColor();
      if (color.grayscale < 0.5)
      {
        return Color.white;
      } else {
        return Color.black;
      }
    }
  }

}
