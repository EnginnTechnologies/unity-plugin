using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class EditCharacterWindow : EditorWindow
  {
    Character character = new Character();

    public void SetCharacter(Character newCharacter)
    {
      Debug.Log($"[EditCharacterWindow] SetCharacter({JsonUtility.ToJson(newCharacter)})");
      character = newCharacter;
    }

    void OnGUI()
    {
      GUILayout.Label("Edit Character", EditorStyles.boldLabel);

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

      if(GUILayout.Button("Update"))
      {
        OnUpdate();
      }
    }

    void OnUpdate()
    {
      Debug.Log("[EditCharacterWindow] OnUpdate");
      Debug.Log($"Character: {JsonUtility.ToJson(character)}");
      bool updated = character.Update();
      Debug.Log($"Response: {updated}");
      if(updated)
      {
        Debug.Log($"Character updated: {JsonUtility.ToJson(character)}");
        Close();
      } else {
        Debug.LogError($"Character errors: {character.GetErrorsAsJson()}");
      }

    }
  }

}
