using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class SynthesisWizardWindow : EnginnEditorWindow
  {

    enum ImportMethod
    {
      None = 0,
      CSV = 1,
      UnityAssets = 2
    }

    enum ExportMethod
    {
      None = 0,
      Files = 1,
      UnityAssets = 2
    }

    private int step = 0;
    private ImportMethod importMethod = ImportMethod.None;
    private ExportMethod exportMethod = ExportMethod.None;

    public SynthesisWizardWindow()
    {
      Debug.Log("[SynthesisWizardWindow] constructor");
      minSize = new Vector2(1000f, 400f);
      titleContent = new GUIContent("Enginn speech synthesis");
    }

    void OnGUI()
    {
      switch(step)
      {
        case 0:
          OnGUIStep0();
          break;
        case 1:
          OnGUIStep1();
          break;
        case 2:
          OnGUIStep2();
          break;
        case 3:
          OnGUIStep3();
          break;
        case 4:
          OnGUIStep4();
          break;
        case 5:
          OnGUIStep5();
          break;
      }
    }

    private void Title()
    {
      H1("Enginn speech synthesis");
    }

    void OnGUIStep0()
    {
      Title();

      P("This wizard will guide you through the steps");

      Buttons(false, true);
    }

    void OnGUIStep1()
    {
      Title();

      H2("Step 1: texts import method");

      P("Select an import method");

      Buttons(true, true);
    }

    void OnGUIStep2()
    {
      Title();

      H2("Step 2: texts import");

      Buttons(true, true);
    }

    void OnGUIStep3()
    {
      Title();

      H2("Step 3: texts verification");

      Buttons(true, true);
    }

    void OnGUIStep4()
    {
      H2("Step 4: audio export option");

      Buttons(true, true);
    }

    void OnGUIStep5()
    {
      H2("Step 5: audio files generation");

      BeginButtons();

      if(GUILayout.Button("Stop", GUILayout.Width(100)))
      {
        Close();
      }

      EndButtons();
    }

    private void BeginButtons()
    {
      GUIStyle style = new GUIStyle();
      style.alignment = TextAnchor.MiddleCenter;
      style.stretchWidth = true;
      style.padding = new RectOffset(0, 0, 30, 30); // left, right, top, bottom
      GUILayout.BeginHorizontal(style);
      GUILayout.FlexibleSpace();
    }

    private void EndButtons()
    {
      GUILayout.FlexibleSpace();
      GUILayout.EndHorizontal();
    }

    private GUILayoutOption ButtonStyle()
    {
      return GUILayout.Width(100);
    }

    private void Buttons(bool canPrev, bool canNext)
    {
      BeginButtons();

      if(canPrev)
      {
        GUI.enabled = testCanPrev();
        if(GUILayout.Button("< Previous", ButtonStyle()))
        {
          Prev();
        }
        GUI.enabled = true;
      }

      GUILayout.Space(50);

      if(canNext)
      {
        GUI.enabled = testCanNext();
        if(GUILayout.Button("Next >", ButtonStyle()))
        {
          Next();
        }
        GUI.enabled = true;
      }

      EndButtons();
    }

    private bool testCanPrev()
    {
      switch(step)
      {
        case 0:
          return false;
      }
      return true;
    }

    private bool testCanNext()
    {
      switch(step)
      {
        case 1:
          if(importMethod == ImportMethod.None)
          {
            return false;
          }
          break;
        case 4:
          if(exportMethod == ExportMethod.None)
          {
            return false;
          }
          break;
      }
      return true;
    }

    private void Prev()
    {
      // verifications to prevent going to the previous step
      if(testCanPrev())
      {
        step--;
      }
    }

    private void Next()
    {
      // verifications to prevent going to the next step
      if(testCanNext())
      {
        step++;
      }
    }
  }

}
