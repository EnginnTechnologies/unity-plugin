using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class ListCharactersWindow : EditorWindow
  {
    Character[] characters;

    public void FetchCharacters()
    {
      // Debug.Log("[ListCharactersWindow] FetchCharacters");
      characters = Api.GetCharacters();
      foreach (Character character in characters)
      {
        character.DownloadAvatar();
      }
    }

    void OnGUI()
    {
      // Debug.Log("[ListCharactersWindow] OnGUI");
      GUILayout.Label("My Characters", EditorStyles.boldLabel);

      GUIStyle style = new GUIStyle();
      style.richText = true;
      foreach (Character character in characters)
      {
        Texture2D avatar = character.GetAvatar();
        if (avatar != null)
        {
          Rect rect = EditorGUILayout.GetControlRect(false, 200);
          GUI.DrawTexture(rect, avatar, ScaleMode.ScaleToFit);
        }

        GUILayout.Label($"<b>{character.name}</b> #{character.id}", style);

        if(GUILayout.Button("Edit"))
        {
          Router.EditCharacter(character);
        }

        if(GUILayout.Button("Delete"))
        {
          Router.DestroyCharacter(character);
        }
      }

      if(GUILayout.Button("Refresh"))
      {
        FetchCharacters();
      }

      if(GUILayout.Button("Close"))
      {
        Close();
      }

      if(GUILayout.Button("New"))
      {
        Router.NewCharacter();
      }
    }
  }

}
