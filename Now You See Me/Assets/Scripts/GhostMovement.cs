using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
    public Transform player;          // 玩家的 Transform，需要在检查器中赋值
    public float chaseRange = 10f;    // 追逐范围
    public float moveSpeed = 5f;      // 移动速度
    public float rotationSpeed = 5f;  // 旋转速度

    public float randomMoveDuration = 2f; // 随机移动的持续时间
    private float randomMoveTimer = 0f;

    private Vector3 randomDirection;

    // 定义范围变量
    public float minX = -50f;
    public float maxX = 50f;
    public float minY = 0f;
    public float maxY = 2f;
    public float minZ = -50f;
    public float maxZ = 50f;

    private Rigidbody rb;

    // 抖动相关变量
    private bool isShaking = false;          // 标识幽灵是否正在抖动
    private float shakeDuration = 3f;        // 抖动持续时间
    private float shakeMagnitude = 0.1f;     // 抖动幅度
    private Vector3 originalPosition;        // 幽灵的原始位置

    // 灯光相关变量
    private Light ghostLight;                // 引用幽灵的灯光组件

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 获取幽灵上的 Light 组件
        ghostLight = GetComponentInChildren<Light>();

        // 初始时关闭灯光
        if (ghostLight != null)
        {
            ghostLight.enabled = false;
        }
    }

    void Update()
    {
        // 检测 F 键的按下
        if (Input.GetKeyDown(KeyCode.F) && !isShaking)
        {
            StartCoroutine(Shake());
            return; // 本帧不执行其他逻辑
        }

        // 如果幽灵正在抖动，暂停其他行为
        if (isShaking)
        {
            return;
        }

        // 检查幽灵是否超出范围
        if (IsOutOfBounds())
        {
            // 返回原点
            ReturnToOrigin();
        }
        else
        {
            // 计算幽灵与玩家之间的距离
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= chaseRange)
            {
                // 追逐玩家
                ChasePlayer();
            }
            else
            {
                // 随机移动
                RandomMovement();
            }
        }
    }

    void ChasePlayer()
    {
        // 计算朝向玩家的方向
        Vector3 direction = (player.position - transform.position).normalized;

        // 检测前方是否有障碍物
        if (IsObstacleAhead())
        {
            // 避开障碍物
            direction += AvoidObstacle();
        }

        // 旋转幽灵朝向目标方向
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // 移动幽灵
        rb.velocity = transform.forward * moveSpeed;
    }

    void RandomMovement()
    {
        // 更新计时器
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f)
        {
            // 重置计时器
            randomMoveTimer = randomMoveDuration;

            // 在45度到135度之间生成随机角度
            float randomAngle = Random.Range(45f, 135f);

            // 随机决定左转或右转
            float sign = Random.value < 0.5f ? -1f : 1f;
            randomAngle *= sign;

            // 计算随机方向（相对于幽灵当前的前方方向）
            randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * transform.forward;
        }

        // 检测前方是否有障碍物
        Vector3 moveDirection = randomDirection;

        if (IsObstacleAhead())
        {
            // 避开障碍物
            moveDirection += AvoidObstacle();
        }

        // 旋转幽灵朝向目标方向
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // 移动幽灵
        rb.velocity = transform.forward * moveSpeed;
    }

    bool IsObstacleAhead()
    {
        RaycastHit hit;
        float detectionDistance = 1f; // 检测前方1米内的障碍物

        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            // 检测到障碍物
            return true;
        }
        return false;
    }

    Vector3 AvoidObstacle()
    {
        // 简单的避障实现，可以改进为更复杂的算法
        // 这里让幽灵向左或向右转一定角度
        float avoidAngle = 90f;
        float sign = Random.value < 0.5f ? -1f : 1f;
        Vector3 avoidDirection = Quaternion.Euler(0f, avoidAngle * sign, 0f) * transform.forward;
        return avoidDirection.normalized;
    }

    // 检查幽灵是否超出指定范围
    bool IsOutOfBounds()
    {
        Vector3 pos = transform.position;
        return pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY || pos.z < minZ || pos.z > maxZ;
    }

    // 将幽灵返回到原点
    void ReturnToOrigin()
    {
        // 停止移动
        rb.velocity = Vector3.zero;

        // 传送到原点
        transform.position = Vector3.zero;

        // 重置随机移动计时器
        randomMoveTimer = 0f;
    }

    // 实现抖动效果的协程
    IEnumerator Shake()
    {
        isShaking = true;
        originalPosition = transform.position;

        // 停止移动
        rb.velocity = Vector3.zero;

        // 打开灯光
        if (ghostLight != null)
        {
            ghostLight.enabled = true;
        }

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // 生成随机的抖动偏移
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetZ = Random.Range(-1f, 1f) * shakeMagnitude;

            // 应用抖动偏移
            transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // 抖动结束，恢复原始位置
        transform.position = originalPosition;
        isShaking = false;

        // 关闭灯光
        if (ghostLight != null)
        {
            ghostLight.enabled = false;
        }
    }
}