using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace Enginn
{

  public class Api
  {

    //-------------------------------------------------------------------------
    // CHARACTERS
    //-------------------------------------------------------------------------

    public static Character[] GetCharacters() {
      var response = NewClient().DownloadString($"{GetApiBaseUrl()}/characters");
      return JsonUtility.FromJson<ApiResponse<Character[]>>(response).result;
    }

    public static void CreateCharacter(Character character) {
      var payload = "{\"character\": " + JsonUtility.ToJson(character) + "}";
      var response = NewClient().UploadString($"{GetApiBaseUrl()}/characters", payload);
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

    public static void UpdateCharacter(Character character) {
      var payload = "{\"character\": " + JsonUtility.ToJson(character) + "}";
      var response = NewClient().UploadString(
        $"{GetApiBaseUrl()}/characters/{character.id}",
        WebRequestMethods.Http.Put,
        payload
      );
      Debug.Log($"API response: {response}");
      var apiResponse = JsonUtility.FromJson<ApiResponse<Character>>(response);

      switch (apiResponse.status)
      {
        case 200:
          character.updated_at = apiResponse.result.updated_at;
          break;
        case 422:
          Debug.Log($"API errors: {apiResponse.GetErrorsAsJson()}");
          character.SetErrors(apiResponse.GetErrorsDictionnary());
          break;
        default:
          throw new WebException($"API error {apiResponse.status}");
      }
    }

    public static void DestroyCharacter(Character character) {
      var response = NewClient().UploadString(
        $"{GetApiBaseUrl()}/characters/{character.id}",
        "DELETE",
        ""
      );
      Debug.Log($"API response: {response}");
      var apiResponse = JsonUtility.FromJson<ApiResponse<Character>>(response);

      switch (apiResponse.status)
      {
        case 204:
          break;
        case 422:
          Debug.Log($"API errors: {apiResponse.GetErrorsAsJson()}");
          character.SetErrors(apiResponse.GetErrorsDictionnary());
          break;
        default:
          throw new WebException($"API error {apiResponse.status}");
      }
    }

    //-------------------------------------------------------------------------
    // CHARACTER SYNTHESES
    //-------------------------------------------------------------------------

    public static void CreateCharacterSynthesis(CharacterSynthesis characterSynthesis) {
      var payload = "{\"character_synthesis\": " + JsonUtility.ToJson(characterSynthesis) + "}";
      var response = NewClient().UploadString($"{GetApiBaseUrl()}/character_syntheses", payload);
      Debug.Log($"API response: {response}");
      var apiResponse = JsonUtility.FromJson<ApiResponse<CharacterSynthesis>>(response);

      switch (apiResponse.status)
      {
        case 201:
          characterSynthesis.id = apiResponse.result.id;
          characterSynthesis.created_at = apiResponse.result.created_at;
          characterSynthesis.synthesis_id = apiResponse.result.synthesis_id;
          characterSynthesis.synthesis_result_file_url = apiResponse.result.synthesis_result_file_url;
          break;
        case 422:
          Debug.Log($"API errors: {apiResponse.GetErrorsAsJson()}");
          characterSynthesis.SetErrors(apiResponse.GetErrorsDictionnary());
          break;
        default:
          throw new WebException($"API error {apiResponse.status}");
      }
    }

    //-------------------------------------------------------------------------
    // HELPERS
    //-------------------------------------------------------------------------

    public static Texture2D DownloadImage(string url) {
      // Debug.Log($"[Api] DownloadImage {url}");
      var client = new WebClient();
      byte[] data = client.DownloadData(url);
      Texture2D texture = new Texture2D(2, 2);
      texture.LoadImage(data);
      return texture;
    }

    public static bool DownloadWav(string url, string path) {
      // Debug.Log($"[Api] DownloadWav {url}");
      var client = new WebClient();
      byte[] data = client.DownloadData(url);

      if(File.Exists(path))
      {
        Debug.LogError($"File at {path} already exists");
        return false;
      }

      File.WriteAllBytes(path, data);
      return true;
    }

    private static string GetApiBaseUrl()
    {
      return EditorSettings.GetApiBaseUrl();
    }

    private static WebClient NewClient() {
      var client = new WebClient();
      client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
      client.Headers.Add("Authorization", $"Bearer {ProjectSettings.GetProjectApiToken()}");
      return client;
    }

  }

  [Serializable]
  internal class ApiError
  {
    public string attribute;
    public string error;

    public void SetAttribute(string newAttribute)
    {
      attribute = newAttribute;
    }

    public void SetError(string newError)
    {
      error = newError;
    }
  }

  [Serializable]
  internal class ApiResponse<T>
  {
    public int status;
    public ApiError[] errors;
    public T result;

    public void SetStatus(int newStatus)
    {
      status = newStatus;
    }

    public void SetErrors(ApiError[] newErrors)
    {
      errors = newErrors;
    }

    public void SetResult(T newResult)
    {
      result = newResult;
    }

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
