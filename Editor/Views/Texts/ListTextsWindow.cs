using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class ListTextsWindow : ScrollableEditorWindow
  {
    Text[] texts = {};

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public ListTextsWindow()
    {
      titleContent = new GUIContent("Enginn - Project texts");
    }

    protected override void OnGUITitle()
    {
      H1("Project texts");
    }

    protected override void OnGUIContent()
    {

      BeginCenter();

      EditorGUILayout.BeginVertical();

      List<string> headers = new List<string>()
      {
        "Slug",
        "Color",
        "Syntheses",
        "Character",
        "Text",
        "Modifier",
        "Result",
        "Actions"
      };
      List<int> widths = new List<int>()
      {
        0,
        0,
        0,
        100,
        200,
        50,
        100,
        50
      };

      TableHeaderRow(headers, widths);

      if (texts == null)
      {
        Debug.Log("texts is null");
      } else {
        foreach (Text text in texts)
        {
          List<string> values = new List<string>(){
            text.slug,
            text.color_name,
            text.character_syntheses_count.ToString(),
            text.main_character_name,
            text.main_synthesis_text,
            text.GetMainSynthesisModifierName(),
            "",
            ""
          };
          TableBodyRow(values, widths);
        }
      }

      EditorGUILayout.EndVertical();

      EndCenter();
    }

    protected override void OnGUIButtons()
    {
      if(GUILayout.Button("Close", GUILayout.Width(100)))
      {
        Close();
      }

      GUILayout.Space(50);

      if(GUILayout.Button("Refresh", GUILayout.Width(100)))
      {
        FetchTexts();
      }
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void FetchTexts()
    {
      texts = Api.GetTexts();
    }
  }

}
