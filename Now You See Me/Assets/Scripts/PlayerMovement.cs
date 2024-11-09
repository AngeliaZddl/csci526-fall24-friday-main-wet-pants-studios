using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;  // �ƶ��ٶ�
    public float mouseSensitivity = 2.0f;  // ���������
    public Transform playerCamera;  // ���������

    private float rotationX = 0.0f;  // X����ת

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ���������
    }

    void Update()
    {
        // ��ȡ����
        float moveX = Input.GetAxis("Horizontal");  // A/D �� ��/�Ҽ�ͷ
        float moveZ = Input.GetAxis("Vertical");    // W/S �� ��/�¼�ͷ

        // �����ƶ�����
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // �ƶ����
        CharacterController controller = GetComponent<CharacterController>();
        controller.Move(move * speed * Time.deltaTime);

        // ��ȡ������벢��ת
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;  // ���������ӽ�
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // ����������ת��Χ

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);  // �����������ת
        transform.Rotate(Vector3.up * mouseX);  // ��ת��Ҷ���
    }
}
