using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Chest
{
    public bool showChest;
    public class Inventory : MonoBehaviour
    {
        /*
        1 create a ui screen where you can see both the chest and player inventory
        2 randomize items in chest
        3 randomize size of items in chest
        4 move items between both the player inventory and chest inventory
        
        a shop is a chest where you move things and gain or lose money
        */
        private void OnGUI()
        {
            if (showChest)
            {

            }
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
        }
    }
}
