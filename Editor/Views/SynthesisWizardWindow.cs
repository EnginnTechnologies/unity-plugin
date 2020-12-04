using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

    // enum ExportMethod
    // {
    //   None = -1,
    //   Files = 0,
    //   UnityAssets = 1
    // }

    private Dictionary<int, Character> characters;

    private int step = 0;
    private ImportMethod importMethod = ImportMethod.None;
    // private ExportMethod exportMethod = ExportMethod.None;
    private bool replaceExistingFiles = false;

    private TextAsset importAsset;
    private string importPath;

    private List<CharacterSynthesis> characterSyntheses;

    private bool importFileRead = false;
    private List<string> importFileErrors;

    private float exportProgress = 0.7f;
    private bool exportStarted = false;
    private List<string> exportErrors;
    private int exportSkipped = 0;

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
      string[] selStrings = {"External CSV file", "Unity CSV asset"};
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);
      importMethod = (ImportMethod)GUILayout.SelectionGrid((int)importMethod, selStrings, 1, radioStyle);
      EndCenter();
    }

    void OnGUIContentStep2()
    {
      if (importMethod == ImportMethod.UnityAssets)
      {

        P("Select an existing CSV asset");

        GUILayout.Space(50);

        BeginCenter();
        importAsset = TextAssetField(importAsset);
        EndCenter();

      } else {

        P("Please select the CSV file you wish to import.");

        GUILayout.Space(50);

        BeginCenter();
        SetImportPath(FilePathField(importPath));
        EndCenter();

      }
    }

    void SetImportPath(string newImportPath)
    {
      if (!String.IsNullOrEmpty(newImportPath))
      {
        Debug.Log($"Set importPath to \"{newImportPath}\"");
        importPath = newImportPath;
      }
    }

    void OnGUIContentStep3()
    {
      P("Verify texts");

      GUILayout.Space(50);

      BeginCenter();

      if (importFileErrors.Count > 0)
      {

        foreach (string error in importFileErrors)
        {
          P($"<color=red>{error}</color>");
        }

      } else {

        EditorGUILayout.BeginVertical();

        List<string> headers = new List<string>()
        {
          "Line",
          "Slug",
          "Character",
          "Text",
          "Modifier"
        };
        List<int> widths = new List<int>()
        {
          50,
          150,
          150,
          350,
          100
        };

        TableHeaderRow(headers, widths);

        foreach (CharacterSynthesis characterSynthesis in characterSyntheses)
        {
          string characterName;
          if (characters.ContainsKey(characterSynthesis.character_id))
          {
            characterName = characters[characterSynthesis.character_id].name;
          } else {
            characterName = $"Unknown ID {characterSynthesis.character_id}";
          }
          List<string> values = new List<string>(){
            characterSynthesis.GetImportFileLine().ToString(),
            characterSynthesis.slug,
            characterName,
            characterSynthesis.text,
            characterSynthesis.GetModifierName()
          };
          TableBodyRow(values, widths);
        }

        EditorGUILayout.EndVertical();

      }

      EndCenter();
    }

    void OnGUIContentStep4()
    {
      GUIStyle radioStyle = new GUIStyle(EditorStyles.radioButton);
      radioStyle.padding = new RectOffset(20, 0, 0, 0);

      GUILayout.Space(50);

      BeginCenter();
      EditorGUILayout.BeginVertical();

      // P("Select an export method", TextAnchor.MiddleLeft);
      // exportMethod = (ExportMethod) GUILayout.SelectionGrid(
      //   (int) exportMethod,
      //   (new[] {"Files (named by slug)", "Unity assets"}),
      //   1,
      //   radioStyle
      // );

      // GUILayout.Space(20);

      P($"Result files will be put in {CharacterSynthesis.AbsoluteResultPath()}");
      P("What should happen if the file already exists?", TextAnchor.MiddleLeft);
      replaceExistingFiles = 1 == GUILayout.SelectionGrid(
        replaceExistingFiles ? 1 : 0,
        (new[] {"Don't synthesize (skip the line)", "Replace the existing file"}),
        1,
        radioStyle
      );

      EditorGUILayout.EndVertical();
      EndCenter();
    }

    void OnGUIContentStep5()
    {
      if (exportStarted)
      {

        if (exportProgress < 1)
        {
          P("Synthesis in progress, please wait");
        } else {
          P("Synthesis done, you can close this window");
        }

        GUILayout.Space(50);

        BeginCenter();
        EditorGUILayout.BeginVertical();
        BeginCenter();
        EditorGUI.ProgressBar(
          EditorGUILayout.GetControlRect(false, 20, GUILayout.Width(400)),
          exportProgress,
          $"{exportProgress * characterSyntheses.Count} / {characterSyntheses.Count}"
        );
        EndCenter();
        if (exportSkipped > 0)
        {
          BeginCenter();
          P($"({exportSkipped} already existing and skipped)");
          EndCenter();
        }
        GUILayout.Space(10);
        BeginCenter();
        EditorGUILayout.BeginVertical();
        foreach (string error in exportErrors)
        {
          P($"<color=red>{error}</color>");

        }
        EditorGUILayout.EndVertical();
        EndCenter();
        EditorGUILayout.EndVertical();
        EndCenter();

      } else {

        P($"You are about to perform {characterSyntheses.Count} syntheses");

        GUILayout.Space(50);

        BeginCenter();
        if (GUILayout.Button("Start synthesis", ButtonStyle(200)))
        {
          DoExport();
        }
        EndCenter();

      }
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
        string buttonText;
        if (step == 0)
        {
          buttonText = "Start";
        } else {
          buttonText = "Next >";
        }
        GUI.enabled = testCanNext();
        if(GUILayout.Button(buttonText, ButtonStyle()))
        {
          Next();
        }
        GUI.enabled = true;
      }

      if(step == 5)
      {
        string buttonText;
        if (exportProgress < 1)
        {
          buttonText = "Cancel";
        } else {
          buttonText = "Close";
        }
        if(GUILayout.Button(buttonText, ButtonStyle()))
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
        case 2:
          switch (importMethod)
          {
            case ImportMethod.UnityAssets:
              if (importAsset == null)
              {
                return false;
              }
              break;
            case ImportMethod.CSV:
              if (String.IsNullOrEmpty(importPath))
              {
                return false;
              }
              break;
          }
          break;
        case 3:
          if(importFileErrors.Count > 0)
          {
            return false;
          }
          break;
        // case 4:
        //   if(exportMethod == ExportMethod.None)
        //   {
        //     return false;
        //   }
        //   break;
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
        // things to do before
        switch(step)
        {
          // case 0: // things to do before selecting the import method
          case 1: // things to do before selecting the import file
            importPath = null;
            importAsset = null;
            break;
          case 2: // things to do before displaying the import file content
            importFileRead = false;
            importFileErrors = new List<string>();
            FetchCharacters();
            ReadImportFile();
            break;
          // case 3: // things to do before selecting the export method
          case 4: // things to do before performing the export
            exportProgress = 0f;
            exportStarted = false;
            exportSkipped = 0;
            break;
        }
        step++;
      }
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    private void FetchCharacters()
    {
      characters = new Dictionary<int, Character>();
      foreach (Character character in Api.GetCharacters())
      {
        characters[character.id] = character;
      }
    }

    private void ReadImportFile()
    {
      if(importFileRead)
      {
        return;
      }

      importFileRead = true;
      int line_idx = 0;
      characterSyntheses = new List<CharacterSynthesis>();

      string importFileContent = null;
      switch (importMethod)
      {
        case ImportMethod.UnityAssets:
          importFileContent = importAsset.text;
          break;
        case ImportMethod.CSV:
          StreamReader reader = new StreamReader(importPath);
          importFileContent = reader.ReadToEnd();
          reader.Close();
          break;
      }

      foreach (Dictionary<string, string> line in DictionaryCSVReader.FromString(importFileContent))
      {
        line_idx++;

        // checks
        if (line_idx == 1)
        {
          if (!line.ContainsKey("text"))
          {
            Debug.LogError("Column \"text\" not found in file");
            importFileErrors.Add("Column \"text\" not found in file");
          }
          if (!line.ContainsKey("character_id"))
          {
            Debug.LogError("Column \"character_id\" not found in file");
            importFileErrors.Add("Column \"character_id\" not found in file");
          }
          if (!line.ContainsKey("slug"))
          {
            Debug.LogError("Column \"slug\" not found in file");
            importFileErrors.Add("Column \"slug\" not found in file");
          }
          if (!line.ContainsKey("modifier"))
          {
            Debug.LogError("Column \"modifier\" not found in file");
            importFileErrors.Add("Column \"modifier\" not found in file");
          }
          if (importFileErrors.Count > 0)
          {
            return;
          }
        }

        CharacterSynthesis characterSynthesis = new CharacterSynthesis();
        characterSynthesis.text = line["text"];
        characterSynthesis.modifier = line["modifier"];
        characterSynthesis.character_id = int.Parse(line["character_id"]);
        characterSynthesis.slug = line["slug"];
        characterSynthesis.SetImportFileLine(line_idx);
        characterSyntheses.Add(characterSynthesis);
      }
    }

    private async void DoExport()
    {
      // exit if already started
      if (exportStarted)
      {
        return;
      }

      // Api cache will be used by threads so refresh it
      Api.refreshCache();

      // start it
      exportStarted = true;
      exportErrors = new List<string>();

      var synthesisTasks = new List<Task>();
      int synthesis_idx = 0;
      foreach (CharacterSynthesis characterSynthesis in characterSyntheses)
      {
        synthesisTasks.Add(PerformSynthesis(synthesis_idx));
        synthesis_idx++;
      }

      while (synthesisTasks.Count > 0)
      {
        Task finishedTask = await Task.WhenAny(synthesisTasks);
        synthesisTasks.Remove(finishedTask);
        exportProgress = (characterSyntheses.Count - synthesisTasks.Count) / ((float) characterSyntheses.Count);
        Repaint();
      }
    }

    private async Task PerformSynthesis(int idx)
    {
      await Task.Run( () => {
        try
        {
          CharacterSynthesis characterSynthesis = characterSyntheses[idx];

          if (!replaceExistingFiles && characterSynthesis.ResultFileExists())
          {
            Debug.Log($"Audio file for {characterSynthesis.slug} already existing: skip it");
            exportSkipped++;
          } else {
            if(characterSynthesis.Create())
            {
              if(characterSynthesis.DownloadResultFile())
              {
                // Debug.Log("result file created");
              } else {
                Debug.LogError("result file couldn't be downloaded");
                exportErrors.Add($"Audio file for {characterSynthesis.slug} couldn't be downloaded");
              }
            } else {
              Debug.LogError($"CharacterSynthesis errors: {characterSynthesis.GetErrorsAsJson()}");
              exportErrors.Add($"Synthesis error for {characterSynthesis.slug}");
            }
          }
        } catch (Exception e) {
          exportErrors.Add($"Synthesis error for line {idx+1}: {e}");
          Debug.LogError($"ERROR: {e}");
        }
      });
    }

  }

}
