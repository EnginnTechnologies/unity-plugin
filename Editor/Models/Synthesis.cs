// using System;

namespace Enginn
{

  public class Synthesis
  {

    public class Modifier
    {
      public const string None = "none";
      public const string Phone = "phone";
      // public const string BadSpeaker = "badspeaker";
    }

    public static readonly string[] Modifiers = {Modifier.None, Modifier.Phone};//, Modifier.BadSpeaker};
    public static readonly string[] ModifierNames = {"None", "Phone"};//, "Bad speaker"};

    public static string GetModifierName(string modifier)
    {
      int idx = -1;
      foreach (string synthesisModifier in Modifiers)
      {
        idx++;
        if(modifier == synthesisModifier)
        {
          return ModifierNames[idx];
        }
      }
      return "None";
    }

    public static int GetModifierIndex(string modifier)
    {
      int idx = -1;
      foreach (string synthesisModifier in Modifiers)
      {
        idx++;
        if(modifier == synthesisModifier)
        {
          return idx;
        }
      }
      return 0;
    }

  }

}
