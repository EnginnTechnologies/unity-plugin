using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class CharacterFormWindow : ScrollableEditorWindow
  {
    protected Character character = new Character();
    protected int genderIndex = -1;
    protected int ageIndex = -1;
    protected int pitchIndex = -1;
    private List<string> testTexts = new List<string>(){""};
    private int textBeingTested = -1;

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    protected override void OnGUIContent()
    {
      H2("Character attributes");

      BeginColumns();

      // COLUMN 1
      BeginColumn(400);

      // NAME
      BeginCenteredFormField();
      FormLabel("Name", 150);
      character.name = FormTextField(character.name, 250);
      EndCenter();

      // AVATAR
      BeginCenteredFormField();
      FormLabel("Avatar", 150);
      character.SetAvatarTexture(TextureField(character.GetAvatarTexture(), 250));
      EndCenter();

      EndColumn();
      // END COLUMN 1

      // COLUMN 2
      BeginColumn(300);

      // GENDER
      BeginCenteredFormField();
      FormLabel("Gender", 150);
      SetGenderIndex(FormRadio(genderIndex, Character.GenderNames, 150));
      EndCenter();

      // AGE
      BeginCenteredFormField();
      FormLabel("Age", 150);
      SetAgeIndex(FormRadio(ageIndex, Character.AgeNames, 150));
      EndCenter();

      // PITCH
      BeginCenteredFormField();
      FormLabel("Pitch", 150);
      SetPitchIndex(FormRadio(pitchIndex, Character.PitchNames, 150));
      EndCenter();

      // IS NASAL
      BeginCenteredFormField();
      FormLabel("Has a nasal voice", 150);
      character.is_nasal = FormToggle(character.is_nasal, 150);
      EndCenter();

      EndColumn();
      // END COLUMN 1

      EndColumns();

      H2("Voice preview");

      // TEST
      BeginCenteredFormField();
      // FormLabel("Test texts", 150);
      EditorGUILayout.BeginVertical();
      for (int idx = 0; idx < testTexts.Count; idx++)
      {
        GUILayout.BeginHorizontal();
        FormLabel($"Text {idx + 1}", 150);
        testTexts[idx] = FormTextField(testTexts[idx], 250);
        GUILayout.Space(10);
        GUI.enabled = TestCanTest();
        if (GUILayout.Button("▶"))
        {
          TestText(idx);
        }
        GUILayout.Space(10);
        GUI.enabled = textBeingTested != idx;
        if (GUILayout.Button("-"))
        {
          testTexts.RemoveAt(idx);
        }
        GUI.enabled = true;
        GUILayout.EndHorizontal();
      }
      GUILayout.BeginHorizontal();
      FormLabel("", 150);
      if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
      {
        testTexts.Add("");
      }
      GUILayout.EndHorizontal();
      EditorGUILayout.EndVertical();
      EndCenter();

      // ERRORS
      BeginCenteredFormField();
      Dictionary<string, List<string>> errors = character.GetErrors();
      if(errors.Count > 0) {
        FormLabel($"<color=red>Errors</color>");
        FormErrors(errors);
      }
      EndCenter();
    }

    protected override void OnGUIButtons()
    {
      if (GUILayout.Button("Cancel", ButtonStyle(100)))
      {
        Router.ListCharacters();
        Close();
      }

      GUILayout.Space(50);

      GUI.enabled = TestCanSubmit();
      if (GUILayout.Button(SubmitText(), ButtonStyle(100)))
      {
        OnSubmit();
      }
      GUI.enabled = true;
    }

    protected virtual string SubmitText()
    {
      return "Submit";
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    private bool TestCanTest()
    {
      return (
        (
          textBeingTested < 0
        ) && (
          genderIndex >= 0
        ) && (
          ageIndex >= 0
        ) && (
          pitchIndex >= 0
        )
      );
    }

    private bool TestCanSubmit()
    {
      return (
        (
          textBeingTested < 0
        ) && (
          !String.IsNullOrEmpty(character.name)
        ) && (
          genderIndex >= 0
        ) && (
          ageIndex >= 0
        ) && (
          pitchIndex >= 0
        )
      );
    }

    private void SetGenderIndex(int newGenderIndex)
    {
      genderIndex = newGenderIndex;
      if(genderIndex >= 0)
      {
        character.gender = Character.Genders[genderIndex];
      } else {
        character.gender = null;
      }
    }

    private void SetAgeIndex(int newAgeIndex)
    {
      ageIndex = newAgeIndex;
      if(ageIndex >= 0)
      {
        character.age = Character.Ages[ageIndex];
      } else {
        character.age = null;
      }
    }

    private void SetPitchIndex(int newPitchIndex)
    {
      pitchIndex = newPitchIndex;
      if(pitchIndex >= 0)
      {
        character.pitch = Character.Pitches[pitchIndex];
      } else {
        character.pitch = null;
      }
    }

    private void OnSubmit()
    {
      if(Submit())
      {
        Router.ListCharacters();
        Close();
      } else {
        Debug.LogError($"Character errors: {character.GetErrorsAsJson()}");
      }
    }

    protected virtual bool Submit()
    {
      return true;
    }

    private void TestText(int textIndex)
    {
      if (textBeingTested >= 0)
      {
        return;
      }

      textBeingTested = textIndex;
      string text = testTexts[textIndex];
      TestSynthesis testSynthesis = new TestSynthesis(character, text);

      try
      {
        if (testSynthesis.Create())
        {
          GetAudioPlayer().Stream(testSynthesis.synthesis_result_file_url);
        } else {
          Debug.LogError($"Synthesis errors: {testSynthesis.GetErrorsAsJson()}");
        }
      } finally {
        // in all cases, allow to try again or test another text
        textBeingTested = -1;
      }
    }
  }

}
