// using System;
using System.Collections.Generic;
// using System.IO;
// using System.Net;
// using System.Text;

namespace Enginn
{

  public class Model
  {

    protected Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

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

  }

}
