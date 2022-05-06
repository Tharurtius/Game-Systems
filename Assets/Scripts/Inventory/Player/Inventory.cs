using System.Collections.Generic; //lists and dictionaries
using UnityEngine;
namespace Inventory.Player
{
    public class Inventory : MonoBehaviour
    {
        #region Variables
        //items and our inventory
        public static List<Item> playerInv = new List<Item>();
        public Item selectedItem;
        public static bool showInv;
        public static int money;

        //Scrolling and Sorting
        public Vector2 scrollPos;
        public string sortType = "All";
        public string[] enumTypesForItems = { "All", "Food", "Weapon", "Apparel", "Crafting", "Ingredient", "Potion", "Scroll", "Quest", "Misc" };

        //Equipment and Dropping
        public Transform dropLocation;
        [System.Serializable]
        public struct Equipment
        {
            public string slotName;
            public Transform equipmentLocation;
            public GameObject currentShop;
        }
        public Equipment[] equipmentSlots;
        //Other Inventories
        public static Chest.Inventory currentChest;
        public static Shop.Inventory currentShop;

        #endregion
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR 
            playerInv.Add(ItemData.CreateItem(0));
#endif
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                playerInv.Add(ItemData.CreateItem(0));
            }
#endif
            //if inventory key is pressed
            if (Input.GetKeyDown(KeyBinds.keys["Inventory"]))
            {
                //toggle both show inventory and game state
                showInv = !showInv;//makes the value the opposite value
                GameManager.gamePlayStates = showInv ? GamePlayStates.MenuPause : GamePlayStates.Game;
            }
        }
        void Display()
        {
            //if we want to display everything in our inventory
            if (sortType == "All")
            {
                //if we have 34 or less (space at top and bottom)
                if (playerInv.Count <= 34)
                {
                    for (int i = 0; i < playerInv.Count; i++)
                    {
                        //show a button with the item name
                        if (GUI.Button(new Rect(0.5f * GameManager.scr.x, 0.25f * GameManager.scr.y + i * (0.25f * GameManager.scr.y), 3f * GameManager.scr.x, 0.25f * GameManager.scr.y), playerInv[i].Name))
                        {
                            //if that button is pressed then that item is the item we have selected
                            selectedItem = playerInv[i];
                        }
                    }

                }
                else //more than 34 items
                {
                    //our movable scroll position
                    scrollPos =
                    //the start of our viewable area
                    GUI.BeginScrollView(
                    //our View Window
                    new Rect(0, 0.25f * GameManager.scr.y, 3.75f * GameManager.scr.x, 8.5f * GameManager.scr.y),
                    //our current scroll position within that window
                    scrollPos,
                    //scroll zone (extra space) that we can move within the View Window
                    new Rect(0, 0, 0, playerInv.Count * 0.25f * GameManager.scr.y),
                    //can we see the horizonral bar?
                    false,
                    //can we see the vertical bar?
                    true);
                    #region Elements inside Scroll View
                    for (int i = 0; i < playerInv.Count; i++)//for all the items in our list
                    {
                        //show a button with the item name
                        if (GUI.Button(new Rect(0.5f * GameManager.scr.x, i * (0.25f * GameManager.scr.y), 3 * GameManager.scr.x, 0.25f * GameManager.scr.y), playerInv[i].Name))
                        {
                            //if that button is pressed then that item is the item we have selected
                            selectedItem = playerInv[i];
                        }
                    }
                    #endregion
                    //End Scroll View else you get a pushing more clis error
                    GUI.EndScrollView();
                }
            }
            //else we are displaying based off Type
            else
            {
                ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
                //amount of that type
                int a = 0;
                //slot position
                int s = 0;
                //search our list and if we find a match 
                for (int i = 0; i < playerInv.Count; i++)
                {
                    if (playerInv[i].Type == type)
                    {
                        a++;//increase the count of that type so we know how many we have
                    }
                }
                //we are less than or equal to 34 items of this type
                if (a <= 34)
                {
                    for (int i = 0; i < playerInv.Count; i++)
                    {
                        if (playerInv[i].Type == type)
                        {
                            //show a button with the item name
                            if (GUI.Button(new Rect(0.5f * GameManager.scr.x, 0.25f * GameManager.scr.y + s * (0.25f * GameManager.scr.y), 3f * GameManager.scr.x, 0.25f * GameManager.scr.y), playerInv[i].Name))
                            {
                                //if that button is pressed then that item is the item we have selected
                                selectedItem = playerInv[i];
                            }
                            s++;
                        }
                    }
                }
                else//more than 34 items of this type
                {
                    scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * GameManager.scr.y, 3.75f * GameManager.scr.x, 8.5f * GameManager.scr.y), scrollPos, new Rect(0, 0, 0, a * 0.25f * GameManager.scr.y), false, true);
                    #region Elements inside Scroll View
                    for (int i = 0; i < playerInv.Count; i++)//for all the items in our list
                    {
                        if (playerInv[i].Type == type)
                        {
                            //show a button with the item name
                            if (GUI.Button(new Rect(0.5f * GameManager.scr.x, s * (0.25f * GameManager.scr.y), 3 * GameManager.scr.x, 0.25f * GameManager.scr.y), playerInv[i].Name))
                            {
                                //if that button is pressed then that item is the item we have selected
                                selectedItem = playerInv[i];
                            }
                            s++;
                        }
                    }
                    #endregion
                    //End Scroll View else you get a pushing more clis error
                    GUI.EndScrollView();
                }
            }
        }

        void UseItem()
        {
            //boxes
            //empty - 4/0.75/3.5/7
            //icon - 4.25/1/3/3 selectedItem.Icon
            //name - 4.55/4/2.5/0.5 selectedItem.Name
            GUI.Box(new Rect(4f * GameManager.scr.x, 0.75f * GameManager.scr.y, 3.5f * GameManager.scr.x, 7 * GameManager.scr.y), "");
            GUI.Box(new Rect(4.25f * GameManager.scr.x, 1f* GameManager.scr.y, 3 * GameManager.scr.x, 3 * GameManager.scr.y), ))
        }
        private void OnGUI()
        {
            if (showInv)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
                for (int i = 0; i < enumTypesForItems.Length; i++)
                {
                    if (GUI.Button(new Rect(4f * GameManager.scr.x + i * GameManager.scr.x, GameManager.scr.y * 0.5f, GameManager.scr.x, 0.25f * GameManager.scr.y), enumTypesForItems[i]))
                    {
                        sortType = enumTypesForItems[i];
                    }
                }
                Display();
                if (selectedItem != null)
                {
                    UseItem();
                }
            }
        }
    }
}
