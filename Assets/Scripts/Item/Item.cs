using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public int itemCode;
    public int level;
    public int power;
    public Sprite itemSprite;

    public bool IsEmpty => itemCode == -1;

    public Item()
    {
        name = string.Empty;
        itemCode = -1;
        level = -1;
        power = -1;
        itemSprite = null;
    }

    public Item(string name, int itemCode, int level, int power, Sprite itemSprite)
    {
        this.name = name;
        this.itemCode = itemCode;
        this.level = level;
        this.power = power;
        this.itemSprite = itemSprite;
    }

    public Item GetCopy()
    {
        return new Item(name, itemCode, level, power, itemSprite);         
    }
}
