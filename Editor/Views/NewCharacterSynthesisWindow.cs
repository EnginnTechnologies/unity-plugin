using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class NewCharacterSynthesisWindow : ScrollableEditorWindow
  {

    private Character[] characters;
    private string[] characterNames;
    private int characterIndex = -1;

    private CharacterSynthesis characterSynthesis = new CharacterSynthesis();
    private int modifierIndex = 0;

    private string fileName = "";

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public NewCharacterSynthesisWindow()
    {
      titleContent = new GUIContent("Enginn - New synthesis");
      fileName = DateTime.Now.ToString("yyyy-MM-dd_h-mm");
    }

    protected override void OnGUITitle()
    {
      H1("Enginn speech synthesis");
    }

    protected override void OnGUIContent()
    {
      P("Please choose a character and specify a text to perform the synthesis.");

      GUILayout.Space(50);

      // SLUG
      BeginCenteredFormField();
      FormLabel("Slug");
      characterSynthesis.slug = FormTextField(characterSynthesis.slug);
      EndCenter();

      // CHARACTER
      BeginCenteredFormField();
      FormLabel("Character");
      SetCharacterIndex(FormRadio(characterIndex, characterNames));
      EndCenter();

      // TEXT
      BeginCenteredFormField();
      FormLabel("Text");
      characterSynthesis.text = FormTextField(characterSynthesis.text);
      EndCenter();

      // MODIFIER
      BeginCenteredFormField();
      FormLabel("Modifier");
      SetModifierIndex(FormRadio(modifierIndex, Synthesis.ModifierNames));
      EndCenter();

      // FILE NAME
      BeginCenteredFormField();
      FormLabel("File name");
      fileName = FormTextField(fileName);
      EndCenter();

      // ERRORS
      BeginCenteredFormField();
      Dictionary<string, List<string>> errors = characterSynthesis.GetErrors();
      if(errors.Count > 0) {
        FormLabel($"<color=red>Errors</color>");
        FormErrors(errors);
      }
      EndCenter();
    }

    protected override void OnGUIButtons()
    {
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
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

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
          !String.IsNullOrEmpty(characterSynthesis.slug)
        ) && (
          !String.IsNullOrEmpty(characterSynthesis.text)
        ) && (
          !String.IsNullOrEmpty(fileName)
        )
      );
    }

    void OnCreate()
    {
      if(characterSynthesis.Create())
      {
        if(characterSynthesis.DownloadResultFile(fileName))
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
