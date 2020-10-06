using System;
using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Menu
  {

    [MenuItem("Enginn/Characters/Fetch")]
    public static void FetchCharacters()
    {
      Character[] characters = Api.GetCharacters();
      Debug.Log($"Characters count: {characters.Length}");
      foreach (Character character in characters)
      {
        Debug.Log($"Character #{character.id}: {character.name}");
      }
    }

    [MenuItem("Enginn/Test/Character creation success")]
    public static void CreateCharacterSuccess()
    {
      Character character = new Character();
      character.name = $"From Unity {DateTime.Now.ToString("yyyyMMddHHmmssffff")}";
      bool created = character.Create();
      Debug.Log($"Response: {created}");
      Debug.Log($"Character: {JsonUtility.ToJson(character)}");
    }

    [MenuItem("Enginn/Test/Character creation error")]
    public static void CreateCharacterError()
    {
      Character character = new Character();
      bool created = character.Create();
      Debug.Log($"Response: {created}");
      Debug.Log($"Character: {JsonUtility.ToJson(character)}");
      Debug.Log($"Character errors: {character.GetErrorsAsJson()}");
    }

  }

}
