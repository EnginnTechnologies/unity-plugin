using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class NewCharacterWindow : ScrollableEditorWindow
  {
    private Character character = new Character();

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

    protected override void OnGUIContent()
    {
      // NAME
      BeginCenteredFormField();
      FormLabel("Name");
      character.name = FormTextField(character.name);
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
        GUIStyle style = new GUIStyle();
        style.richText = true;
        GUILayout.Label($"<color=red>Errors: {character.GetErrorsAsJson()}</color>", style);
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

    private bool TestCanCreate()
    {
      // TODO
      return true;
    }

    void OnCreate()
    {
      if(character.Create())
      {
        Close();
      } else {
        Debug.LogError($"Character errors: {character.GetErrorsAsJson()}");
      }
    }
  }

}
