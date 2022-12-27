using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "ItemDB", menuName = "New Item DB")]
// 에셋에 파일을 만들어 내용을 저장하겠다. Scene내부의 오브젝트관리가 아닌 에셋 관리로 광범위하게 사용가능
public class ItemDB : ScriptableObject      // 데이터를 에셋화 시킨다.
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
