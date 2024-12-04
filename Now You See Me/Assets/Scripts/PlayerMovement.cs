using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;  // �ƶ��ٶ�
    public float mouseSensitivity = 1.0f;  // ���������
    public Transform playerCamera;  // ��������

    public bool moveAllowed = true;
    public bool turnAllowed = true;

    private float rotationX = 0.0f;  // X����ת�Ƕ�
    private Vector3 lastPosition;  // ��һ֡��λ��
    public float totalDistanceMoved = 0.0f;  // ���ƶ�����

    public AudioClip walkingClip;  // ������Ч
    private AudioSource audioSource;  // ��Ƶ������
    private bool isWalking = false;  // �Ƿ���������

    void Start()
    {
        UnityServices.InitializeAsync();  // ��ʼ�� Unity ����
        lastPosition = transform.position;  // ��¼��ʼλ��

        // ��ʼ�� AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkingClip;
        audioSource.loop = true;  // ѭ������
        audioSource.volume = 0.1f;  // ������С
        audioSource.pitch = 1f;  // ������Ƶ�����ٶ�Ϊ������
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

            // �������״̬��������Ч
            bool isCurrentlyWalking = moveX != 0 || moveZ != 0;
            if (isCurrentlyWalking && !isWalking)
            {
                audioSource.Play();  // ������Ч
                isWalking = true;
            }
            else if (!isCurrentlyWalking && isWalking)
            {
                audioSource.Stop();  // ֹͣ������Ч
                isWalking = false;
            }

            // �����ƶ�����
            CalculateDistanceMoved();
        }
        else
        {
            // ����������ƶ�������ֹͣ��Ч
            if (isWalking)
            {
                audioSource.Stop();
                isWalking = false;
            }
        }

        if (turnAllowed)
        {
            // �����ת����
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }


    // �����ƶ�����
    void CalculateDistanceMoved()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        totalDistanceMoved += distanceThisFrame;
        lastPosition = transform.position;
    }
}
