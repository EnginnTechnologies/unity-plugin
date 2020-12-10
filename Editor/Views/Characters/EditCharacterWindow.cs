using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class EditCharacterWindow : CharacterFormWindow
  {

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public EditCharacterWindow()
    {
      titleContent = new GUIContent("Enginn - Edit character");
    }

    protected override void OnGUITitle()
    {
      H1("Edit character");
    }

    protected override string SubmitText()
    {
      return "Update";
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void SetCharacter(Character newCharacter)
    {
      character = newCharacter;
      int idx = -1;
      foreach (string gender in Character.Genders)
      {
        idx++;
        if(gender == character.gender)
        {
          genderIndex = idx;
        }
      }
      idx = -1;
      foreach (string age in Character.Ages)
      {
        idx++;
        if(age == character.age)
        {
          ageIndex = idx;
        }
      }
      idx = -1;
      foreach (string pitch in Character.Pitches)
      {
        idx++;
        if(pitch == character.pitch)
        {
          pitchIndex = idx;
        }
      }
    }

    protected override bool Submit()
    {
      return character.Update();
    }

  }

}
