using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class NewCharacterSynthesisWindow : ScrollableEditorWindow
  {

    private CharacterSynthesis characterSynthesis = new CharacterSynthesis();

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public NewCharacterSynthesisWindow()
    {
      titleContent = new GUIContent("Enginn - New synthesis");
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
      characterSynthesis.text_slug = FormTextField(characterSynthesis.text_slug);
      EndCenter();

      // CHARACTER
      BeginCenteredFormField();
      FormLabel("Character");
      characterSynthesis.character_id = FormCharacterIdField(
        characterSynthesis.character_id,
        200
      );
      EndCenter();

      // TEXT
      BeginCenteredFormField();
      FormLabel("Text");
      characterSynthesis.text = FormTextField(characterSynthesis.text);
      EndCenter();

      // MODIFIER
      BeginCenteredFormField();
      FormLabel("Modifier");
      characterSynthesis.modifier = FormModifierField(
        characterSynthesis.modifier,
        200
      );
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

    public void FetchData()
    {
      Project.RefreshCurrent();
      Project.FetchCharacters();
    }

    public void SetCharacterSynthesis(CharacterSynthesis newCharacterSynthesis)
    {
      characterSynthesis = newCharacterSynthesis;
    }

    private bool TestCanCreate()
    {
      return (
        (
          characterSynthesis.character_id > 0
        ) && (
          !String.IsNullOrEmpty(characterSynthesis.text_slug)
        ) && (
          !String.IsNullOrEmpty(characterSynthesis.text)
        )
      );
    }

    void OnCreate()
    {
      if(characterSynthesis.Create())
      {
        if(characterSynthesis.DownloadResultFile())
        {
          AssetDatabase.Refresh();
          Close();
        } else {
          Debug.LogError("result file couldn't be downloaded");
        }
      } else {
        Debug.LogError($"CharacterSynthesis errors: {characterSynthesis.GetErrorsAsJson()}");
      }
    }

  }

}
