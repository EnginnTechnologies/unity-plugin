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
      NewCharacterSynthesisWindow window = new NewCharacterSynthesisWindow();
      window.FetchData();
      if (model != null)
      {
        window.SetCharacterSynthesis(model);
      }
      window.Show();
    }

    public static void SynthesisHistory(Text filterText = null)
    {
      SynthesisHistoryWindow window = (SynthesisHistoryWindow)EditorWindow.GetWindow(
        typeof(SynthesisHistoryWindow)
      );
      if (filterText != null)
      {
        window.SetFilterText(filterText);
      }
      window.FetchData();
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

    public static void ShowText(Text text)
    {
      ShowTextWindow window = new ShowTextWindow(text.id);
      window.FetchData();
      window.Show();
    }

  }

}
