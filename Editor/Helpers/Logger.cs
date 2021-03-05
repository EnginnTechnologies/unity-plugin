using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Enginn
{

  public class Logger
  {

    private const string LOG_FILE_PATH = "Logs/Enginn.log";
    private static StreamWriter writer;

    public static void Log(string line) {
      string dt = DateTime.Now.ToString("O");
      string pid = Process.GetCurrentProcess().Id.ToString();
      int? tid = Task.CurrentId;
      if(tid != null)
      {
        pid = $"{pid}:{tid}";
      }
      GetWriter().WriteLine($"{dt} #{pid} {line}");
      GetWriter().Flush();
    }

    //-------------------------------------------------------------------------
    // HELPERS
    //-------------------------------------------------------------------------

    private static StreamWriter GetWriter() {
      if(writer == null) {
        if(File.Exists(LOG_FILE_PATH)) {
          writer = File.AppendText(LOG_FILE_PATH);
        } else {
          writer = File.CreateText(LOG_FILE_PATH);
        }
      }
      return writer;
    }
  }

}
