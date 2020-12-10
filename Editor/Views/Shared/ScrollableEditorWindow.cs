using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class ScrollableEditorWindow : EnginnEditorWindow
  {

    private Vector2 scrollPos;

    public ScrollableEditorWindow()
    {
      minSize = new Vector2(1000f, 600f);
      titleContent = new GUIContent("Enginn");
    }

    void OnGUI()
    {
      OnGUITitle();

      OnGUIContentContainer();

      GUILayout.FlexibleSpace();

      OnGUIButtonsContainer();
    }

    protected virtual void OnGUITitle()
    {
      // override this
      H1("Enginn");
    }

    void OnGUIContentContainer()
    {
      scrollPos = EditorGUILayout.BeginScrollView(
        scrollPos,
        GUILayout.Width(position.width),
        GUILayout.Height(position.height - 200)
      );

      OnGUIContent();

      EditorGUILayout.EndScrollView();
    }

    protected virtual void OnGUIContent()
    {
      // override this
    }

    void OnGUIButtonsContainer()
    {
      GUIStyle style = new GUIStyle();
      style.padding = new RectOffset(0, 0, 30, 30); // left, right, top, bottom
      GUILayout.BeginHorizontal(style);
      GUILayout.FlexibleSpace();

      OnGUIButtons();

      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    protected virtual void OnGUIButtons()
    {
      // override this
      if(GUILayout.Button("Cancel", GUILayout.Width(100)))
      {
        Close();
      }
    }

  }

}
