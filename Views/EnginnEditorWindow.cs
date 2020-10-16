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

  }

}
