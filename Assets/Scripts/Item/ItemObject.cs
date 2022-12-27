using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteraction
{
    [SerializeField] ItemDB itemDB;
    [SerializeField] int[] itemTable;

    Item item;

    private void Start()
    {
        item = itemDB.GetItem(itemTable[Random.Range(0, itemTable.Length)]);  // 랜덤하게 아이템 코드 0에서 itemTable의 길이까지 랜덤으로 드랍.
    }

    public string Name => item.name;
    public void OnInteract(Transform owner)
    {
        Inventory inven = owner.GetComponentInParent<Inventory>();
        if (inven != null)
        {
            inven.AddItem(item);
            Destroy(gameObject);
        }
    }
    
}
