using System;
using System.IO;

namespace Enginn
{

  [System.Serializable]
  public class CharacterSynthesis : Model
  {

    public int id;
    public string modifier = Synthesis.Modifier.None;
    public string text;
    public string created_at;
    public int character_id;
    public int synthesis_id;
    public string synthesis_result_file_url;

    private string slug;
    private int importFileLine;

    public bool Create()
    {
      Api.CreateCharacterSynthesis(this);
      return id > 0;
    }

    public string ResultFilePath(string fileName = null)
    {
      string folderPath = "Assets/EnginnResults";
      if (String.IsNullOrEmpty(fileName))
      {
        if (String.IsNullOrEmpty(slug))
        {
          throw new System.ArgumentException("Neither fileName nor slug provided", "fileName");
        }
        return $"{folderPath}/{slug}.wav";
      }
      return $"{folderPath}/{fileName}.wav";
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
      string folderPath = "Assets/EnginnResults";
      if(!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }
      string filePath = ResultFilePath(fileName);
      return Api.DownloadWav(synthesis_result_file_url, filePath);
    }

    public void SetSlug(string value)
    {
      slug = value;
    }
    public string GetSlug()
    {
      return slug;
    }

    public void SetImportFileLine(int value)
    {
      importFileLine = value;
    }
    public int GetImportFileLine()
    {
      return importFileLine;
    }

  }

}
