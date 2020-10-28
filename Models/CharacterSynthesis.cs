using System;
using System.IO;

namespace Enginn
{

  [System.Serializable]
  public class CharacterSynthesis : Model
  {

    public int id;
    public string modifier = Synthesis.Modifier.None;
    public string text = "";
    public string created_at;
    public int character_id;
    public int synthesis_id;
    public string synthesis_result_file_url;

    public bool Create()
    {
      Api.CreateCharacterSynthesis(this);
      return id > 0;
    }

    public bool DownloadResultFile()
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
      string filePath = $"{folderPath}/{id}.wav";
      return Api.DownloadWav(synthesis_result_file_url, filePath);
    }

  }

}
