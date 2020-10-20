using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class CreateCharacterWindow : EnginnEditorWindow
  {
    Character character = new Character();

    void OnGUI()
    {
      GUILayout.Label("New Character", EditorStyles.boldLabel);

      character.name = EditorGUILayout.TextField("Name", character.name);

      character.SetAvatarTexture(TextureField("Avatar", character.GetAvatarTexture()));

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
