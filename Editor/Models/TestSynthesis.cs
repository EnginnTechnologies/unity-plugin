using System;
using System.IO;
using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class TestSynthesis : Model
  {

    public int id;
    public string modifier = Synthesis.Modifier.None;
    public string text;
    public string character_gender;
    public string character_pitch;
    public string character_age;
    public bool character_is_nasal;
    public int synthesis_id;
    public string synthesis_result_file_url;

    public TestSynthesis(Character character, string s_text, string s_modifier = null)
    {
      character_gender = character.gender;
      character_pitch = character.pitch;
      character_age = character.age;
      character_is_nasal = character.is_nasal;
      text = s_text;
      modifier = s_modifier;
    }

    public bool Create()
    {
      Api.CreateTestSynthesis(this);
      return id > 0;
    }

  }

}
