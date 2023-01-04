using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] GameObject panel;
    [SerializeField] RectTransform slotParent;
    [SerializeField] ItemInfoPanel infoPanel;

    ItemSlotUI[] slots;

    public bool isOpenInven => panel.activeSelf;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        panel.SetActive(false);

        slots = slotParent.GetComponentsInChildren<ItemSlotUI>();
        infoPanel.gameObject.SetActive(false);
    }

    public bool Switch(Item[] itemList)
    {
        infoPanel.gameObject.SetActive(false);  // 최초에 인벤토리가 열릴때 끄자.
        panel.SetActive(!panel.activeSelf);     // ON, OFF를 반대로 돌린다.

        if(panel.activeSelf)
        {
            for (int i = 0; i < slots.Length; i++)
                slots[i].Setup(itemList[i]);
        }

        // 인벤토리 ui가 나올때 마우스 보이게하기
        if(panel.activeSelf)                                
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        return panel.activeSelf;
    }   
    public void OnSelectedSlot(ItemSlotUI slot)
    {
        infoPanel.OnOpenPanel(slot.GetComponent<RectTransform>());
    }
    public void OnDeselectedSlot()
    {
        infoPanel.gameObject.SetActive(false);
    }

}

