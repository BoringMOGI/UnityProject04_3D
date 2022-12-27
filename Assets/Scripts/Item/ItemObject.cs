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
        item = itemDB.GetItem(itemTable[Random.Range(0, itemTable.Length)]);  // �����ϰ� ������ �ڵ� 0���� itemTable�� ���̱��� �������� ���.
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
