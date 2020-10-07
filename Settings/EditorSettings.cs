using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class EditorSettings
  {

    private const string API_KEY_PREF_KEY = "EnginnApiKey";

    public static string apiKey = EditorPrefs.GetString(API_KEY_PREF_KEY);

    [PreferenceItem("Enginn")]
    public static void EnginSettings()
    {
      apiKey = EditorGUILayout.TextField("API Key", apiKey);
      SaveApiKey();
    }

    static void SaveApiKey() {
      EditorPrefs.SetString(API_KEY_PREF_KEY, apiKey);
    }

  }

}
