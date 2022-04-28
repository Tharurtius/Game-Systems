using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUIGrid : MonoBehaviour
{
    private void OnGUI()
    {
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                //              Start Pos and yy//Size x and y
                GUI.Box(new Rect(GameManager.scr.x * x, GameManager.scr.y * y, GameManager.scr.x, GameManager.scr.x), x + " : " + y);
            }
        }
    }
}
