using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "ItemDB", menuName = "New Item DB")]
// ���¿� ������ ����� ������ �����ϰڴ�. Scene������ ������Ʈ������ �ƴ� ���� ������ �������ϰ� ��밡��
public class ItemDB : ScriptableObject      // �����͸� ����ȭ ��Ų��.
{
    [SerializeField] Item[] items;

    public Item GetItem(int itemCode)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].itemCode == itemCode)
                return items[i].GetCopy();
        }

        return null;
    }
}
