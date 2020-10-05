using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Menu
  {

    [MenuItem("Enginn/Characters")]
    public static void fetchCharacters()
    {
      Character[] characters = Api.getCharacters();
      Debug.Log($"Characters count: {characters.Length}");
      foreach (Character character in characters)
      {
        Debug.Log($"Character #{character.id}: {character.name}");
      }
    }

  }

}
