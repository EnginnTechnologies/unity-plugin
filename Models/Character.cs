using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Character
  {

    public int id;
    public string avatar_url;
    public string name;
    public string created_at;
    public string updated_at;
    public int project_id;

    private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

    private Texture2D avatar_texture;
    public string avatar_bytes;

    public Texture2D GetAvatarTexture()
    {
      return avatar_texture;
    }

    public void SetAvatarTexture(Texture2D v)
    {
      if ( v == avatar_texture)
      {
        return;
      }

      avatar_texture = v;

      if (null == avatar_texture) return;

      string assetPath = AssetDatabase.GetAssetPath(avatar_texture);
      var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
      if (tImporter != null)
      {
        tImporter.textureType = TextureImporterType.Default;
        tImporter.isReadable = true;
        AssetDatabase.ImportAsset(assetPath);
        AssetDatabase.Refresh();
      }
      avatar_bytes = Convert.ToBase64String(avatar_texture.EncodeToPNG());
    }

    public void ClearErrors()
    {
      errors.Clear();
    }

    public void AddError(string attribute, string error)
    {
      if (!errors.ContainsKey(attribute)) {
        errors[attribute] = new List<string>();
      }
      errors[attribute].Add(error);
    }

    public void SetErrors(Dictionary<string, List<string>> newErrors)
    {
      errors = newErrors;
    }

    public Dictionary<string, List<string>> GetErrors()
    {
      return errors;
    }

    public string GetErrorsAsJson()
    {
      var result = "";
      foreach(var error in errors)
      {
        result += string.Format("\n\"{0}\": [{1}]", error.Key, string.Join(",", error.Value));
      }
      return result;
    }

    public Character ShallowCopy()
    {
       return (Character) this.MemberwiseClone();
    }

    public bool Create()
    {
      Api.CreateCharacter(this);
      return id > 0;
    }

    public bool Update()
    {
      Api.UpdateCharacter(this);
      return errors.Count < 1;
    }

    public bool Destroy()
    {
      Api.DestroyCharacter(this);
      return errors.Count < 1;
    }

    public void DownloadAvatar()
    {
      if (String.IsNullOrEmpty(avatar_url))
      {
        return;
      }
      avatar_texture = Api.DownloadImage(avatar_url);
    }

  }

}
