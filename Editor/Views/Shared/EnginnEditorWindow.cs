using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class EnginnEditorWindow : EditorWindow
  {

    protected static GUIStyle h1Style = null;
    protected static GUIStyle h2Style = null;
    protected static GUIStyle pStyle = null;
    protected static GUIStyle formLabelStyle = null;

    protected static GUIStyle H1Style()
    {
      if(h1Style == null)
      {
        h1Style = new GUIStyle();
        h1Style.richText = true;
        h1Style.fontSize = 40;
        h1Style.alignment = TextAnchor.MiddleCenter;
        h1Style.padding = new RectOffset(0, 0, 30, 30); // left, right, top, bottom
        h1Style.normal.textColor = Color.white;
      }
      return h1Style;
    }

    protected static GUIStyle H2Style()
    {
      if(h2Style == null)
      {
        h2Style = new GUIStyle();
        h2Style.richText = true;
        h2Style.fontSize = 24;
        h2Style.alignment = TextAnchor.MiddleCenter;
        h2Style.padding = new RectOffset(0, 0, 20, 20); // left, right, top, bottom
        h2Style.normal.textColor = Color.white;
      }
      return h2Style;
    }

    protected static GUIStyle PStyle()
    {
      if(pStyle == null)
      {
        pStyle = new GUIStyle();
        pStyle.richText = true;
        pStyle.fontSize = 14;
        pStyle.padding = new RectOffset(0, 0, 10, 10); // left, right, top, bottom
        pStyle.normal.textColor = Color.white;
      }
      return pStyle;
    }

    protected static GUIStyle FormLabelStyle()
    {
      if(formLabelStyle == null)
      {
        formLabelStyle = new GUIStyle();
        formLabelStyle.richText = true;
        formLabelStyle.fontSize = 12;
        formLabelStyle.normal.textColor = Color.white;
      }
      return formLabelStyle;
    }

    protected GUILayoutOption ButtonStyle(int width = 100)
    {
      return GUILayout.Width(width);
    }

    protected static void H1(string content)
    {
      GUILayout.Label(content, H1Style());
    }

    protected static void H2(string content)
    {
      GUILayout.Label(content, H2Style());
    }

    protected static void P(string content, TextAnchor alignment = TextAnchor.MiddleCenter)
    {
      GUIStyle style = PStyle();
      style.alignment = alignment;
      GUILayout.Label(content, PStyle());
    }

    public void BeginColumns()
    {
      GUILayout.BeginHorizontal();
    }

    public void BeginColumn(int width = 300)
    {
      GUILayout.FlexibleSpace();
      EditorGUILayout.BeginVertical(GUILayout.Width(width));
    }

    public void EndColumn()
    {
      EditorGUILayout.EndVertical();
    }

    public void EndColumns()
    {
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    public void BeginCenter(GUIStyle style = null)
    {
      if(style == null)
      {
        GUILayout.BeginHorizontal();
      } else {
        GUILayout.BeginHorizontal(style);
      }
      GUILayout.FlexibleSpace();
    }

    public void EndCenter()
    {
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    public void BeginCenteredFormField()
    {
      GUIStyle style = new GUIStyle();
      style.padding = new RectOffset(0, 0, 10, 10); // left, right, top, bottom
      BeginCenter(style);
    }

    public void FormLabel(string text, int width = 150)
    {
      EditorGUILayout.LabelField(
        text,
        FormLabelStyle(),
        GUILayout.Width(width)
      );
    }

    public void FormFieldHint(string text, int width = 150)
    {
      GUILayout.Space(5);
      EditorGUILayout.LabelField(
        text,
        FormLabelStyle(),
        GUILayout.Width(width)
      );
    }

    public int FormRadio(int selected, string[] texts, int width = 400)
    {
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);

      return GUILayout.SelectionGrid(
        selected,
        texts,
        1,
        radioStyle,
        GUILayout.Width(width)
      );
    }

    public ProjectColor FormColorField(ProjectColor selected, int width = 400)
    {
      GUIStyle style;
      if (selected == null || selected.id == 0)
      {
        style = new GUIStyle(EditorStyles.label);
      } else {
        style = new GUIStyle();
      }
      style.clipping = TextClipping.Clip;
      style.alignment = TextAnchor.MiddleLeft;
      style.padding = new RectOffset(10, 10, 2, 2); // left, right, top, bottom
      if (selected == null || selected.id == 0)
      {
        style.normal.textColor = Color.white;
        style.focused.textColor = Color.white;
      } else {
        style.normal.background = Colors.MakeTexture(
          width,
          1, // height
          selected.GetColor()
        );
        style.normal.textColor = selected.GetTextColor();
      }
      return Project.GetColorById(
        EditorGUILayout.IntPopup(
          (selected == null || selected.id == 0) ? 0 : selected.id,
          Project.GetColorDisplayedOptions(),
          Project.GetColorOptionValues(),
          style,
          GUILayout.Width(width)
        )
      );
    }

    public int FormCharacterIdField(int selected, int width = 400)
    {
      return EditorGUILayout.IntPopup(
        selected,
        Project.GetCharacterDisplayedOptions(),
        Project.GetCharacterIdOptionValues(),
        GUILayout.Width(width)
      );
    }

    public string FormModifierField(string selected, int width = 400)
    {
      return Synthesis.Modifiers[
        FormRadio(
          Synthesis.GetModifierIndex(selected),
          Synthesis.ModifierNames,
          width
        )
      ];
    }

    public string FormTextField(string text, int width = 400)
    {
      return EditorGUILayout.TextField(
        text,
        GUILayout.Width(width)
      );
    }

    public bool FormToggle(bool value, int width = 400)
    {
      return EditorGUILayout.Toggle(
        value,
        GUILayout.Width(width)
      );
    }

    public void TableHeaderCell(string content, int width)
    {
      GUIStyle tableHeaderStyle = new GUIStyle();
      tableHeaderStyle.fixedWidth = width;
      tableHeaderStyle.richText = true;
      tableHeaderStyle.fontSize = 12;
      tableHeaderStyle.fontStyle = FontStyle.Bold;
      tableHeaderStyle.alignment = TextAnchor.MiddleLeft;
      tableHeaderStyle.padding = new RectOffset(10, 10, 2, 2); // left, right, top, bottom
      tableHeaderStyle.normal.textColor = Color.white;
      tableHeaderStyle.normal.background = Colors.MakeTexture(
        width,
        1, // height
        new Color(1.0f, 1.0f, 1.0f, 0.1f)
      );

      EditorGUILayout.LabelField(
        content,
        tableHeaderStyle,
        GUILayout.Width(width)
      );
    }

    public void BeginTableRow()
    {
      EditorGUILayout.BeginHorizontal();
    }
    public void EndTableRow()
    {
      EditorGUILayout.EndHorizontal();
    }

    public void TableHeaderRow(List<string> contents, List<int> widths)
    {
      BeginTableRow();
      int idx = 0;
      foreach (string content in contents)
      {
        TableHeaderCell(content, widths[idx]);
        idx++;
      }
      EndTableRow();
    }

    public void TableBodyCell(object content, int width)
    {
      if (content is string) {
        GUIStyle tableBodyStyle = new GUIStyle();
        tableBodyStyle.fixedWidth = width;
        tableBodyStyle.richText = true;
        tableBodyStyle.fontSize = 12;
        tableBodyStyle.clipping = TextClipping.Clip;
        tableBodyStyle.alignment = TextAnchor.MiddleLeft;
        tableBodyStyle.padding = new RectOffset(10, 10, 2, 2); // left, right, top, bottom
        tableBodyStyle.normal.textColor = Color.white;

        EditorGUILayout.LabelField(
          (string)content,
          tableBodyStyle,
          GUILayout.Width(width)
        );
      } else {
        Debug.Log($"Unknown object type {content}");
      }
    }

    public void TableBodyRow(object[] contents, List<int> widths)
    {
      BeginTableRow();
      int idx = 0;
      foreach (object content in contents)
      {
        TableBodyCell(content, widths[idx]);
        idx++;
      }
      EndTableRow();
    }

    protected string FilePathField(string currentPath, int width = 200)
    {
      EditorGUILayout.BeginVertical();
      if (!String.IsNullOrEmpty(currentPath))
      {
        BeginCenter();
        P($"Selected file: {currentPath}");
        EndCenter();
      }
      string buttonLabel;
      if (String.IsNullOrEmpty(currentPath))
      {
        buttonLabel = "Select a file";
      } else {
        buttonLabel = "Select another file";
      }
      BeginCenter();
      if (GUILayout.Button(buttonLabel, GUILayout.Width(width)))
      {
        return EditorUtility.OpenFilePanel(buttonLabel, "", "csv");
      }
      EndCenter();
      EditorGUILayout.EndVertical();
      return null;
    }

    protected static TextAsset TextAssetField(TextAsset asset)
    {
      EditorGUILayout.BeginVertical(GUILayout.Width(200));

      TextAsset result = (TextAsset)EditorGUILayout.ObjectField(
        asset,
        typeof(TextAsset),
        false, // allowSceneObjects
        GUILayout.Height(20),
        GUILayout.ExpandWidth(true)
      );

      EditorGUILayout.EndVertical();

      return result;
    }

    protected static Texture2D TextureField(Texture2D texture, int width = 400)
    {
      EditorGUILayout.BeginVertical(GUILayout.Width(width));

      Texture2D result = (Texture2D)EditorGUILayout.ObjectField(
        texture,
        typeof(Texture2D),
        false, // allowSceneObjects
        GUILayout.Height(width),
        GUILayout.ExpandWidth(false),
        GUILayout.Width(width)
      );

      EditorGUILayout.EndVertical();

      return result;
    }

    protected void FormErrors(Dictionary<string, List<string>> errors, int width = 400)
    {
      GUIStyle style = new GUIStyle();
      style.richText = true;

      EditorGUILayout.BeginVertical(GUILayout.Width(width));

      foreach(var error in errors)
      {
        foreach(var errorCode in error.Value)
        {
          string attr = error.Key;
          string msg = "";
          switch (errorCode)
          {
            case "blank":
              msg = $"{attr} is mandatory";
              break;
            case "inclusion":
              msg = $"{attr} is not an accepted value";
              break;
            default:
              msg = $"{attr}: {errorCode}";
              break;
          }
          GUILayout.Label($"<color=red>{msg}</color>", style);
        }
      }

      EditorGUILayout.EndVertical();
    }

    public string FormatDate(string dt, string format = "g")
    {
      return String.IsNullOrEmpty(dt) ? "" : DateTime.Parse(dt).ToString(format);
    }

    //-------------------------------------------------------------------------
    // AUDIO
    //-------------------------------------------------------------------------

    private AudioSource audioPlayer;

    protected AudioSource GetAudioPlayer()
    {
      if (audioPlayer == null)
      {
        GameObject obj = EditorUtility.CreateGameObjectWithHideFlags(
          "AudioPlayer",
          HideFlags.HideAndDontSave
        );
        audioPlayer = obj.AddComponent(typeof(AudioSource)) as AudioSource;
      }

      return audioPlayer;
    }

    // with a URI
    protected async void PlayRemoteAudio(string url)
    {
      // Debug.Log($"play remote audio {url}");
      if (!CheckAudioAvailable()){
        return;
      }
      AudioSource audioPlayer = GetAudioPlayer();
      audioPlayer.clip = await Api.LoadAudioClip(url);
      audioPlayer.Play();
    }

    // with an absolute path
    protected async void PlayLocalAudio(string path)
    {
      // Debug.Log($"play local audio {path}");
      if (!CheckAudioAvailable()){
        return;
      }
      AudioSource audioPlayer = GetAudioPlayer();
      audioPlayer.clip = await Api.LoadAudioClip("file://" + path);
      audioPlayer.Play();
    }

    protected bool CheckAudioAvailable()
    {
      if (EditorUtility.audioMasterMute)
      {
        EditorUtility.DisplayDialog(
          "Game audio is muted",
          "Please unmute game audio to be able to listen to this.",
          "OK"
        );
        return false;
      }
      return true;
    }

  }

}
