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
                slots[i].Setup(inven.itemList[i]);
            }
        }

        if (gameObject.activeSelf)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;


        return gameObject.activeSelf;
    }
}
