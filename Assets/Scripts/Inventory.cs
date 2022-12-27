using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> inven;

    private void Start()
    {
        inven = new List<Item>();
    }

    public void AddItem(Item item)
    {
        inven.Add(item);
    }

}
