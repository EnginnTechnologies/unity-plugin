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

    public string GetHex()
    {
      return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
    }

    public string GetNameOrHex()
    {
      if (String.IsNullOrEmpty(name))
      {
        return GetHex();
      } else {
        return name;
      }
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
