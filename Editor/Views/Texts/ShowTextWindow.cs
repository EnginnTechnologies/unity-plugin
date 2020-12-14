using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// using System.IO;
// using System.Net;
// using System.Text;

namespace Enginn
{

  public class ShowTextWindow : ScrollableEditorWindow
  {
    private const int LATEST_CHARACTER_SYNTHESES_COUNT = 10;

    int text_id;
    Text text;
    CharacterSynthesis mainCharacterSynthesis;
    CharacterSynthesis[] characterSyntheses;

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public ShowTextWindow(int i_text_id)
    {
      text_id = i_text_id;
      titleContent = new GUIContent("Enginn - Text");
    }

    protected override void OnGUITitle()
    {
      H1($"Text #{text_id}");
    }

    protected override void OnGUIContent()
    {
      BeginColumns();

      // COLUMN 1

      BeginColumn(400);

      GUILayout.BeginHorizontal();
      FormLabel("Slug:", 40);
      FormLabel(text.slug, 300);
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      FormLabel("Color:", 40);
      text.SetColor(FormColorField(text.GetColor(), 150));
      GUILayout.EndHorizontal();

      EndColumn();

      // COLUMN 2
      BeginColumn(300);

      GUILayout.BeginHorizontal();
      FormLabel("Syntheses:", 80);
      FormLabel(text.character_syntheses_count.ToString(), 40);
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      if (GUILayout.Button("View all", GUILayout.ExpandWidth(false)))
      {
        ViewHistory();
      }
      GUILayout.Space(10);
      if (GUILayout.Button("Resynthesize", GUILayout.ExpandWidth(false)))
      {
        Resynthesize();
        Close();
      }
      GUILayout.EndHorizontal();

      EndColumn();

      EndColumns();

      // MAIN SYNTHESIS

      BeginCenter();
      H2("Main synthesis");
      EndCenter();

      OnGUIContentMainSynthesis();

      // OTHER SYNTHESES

      BeginCenter();
      H2($"Latest other {LATEST_CHARACTER_SYNTHESES_COUNT} syntheses");
      EndCenter();

      OnGUIContentOtherSyntheses();
    }

    private void OnGUIContentMainSynthesis()
    {
      BeginCenter();

      EditorGUILayout.BeginVertical();

      List<string> headers = new List<string>()
      {
        "Date",
        "Character",
        "Text",
        "Modifier",
        "Result"
      };
      List<int> widths = new List<int>()
      {
        150,
        200,
        200,
        100,
        60
      };

      TableHeaderRow(headers, widths);

      BeginTableRow();

      TableBodyCell(
        FormatDate(mainCharacterSynthesis.created_at),
        widths[0]
      );
      TableBodyCell(mainCharacterSynthesis.character_name, widths[1]);
      TableBodyCell(mainCharacterSynthesis.synthesis_text, widths[2]);
      TableBodyCell(mainCharacterSynthesis.GetSynthesisModifierName(), widths[3]);

      GUILayout.BeginVertical(GUILayout.Width(widths[4]));
      BeginCenter();
      if (text.ResultFileExists())
      {
        if (GUILayout.Button("▶", GUILayout.ExpandWidth(false)))
        {
          PlayMain();
        }
      } else {
        if (GUILayout.Button("⇩", GUILayout.ExpandWidth(false)))
        {
          DownloadMain();
        }
      }
      EndCenter();
      EditorGUILayout.EndVertical();

      EndTableRow();

      EditorGUILayout.EndVertical();

      EndCenter();
    }

    private void OnGUIContentOtherSyntheses()
    {
      BeginCenter();

      EditorGUILayout.BeginVertical();

      List<string> headers = new List<string>()
      {
        "Date",
        "Character",
        "Text",
        "Modifier",
        "Result",
        "★"
      };
      List<int> widths = new List<int>()
      {
        150,
        200,
        200,
        100,
        60,
        30
      };

      TableHeaderRow(headers, widths);

      if (characterSyntheses == null)
      {
        P("None yet");
      } else {
        int idx = 0;
        foreach (CharacterSynthesis characterSynthesis in characterSyntheses)
        {
          if (characterSynthesis.id == text.main_character_synthesis_id)
          {
            continue;
          }
          idx++;
          if (idx > LATEST_CHARACTER_SYNTHESES_COUNT)
          {
            // we fetched 1 more element in case the main one was among them
            // but the main element was older
            break;
          }
          BeginTableRow();

          TableBodyCell(
            FormatDate(characterSynthesis.created_at),
            widths[0]
          );
          TableBodyCell(characterSynthesis.character_name, widths[1]);
          TableBodyCell(characterSynthesis.synthesis_text, widths[2]);
          TableBodyCell(characterSynthesis.GetSynthesisModifierName(), widths[3]);

          GUILayout.BeginVertical(GUILayout.Width(widths[4]));
          BeginCenter();
          if (GUILayout.Button("▶", GUILayout.ExpandWidth(false)))
          {
            PlayOther(characterSynthesis);
          }
          EndCenter();
          EditorGUILayout.EndVertical();

          GUILayout.BeginVertical(GUILayout.Width(widths[5]));
          BeginCenter();
          if (GUILayout.Button("★", GUILayout.ExpandWidth(false)))
          {
            MakeMain(characterSynthesis);
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

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void FetchData()
    {
      text = Api.GetText(text_id);
      mainCharacterSynthesis = text.GetMainCharacterSynthesis();
      characterSyntheses = Api.GetCharacterSyntheses(
        text,
        LATEST_CHARACTER_SYNTHESES_COUNT + 1
      );
    }

    private void PlayMain()
    {
      PlayLocalAudio(text.GetMainResultFileAbsolutePath());
    }

    private void DownloadMain()
    {
      text.DownloadMainResultFile();
      AssetDatabase.Refresh();
    }

    private void PlayOther(CharacterSynthesis characterSynthesis)
    {
      PlayRemoteAudio(characterSynthesis.synthesis_result_file_url);
    }

    private void MakeMain(CharacterSynthesis characterSynthesis)
    {
      // a false value means we don't need to refresh the window
      if (text.SetMainCharacterSynthesisId(characterSynthesis.id))
      {
        FetchData();
      }
    }

    private void ViewHistory()
    {
      Router.SynthesisHistory(text);
    }

    private void Resynthesize()
    {
      CharacterSynthesis newCharacterSynthesis = new CharacterSynthesis();
      newCharacterSynthesis.synthesis_text = text.main_synthesis_text;
      newCharacterSynthesis.synthesis_modifier = text.main_synthesis_modifier;
      newCharacterSynthesis.character_id = text.main_character_id;
      newCharacterSynthesis.text_slug = text.slug;
      Router.NewCharacterSynthesis(newCharacterSynthesis);
    }

  }

}
