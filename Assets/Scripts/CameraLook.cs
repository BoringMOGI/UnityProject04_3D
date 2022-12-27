using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Range(0.1f, 3.0f)]
    [SerializeField] float sensitiveityX;
    [Range(0.1f, 3.0f)]
    [SerializeField] float sensitiveityY;

    [SerializeField] float minRotateX;
    [SerializeField] float maxRotateX;

    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;

    [SerializeField] Transform cam;
    [SerializeField] Transform body;

    

    float rotateX; // X������ ȸ���� ��.
    void Start()
    {
        
        rotateX = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Movement3D2.isLockControl)          // ���� Ȥ�� ��Ż�� �����ϸ� ���콺 ȭ���� ���.
            return;
        
        // ���� ȸ��.
        float x = Input.GetAxis("Mouse X");
        body.Rotate(Vector3.up, x * 180f * sensitiveityX * Time.deltaTime);

        // ���� ȸ��.
        float y = Input.GetAxis("Mouse Y") * -1f;
        rotateX += y * 90f * sensitiveityY * Time.deltaTime;
        rotateX = Mathf.Clamp(rotateX, minRotateX, maxRotateX);
        transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);

        // ���콺 ��
        // ��� �� : +0.1, �ϴ� �� : -0.1, �߸� �� : 0
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if (wheel != 0)
        {
            Vector3 local = cam.localPosition;
            local.z = Mathf.Clamp(local.z + (wheel * 10), minDistance, maxDistance);
            cam.localPosition = local;
        }
    }    


}
