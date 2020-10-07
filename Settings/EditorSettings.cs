using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class EditorSettings
  {

    private const string API_BASE_URL = "EnginnApiBaseUrl";

    private static string apiBaseUrl = EditorPrefs.GetString(API_BASE_URL);

    public static string GetApiBaseUrl()
    {
      if (apiBaseUrl.Length > 0)
        return apiBaseUrl;
      return "https://app.enginn.tech/api/v1/";
    }

    private static void SaveApiBaseUrl()
    {
      EditorPrefs.SetString(API_BASE_URL, apiBaseUrl);
    }

    [PreferenceItem("Enginn")]
    public static void EnginSettings()
    {
      apiBaseUrl = EditorGUILayout.TextField("API Base URL", GetApiBaseUrl());
      SaveApiBaseUrl();
    }

  }

}
