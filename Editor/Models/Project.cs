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

    private string[] colorDisplayedOptions = null;
    private int[] colorOptionValues = null;

    private static Project current = null;
    private static Project GetCurrent(bool refresh = false)
    {
      if (current == null || refresh)
      {
        current = Api.GetProject();
      }
      return current;
    }
    public static void RefreshCurrent()
    {
      GetCurrent(true);
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

    private string[] _GetColorDisplayedOptions()
    {
      if(colorDisplayedOptions == null)
      {
        string caret = " \u2304".ToString();
        List<string> options = new List<string>();
        options.Add("None" + caret);
        foreach (ProjectColor color in colors)
        {
          options.Add(color.GetNameOrHex() + caret);
        }
        colorDisplayedOptions = options.ToArray();
      }
      return colorDisplayedOptions;
    }
    public static string[] GetColorDisplayedOptions()
    {
      return GetCurrent()._GetColorDisplayedOptions();
    }

    private int[] _GetColorOptionValues()
    {
      if(colorOptionValues == null)
      {
        List<int> options = new List<int>();
        options.Add(0);
        foreach (ProjectColor color in colors)
        {
          options.Add(color.id);
        }
        colorOptionValues = options.ToArray();
      }
      return colorOptionValues;
    }
    public static int[] GetColorOptionValues()
    {
      return GetCurrent()._GetColorOptionValues();
    }

  }

}
