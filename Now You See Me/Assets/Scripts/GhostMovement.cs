using UnityEngine;

public class RandomBouncingMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    private Vector3 moveDirection;
    private Vector3 originalPosition;

    private float minX = -50;
    private float maxX = 50;
    private float minZ = -50;
    private float maxZ = 50;
    private float minY = 0;
    private float maxY = 2;

    [HideInInspector] public bool isPaused = false; // 控制暂停的标志，外部可访问

    void Start()
    {
        originalPosition = transform.position;
        GetRandomDirection(); // 初始化随机方向
    }

    void Update()
    {
        if (isPaused)
        {
            Debug.Log($"{gameObject.name} is paused.");
            return; // 如果暂停，跳过移动逻辑
        }

        // 移动物体
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 检查边界并重置位置
        if (transform.position.x < minX || transform.position.x > maxX ||
            transform.position.z < minZ || transform.position.z > maxZ ||
            transform.position.y < minY || transform.position.y > maxY)
        {
            transform.position = originalPosition;
            GetRandomDirection();
        }
    }

    void GetRandomDirection()
    {
        // 在 x 和 z 轴生成一个随机方向
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检查是否碰撞到墙壁或其他幽灵
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ghost"))
        {
            // 根据碰撞的法线反射方向
            moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
        }
    }
}
