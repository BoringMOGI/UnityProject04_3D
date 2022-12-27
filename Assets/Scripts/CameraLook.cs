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

    

    float rotateX; // X축으로 회전한 값.
    void Start()
    {
        
        rotateX = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Movement3D2.isLockControl)          // 보스 혹은 포탈에 접촉하면 마우스 화면이 잠김.
            return;
        
        // 수평 회전.
        float x = Input.GetAxis("Mouse X");
        body.Rotate(Vector3.up, x * 180f * sensitiveityX * Time.deltaTime);

        // 수직 회전.
        float y = Input.GetAxis("Mouse Y") * -1f;
        rotateX += y * 90f * sensitiveityY * Time.deltaTime;
        rotateX = Mathf.Clamp(rotateX, minRotateX, maxRotateX);
        transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);

        // 마우스 휠
        // 상단 휠 : +0.1, 하단 휠 : -0.1, 중립 휠 : 0
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if (wheel != 0)
        {
            Vector3 local = cam.localPosition;
            local.z = Mathf.Clamp(local.z + (wheel * 10), minDistance, maxDistance);
            cam.localPosition = local;
        }
    }    


}
