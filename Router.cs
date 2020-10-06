using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Router
  {

    public static void ListCharacters()
    {
      // Debug.Log("[Router] ListCharacters");

      // Get existing open window or if none, make a new one
      ListCharactersWindow window = (ListCharactersWindow)EditorWindow.GetWindow(
        typeof(ListCharactersWindow)
      );
      window.FetchCharacters();
      window.Show();
    }

    public static void NewCharacter()
    {
      // Debug.Log("[Router] NewCharacter");

      // Get existing open window or if none, make a new one
      CreateCharacterWindow window = (CreateCharacterWindow)EditorWindow.GetWindow(
        typeof(CreateCharacterWindow)
      );
      window.Show();
    }

    public static void EditCharacter(Character character)
    {

      // Get existing open window or if none, make a new one
      EditCharacterWindow window = (EditCharacterWindow)EditorWindow.GetWindow(
        typeof(EditCharacterWindow)
      );
      Character clone = character.ShallowCopy();
      window.SetCharacter(clone);
      window.Show();
    }

  }

}
