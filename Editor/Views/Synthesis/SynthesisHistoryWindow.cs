using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class SynthesisHistoryWindow : ScrollableEditorWindow
  {
    CharacterSynthesis[] characterSyntheses = {};
    Text filterText;

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public SynthesisHistoryWindow()
    {
      titleContent = new GUIContent("Enginn - Synthesis history");
    }

    protected override void OnGUITitle()
    {
      H1("Synthesis history");
    }

    protected override void OnGUIContent()
    {

      if (filterText != null)
      {
        BeginCenter();
        P($"For text: {filterText.slug}");
        EndCenter();
        GUILayout.Space(30);
      }

      BeginCenter();

      EditorGUILayout.BeginVertical();

      List<string> headers = new List<string>()
      {
        "Date",
        "Slug",
        "★",
        "Character",
        "Text",
        "Modifier",
        "Result"
      };
      List<int> widths = new List<int>()
      {
        150,
        100,
        30,
        200,
        200,
        100,
        60
      };

      TableHeaderRow(headers, widths);

      if (characterSyntheses == null)
      {
        Debug.Log("No history yet");
      } else {
        foreach (CharacterSynthesis characterSynthesis in characterSyntheses)
        {
          BeginTableRow();

          TableBodyCell(
            FormatDate(characterSynthesis.created_at),
            widths[0]
          );
          TableBodyCell(characterSynthesis.text_slug, widths[1]);
          TableBodyCell(
            characterSynthesis.is_main ? "★" : "",
            widths[2]
          );
          TableBodyCell(characterSynthesis.character_name, widths[3]);
          TableBodyCell(characterSynthesis.synthesis_text, widths[4]);
          TableBodyCell(characterSynthesis.GetSynthesisModifierName(), widths[5]);

          GUILayout.BeginVertical(GUILayout.Width(widths[6]));
          BeginCenter();
          if (characterSynthesis.is_main)
          {
            if (characterSynthesis.MainResultFileExists())
            {
              if (GUILayout.Button("▶", GUILayout.ExpandWidth(false)))
              {
                PlayMain(characterSynthesis);
              }
            } else {
              if (GUILayout.Button("▶", GUILayout.ExpandWidth(false)))
              {
                PlayOther(characterSynthesis);
              }
            }
          } else {
            if (GUILayout.Button("▶", GUILayout.ExpandWidth(false)))
            {
              PlayOther(characterSynthesis);
            }
          }
          EndCenter();
          EditorGUILayout.EndVertical();

          EndTableRow();
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
        FetchData();
      }
    }

    protected void PlayMain(CharacterSynthesis characterSynthesis)
    {
      PlayLocalAudio(characterSynthesis.GetResultFileAbsolutePath());
    }

    protected void PlayOther(CharacterSynthesis characterSynthesis)
    {
      PlayRemoteAudio(characterSynthesis.synthesis_result_file_url);
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void SetFilterText(Text text)
    {
      filterText = text;
    }

    public void FetchData()
    {
      characterSyntheses = Api.GetCharacterSyntheses(filterText);
    }

  }

}
