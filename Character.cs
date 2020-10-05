using UnityEngine;

namespace Enginn
{

  [System.Serializable]
  public class Character
  {

    public string id;
    public string name;
    public string created_at;
    public string updated_at;
    public int project_id;

  }

  [System.Serializable]
  public class Characters
  {
    public Character[] characters;
  }

}
