using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Text countText;
    [SerializeField] GameObject selectedPanel;

    // Item은 Serialize(직렬화) 때문에 null이 될 수 없다.
    public void Setup(Item item)
    {
        selectedPanel.SetActive(false);

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
