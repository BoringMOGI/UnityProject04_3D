using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int MAX_COUNT = 28;        // 최대 소지수.
    public Item[] itemList;

    void Start()
    {
        itemList = new Item[MAX_COUNT];
        for(int i = 0; i<itemList.Length; i++)
            itemList[i] = new Item();
    }
    public void AddItem(Item item)
    {
        for(int i = 0; i < itemList.Length; i++)
        {
            if(itemList[i].IsEmpty)
            {
                itemList[i] = item;
                return;
            }
        }
    }
}
