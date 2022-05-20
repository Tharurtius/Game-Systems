using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Chest
{
    public class Inventory : MonoBehaviour
    {
        public bool showChest;
        public List<Item> chestInv = new List<Item>();
        public Item selectedItemPlayer, selectedItemChest;
        public Vector2 scrollPlayer, scrollChest;
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
                //background
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), gameObject.name);
                //exit button
                if (GUI.Button(GameManager.MakeRect(7, 8, 2, 1), "Exit " + gameObject.name))
                {
                    ShowInv();
                    //remove selected items
                    selectedItemChest = null;
                    selectedItemPlayer = null;
                }
                //actual inventory
                Display();
                if (selectedItemPlayer != null)
                {
                    PlayerItem();
                }
                if (selectedItemChest != null)
                {
                    ChestItem();
                }
            }
        }
        void Display()
        {
            #region Player Inventory
            //player inventory stuff
            if (Player.Inventory.playerInv.Count <= 34)
            {
                for (int i = 0; i < Player.Inventory.playerInv.Count; i++)
                {
                    float height = 0.25f * GameManager.scr.y + i * (0.25f * GameManager.scr.y);
                    height /= GameManager.scr.y;//to compensate for makerect
                    //show a button with the item name
                    if (GUI.Button(GameManager.MakeRect(0.5f, height, 3f, 0.25f), Player.Inventory.playerInv[i].Name))
                    {
                        //if that button is pressed then that item is the item we have selected
                        selectedItemPlayer = Player.Inventory.playerInv[i];
                    }
                }
            }
            else
            {
                scrollPlayer = GUI.BeginScrollView(GameManager.MakeRect(0, 0.25f, 3.75f, 8.5f), scrollPlayer, GameManager.MakeRect(0, 0, 0, Player.Inventory.playerInv.Count * 0.25f), false, true);
                //Elements inside Scroll View
                for (int i = 0; i < Player.Inventory.playerInv.Count; i++)//for all the items in our list
                {
                    //show a button with the item name
                    if (GUI.Button(GameManager.MakeRect(0.5f, i * 0.25f, 3, 0.25f), Player.Inventory.playerInv[i].Name))
                    {
                        //if that button is pressed then that item is the item we have selected
                        selectedItemPlayer = Player.Inventory.playerInv[i];
                    }
                }
            }
            GUI.EndScrollView();
            #endregion
            #region Chest Inventory
            if (chestInv.Count <= 34)
            {
                for (int i = 0; i < chestInv.Count; i++)
                {
                    float height = 0.25f * GameManager.scr.y + i * (0.25f * GameManager.scr.y);
                    height /= GameManager.scr.y;//to compensate for makerect
                    //show a button with the item name
                    if (GUI.Button(GameManager.MakeRect(12.5f, height, 3f, 0.25f), chestInv[i].Name))
                    {
                        //if that button is pressed then that item is the item we have selected
                        selectedItemChest = chestInv[i];
                    }
                }
            }
            else
            {
                scrollChest = GUI.BeginScrollView(GameManager.MakeRect(12, 0.25f, 3.75f, 8.5f), scrollChest, GameManager.MakeRect(12, 0, 0, chestInv.Count * 0.25f), false, true);
                //Elements inside Scroll View
                for (int i = 0; i < chestInv.Count; i++)//for all the items in our list
                {
                    //show a button with the item name
                    if (GUI.Button(GameManager.MakeRect(12.5f, i * 0.25f, 3, 0.25f), chestInv[i].Name))
                    {
                        //if that button is pressed then that item is the item we have selected
                        selectedItemChest = chestInv[i];
                    }
                }
            }
            GUI.EndScrollView();
            #endregion
        }
        void PlayerItem()
        {
            if (selectedItemPlayer != null)
            {
                //player item
                GUI.Box(GameManager.MakeRect(4f, 0.75f, 3.5f, 7f), "");
                GUI.Box(GameManager.MakeRect(4.25f, 1f, 3f, 3f), selectedItemPlayer.Icon);
                GUI.Box(GameManager.MakeRect(4.55f, 4f, 2.5f, 0.5f), selectedItemPlayer.Name);
            }
            string itemDescription;
            if (selectedItemPlayer.Amount < 0)
            {
                itemDescription = selectedItemPlayer.Description + "\nValue: " + selectedItemPlayer.Value + "\nAmount: " + selectedItemPlayer.Amount;
            }
            else
            {
                itemDescription = selectedItemPlayer.Description + "\nValue: " + selectedItemPlayer.Value;
            }
            GUI.Box(GameManager.MakeRect(4.25f, 4.5f, 3f, 3f), itemDescription);
        }
        void ChestItem()
        {
            if (selectedItemChest != null)
            {
                //chest item
                GUI.Box(GameManager.MakeRect(8.75f, 0.75f, 3.5f, 7f), "");
                GUI.Box(GameManager.MakeRect(9f, 1f, 3f, 3f), selectedItemChest.Icon);
                GUI.Box(GameManager.MakeRect(9.3f, 4f, 2.5f, 0.5f), selectedItemChest.Name);
            }
            string itemDescription;
            if (selectedItemChest.Amount < 0)
            {
                itemDescription = selectedItemChest.Description + "\nValue: " + selectedItemChest.Value + "\nAmount: " + selectedItemChest.Amount;
            }
            else
            {
                itemDescription = selectedItemChest.Description + "\nValue: " + selectedItemChest.Value;
            }
            GUI.Box(GameManager.MakeRect(9f, 4.5f, 3f, 3f), itemDescription);
        }
        public void ShowInv()
        {
            showChest = !showChest;
            GameManager.gamePlayStates = showChest ? GamePlayStates.MenuPause : GamePlayStates.Game;
        }
        #region Debug stuff
        //spawn shit
        private void Start()
        {
            //#if UNITY_EDITOR
            //            chestInv.Add(ItemData.CreateItem(0));
            //            chestInv.Add(ItemData.CreateItem(100));
            //            chestInv.Add(ItemData.CreateItem(200));
            //            chestInv.Add(ItemData.CreateItem(900));
            //#endif
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                chestInv.Add(ItemData.CreateItem(0));
                chestInv.Add(ItemData.CreateItem(100));
                chestInv.Add(ItemData.CreateItem(200));
                chestInv.Add(ItemData.CreateItem(900));
            }
#endif
        }
        #endregion
    }
}
