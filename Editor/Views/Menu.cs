using System;
using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Menu
  {

    [MenuItem("Enginn/Characters/List")]
    static void ListCharacters()
    {
      // Debug.Log("[Menu] ListCharacters");
      Router.ListCharacters();
    }

    [MenuItem("Enginn/Characters/New")]
    static void NewCharacter()
    {
      // Debug.Log("[Menu] NewCharacter");
      Router.NewCharacter();
    }

    [MenuItem("Enginn/Synthesis/Wizard")]
    static void SynthesisWizard()
    {
      // Debug.Log("[Menu] SynthesisWizard");
      Router.SynthesisWizard();
    }

    [MenuItem("Enginn/Synthesis/New")]
    static void NewCharacterSynthesis()
    {
      // Debug.Log("[Menu] NewCharacterSynthesis");
      Router.NewCharacterSynthesis();
    }

    [MenuItem("Enginn/Check for updates")]
    static void UpdatePackage()
    {
      UnityEditor.PackageManager.Client.Add("https://github.com/EnginnTechnologies/unity-plugin.git");
    }

    // ------------------------------------------------------------------------
    // TESTS
    // ------------------------------------------------------------------------

    // [MenuItem("Enginn/Characters/Fetch")]
    // public static void FetchCharacters()
    // {
    //   Character[] characters = Api.GetCharacters();
    //   Debug.Log($"Characters count: {characters.Length}");
    //   foreach (Character character in characters)
    //   {
    //     Debug.Log($"Character #{character.id}: {character.name}");
    //   }
    // }

    // [MenuItem("Enginn/Test/Character creation success")]
    // public static void CreateCharacterSuccess()
    // {
    //   Character character = new Character();
    //   character.name = $"From Unity {DateTime.Now.ToString("yyyyMMddHHmmssffff")}";
    //   bool created = character.Create();
    //   Debug.Log($"Response: {created}");
    //   Debug.Log($"Character: {JsonUtility.ToJson(character)}");
    // }

    // [MenuItem("Enginn/Test/Character creation error")]
    // public static void CreateCharacterError()
    // {
    //   Character character = new Character();
    //   bool created = character.Create();
    //   Debug.Log($"Response: {created}");
    //   Debug.Log($"Character: {JsonUtility.ToJson(character)}");
    //   Debug.Log($"Character errors: {character.GetErrorsAsJson()}");
    // }

  }

}
