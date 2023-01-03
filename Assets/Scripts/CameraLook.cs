using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void Rotate(Vector2 _input)
    {
        // ���� ȸ��.
        float x = _input.x;
        body.Rotate(Vector3.up, x * 180f * sensitiveityX * Time.deltaTime);

        // ���� ȸ��.
        float y = _input.y * -1f;
        rotateX += y * 90f * sensitiveityY * Time.deltaTime;
        rotateX = Mathf.Clamp(rotateX, minRotateX, maxRotateX);
        transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);                
    }    
    public void Zoom(bool isZoom)
    {
        // ���콺 ��
        // ��� �� : +0.1, �ϴ� �� : -0.1, �߸� �� : 0
        // float wheel = Input.GetAxis("Mouse ScrollWheel");

        Vector3 local = cam.localPosition;
        float value = isZoom ? -1 : 1;

        local.z = Mathf.Clamp(local.z + (value * 10), minDistance, maxDistance);
        cam.localPosition = local;
    }


}
