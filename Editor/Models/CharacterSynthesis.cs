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
    public string modifier = Synthesis.Modifier.None;
    public string text_slug;
    public string text;
    public string created_at;
    public int character_id;
    public int synthesis_id;
    public string synthesis_result_file_url;

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

    public string GetModifierName()
    {
      return Synthesis.GetModifierName(modifier);
    }

  }

}
