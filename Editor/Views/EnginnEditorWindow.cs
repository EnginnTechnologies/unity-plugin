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
        pStyle.alignment = TextAnchor.MiddleCenter;
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

    protected static void H1(string content)
    {
      GUILayout.Label(content, H1Style());
    }

    protected static void H2(string content)
    {
      GUILayout.Label(content, H2Style());
    }

    protected static void P(string content)
    {
      GUILayout.Label(content, PStyle());
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

    public void FormLabel(string text)
    {
      EditorGUILayout.LabelField(
        text,
        FormLabelStyle(),
        GUILayout.Width(150)
      );
    }

    public int FormRadio(int selected, string[] texts)
    {
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);

      return GUILayout.SelectionGrid(
        selected,
        texts,
        1,
        radioStyle,
        GUILayout.Width(400)
      );
    }

    public string FormTextField(string text)
    {
      return EditorGUILayout.TextField(
        text,
        GUILayout.Width(400)
      );
    }

    protected static Texture2D TextureField(Texture2D texture)
    {
      EditorGUILayout.BeginVertical(GUILayout.Width(400));

      Texture2D result = (Texture2D)EditorGUILayout.ObjectField(
        texture,
        typeof(Texture2D),
        false, // allowSceneObjects
        GUILayout.Height(200),
        GUILayout.ExpandWidth(false)
      );

      EditorGUILayout.EndVertical();

      return result;
    }

    protected void FormErrors(Dictionary<string, List<string>> errors)
    {
      GUIStyle style = new GUIStyle();
      style.richText = true;

      EditorGUILayout.BeginVertical(GUILayout.Width(400));

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

    protected static Texture2D MakeTexture(int width, int height, Color col)
    {
      Color[] pix = new Color[width*height];

      for(int i = 0; i < pix.Length; i++)
        pix[i] = col;

      Texture2D result = new Texture2D(width, height);
      result.SetPixels(pix);
      result.Apply();

      return result;
    }

  }

}
