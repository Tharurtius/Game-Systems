using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOneChoice : Dialogue
{
    public int choiceIndex;
    private void OnGUI()
    {
        if (showDlg)
        {
            GUI.Box(new Rect(0, GameManager.scr.y * 6, GameManager.scr.x * 16, GameManager.scr.y * 3), text[index]);
            //box
            if (index < text.Length - 1 && index != choiceIndex) //not choice
            {
                //button - next
                // ++
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Next"))
                {
                    index++;
                }
            }
            else if (index == choiceIndex) //choice
            {
                //button - Yes 14
                //++
                if (GUI.Button(new Rect(GameManager.scr.x * 14f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Yes"))
                {
                    index++;
                }
                //button - No 15
                // end of Dlg
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "No"))
                {
                    index = 4;
                }
            }
            else //bye
            {
                //button - bye
                // 0
                // false
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
