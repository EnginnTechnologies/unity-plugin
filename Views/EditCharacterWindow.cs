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

    private static Texture2D TextureField(string name, Texture2D texture)
    {
      GUILayout.BeginVertical();
      var style = new GUIStyle(GUI.skin.label);
      style.alignment = TextAnchor.UpperCenter;
      style.fixedWidth = 70;
      GUILayout.Label(name, style);
      var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
      GUILayout.EndVertical();
      return result;
    }

    void OnGUI()
    {
      GUILayout.Label("Edit Character", EditorStyles.boldLabel);

      character.name = EditorGUILayout.TextField("Name", character.name);

      character.avatar = TextureField("Avatar", character.avatar);

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
