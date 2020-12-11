using System;
using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Menu
  {

    // ------------------------------------------------------------------------
    // CHARACTERS
    // ------------------------------------------------------------------------

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

    // ------------------------------------------------------------------------
    // SYNTHESIS
    // ------------------------------------------------------------------------

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

    // ------------------------------------------------------------------------
    // TEXTS
    // ------------------------------------------------------------------------

    [MenuItem("Enginn/Texts Manager")]
    static void ListTexts()
    {
      // Debug.Log("[Menu] ListTexts");
      Router.ListTexts();
    }

    // ------------------------------------------------------------------------
    // OTHERS
    // ------------------------------------------------------------------------

    [MenuItem("Enginn/Check for updates")]
    static void UpdatePackage()
    {
      UnityEditor.PackageManager.Client.Add("https://github.com/EnginnTechnologies/unity-plugin.git");
    }

  }

}
