using System;
// using System.IO;
// using System.Net;
// using System.Text;
using UnityEditor;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Character : Model
  {

    public int id;
    public string avatar_url;
    public string name;
    public string gender;
    public string pitch;
    public string age;
    public bool is_nasal;
    public string created_at;
    public string updated_at;
    public int project_id;

    private Texture2D avatar_texture;
    public string avatar_bytes;

    // ------------------------------------------------------------------------
    // CONSTANTS
    // ------------------------------------------------------------------------

    public class Gender
    {
      public const string Male = "male";
      public const string Female = "female";
    }

    public static readonly string[] Genders = {Gender.Male, Gender.Female};
    public static readonly string[] GenderNames = {"Male", "Female"};

    public class Age
    {
      public const string Mid = "a0_mid";
      public const string Old = "a1_old";
    }

    public static readonly string[] Ages = {Age.Mid, Age.Old};
    public static readonly string[] AgeNames = {"Middle aged", "Older"};

    public class Pitch
    {
      public const string Low = "p0_low";
      public const string Mid = "p1_mid";
      public const string High = "p2_high";
    }

    public static readonly string[] Pitches = {Pitch.Low, Pitch.Mid, Pitch.High};
    public static readonly string[] PitchNames = {"Lower", "Average", "Higher"};

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

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
      avatar_bytes = Convert.ToBase64String(avatar_texture.EncodeToPNG());
    }

  }

}
