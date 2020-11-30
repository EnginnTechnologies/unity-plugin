using System;
using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class EditorSettings
  {

    private const string API_BASE_URL = "EnginnApiBaseUrl";
    private const string DEFAULT_API_BASE_URL = "https://app.enginn.tech/api/v1";

    private static string apiBaseUrl = EditorPrefs.GetString(API_BASE_URL);

    public static string GetApiBaseUrl()
    {
      if (!String.IsNullOrEmpty(apiBaseUrl))
        return apiBaseUrl;
      return DEFAULT_API_BASE_URL;
    }

    private static void SaveApiBaseUrl()
    {
      EditorPrefs.SetString(API_BASE_URL, apiBaseUrl);
      RefreshCaches(apiBaseUrl);
    }

    private static void RefreshCaches(string apiBaseUrl)
    {
      // we need this for background tasks
      Api.SetApiBaseUrl(apiBaseUrl);
    }

    [PreferenceItem("Enginn")]
    public static void EnginSettings()
    {
      apiBaseUrl = EditorGUILayout.TextField("API Base URL", GetApiBaseUrl());
      SaveApiBaseUrl();
    }

  }

}
