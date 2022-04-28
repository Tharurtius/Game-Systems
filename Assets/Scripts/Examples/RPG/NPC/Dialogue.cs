using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Display Dialogue")]
    public string[] text;
    [Header("Can we see the DLG")]
    public bool showDlg;
    [Header("Index Markers")]
    public int index;
    private void OnGUI()
    {
        if (showDlg)
        {
            //box
            //entire bottom third of the screen
            GUI.Box(new Rect(0, GameManager.scr.y * 6, GameManager.scr.x * 16, GameManager.scr.y*3), text[index]);
            //button
            if (index < text.Length - 1)
            {
                //button thank you NEXT
                //1 ++
                //bottom right corner, the button is half its normal size
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Next"))
                {
                    index++;
                }
            }
            else
            {
                //button to exit the dialogue
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Bye"))
                {
                    index = 0;
                    showDlg = false;
                    GameManager.gamePlayStates = GamePlayStates.Game;
                }
            }
        }
    }
}
