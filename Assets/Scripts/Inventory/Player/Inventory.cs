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
        public string[] enumTypesForItems = { "All", "Food", "Weapon", "Apparel", "Crafting", "Ingredient", "Potion", "Scroll", "Quest", "Money" };

        //Equipment and Dropping
        public Transform dropLocation;
        [System.Serializable]
        public struct Equipment
        {
            public string slotName;
            public Transform equipmentLocation;
            public GameObject currentItem;
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
            //playerInv.Add(ItemData.CreateItem(0));
            //playerInv.Add(ItemData.CreateItem(100));
            //playerInv.Add(ItemData.CreateItem(200));
            //playerInv.Add(ItemData.CreateItem(900));
#endif
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            //if (Input.GetKeyDown(KeyCode.KeypadPlus))
            //{
            //    playerInv.Add(ItemData.CreateItem(0));
            //    playerInv.Add(ItemData.CreateItem(100));
            //    playerInv.Add(ItemData.CreateItem(200));
            //    playerInv.Add(ItemData.CreateItem(900));
            //}
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
            GUI.Box(new Rect(4f * GameManager.scr.x, 0.75f * GameManager.scr.y, 3.5f * GameManager.scr.x, 7f * GameManager.scr.y), "");
            GUI.Box(new Rect(4.25f * GameManager.scr.x, 1f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Icon);
            GUI.Box(new Rect(4.55f * GameManager.scr.x, 4f * GameManager.scr.y, 2.5f * GameManager.scr.x, 0.5f * GameManager.scr.y), selectedItem.Name);
            //description test
            //GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.y, 3f * GameManager.scr.y), selectedItem.Description);
            switch (selectedItem.Type)
            {
                case ItemTypes.Food:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount + "\nHeal: " + selectedItem.Heal);
                    if (PlayerHandler.playerHandlerInstance.attributes[0].curValue < PlayerHandler.playerHandlerInstance.attributes[0].maxValue)
                    {
                        if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Eat"))
                        {
                            PlayerHandler.playerHandlerInstance.attributes[0].curValue = Mathf.Clamp(PlayerHandler.playerHandlerInstance.attributes[0].curValue += selectedItem.Heal, 0, PlayerHandler.playerHandlerInstance.attributes[0].maxValue);
                            if (selectedItem.Amount > 1)
                            {
                                selectedItem.Amount--;
                            }
                            else
                            {
                                playerInv.Remove(selectedItem);
                                selectedItem = null;
                                return;
                            }
                        }
                    }
                    break;
                case ItemTypes.Weapon:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nDamage: " + selectedItem.Damage);
                    //if we are not holding a weapon or the weapon we are holding is different
                    if (equipmentSlots[0].currentItem == null || selectedItem.Name != equipmentSlots[0].currentItem.name)
                    {
                        if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Wield"))
                        {
                            if (equipmentSlots[0].currentItem != null)
                            {
                                Destroy(equipmentSlots[0].currentItem);
                            }
                            GameObject curItem = Instantiate(selectedItem.Prefab, equipmentSlots[0].equipmentLocation);
                            curItem.name = selectedItem.Name;
                            equipmentSlots[0].currentItem = curItem;
                        }
                    }
                    else//else we are holding the item already
                    {
                        if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Unwield"))
                        {
                            Destroy(equipmentSlots[0].currentItem);
                        }
                    }
                    break;
                case ItemTypes.Apparel:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nArmor: " + selectedItem.Armour);
                    //if we are wearing the armour or the armour we are wearing is different
                    if (equipmentSlots[1].currentItem == null || selectedItem.Name != equipmentSlots[1].currentItem.name)
                    {
                        if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Wear"))
                        {
                            if (equipmentSlots[1].currentItem != null)
                            {
                                Destroy(equipmentSlots[1].currentItem);
                            }
                            GameObject curItem = Instantiate(selectedItem.Prefab, equipmentSlots[1].equipmentLocation);
                            curItem.name = selectedItem.Name;
                            equipmentSlots[1].currentItem = curItem;
                        }
                    }
                    else//else we are wearing the item already
                    {
                        if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Take off"))
                        {
                            Destroy(equipmentSlots[1].currentItem);
                        }
                    }
                    break;
                case ItemTypes.Crafting:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                    if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Use"))
                    {
                        Debug.LogWarning("Not Written");
                    }
                    break;
                case ItemTypes.Ingredient:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                    if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Use"))
                    {
                        Debug.LogWarning("Not Written");
                    }
                    break;
                case ItemTypes.Potion:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Quaff"))
                    {
                        Debug.LogWarning("Not Written");
                    }
                    break;
                case ItemTypes.Scroll:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                    if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Read"))
                    {
                        Debug.LogWarning("Not Written");
                    }
                    break;
                case ItemTypes.Quest:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                    break;
                case ItemTypes.Money:
                    GUI.Box(new Rect(4.25f * GameManager.scr.x, 4.5f * GameManager.scr.y, 3f * GameManager.scr.x, 3f * GameManager.scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                    if (GUI.Button(new Rect(6.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Use"))
                    {
                        Debug.LogWarning("Not Written");
                    }
                    break;
                default:
                    break;
            }
            //experiment
            if (selectedItem.Type != ItemTypes.Quest)
            {
                if (GUI.Button(new Rect(5.25f * GameManager.scr.x, 7.25f * GameManager.scr.y, 1 * GameManager.scr.x, 0.25f * GameManager.scr.y), "Drop"))
                {
                    GameObject droppedItem = Instantiate(selectedItem.Prefab, dropLocation.position, dropLocation.rotation);
                    droppedItem.name = selectedItem.Name;
                    droppedItem.AddComponent<Rigidbody>().useGravity = true;
                    droppedItem.GetComponent<ItemHandler>().enabled = true;
                    //if the item is equipped
                    for (int i = 0; i < equipmentSlots.Length; i++)
                    {
                        if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                        {
                            //remove item from scene
                            Destroy(equipmentSlots[i].currentItem);
                        }
                    }
                    //if it stacks reduce stack
                    if (selectedItem.Amount > 1)
                    {
                        selectedItem.Amount--;
                    }
                    else//else remove
                    {
                        playerInv.Remove(selectedItem);
                        selectedItem = null;
                        return;
                    }
                }
            }
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
