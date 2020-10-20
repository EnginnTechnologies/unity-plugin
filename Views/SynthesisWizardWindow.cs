using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class SynthesisWizardWindow : EditorWindow
  {
    private int step = 0;

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
      }
    }

    void OnGUIStep0()
    {
      GUILayout.Label("Step 0", EditorStyles.boldLabel);

      if(GUILayout.Button("Next"))
      {
        Next();
      }
    }

    void OnGUIStep1()
    {
      GUILayout.Label("Step 1", EditorStyles.boldLabel);

      if(GUILayout.Button("Previous"))
      {
        Prev();
      }
      if(GUILayout.Button("Next"))
      {
        Next();
      }
    }

    void OnGUIStep2()
    {
      GUILayout.Label("Step 2", EditorStyles.boldLabel);

      if(GUILayout.Button("Previous"))
      {
        Prev();
      }
      if(GUILayout.Button("Next"))
      {
        Next();
      }
    }

    void OnGUIStep3()
    {
      GUILayout.Label("Step 3", EditorStyles.boldLabel);

      if(GUILayout.Button("Previous"))
      {
        Prev();
      }
      if(GUILayout.Button("Next"))
      {
        Next();
      }
    }

    void OnGUIStep4()
    {
      GUILayout.Label("Step 4", EditorStyles.boldLabel);

      if(GUILayout.Button("Previous"))
      {
        Prev();
      }
      if(GUILayout.Button("Next"))
      {
        Next();
      }
    }

    private void Prev()
    {
      if(step == 0)
      {
        return;
      }
      step--;
    }

    private void Next()
    {
      if(step == 4)
      {
        return;
      }
      step++;
    }
  }

}
