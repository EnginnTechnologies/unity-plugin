using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class DestroyCharacterWindow : ScrollableEditorWindow
  {
    Character character = new Character();

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public DestroyCharacterWindow()
    {
      titleContent = new GUIContent("Enginn - Delete character");
    }

    protected override void OnGUITitle()
    {
      H1("Delete character");
    }

    protected override void OnGUIContent()
    {

      // AVATAR

      Texture2D avatar = character.GetAvatarTexture();
      if (avatar != null)
      {
        BeginCenter();
        Rect rect = EditorGUILayout.GetControlRect(false, 200); // hasLabel, height
        GUI.DrawTexture(rect, avatar, ScaleMode.ScaleToFit);
        EndCenter();
      }

      // NAME

      P($"<b>{character.name}</b> (#{character.id})");

      // CONFIRMATION

      GUILayout.Space(50);
      P("Are you sure?");

      Dictionary<string, List<string>> errors = character.GetErrors();
      if(errors.Count > 0) {
        P($"<color=red>Errors: {character.GetErrorsAsJson()}</color>");
      }
    }

    protected override void OnGUIButtons()
    {
      if(GUILayout.Button("No, cancel", GUILayout.Width(100)))
      {
        Router.ListCharacters();
        Close();
      }

      GUILayout.Space(50);

      if(GUILayout.Button("Yes, delete", GUILayout.Width(100)))
      {
        OnDelete();
      }
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void SetCharacter(Character newCharacter)
    {
      character = newCharacter;
    }

    void OnDelete()
    {
      if(character.Destroy())
      {
        Router.ListCharacters();
        Close();
      } else {
        Debug.LogError($"Character errors: {character.GetErrorsAsJson()}");
      }
    }

  }

}
