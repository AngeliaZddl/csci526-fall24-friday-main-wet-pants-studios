using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;  // 移动速度
    public float mouseSensitivity = 1.0f;  // 鼠标灵敏度
    public Transform playerCamera;  // 玩家摄像机

    public bool moveAllowed = true;
    public bool turnAllowed = true;

    private float rotationX = 0.0f;  // X轴旋转角度
    private Vector3 lastPosition;  // 上一帧的位置
    public float totalDistanceMoved = 0.0f;  // 总移动距离

    public AudioClip walkingClip;  // 行走音效
    private AudioSource audioSource;  // 音频播放器
    private bool isWalking = false;  // 是否正在行走

    void Start()
    {
        UnityServices.InitializeAsync();  // 初始化 Unity 服务
        lastPosition = transform.position;  // 记录初始位置

        // 初始化 AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkingClip;
        audioSource.loop = true;  // 循环播放
        audioSource.volume = 0.1f;  // 音量大小
        audioSource.pitch = 1f;  // 设置音频播放速度为二倍速
    }

    void Update()
    {
        if (moveAllowed)
        {
            // 移动输入
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // 计算移动方向
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(move * speed * Time.deltaTime);

            // 检测行走状态并播放音效
            bool isCurrentlyWalking = moveX != 0 || moveZ != 0;
            if (isCurrentlyWalking && !isWalking)
            {
                audioSource.Play();  // 播放音效
                isWalking = true;
            }
            else if (!isCurrentlyWalking && isWalking)
            {
                audioSource.Stop();  // 停止播放音效
                isWalking = false;
            }

            // 计算移动距离
            CalculateDistanceMoved();
        }
        else
        {
            // 如果不允许移动，立即停止音效
            if (isWalking)
            {
                audioSource.Stop();
                isWalking = false;
            }
        }

        if (turnAllowed)
        {
            // 鼠标旋转输入
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }


    // 计算移动距离
    void CalculateDistanceMoved()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        totalDistanceMoved += distanceThisFrame;
        lastPosition = transform.position;
    }
}
