using UnityEngine;

[System.Serializable]
public class ItemObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int quantity;

    public ItemObject(string name, string desc, Sprite iconSprite, int qty = 1)
    {
        itemName = name;
        description = desc;
        icon = iconSprite;
        quantity = qty;
    }
}

