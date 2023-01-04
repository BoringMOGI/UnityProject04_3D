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

    [SerializeField] Vector3[] corners;      // 패널의 각 코너에 대한 위치 값. (순서 : 좌하,좌상,우상,우하)
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
        // 배열 corners에 rectTransform의 각 모서리 좌표를 대입한다.
        // 해당 모서리 좌표가 캔버스 안에 있는지 체크한다.
        rectTransform.GetWorldCorners(corners);
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = canvasRect.InverseTransformPoint(corners[i]);      // world포지션을 rect의 screen point로 변경.
            isInCorners[i] = canvasRect.rect.Contains(corners[i]);
        }

        // 0번,2번 코너의 화면 이탈 여부에 따라 pivot갱신.
        if (!isInCorners[1] && !isInCorners[2])
            rectTransform.pivot = new Vector2(1, 1);
        if (!isInCorners[2] && !isInCorners[3])
            rectTransform.pivot = new Vector2(1, 0);
        if (!isInCorners[0] && !isInCorners[3])
            rectTransform.pivot = new Vector2(0, 0);
    }
}
