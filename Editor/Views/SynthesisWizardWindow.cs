using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class SynthesisWizardWindow : ScrollableEditorWindow
  {

    enum ImportMethod
    {
      None = -1,
      CSV = 0,
      UnityAssets = 1
    }

    enum ExportMethod
    {
      None = -1,
      Files = 0,
      UnityAssets = 1
    }

    private int step = 0;
    private ImportMethod importMethod = ImportMethod.None;
    private ExportMethod exportMethod = ExportMethod.None;

    private TextAsset importFile;
    private List<Dictionary<string, string>> importFileContent;

    public SynthesisWizardWindow()
    {
      titleContent = new GUIContent("Enginn - Synthesis wizard");
    }

    protected override void OnGUITitle()
    {
      H1("Enginn speech synthesis");
    }

    protected override void OnGUIContent()
    {
      switch(step)
      {
        case 0:
          OnGUIContentStep0();
          break;
        case 1:
          H2("Step 1: texts import method");
          OnGUIContentStep1();
          break;
        case 2:
          H2("Step 2: texts import");
          OnGUIContentStep2();
          break;
        case 3:
          H2("Step 3: texts verification");
          OnGUIContentStep3();
          break;
        case 4:
          H2("Step 4: audio export option");
          OnGUIContentStep4();
          break;
        case 5:
          H2("Step 5: audio files generation");
          OnGUIContentStep5();
          break;
      }
    }

    void OnGUIContentStep0()
    {
      P("This wizard will guide you through the steps");
    }

    void OnGUIContentStep1()
    {
      P("Select an import method");

      GUILayout.Space(50);

      BeginCenter();
      string[] selStrings = {"CSV file", "Unity assets"};
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);
      importMethod = (ImportMethod)GUILayout.SelectionGrid((int)importMethod, selStrings, 1, radioStyle);
      EndCenter();
    }

    void OnGUIContentStep2()
    {
      P("Select an existing asset");

      GUILayout.Space(50);

      BeginCenter();
      if(importFile == null)
      {
        importFileContent = null;
      }
      importFile = TextAssetField(importFile);
      EndCenter();
    }

    void OnGUIContentStep3()
    {
      P("Verify texts");

      GUILayout.Space(50);

      BeginCenter();
      int idx = 0;
      foreach (Dictionary<string, string> line in GetImportFileContent())
      {
        idx++;
        P($"Line {idx}:");
        slug = line["slug"];
        P($"- slug = {slug}");
        text = line["text"];
        P($"- text = {text}");
      }
      EndCenter();
    }

    private List<Dictionary<string, string>> GetImportFileContent()
    {
      if(importFile == null)
      {
        return null;
      }
      if(importFileContent == null)
      {
        importFileContent = DictionaryCSVReader.FromString(importFile.text);
      }
      return importFileContent;
    }

    void OnGUIContentStep4()
    {
    }

    void OnGUIContentStep5()
    {
    }

    private GUILayoutOption ButtonStyle()
    {
      return GUILayout.Width(100);
    }

    protected override void OnGUIButtons()
    {
      if(step > 0)
      {
        GUI.enabled = testCanPrev();
        if(GUILayout.Button("< Previous", ButtonStyle()))
        {
          Prev();
        }
        GUI.enabled = true;
      }

      GUILayout.Space(50);

      if(step < 5)
      {
        GUI.enabled = testCanNext();
        if(GUILayout.Button("Next >", ButtonStyle()))
        {
          Next();
        }
        GUI.enabled = true;
      }

      if(step == 5)
      {
        if(GUILayout.Button("Stop", ButtonStyle()))
        {
          Close();
        }
      }
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
