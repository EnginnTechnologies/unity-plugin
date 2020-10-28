using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class NewCharacterSynthesisWindow : EnginnEditorWindow
  {

    private Character[] characters;
    private string[] characterNames;
    private int characterIndex = -1;

    private CharacterSynthesis characterSynthesis = new CharacterSynthesis();
    private int modifierIndex = 0;

    public NewCharacterSynthesisWindow()
    {
      // Debug.Log("[NewCharacterSynthesisWindow] constructor");
      minSize = new Vector2(1000f, 400f);
      titleContent = new GUIContent("Enginn speech synthesis");
    }

    void OnGUI()
    {
      H1("Enginn speech synthesis");
      P("Please choose a character and specify a text to perform the synthesis.");
      OnGUIForm();
      OnGUIButtons();
    }

    void OnGUIForm()
    {
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);

      // CHARACTER
      GUILayout.BeginHorizontal();
      GUILayout.FlexibleSpace();
      SetCharacterIndex(GUILayout.SelectionGrid(characterIndex, characterNames, 1, radioStyle));
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();

      // TEXT
      GUILayout.BeginHorizontal();
      GUILayout.FlexibleSpace();
      characterSynthesis.text = EditorGUILayout.TextField("Text", characterSynthesis.text, GUILayout.Width(400));
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();

      // MODIFIER
      GUILayout.BeginHorizontal();
      GUILayout.FlexibleSpace();
      SetModifierIndex(GUILayout.SelectionGrid(modifierIndex, Synthesis.ModifierNames, 1, radioStyle));
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    void OnGUIButtons()
    {
      GUIStyle style = new GUIStyle();
      style.padding = new RectOffset(0, 0, 30, 30); // left, right, top, bottom
      GUILayout.BeginHorizontal(style);
      GUILayout.FlexibleSpace();

      if(GUILayout.Button("Cancel", GUILayout.Width(100)))
      {
        Close();
      }

      GUILayout.Space(50);

      GUI.enabled = TestCanCreate();
      if(GUILayout.Button("Create", GUILayout.Width(100)))
      {
        OnCreate();
      }
      GUI.enabled = true;

      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    public void FetchCharacters()
    {
      characters = Api.GetCharacters();
      characterNames = characters.Select(c => c.name).ToArray();
    }

    private bool TestCanCreate()
    {
      return (
        (
          characterSynthesis.character_id > 0
        ) && (
          characterSynthesis.text.Length > 0
        )
      );
    }

    void OnCreate()
    {
      // Debug.Log("[NewCharacterSynthesisWindow] Create");
      // Debug.Log($"CharacterSynthesis: {JsonUtility.ToJson(characterSynthesis)}");
      bool created = characterSynthesis.Create();
      // Debug.Log($"Response: {created}");
      if(created)
      {
        if(characterSynthesis.DownloadResultFile())
        {
          Close();
        } else {
          Debug.LogError("result file couldn't be downloaded");
        }
      } else {
        Debug.LogError($"CharacterSynthesis errors: {characterSynthesis.GetErrorsAsJson()}");
      }
    }

    private void SetCharacterIndex(int newCharacterIndex)
    {
      // Debug.Log($"[NewCharacterSynthesisWindow] SetCharacterIndex({newCharacterIndex})");
      characterIndex = newCharacterIndex;
      if(newCharacterIndex >= 0)
      {
        characterSynthesis.character_id = characters[characterIndex].id;
      } else {
        characterSynthesis.character_id = 0;
      }
    }

    private void SetModifierIndex(int newModifierIndex)
    {
      modifierIndex = newModifierIndex;
      characterSynthesis.modifier = Synthesis.Modifiers[modifierIndex];
    }
  }

}
