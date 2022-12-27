using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList;

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
