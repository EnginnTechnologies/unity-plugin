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
      NewCharacterWindow window = (NewCharacterWindow)EditorWindow.GetWindow(
        typeof(NewCharacterWindow)
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

    public static void DestroyCharacter(Character character)
    {

      // Get existing open window or if none, make a new one
      DestroyCharacterWindow window = (DestroyCharacterWindow)EditorWindow.GetWindow(
        typeof(DestroyCharacterWindow)
      );
      window.SetCharacter(character);
      window.Show();
    }

    public static void SynthesisWizard()
    {

      // Get existing open window or if none, make a new one
      SynthesisWizardWindow window = (SynthesisWizardWindow)EditorWindow.GetWindow(
        typeof(SynthesisWizardWindow)
      );
      window.Show();
    }

    public static void NewCharacterSynthesis()
    {

      // Get existing open window or if none, make a new one
      NewCharacterSynthesisWindow window = (NewCharacterSynthesisWindow)EditorWindow.GetWindow(
        typeof(NewCharacterSynthesisWindow)
      );
      window.FetchCharacters();
      window.Show();
    }

    public static void SynthesisHistory()
    {

      // Get existing open window or if none, make a new one
      SynthesisHistoryWindow window = (SynthesisHistoryWindow)EditorWindow.GetWindow(
        typeof(SynthesisHistoryWindow)
      );
      window.Show();
    }

  }

}
