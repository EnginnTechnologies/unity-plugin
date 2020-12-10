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
    public string main_synthesis_modifier;
    public string main_synthesis_text;
    public string main_synthesis_result_file_url;

    public string slug;

    public string created_at;

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

    public void SetColor(ProjectColor newColor)
    {
      color = newColor;

      if (color == null)
      {
        color_id = 0;
      } else {
        color_id = color.id;
      }
    }

  }

}
