using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Character
  {

    public int id;
    public string name;
    public string created_at;
    public string updated_at;
    public int project_id;

    private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

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

  }

}
