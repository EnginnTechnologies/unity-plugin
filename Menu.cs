using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Menu
  {

    [MenuItem("Enginn/Characters")]
    public static void FetchCharacters()
    {
      Character[] characters = Api.GetCharacters();
      Debug.Log($"Characters count: {characters.Length}");
      foreach (Character character in characters)
      {
        Debug.Log($"Character #{character.id}: {character.name}");
      }
    }

  }

}
