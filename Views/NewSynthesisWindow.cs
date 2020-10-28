using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class NewSynthesisWindow : EnginnEditorWindow
  {

    private Character character = new Character();
    private string text = "";
    private Modifier modifier;

    enum Modifier
    {
      None = 0,
      Phone = 1,
      BadSpeaker = 2
    }

    public NewSynthesisWindow()
    {
      Debug.Log("[NewSynthesisWindow] constructor");
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
      // TEXT
      GUILayout.BeginHorizontal();
      GUILayout.FlexibleSpace();
      text = EditorGUILayout.TextField("Text", text, GUILayout.Width(400));
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();

      // MODIFIER
      GUILayout.BeginHorizontal();
      GUILayout.FlexibleSpace();
      string[] modifierNames = {"None", "Phone", "Bad speaker"};
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);
      modifier = (Modifier)GUILayout.SelectionGrid((int)modifier, modifierNames, 1, radioStyle);
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

      GUI.enabled = TestCanSubmit();
      if(GUILayout.Button("Submit", GUILayout.Width(100)))
      {
        Submit();
      }
      GUI.enabled = true;

      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    private bool TestCanSubmit()
    {
      return (
        (
          character.id > 0
        ) && (
          text.Length > 0
        )
      );
    }

    private void Submit()
    {
      Debug.Log("[NewSynthesisWindow] submit");
    }
  }

}
