using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Shop
{
    public class Inventory : MonoBehaviour
    {
        public bool showShop;
        public List<Item> shopInv = new List<Item>();
        public Item selectedItemPlayer, selectedItemShop;
        public Vector2 scrollPlayer, scrollShop;
        /*
        1 create a ui screen where you can see both the chest and player inventory
        2 randomize items in chest
        3 randomize size of items in chest
        4 move items between both the player inventory and chest inventory
        
        a shop is a chest where you move things and gain or lose money
        */
        private void OnGUI()
        {
            if (showShop)
            {
                //background
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), gameObject.name);
                //exit button
                if (GUI.Button(GameManager.MakeRect(7, 8, 2, 1), "Exit " + gameObject.name))
                {
                    ShowInv();
                    //remove selected items
                    selectedItemShop = null;
                    selectedItemPlayer = null;
                }
                //actual inventory
                Display();
                if (selectedItemPlayer != null)
                {
                    PlayerItem();
                }
                if (selectedItemShop != null)
                {
                    ShopItem();
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
            if (shopInv.Count <= 34)
            {
                for (int i = 0; i < shopInv.Count; i++)
                {
                    float height = 0.25f * GameManager.scr.y + i * (0.25f * GameManager.scr.y);
                    height /= GameManager.scr.y;//to compensate for makerect
                    //show a button with the item name
                    if (GUI.Button(GameManager.MakeRect(12.5f, height, 3f, 0.25f), shopInv[i].Name))
                    {
                        //if that button is pressed then that item is the item we have selected
                        selectedItemShop = shopInv[i];
                    }
                }
            }
            else
            {
                scrollShop = GUI.BeginScrollView(GameManager.MakeRect(12, 0.25f, 3.75f, 8.5f), scrollShop, GameManager.MakeRect(12, 0, 0, shopInv.Count * 0.25f), false, true);
                //Elements inside Scroll View
                for (int i = 0; i < shopInv.Count; i++)//for all the items in our list
                {
                    //show a button with the item name
                    if (GUI.Button(GameManager.MakeRect(12.5f, i * 0.25f, 3, 0.25f), shopInv[i].Name))
                    {
                        //if that button is pressed then that item is the item we have selected
                        selectedItemShop = shopInv[i];
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
            if (selectedItemPlayer.Amount > 0)
            {
                itemDescription = selectedItemPlayer.Description + "\nSell Price: " + selectedItemPlayer.Sell + "\nAmount: " + selectedItemPlayer.Amount;
            }
            else
            {
                itemDescription = selectedItemPlayer.Description + "\nSell Price: " + selectedItemPlayer.Sell;
            }
            GUI.Box(GameManager.MakeRect(4.25f, 4.5f, 3f, 3f), itemDescription);

            //item give
            if (GUI.Button(GameManager.MakeRect(5.25f, 7.25f, 1, 0.25f), "Sell"))
            {
                //get money
                Player.Inventory.money += selectedItemPlayer.Sell;
                bool found = false;
                int addindex = 0;
                Item target;
                //if item is stackable
                if (selectedItemPlayer.Amount > 0)
                {
                    //find item in chest
                    for (int i = 0; i < shopInv.Count; i++)
                    {
                        target = shopInv[i];
                        if (selectedItemPlayer.Id == target.Id)
                        {
                            found = true;
                            addindex = i;
                            break;
                        }
                    }
                    //if found
                    if (found)
                    {
                        shopInv[addindex].Amount++;
                    }
                    else//add item
                    {
                        shopInv.Add(ItemData.CreateItem(selectedItemPlayer.Id));
                    }
                }
                //if item isnt stackable
                else
                {
                    shopInv.Add(ItemData.CreateItem(selectedItemPlayer.Id));
                }
                //remove from players end
                if (selectedItemPlayer.Amount > 1)
                {
                    selectedItemPlayer.Amount--;
                }
                else
                {
                    Player.Inventory.playerInv.Remove(selectedItemPlayer);
                    selectedItemPlayer = null;
                    return;
                }
            }
        }
        void ShopItem()
        {
            if (selectedItemShop != null)
            {
                //chest item
                GUI.Box(GameManager.MakeRect(8.75f, 0.75f, 3.5f, 7f), "");
                GUI.Box(GameManager.MakeRect(9f, 1f, 3f, 3f), selectedItemShop.Icon);
                GUI.Box(GameManager.MakeRect(9.3f, 4f, 2.5f, 0.5f), selectedItemShop.Name);
            }
            string itemDescription;
            if (selectedItemShop.Amount > 0)
            {
                itemDescription = selectedItemShop.Description + "\nBuy Price: " + selectedItemShop.Buy + "\nAmount: " + selectedItemShop.Amount;
            }
            else
            {
                itemDescription = selectedItemShop.Description + "\nBuy Price: " + selectedItemShop.Buy;
            }
            GUI.Box(GameManager.MakeRect(9f, 4.5f, 3f, 3f), itemDescription);
            //item take
            if (GUI.Button(GameManager.MakeRect(10f, 7.25f, 1, 0.25f), "Buy"))
            {
                if (Player.Inventory.money >= selectedItemShop.Buy)
                {
                    Player.Inventory.money -= selectedItemShop.Buy;
                    bool found = false;
                    int addindex = 0;
                    Item target;
                    //if item is stackable
                    if (selectedItemShop.Amount > 0)
                    {
                        //find item in player
                        for (int i = 0; i < Player.Inventory.playerInv.Count; i++)
                        {
                            target = Player.Inventory.playerInv[i];
                            if (selectedItemShop.Id == target.Id)
                            {
                                found = true;
                                addindex = i;
                                break;
                            }
                        }
                        //if found
                        if (found)
                        {
                            Player.Inventory.playerInv[addindex].Amount++;
                        }
                        else//add item
                        {
                            Player.Inventory.playerInv.Add(ItemData.CreateItem(selectedItemShop.Id));
                        }
                    }
                    //if item isnt stackable
                    else
                    {
                        Player.Inventory.playerInv.Add(ItemData.CreateItem(selectedItemShop.Id));
                    }
                    //remove from players end
                    if (selectedItemShop.Amount > 1)
                    {
                        selectedItemShop.Amount--;
                    }
                    else
                    {
                        shopInv.Remove(selectedItemShop);
                        selectedItemShop = null;
                        return;
                    }
                }
                else
                {
                    Debug.Log("Cannot afford item");
                }
            }
        }
        public void ShowInv()
        {
            showShop = !showShop;
            //GameManager.gamePlayStates = showChest ? GamePlayStates.MenuPause : GamePlayStates.Game;
            //Uses GameManager static function
            if (showShop)
            {
                GameManager.PauseGame();
            }
            else
            {
                GameManager.UnPauseGame();
            }
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
                shopInv.Add(ItemData.CreateItem(0));
                shopInv.Add(ItemData.CreateItem(100));
                shopInv.Add(ItemData.CreateItem(200));
                shopInv.Add(ItemData.CreateItem(900));
            }
#endif
        }
        #endregion
    }
}