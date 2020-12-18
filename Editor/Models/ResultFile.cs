using System;
using System.IO;
using UnityEngine;

namespace Enginn
{

  public class ResultFile
  {

    public const string RESULT_PATH = "Assets/EnginnResults";

    private string slug;

    public ResultFile(string s_slug)
    {
      slug = s_slug;
    }

    public static string GetRootPath()
    {
      string assetsPath = Application.dataPath;
      return assetsPath.Substring(0, assetsPath.Length - "Assets".Length);
    }

    public static string GetAbsolutePath(string slug = null)
    {
      return GetRootPath() + GetPath(slug);
    }
    public string GetAbsolutePath()
    {
      return GetAbsolutePath(slug);
    }

    public static string GetPath(string slug = null)
    {
      if (String.IsNullOrEmpty(slug))
      {
        return RESULT_PATH;
      } else {
		var fileName = String.Copy(slug);
		foreach (var c in Path.GetInvalidFileNameChars())
		{
		  fileName = fileName.Replace(c, '-');
		}
        return $"{RESULT_PATH}/{fileName}.wav";
      }
    }
    public string GetPath()
    {
      return GetPath(slug);
    }

    public static bool Exists(string slug)
    {
      return File.Exists(GetPath(slug));
    }
    public bool Exists()
    {
      return Exists(slug);
    }

    public static bool DownloadFrom(string slug, string uri)
    {
      if (String.IsNullOrEmpty(uri))
      {
        return false;
      }
      if(!Directory.Exists(RESULT_PATH))
      {
        Directory.CreateDirectory(RESULT_PATH);
      }
      string filePath = GetPath(slug);
      return Api.DownloadWav(uri, filePath);
    }
    public bool DownloadFrom(string uri)
    {
      return DownloadFrom(slug, uri);
    }

  }

}
