using UnityEditor;
using UnityEngine;

public class Enginn
{

  private static string apiKey = EditorPrefs.GetString("EnginnApiKey");

  static void saveApiKey() {
    EditorPrefs.SetString("EnginnApiKey", apiKey);
  }

  [PreferenceItem("Enginn")]
  public static void EnginSettings()
  {
    apiKey = EditorGUILayout.TextField("API Key", apiKey);
    saveApiKey();
  }

  [MenuItem("Enginn/Hello")]
  public static void SayHello()
  {
    Debug.Log("Hello ! :)");
  }

}
