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

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    protected override void OnGUIContent()
    {
      // NAME
      BeginCenteredFormField();
      FormLabel("Name");
      character.name = FormTextField(character.name);
      EndCenter();

      // GENDER
      BeginCenteredFormField();
      FormLabel("Gender");
      SetGenderIndex(FormRadio(genderIndex, Character.GenderNames));
      EndCenter();

      // AGE
      BeginCenteredFormField();
      FormLabel("Age");
      SetAgeIndex(FormRadio(ageIndex, Character.AgeNames));
      EndCenter();

      // PITCH
      BeginCenteredFormField();
      FormLabel("Pitch");
      SetPitchIndex(FormRadio(pitchIndex, Character.PitchNames));
      EndCenter();

      // IS NASAL
      BeginCenteredFormField();
      FormLabel("Has a nasal voice");
      character.is_nasal = FormToggle(character.is_nasal);
      EndCenter();

      // AVATAR
      BeginCenteredFormField();
      FormLabel("Avatar");
      character.SetAvatarTexture(TextureField(character.GetAvatarTexture()));
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
      if(GUILayout.Button("Cancel", GUILayout.Width(100)))
      {
        Router.ListCharacters();
        Close();
      }

      GUILayout.Space(50);

      GUI.enabled = TestCanSubmit();
      if(GUILayout.Button(SubmitText(), GUILayout.Width(100)))
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

    private bool TestCanSubmit()
    {
      return (
        (
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
  }

}
