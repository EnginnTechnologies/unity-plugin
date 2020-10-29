using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class DestroyCharacterWindow : EditorWindow
  {
    Character character = new Character();

    public void SetCharacter(Character newCharacter)
    {
      Debug.Log($"[DestroyCharacterWindow] SetCharacter({JsonUtility.ToJson(newCharacter)})");
      character = newCharacter;
    }

    void OnGUI()
    {
      GUILayout.Label("Destroy Character", EditorStyles.boldLabel);

      GUIStyle style = new GUIStyle();
      style.richText = true;
      GUILayout.Label($"<b>{character.name}</b> #{character.id}", style);

      GUILayout.Label("Are you sure?");

      Dictionary<string, List<string>> errors = character.GetErrors();
      if(errors.Count > 0) {
        GUILayout.Label($"<color=red>Errors: {character.GetErrorsAsJson()}</color>", style);
      }

      if(GUILayout.Button("Cancel"))
      {
        Close();
      }

      if(GUILayout.Button("Destroy"))
      {
        OnDelete();
      }
    }

    void OnDelete()
    {
      Debug.Log("[DestroyCharacterWindow] OnDelete");
      bool destroyed = character.Destroy();
      Debug.Log($"Response: {destroyed}");
      if(destroyed)
      {
        Debug.Log($"Character destroyed");
        Close();
      } else {
        Debug.LogError($"Character errors: {character.GetErrorsAsJson()}");
      }

    }
  }

}
