using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Enginn
{

  // Create a new type of Settings Asset.
  class ProjectSettings : ScriptableObject
  {
    public const string k_ProjectSettingsPath = "Assets/EnginnSettings.asset";

    [SerializeField]
    private string m_ProjectApiToken;

    internal static ProjectSettings GetOrCreateSettings()
    {
      var settings = AssetDatabase.LoadAssetAtPath<ProjectSettings>(k_ProjectSettingsPath);
      if (settings == null)
      {
        settings = ScriptableObject.CreateInstance<ProjectSettings>();
        settings.m_ProjectApiToken = "";
        AssetDatabase.CreateAsset(settings, k_ProjectSettingsPath);
        AssetDatabase.SaveAssets();
      }
      return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
      return new SerializedObject(GetOrCreateSettings());
    }

    public static string GetProjectApiToken()
    {
      return GetSerializedSettings().FindProperty("m_ProjectApiToken").stringValue;
    }
  }

  // Create ProjectSettingsProvider by deriving from SettingsProvider:
  class ProjectSettingsProvider : SettingsProvider
  {
    private SerializedObject m_ProjectSettings;

    class Styles
    {
      public static GUIContent projectId = new GUIContent("Project API key");
    }

    // const string k_ProjectSettingsPath = "ProjectSettings/EnginnSettings.asset";
    public ProjectSettingsProvider(string path, SettingsScope scope = SettingsScope.User)
      : base(path, scope) {}

    // This function is called when the user clicks on the Enginn element in the Settings window.
    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
      m_ProjectSettings = ProjectSettings.GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
      // Debug.Log("[ProjectSettingsProvider] OnGUI");
      // Use IMGUI to display UI:
      var m_ProjectApiToken = m_ProjectSettings.FindProperty("m_ProjectApiToken");
      EditorGUILayout.PropertyField(m_ProjectApiToken, Styles.projectId);
      DoSave();
    }

    private void DoSave()
    {
      // Debug.Log("[ProjectSettingsProvider] DoSave");
      // Debug.Log($"-> {m_ProjectSettings.FindProperty("m_ProjectApiToken").stringValue}");
      m_ProjectSettings.ApplyModifiedProperties();
      AssetDatabase.SaveAssets();
    }

    // Register the SettingsProvider
    [SettingsProvider]
    public static SettingsProvider CreateProjectSettingsProvider()
    {
      // Debug.Log("[ProjectSettingsProvider] CreateProjectSettingsProvider");
      var provider = new ProjectSettingsProvider("Project/Enginn", SettingsScope.Project);

      // Automatically extract all keywords from the Styles.
      provider.keywords = new HashSet<string>(new[] { "Enginn" });
      return provider;
    }
  }

}
