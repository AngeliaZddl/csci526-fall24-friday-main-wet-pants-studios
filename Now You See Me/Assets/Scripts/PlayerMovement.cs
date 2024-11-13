using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;  // 移动速度
    public float mouseSensitivity = 2.0f;  // 鼠标灵敏度
    public Transform playerCamera;  // 玩家摄像头

    public bool moveAllowed = true;
    public bool turnAllowed = true;

    private float rotationX = 0.0f;  // X轴旋转角度
    private Vector3 lastPosition;  // 上一次玩家位置
    public float totalDistanceMoved = 0.0f;  // 累计移动距离

    void Start()
    {
        UnityServices.InitializeAsync();  // 初始化 Unity 服务
        lastPosition = transform.position;  // 初始化上一次位置为玩家初始位置
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

            // 计算移动距离
            CalculateDistanceMoved();
        }

        if (turnAllowed)
        {
            // 旋转摄像头
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    // 计算玩家移动的距离
    void CalculateDistanceMoved()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        totalDistanceMoved += distanceThisFrame;
        lastPosition = transform.position;
    }
}
