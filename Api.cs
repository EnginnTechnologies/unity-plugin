using System;
using System.Net;
using UnityEngine;

namespace Enginn
{

  public class Api
  {

    private const String BaseUrl = "http://localhost:3000/api/v1";

    public static Character[] getCharacters() {
      var response = newClient().DownloadString($"{BaseUrl}/characters");
      return JsonUtility.FromJson<ApiResults<Character>>(response).objects;
    }

    private static WebClient newClient() {
      var client = new WebClient();
      client.Headers.Add("Authorization", $"Bearer {Settings.apiKey}");
      return client;
    }

  }

  [Serializable]
  internal class ApiResults<T>
  {
    public T[] objects;
  }

}
