using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Settings
  {

    private static string apiKey = EditorPrefs.GetString("EnginnApiKey");

    public static string getApiKey() {
      return apiKey;
    }

    static void saveApiKey() {
      EditorPrefs.SetString("EnginnApiKey", apiKey);
    }

    [PreferenceItem("Enginn")]
    public static void EnginSettings()
    {
      apiKey = EditorGUILayout.TextField("API Key", apiKey);
      saveApiKey();
    }

  }

}
