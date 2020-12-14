using UnityEditor;
using UnityEngine;

namespace Enginn
{

  public class Router
  {

    public static void ListCharacters()
    {
      ListCharactersWindow window = (ListCharactersWindow)EditorWindow.GetWindow(
        typeof(ListCharactersWindow)
      );
      window.FetchCharacters();
      window.Show();
    }

    public static void NewCharacter()
    {
      NewCharacterWindow window = (NewCharacterWindow)EditorWindow.GetWindow(
        typeof(NewCharacterWindow)
      );
      window.Show();
    }

    public static void EditCharacter(Character character)
    {
      EditCharacterWindow window = (EditCharacterWindow)EditorWindow.GetWindow(
        typeof(EditCharacterWindow)
      );
      Character clone = character.ShallowCopy();
      window.SetCharacter(clone);
      window.Show();
    }

    public static void DestroyCharacter(Character character)
    {
      DestroyCharacterWindow window = (DestroyCharacterWindow)EditorWindow.GetWindow(
        typeof(DestroyCharacterWindow)
      );
      window.SetCharacter(character);
      window.Show();
    }

    public static void SynthesisWizard()
    {
      SynthesisWizardWindow window = (SynthesisWizardWindow)EditorWindow.GetWindow(
        typeof(SynthesisWizardWindow)
      );
      window.Show();
    }

    public static void NewCharacterSynthesis(CharacterSynthesis model = null)
    {
      NewCharacterSynthesisWindow window = (NewCharacterSynthesisWindow)EditorWindow.GetWindow(
        typeof(NewCharacterSynthesisWindow)
      );
      window.FetchData();
      if (model != null)
      {
        window.SetCharacterSynthesis(model);
      }
      window.Show();
    }

    public static void SynthesisHistory()
    {
      SynthesisHistoryWindow window = (SynthesisHistoryWindow)EditorWindow.GetWindow(
        typeof(SynthesisHistoryWindow)
      );
      window.Show();
    }

    public static void ListTexts()
    {
      ListTextsWindow window = (ListTextsWindow)EditorWindow.GetWindow(
        typeof(ListTextsWindow)
      );
      window.FetchTexts();
      window.Show();
    }

  }

}
