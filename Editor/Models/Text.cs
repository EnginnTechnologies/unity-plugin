using System;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Text : Model
  {

    public int id;

    public int color_id;
    public ProjectColor color;

    public int character_syntheses_count;

    public int main_character_synthesis_id;

    public int main_character_id;
    public string main_character_name;

    public int main_synthesis_id;
    public string main_synthesis_created_at;
    public string main_synthesis_modifier;
    public string main_synthesis_text;
    public string main_synthesis_result_file_url;

    public string slug;

    public string GetMainSynthesisModifierName()
    {
      return Synthesis.GetModifierName(main_synthesis_modifier);
    }

    public bool ResultFileExists()
    {
      return ResultFile.Exists(slug);
    }

    public string GetMainResultFileAbsolutePath()
    {
      return ResultFile.GetAbsolutePath(slug);
    }

    public bool DownloadMainResultFile()
    {
      return ResultFile.DownloadFrom(slug, main_synthesis_result_file_url);
    }

    public ProjectColor GetColor()
    {
      return color;
    }

    public void SetColor(ProjectColor newColor, bool save = true)
    {
      if (newColor == null || newColor.id == 0) {
        if (color == null || color.id == 0) {
          return;
        }
      } else {
        if ((color != null) && (newColor.id == color.id)) {
          return;
        }
      }

      color = newColor;

      if (color == null || color.id == 0)
      {
        color_id = 0;
      } else {
        color_id = color.id;
      }

      if (save)
      {
        Update();
      }
    }

    // boolean result is only useful when an update occurs
    public bool SetMainCharacterSynthesisId(int newValue, bool save = true)
    {
      if (main_character_synthesis_id == newValue)
      {
        return false;
      }
      main_character_synthesis_id = newValue;
      if (save)
      {
        return Update();
      } else {
        return false;
      }
    }

    public CharacterSynthesis GetMainCharacterSynthesis()
    {
      var character_synthesis = new CharacterSynthesis();
      character_synthesis.id = main_character_synthesis_id;
      character_synthesis.is_main = true;
      character_synthesis.text_id = id;
      character_synthesis.text_slug = slug;
      character_synthesis.character_id = main_character_id;
      character_synthesis.character_name = main_character_name;
      character_synthesis.synthesis_id = main_synthesis_id;
      character_synthesis.synthesis_modifier = main_synthesis_modifier;
      character_synthesis.synthesis_result_file_url = main_synthesis_result_file_url;
      character_synthesis.synthesis_text = main_synthesis_text;
      character_synthesis.created_at = main_synthesis_created_at;
      return character_synthesis;
    }

    public bool Update()
    {
      Api.UpdateText(this);
      return errors.Count < 1;
    }

  }

}
