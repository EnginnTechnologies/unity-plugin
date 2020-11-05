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

  }

}
