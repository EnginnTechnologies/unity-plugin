using System;
using System.Net;

namespace Enginn
{

  public class ApiClient
  {

    private WebClient webClient;

    public ApiClient(string projectApiToken)
    {
      webClient = new WebClient();
      webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
      webClient.Headers.Add("Authorization", $"Bearer {projectApiToken}");
    }

    public string DownloadString(string address)
    {
      Logger.Log($"[API] GET {address}");
      return webClient.DownloadString(address);
    }

    public string DownloadString (Uri address)
    {
      Logger.Log($"[API] GET {address}");
      return webClient.DownloadString(address);
    }

    public string UploadString(string address, string data)
    {
      Logger.Log($"[API] POST {address}");
      return webClient.UploadString(address, data);
    }

    public string UploadString(Uri address, string data)
    {
      Logger.Log($"[API] POST {address}");
      return webClient.UploadString(address, data);
    }

    public string UploadString(string address, string method, string data)
    {
      Logger.Log($"[API] {method} {address}");
      return webClient.UploadString(address, method, data);
    }

    public string UploadString(Uri address, string method, string data)
    {
      Logger.Log($"[API] {method} {address}");
      return webClient.UploadString(address, method, data);
    }

  }

}
