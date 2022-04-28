using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOneChoiceApproval : DialogueOneChoice
{
    //[Header("Display Dialogue")]
    //public string[] text;
    //[Header("Index Markers")]
    //public int choiceIndex;
    //public int index;
    [Header("How much the NPC likes us")]
    public int approval;
    //[Header("Can we see the DLG")]
    //public bool showDlg;
    [Header("The dialogue based on approval")]
    public string[] likeText;
    public string[] neutralText;
    public string[] dislikeText;

    private void Start()//Start
    {
        text = neutralText;
    }
    //set dialogue and values to neutral

    void OnGUI()
    {
        if (showDlg)
        {
            //Box to display your dialogue
            GUI.Box(new Rect(0, GameManager.scr.y * 6, GameManager.scr.x * 16, GameManager.scr.y * 3), text[index]);
            if (index < text.Length - 1 && index != choiceIndex)//if we are not at the end and not on choice
            {
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Next"))//next
                {
                    index++;
                }
            }
            else if (index == choiceIndex)//else if we are on the choice
            {
                if (GUI.Button(new Rect(GameManager.scr.x * 14f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Yes"))//positive - yes
                {
                    approval = 1;//like you
                    text = likeText;//change what we are reading from
                    index++;//move on
                }
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "No"))//negative - no
                {
                    approval = -1;//like you less
                    text = dislikeText;//change what we are reading from
                    index = text.Length - 1;//skip to last line
                }
            }
            else//else
            {
                if (GUI.Button(new Rect(GameManager.scr.x * 15f, GameManager.scr.y * 8.5f, GameManager.scr.x, GameManager.scr.y * 0.5f), "Bye"))//bye button
                {
                    index = 0;//sets index to 1
                    showDlg = false;//ends convo/exit convo
                    GameManager.gamePlayStates = GamePlayStates.Game;
                }
            }
        }
    }
}