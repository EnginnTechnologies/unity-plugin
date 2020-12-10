using System.Collections.Generic;
// using System.Drawing;
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
        100,
        100,
        100,
        100,
        200,
        100,
        60,
        100
      };

      TableHeaderRow(headers, widths);

      if (texts == null)
      {
        Debug.Log("texts is null");
      } else {
        foreach (Text text in texts)
        {
          BeginTableRow();

          TableBodyCell(text.slug, widths[0]);

          GUIStyle cellStyle = new GUIStyle();
          if (text.color_id > 0)
          {
            cellStyle.normal.background = MakeTexture(
              widths[1],
              1, // height
              text.GetColor()
            );
            cellStyle.normal.textColor = text.GetTextColor();
          } else {
            cellStyle.normal.textColor = Color.white;
          }
          cellStyle.fixedWidth = widths[1];
          cellStyle.richText = true;
          cellStyle.fontSize = 12;
          cellStyle.clipping = TextClipping.Clip;
          cellStyle.alignment = TextAnchor.MiddleLeft;
          cellStyle.padding = new RectOffset(10, 10, 2, 2); // left, right, top, bottom
          EditorGUILayout.LabelField(
            text.color_name,
            cellStyle,
            GUILayout.Width(widths[1])
          );

          TableBodyCell(text.character_syntheses_count.ToString(), widths[2]);
          TableBodyCell(text.main_character_name, widths[3]);
          TableBodyCell(text.main_synthesis_text, widths[4]);
          TableBodyCell(text.GetMainSynthesisModifierName(), widths[5]);

          GUILayout.BeginVertical(GUILayout.Width(widths[6]));
          BeginCenter();
          if (text.ResultFileExists())
          {
            if (GUILayout.Button("▶", GUILayout.ExpandWidth(false)))
            {
              PlayMain(text);
            }
          } else {
            if (GUILayout.Button("⇩", GUILayout.ExpandWidth(false)))
            {
              DownloadMain(text);
            }
          }
          EndCenter();
          EditorGUILayout.EndVertical();

          GUILayout.BeginVertical(GUILayout.Width(widths[7]));

          EditorGUILayout.EndVertical();

          EndTableRow();
        }
      }

      EditorGUILayout.EndVertical();

      EndCenter();
    }

    private void PlayMain(Text text)
    {
      PlayLocalAudio(text.GetMainResultFileAbsolutePath());
    }

    private void DownloadMain(Text text)
    {
      text.DownloadMainResultFile();
      AssetDatabase.Refresh();
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
