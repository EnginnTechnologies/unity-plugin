using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Project : Model
  {

    public int id;

    public string name;
    public ProjectColor[] colors;

    private string[] colorOptions = null;

    private static Project current = null;
    private static Project GetCurrent()
    {
      if (current == null)
      {
        current = Api.GetProject();
      }
      return current;
    }

    private ProjectColor _GetColorById(int id)
    {
      foreach (ProjectColor color in colors)
      {
        if(color.id == id)
        {
          return color;
        }
      }
      return null;
    }
    public static ProjectColor GetColorById(int id)
    {
      return GetCurrent()._GetColorById(id);
    }

    private ProjectColor _GetColorFromOptionsIndex(int idx)
    {
      if (idx < 1 || idx > colors.Length) {
        return null;
      } else {
        return colors[idx - 1];
      }
    }
    public static ProjectColor GetColorFromOptionsIndex(int idx)
    {
      return GetCurrent()._GetColorFromOptionsIndex(idx);
    }

    private int _GetColorOptionsIndex(ProjectColor c)
    {
      if (c == null)
      {
        return 0;
      }
      int idx = 0;
      foreach (ProjectColor color in colors)
      {
        idx++;
        if(color.id == c.id)
        {
          return idx;
        }
      }
      return 0;
    }
    public static int GetColorOptionsIndex(ProjectColor c)
    {
      return GetCurrent()._GetColorOptionsIndex(c);
    }

    private string[] _GetColorOptions()
    {
      if(colorOptions == null)
      {
        List<string> options = new List<string>();
        options.Add("None");
        foreach (ProjectColor color in colors)
        {
          options.Add($"#{color.id}: {color.name}");
        }
        colorOptions = options.ToArray();
      }
      return colorOptions;
    }
    public static string[] GetColorOptions()
    {
      return GetCurrent()._GetColorOptions();
    }

  }

}
