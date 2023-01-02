using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Text countText;

    public void Setup(Item item)
    {
        bool isEmpty = item.IsEmpty;
        itemImage.enabled = !isEmpty;
        countText.enabled = !isEmpty;
        if (!isEmpty)
        {
            itemImage.sprite = item.itemSprite;
            countText.text = item.level.ToString();
        }
    }
    
}
