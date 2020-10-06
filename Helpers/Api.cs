﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace Enginn
{

  public class Api
  {

    private const String BaseUrl = "http://localhost:3000/api/v1";

    public static Character[] GetCharacters() {
      var response = NewClient().DownloadString($"{BaseUrl}/characters");
      return JsonUtility.FromJson<ApiResponse<Character[]>>(response).result;
    }

    public static void CreateCharacter(Character character) {
      var payload = "{\"character\": " + JsonUtility.ToJson(character) + "}";
      var response = NewClient().UploadString($"{BaseUrl}/characters", payload);
      Debug.Log($"API response: {response}");
      var apiResponse = JsonUtility.FromJson<ApiResponse<Character>>(response);

      switch (apiResponse.status)
      {
        case 201:
          character.id = apiResponse.result.id;
          character.created_at = apiResponse.result.created_at;
          break;
        case 422:
          Debug.Log($"API errors: {apiResponse.GetErrorsAsJson()}");
          character.SetErrors(apiResponse.GetErrorsDictionnary());
          break;
        default:
          throw new WebException($"API error {apiResponse.status}");
      }
    }

    private static WebClient NewClient() {
      var client = new WebClient();
      client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
      client.Headers.Add("Authorization", $"Bearer {Settings.apiKey}");
      return client;
    }

  }

  [Serializable]
  internal class ApiError
  {
    public string attribute;
    public string error;
  }

  [Serializable]
  internal class ApiResponse<T>
  {
    public int status;
    public ApiError[] errors;
    public T result;

    public string GetErrorsAsJson()
    {
      var result = "";
      foreach(var error in errors)
      {
        result += $"\n{error.attribute}: {error.error}";
      }
      return result;
    }

    public Dictionary<string, List<string>> GetErrorsDictionnary()
    {
      var result = new Dictionary<string, List<string>>();
      foreach(var apiError in errors)
      {
        if (!result.ContainsKey(apiError.attribute)) {
          result[apiError.attribute] = new List<string>();
        }
        result[apiError.attribute].Add(apiError.error);
      }
      return result;
    }
  }

}