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
        itemImage.enabled = (item != null);
        countText.enabled = (item != null);
        if (item != null)
        {
            itemImage.sprite = item.itemSprite;
            countText.text = item.level.ToString();
        }
        
    }
    
}
