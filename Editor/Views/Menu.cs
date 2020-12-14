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
      Router.ListCharacters();
    }

    [MenuItem("Enginn/Characters/New")]
    static void NewCharacter()
    {
      Router.NewCharacter();
    }

    // ------------------------------------------------------------------------
    // SYNTHESIS
    // ------------------------------------------------------------------------

    [MenuItem("Enginn/Synthesis/Wizard")]
    static void SynthesisWizard()
    {
      Router.SynthesisWizard();
    }

    [MenuItem("Enginn/Synthesis/New")]
    static void NewCharacterSynthesis()
    {
      Router.NewCharacterSynthesis();
    }

    [MenuItem("Enginn/Synthesis/History")]
    static void SynthesisHistory()
    {
      Router.SynthesisHistory();
    }

    // ------------------------------------------------------------------------
    // TEXTS
    // ------------------------------------------------------------------------

    [MenuItem("Enginn/Texts Manager")]
    static void ListTexts()
    {
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
