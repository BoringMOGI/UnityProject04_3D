using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] Inventory inven;
    [SerializeField] ItemSlotUI[] slots;

    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public bool Switch()
    {
        gameObject.SetActive(!gameObject.activeSelf);  // On, Off를 반대로 돌린다.
        if(gameObject.activeSelf)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inven.itemList.Count)
                    slots[i].Setup(inven.itemList[i]);
                else
                    slots[i].Setup(null);
            }
        }

        return gameObject.activeSelf;
    }
}
