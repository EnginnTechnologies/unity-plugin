using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class NewCharacterWindow : CharacterFormWindow
  {

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public NewCharacterWindow()
    {
      titleContent = new GUIContent("Enginn - New character");
    }

    protected override void OnGUITitle()
    {
      H1("New character");
    }

    protected override string SubmitText()
    {
      return "Create";
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    protected override bool Submit()
    {
      return character.Create();
    }

  }

}
