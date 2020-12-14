using System;
using System.IO;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class CharacterSynthesis : Model
  {

    public const string RESULT_PATH = "Assets/EnginnResults";

    public int id;
    public bool is_main;

    public int text_id;
    public string text_slug;

    public int character_id;
    public string character_name;

    public int synthesis_id;
    public string synthesis_modifier = Synthesis.Modifier.None;
    public string synthesis_result_file_url;
    public string synthesis_text;

    private int importFileLine;

    public bool Create()
    {
      Api.CreateCharacterSynthesis(this);
      return id > 0;
    }

    public bool ResultFileExists()
    {
      return ResultFile.Exists(text_slug);
    }

    public bool DownloadResultFile()
    {
      return ResultFile.DownloadFrom(text_slug, synthesis_result_file_url);
    }

    public void SetImportFileLine(int value)
    {
      importFileLine = value;
    }
    public int GetImportFileLine()
    {
      return importFileLine;
    }

    public string GetSynthesisModifierName()
    {
      return Synthesis.GetModifierName(synthesis_modifier);
    }

    public bool MainResultFileExists()
    {
      return ResultFile.Exists(text_slug);
    }

    public string GetResultFileAbsolutePath()
    {
      return ResultFile.GetAbsolutePath(text_slug);
    }

  }

}
