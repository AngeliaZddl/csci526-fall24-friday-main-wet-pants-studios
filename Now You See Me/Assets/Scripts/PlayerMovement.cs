using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;  // �ƶ��ٶ�
    public float mouseSensitivity = 2.0f;  // ���������
    public Transform playerCamera;  // �������ͷ

    public bool moveAllowed = true;
    public bool turnAllowed = true;

    private float rotationX = 0.0f;  // X����ת�Ƕ�
    private Vector3 lastPosition;  // ��һ�����λ��
    public float totalDistanceMoved = 0.0f;  // �ۼ��ƶ�����

    void Start()
    {
        UnityServices.InitializeAsync();  // ��ʼ�� Unity ����
        lastPosition = transform.position;  // ��ʼ����һ��λ��Ϊ��ҳ�ʼλ��
    }

    void Update()
    {
        if (moveAllowed)
        {
            // �ƶ�����
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // �����ƶ�����
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(move * speed * Time.deltaTime);

            // �����ƶ�����
            CalculateDistanceMoved();
        }

        if (turnAllowed)
        {
            // ��ת����ͷ
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    // ��������ƶ��ľ���
    void CalculateDistanceMoved()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        totalDistanceMoved += distanceThisFrame;
        lastPosition = transform.position;
    }
}
