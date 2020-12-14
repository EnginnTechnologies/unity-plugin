using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Project : Model
  {

    // serialized attributes

    public int id;
    public string name;
    public ProjectColor[] colors;

    // other attributes

    private Character[] characters;

    private string[] colorDisplayedOptions;
    private int[] colorOptionValues;

    private string[] characterDisplayedOptions;
    private int[] characterIdOptionValues;

    private static Project current;

    // methods

    public static Project GetCurrent(bool refresh = false)
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
        if (colors != null)
        {
          foreach (ProjectColor color in colors)
          {
            options.Add(color.GetNameOrHex() + caret);
          }
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
        if (colors != null)
        {
          foreach (ProjectColor color in colors)
          {
            options.Add(color.id);
          }
        }
        colorOptionValues = options.ToArray();
      }
      return colorOptionValues;
    }
    public static int[] GetColorOptionValues()
    {
      return GetCurrent()._GetColorOptionValues();
    }

    private void _FetchCharacters()
    {
      characters = Api.GetCharacters();
      characterDisplayedOptions = null;
      characterIdOptionValues = null;
    }
    public static void FetchCharacters()
    {
      GetCurrent()._FetchCharacters();
    }

    public Character[] _GetCharacters()
    {
      return characters;
    }
    public static Character[] GetCharacters()
    {
      return GetCurrent()._GetCharacters();
    }

    private string[] _GetCharacterDisplayedOptions()
    {
      if(characterDisplayedOptions == null)
      {
        List<string> options = new List<string>();
        options.Add("None");
        if (characters != null)
        {
          foreach (Character character in characters)
          {
            options.Add(character.name);
          }
        }
        characterDisplayedOptions = options.ToArray();
      }
      return characterDisplayedOptions;
    }
    public static string[] GetCharacterDisplayedOptions()
    {
      return GetCurrent()._GetCharacterDisplayedOptions();
    }

    private int[] _GetCharacterIdOptionValues()
    {
      if(characterIdOptionValues == null)
      {
        List<int> options = new List<int>();
        options.Add(0);
        if (characters != null)
        {
          foreach (Character character in characters)
          {
            options.Add(character.id);
          }
        }
        characterIdOptionValues = options.ToArray();
      }
      return characterIdOptionValues;
    }
    public static int[] GetCharacterIdOptionValues()
    {
      return GetCurrent()._GetCharacterIdOptionValues();
    }

  }

}
