using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class CharacterFormWindow : ScrollableEditorWindow
  {
    protected Character character = new Character();
    protected int genderIndex = -1;

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
      // TODO
      return true;
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

    private void OnSubmit()
    {
      if(Submit())
      {
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
