using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game/Inventory")]
public class Inventory : ScriptableObject
{
    public static int MAX_COUNT = 28;        // 최대 소지수.
    public Item[] itemList;

    public void Initialize()
    {
        itemList = new Item[MAX_COUNT];
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i< itemList.Length; i++)
        {
            if (itemList[i].IsEmpty)
            {
                itemList[i] = item;
                return;
            }
        }
    }
}
