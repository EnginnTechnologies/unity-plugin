using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class DictionaryCSVReader
{

  private List<string> keys;
  private List<Dictionary<string, string>> lines;

  private void LineReader(int line_index, List<string> line_values)
  {
    if(line_index == 0)
    {
      keys = new List<string>(line_values);
    } else {
      Dictionary<string, string> line = new Dictionary<string, string>();
      int idx = 0;
      foreach (string key in keys)
      {
        line[key] = line_values[idx];
        idx++;
      }
      lines.Add(line);
    }
  }

  private void LoadFromString(string file_contents)
  {
    lines = new List<Dictionary<string, string>>();
    fgCSVReader.LoadFromString(file_contents, new fgCSVReader.ReadLineDelegate(LineReader));
  }

  public static List<Dictionary<string, string>> FromString(string file_contents)
  {
    DictionaryCSVReader reader = new DictionaryCSVReader();
    reader.LoadFromString(file_contents);
    return reader.lines;
  }

}
