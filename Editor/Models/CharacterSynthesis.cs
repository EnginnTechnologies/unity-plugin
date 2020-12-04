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
    public string slug;
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

    public static string AbsoluteResultPath()
    {
      string assetsPath = Application.dataPath;
      string rootPath = assetsPath.Substring(0, assetsPath.Length - "Assets".Length);
      return rootPath + RESULT_PATH;
    }

    public string ResultFilePath(string fileName = null)
    {
      if (String.IsNullOrEmpty(fileName))
      {
        if (String.IsNullOrEmpty(slug))
        {
          throw new System.ArgumentException("Neither fileName nor slug provided", "fileName");
        }
        return $"{RESULT_PATH}/{slug}.wav";
      }
      return $"{RESULT_PATH}/{fileName}.wav";
    }


    public bool ResultFileExists(string fileName = null)
    {
      return File.Exists(ResultFilePath(fileName));
    }

    public bool DownloadResultFile(string fileName = null)
    {
      if (String.IsNullOrEmpty(synthesis_result_file_url))
      {
        return false;
      }
      if(!Directory.Exists(RESULT_PATH))
      {
        Directory.CreateDirectory(RESULT_PATH);
      }
      string filePath = ResultFilePath(fileName);
      return Api.DownloadWav(synthesis_result_file_url, filePath);
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
      int idx = -1;
      foreach (string synthesisModifier in Synthesis.Modifiers)
      {
        idx++;
        if(modifier == synthesisModifier)
        {
          return Synthesis.ModifierNames[idx];
        }
      }
      return "None";
    }

  }

}
