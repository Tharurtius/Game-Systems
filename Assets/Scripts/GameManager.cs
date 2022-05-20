using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GamePlayStates gamePlayStates;
    public static Vector2 scr;
    // Start is called before the first frame update
    void Start()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        gamePlayStates = GamePlayStates.Game;
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width / 16 != scr.x)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
            gamePlayStates = GamePlayStates.Game;
        }
        switch (gamePlayStates) //oly goes through 1 condition
        {
            //A case is the same as an if or else if
            //This is what allows us to check out condition
            case GamePlayStates.PreGame:
                if (!Cursor.visible)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case GamePlayStates.Game:
                if (Cursor.visible)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            case GamePlayStates.MenuPause:
                if (!Cursor.visible)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case GamePlayStates.PostGame:
                if (Cursor.visible)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
                //Default is your else
                //it gets anything that you didnt state above
            default:
                if (!Cursor.visible)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(GameManager.scr.x * (8f - 0.125f), GameManager.scr.y * (4.5f - 0.125f), GameManager.scr.x * 0.25f, GameManager.scr.y * 0.25f), "");
    }
    static public Rect MakeRect(float x, float y, float l, float h)
    {
        Rect space = new Rect(x * GameManager.scr.x, y * GameManager.scr.y, l * GameManager.scr.x, h * GameManager.scr.y);
        return space;
    }
}
public enum GamePlayStates
{
    PreGame,
    Game,
    MenuPause,
    PostGame,
}
