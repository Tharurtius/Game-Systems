using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int _itemId)
    {
        string _name = "";
        string _description = "";
        int _value = 0;
        int _amount = 0;
        string _icon = "";
        string _prefab = "";
        ItemTypes _type = ItemTypes.Apparel;
        int _damage = 0;
        int _armour = 0;
        int _heal = 0;
        int _weight = 0;
        switch (_itemId)
        {
            #region Food 0-99
            case 0:
                _itemId = 0;
                _name = "Apple";
                _description = "Fell on someone's head";
                _value = 10;
                _amount = 1;
                _icon = "Food/Apple";
                _prefab = "Food/Apple";
                _type = ItemTypes.Food;
                _heal = 10;
                _weight = 1;
                break;
            #endregion
            #region Weapon 100-199
            case 100:
                _itemId = 100;
                _name = "Axe";
                _description = "Cuts trees";
                _value = 10;
                _icon = "Weapon/Axe";
                _prefab = "Weapon/Axe";
                _type = ItemTypes.Weapon;
                _damage = 10;
                _weight = 1;
                break;
            #endregion
            #region Apparel 200-299
            case 200:
                _itemId = 200;
                _name = "Helmet";
                _description = "Has horns like in Skyrim";
                _value = 10;
                _icon = "Apparel/hlm_t_01";
                _prefab = "Apparel/Armour/Helmet";
                _type = ItemTypes.Apparel;
                _armour = 10;
                _weight = 1;
                break;
            #endregion
            #region Crafting 300-399
            #endregion
            #region Ingredient 400-499
            #endregion
            #region Potion 500-599
            #endregion
            #region Scroll 600-699
            #endregion
            #region Quest 700-799
            #endregion
            #region Misc 800-899
            #endregion
            default:
                _itemId = 900;
                _name = "Missingno";
                _description = "You messed up";
                _value = 1;
                _amount = 1;
                _icon = "Scroll/missingno";
                _prefab = "Misc/Missing number";
                _type = ItemTypes.Food;
                _heal = 1;
                _weight = 10;
                break;
        }
        Item temp = new Item()
        {
            Id = _itemId,
            Name = _name,
            Description = _description,
            Value = _value,
            Amount = _amount,
            Icon = Resources.Load("Icon/" + _icon) as Texture2D,
            Prefab = Resources.Load("Prefab/" + _prefab) as GameObject,
            Damage = _damage,
            Armour = _armour,
            Heal = _heal,
            Weight = _weight,
            Type = _type
        };
        return temp;
    }
}
