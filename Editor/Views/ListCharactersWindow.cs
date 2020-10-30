using UnityEngine;
using UnityEditor;

namespace Enginn
{

  public class ListCharactersWindow : ScrollableEditorWindow
  {
    Character[] characters;

    private const int nbCharactersPerRow = 2;
    private const int characterBoxWidth = 400;
    private static GUIStyle characterBoxStyle = null;

    // ------------------------------------------------------------------------
    // GUI
    // ------------------------------------------------------------------------

    public ListCharactersWindow()
    {
      titleContent = new GUIContent("Enginn - My characters");
    }

    protected override void OnGUITitle()
    {
      H1("My characters");
    }

    protected static GUIStyle CharacterBoxStyle()
    {
      if(characterBoxStyle == null)
      {
        characterBoxStyle = new GUIStyle();
        characterBoxStyle.normal.background = MakeTexture(
          characterBoxWidth,
          1, // height
          new Color(1.0f, 1.0f, 1.0f, 0.1f)
        );
        characterBoxStyle.margin = new RectOffset(20, 20, 20, 20); // left, right, top, bottom
        characterBoxStyle.padding = new RectOffset(20, 20, 20, 20); // left, right, top, bottom
      }
      return characterBoxStyle;
    }

    protected override void OnGUIContent()
    {
      int idx = -1;

      BeginCenter();

      foreach (Character character in characters)
      {
        idx++;
        if((idx > 0) && (idx % nbCharactersPerRow == 0))
        {
          EndCenter();
          BeginCenter();
        }

        EditorGUILayout.BeginVertical(
          CharacterBoxStyle(),
          GUILayout.Width(characterBoxWidth)
        );

        // AVATAR

        Texture2D avatar = character.GetAvatarTexture();
        if (avatar != null)
        {
          BeginCenter();
          Rect rect = EditorGUILayout.GetControlRect(false, 200); // hasLabel, height
          GUI.DrawTexture(rect, avatar, ScaleMode.ScaleToFit);
          EndCenter();
        }

        // NAME

        P($"<b>{character.name}</b> (#{character.id})");

        // BUTTONS

        GUIStyle style = new GUIStyle();
        style.padding = new RectOffset(0, 0, 10, 0); // left, right, top, bottom
        BeginCenter(style);

        if(GUILayout.Button("Edit", GUILayout.Width(100)))
        {
          Router.EditCharacter(character);
          Close();
        }

        GUILayout.Space(20);

        if(GUILayout.Button("Delete", GUILayout.Width(100)))
        {
          Router.DestroyCharacter(character);
          Close();
        }

        EndCenter();

        EditorGUILayout.EndVertical();
      }

      EndCenter();
    }

    protected override void OnGUIButtons()
    {
      if(GUILayout.Button("Close", GUILayout.Width(100)))
      {
        Close();
      }

      GUILayout.Space(50);

      if(GUILayout.Button("Refresh", GUILayout.Width(100)))
      {
        FetchCharacters();
      }

      GUILayout.Space(50);

      if(GUILayout.Button("New", GUILayout.Width(100)))
      {
        Router.NewCharacter();
        Close();
      }
    }

    // ------------------------------------------------------------------------
    // METHODS
    // ------------------------------------------------------------------------

    public void FetchCharacters()
    {
      // Debug.Log("[ListCharactersWindow] FetchCharacters");
      characters = Api.GetCharacters();
      foreach (Character character in characters)
      {
        character.DownloadAvatar();
      }
    }
  }

}
