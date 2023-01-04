using System.Collections;
using UnityEngine;

public class ItemInfoPanel : MonoBehaviour
{
    private enum CORNER
    {
        LeftDown,
        LeftUp,
        RightUp,
        RightDown,
    }

    [SerializeField] RectTransform canvasRect;

    RectTransform rectTransform;

    [SerializeField] Vector3[] corners;      // �г��� �� �ڳʿ� ���� ��ġ ��. (���� : ����,�»�,���,����)
    [SerializeField] bool[] isInCorners;

    public void OnOpenPanel(RectTransform slot)
    {
        Init();

        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.position = slot.position;
        UpdatePivot();

        rectTransform.position = slot.position;
        gameObject.SetActive(true);
    }

    private void Init()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            corners = new Vector3[4];
            isInCorners = new bool[4];
        }
    }
    private void UpdatePivot()
    {
        // �迭 corners�� rectTransform�� �� �𼭸� ��ǥ�� �����Ѵ�.
        // �ش� �𼭸� ��ǥ�� ĵ���� �ȿ� �ִ��� üũ�Ѵ�.
        rectTransform.GetWorldCorners(corners);
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = canvasRect.InverseTransformPoint(corners[i]);      // world�������� rect�� screen point�� ����.
            isInCorners[i] = canvasRect.rect.Contains(corners[i]);
        }

        // 0��,2�� �ڳ��� ȭ�� ��Ż ���ο� ���� pivot����.
        if (!isInCorners[1] && !isInCorners[2])
            rectTransform.pivot = new Vector2(1, 1);
        if (!isInCorners[2] && !isInCorners[3])
            rectTransform.pivot = new Vector2(1, 0);
        if (!isInCorners[0] && !isInCorners[3])
            rectTransform.pivot = new Vector2(0, 0);
    }
}
