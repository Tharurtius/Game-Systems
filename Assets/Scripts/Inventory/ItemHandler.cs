using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemHandler : MonoBehaviour
{
    public int itemId;
    public int amount;
    public ItemTypes type;

    public void OnCollection()
    {
        if (type == ItemTypes.Money)
        {
            Inventory.Player.Inventory.money += amount;
        }
        else if (type == ItemTypes.Weapon || type == ItemTypes.Apparel || type == ItemTypes.Quest)
        {
            Inventory.Player.Inventory.playerInv.Add(ItemData.CreateItem(itemId));
        }
        else
        {
            bool found = false;
            int addIndex = 0;
            for (int i = 0; i < Inventory.Player.Inventory.playerInv.Count; i++)
            {
                if (itemId == Inventory.Player.Inventory.playerInv[i].Id)
                {
                    found = true;
                    addIndex = i;
                    break;
                }
            }
            if (found)
            {
                Inventory.Player.Inventory.playerInv[addIndex].Amount += amount;
            }
            else
            {
                Inventory.Player.Inventory.playerInv.Add(ItemData.CreateItem(itemId));
                Inventory.Player.Inventory.playerInv.Last<Item>().Amount = amount;
            }
        }
        Destroy(gameObject);
    }
}
