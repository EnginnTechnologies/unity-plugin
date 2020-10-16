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
      Debug.Log($"[Character#{id}] GetAvatarTexture()");
      return avatar_texture;
    }

    public void SetAvatarTexture(Texture2D v)
    {
      Debug.Log($"[Character#{id}] SetAvatarTexture()");

      if ( v == avatar_texture)
      {
        Debug.Log("texture is the same -> do nothing");
        return;
      }

      avatar_texture = v;

      if (null == avatar_texture) return;

      string assetPath = AssetDatabase.GetAssetPath(avatar_texture);
      // Debug.Log($"assetPath: {assetPath}");
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
      Debug.Log("[Character] ClearErrors()");
      errors.Clear();
    }

    public void AddError(string attribute, string error)
    {
      Debug.Log($"[Character] AddError({attribute}, {error})");
      if (!errors.ContainsKey(attribute)) {
        errors[attribute] = new List<string>();
      }
      errors[attribute].Add(error);
      Debug.Log($"-> {GetErrorsAsJson()}");
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
      Debug.Log($"[Character#{id}] DownloadAvatar()");
      if (String.IsNullOrEmpty(avatar_url))
      {
        // Debug.Log("Character has no avatar");
      } else {
        // Debug.Log($"Character has avatar {character.avatar_url}");
        avatar_texture = Api.DownloadImage(avatar_url);
      }
    }

  }

}
