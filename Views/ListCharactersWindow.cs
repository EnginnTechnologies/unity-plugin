using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class ListCharactersWindow : EditorWindow
  {
    Character[] characters;

    public void FetchCharacters()
    {
      Debug.Log("[ListCharactersWindow] FetchCharacters");
      characters = Api.GetCharacters();
    }

    void OnGUI()
    {
      Debug.Log("[ListCharactersWindow] OnGUI");
      GUILayout.Label("My Characters", EditorStyles.boldLabel);

      GUIStyle style = new GUIStyle();
      style.richText = true;
      foreach (Character character in characters)
      {
        GUILayout.Label($"<b>{character.name}</b> #{character.id}", style);
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
