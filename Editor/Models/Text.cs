// using System;

namespace Enginn
{

  [System.Serializable]
  public class Text : Model
  {

    public int id;

    public int color_id;
    public string color_name;
    public string color_code;

    public int character_syntheses_count;

    public int main_character_synthesis_id;

    public int main_character_id;
    public string main_character_name;

    public int main_synthesis_id;
    public string main_synthesis_modifier;
    public string main_synthesis_text;

    public string slug;

    public string created_at;

    public string GetMainSynthesisModifierName()
    {
      return Synthesis.GetModifierName(main_synthesis_modifier);
    }

  }

}
