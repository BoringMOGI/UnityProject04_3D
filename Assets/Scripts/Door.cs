using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{
    float destination = 0;
    float angle = 0;
    bool isOpen;

    public string Name => isOpen ? "�� �ݱ�" : "�� ����";
    public void OnInteract(Transform order)
    {
        isOpen = !isOpen;
        destination = isOpen ? 90f : 0f;
    }
    public void OpenDoor()
    {
        destination = 90f;
    }

    public void CloseDoor()
    {
        destination = 0f;
    }

    void Update()
    {
        angle = Mathf.MoveTowards(angle, destination, 90f * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
