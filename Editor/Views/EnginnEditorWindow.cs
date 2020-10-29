using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class EnginnEditorWindow : EditorWindow
  {

    protected static Texture2D TextureField(string name, Texture2D texture)
    {
      GUILayout.BeginVertical();
      var style = new GUIStyle(GUI.skin.label);
      style.alignment = TextAnchor.UpperCenter;
      style.fixedWidth = 70;
      GUILayout.Label(name, style);
      var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
      GUILayout.EndVertical();
      return result;
    }

    protected static GUIStyle h1Style = null;
    protected static GUIStyle h2Style = null;
    protected static GUIStyle pStyle = null;

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

  }

}
