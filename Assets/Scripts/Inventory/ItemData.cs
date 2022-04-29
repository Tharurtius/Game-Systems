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

            #endregion
            #region Weapon 100-199
            #endregion
            #region Apparel 200-299
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
                _itemId = 0;
                _name = "Missingno";
                _description = "You messed up";
                _value = 1;
                _amount = 1;
                _icon = "Food/Apple";
                _prefab = "Food/Apple";
                _type = ItemTypes.Misc;
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
            Weight = _weight
        };
        return temp;
    }
}
