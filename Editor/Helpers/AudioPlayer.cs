using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Enginn
{
  public class AudioPlayer : MonoBehaviour
  {
    public void Stream(string url)
    {
      if (String.IsNullOrEmpty(url))
      {
        return;
      }
      StartCoroutine(GetAudioClip(url));
    }

    IEnumerator GetAudioClip(string url)
    {
      using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
      {
        yield return www.SendWebRequest();

        if (!String.IsNullOrEmpty(www.error))
        {
          Debug.LogError($"Audio streaming error: {www.error}");
        }
        else
        {
          AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
          AudioSource audioTester = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
          audioTester.clip = clip;
          audioTester.Play();
        }
      }
    }
  }
}
