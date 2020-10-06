using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class CreateCharacterWindow : EditorWindow
  {
    Character character = new Character();

    // Add menu named "My Window" to the Window menu
    [MenuItem("Enginn/Characters/New")]
    static void Init()
    {
      // Get existing open window or if none, make a new one:
      CreateCharacterWindow window = (CreateCharacterWindow)EditorWindow.GetWindow(
        typeof(CreateCharacterWindow)
      );
      window.Show();
    }

    void OnGUI()
    {
      GUILayout.Label("New Character", EditorStyles.boldLabel);

      character.name = EditorGUILayout.TextField("Name", character.name);

      Dictionary<string, List<string>> errors = character.GetErrors();
      if(errors.Count > 0) {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        GUILayout.Label($"<color=red>Errors: {character.GetErrorsAsJson()}</color>", style);
      }

      if(GUILayout.Button("Cancel"))
      {
        Close();
      }

      if(GUILayout.Button("Create"))
      {
        Debug.Log("[CreateCharacterWindow] Clicked create button");
        OnCreate();
      }
    }

    void OnCreate()
    {
      Debug.Log("[CreateCharacterWindow] Create");
      Debug.Log($"Character: {JsonUtility.ToJson(character)}");
      bool created = character.Create();
      Debug.Log($"Response: {created}");
      if(created)
      {
        Debug.Log($"Character created: {JsonUtility.ToJson(character)}");
        Close();
      } else {
        Debug.LogError($"Character errors: {character.GetErrorsAsJson()}");
      }

    }
  }

}
