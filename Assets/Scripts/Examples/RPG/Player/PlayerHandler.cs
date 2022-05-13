using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Stats
{
    //singleton
    public static PlayerHandler playerHandlerInstance;
    private void Awake()
    {
        //There can only be one
        if (playerHandlerInstance != null && playerHandlerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            playerHandlerInstance = this;
        }
    }
}
