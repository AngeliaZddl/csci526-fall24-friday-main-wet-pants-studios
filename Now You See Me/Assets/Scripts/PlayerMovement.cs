using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;  // 移动速度
    public float mouseSensitivity = 2.0f;  // 鼠标灵敏度
    public Transform playerCamera;  // 摄像机对象

    private float rotationX = 0.0f;  // X轴旋转

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // 锁定鼠标光标
    }

    void Update()
    {
        // 获取输入
        float moveX = Input.GetAxis("Horizontal");  // A/D 或 左/右箭头
        float moveZ = Input.GetAxis("Vertical");    // W/S 或 上/下箭头

        // 生成移动向量
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // 移动玩家
        CharacterController controller = GetComponent<CharacterController>();
        controller.Move(move * speed * Time.deltaTime);

        // 获取鼠标输入并旋转
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;  // 控制上下视角
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // 限制上下旋转范围

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);  // 设置摄像机旋转
        transform.Rotate(Vector3.up * mouseX);  // 旋转玩家对象
    }
}
