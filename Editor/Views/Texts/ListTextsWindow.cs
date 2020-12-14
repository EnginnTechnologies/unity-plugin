using System.Collections.Generic;
// using System.Drawing;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class ListTextsWindow : ScrollableEditorWindow
  {
    Text[] texts = {};
    int filterCharacterId = 0;

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

      FormLabel("Filter by character:", 150);
      filterCharacterId = FormCharacterIdField(filterCharacterId, 200);

      EndCenter();

      GUILayout.Space(30);

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
        200,
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
          if (filterCharacterId > 0 && text.main_character_id != filterCharacterId)
          {
            continue;
          }

          BeginTableRow();

          TableBodyCell(text.slug, widths[0]);

          text.SetColor(FormColorField(text.GetColor(), widths[1]));

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
          BeginCenter();
          if (GUILayout.Button("Resynthesize", GUILayout.ExpandWidth(false)))
          {
            Resynthesize(text);
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
        FetchTexts();
      }
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void FetchTexts()
    {
      Project.RefreshCurrent();
      Project.FetchCharacters();
      texts = Api.GetTexts();
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

    private void Resynthesize(Text text)
    {
      CharacterSynthesis newCharacterSynthesis = new CharacterSynthesis();
      newCharacterSynthesis.text = text.main_synthesis_text;
      newCharacterSynthesis.modifier = text.main_synthesis_modifier;
      newCharacterSynthesis.character_id = text.main_character_id;
      newCharacterSynthesis.text_slug = text.slug;
      Router.NewCharacterSynthesis(newCharacterSynthesis);
    }

  }

}
