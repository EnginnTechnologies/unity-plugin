using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class SynthesisHistoryWindow : ScrollableEditorWindow
  {

    public SynthesisHistoryWindow()
    {
      titleContent = new GUIContent("Enginn - Synthesis history");
    }

    protected override void OnGUITitle()
    {
      H1("Enginn speech synthesis history");
    }

    protected override void OnGUIContent()
    {

    }

  }

}
