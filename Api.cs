using System;
using System.Net;
using UnityEngine;

namespace Enginn
{

  public class Api
  {

    static public Character[] getCharacters() {
      var client = new WebClient();
      var apiKey = Settings.getApiKey();
      client.Headers.Add("Authorization", $"Bearer {apiKey}");
      var response = client.DownloadString("http://localhost:3000/api/v1/characters");
      var json = "{\"characters\":" + response + "}";
      var result = JsonUtility.FromJson<Characters>(json);

      return result.characters;
    }

  }

}
