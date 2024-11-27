using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour, IGhostController
{
    public Transform player;          // 玩家Transform，需要在Inspector中分配
    public float chaseRange = 10f;    // 幽灵追逐范围
    public float moveSpeed = 5f;      // 移动速度
    public float rotationSpeed = 5f;  // 转向速度

    public float randomMoveDuration = 2f; // 随机移动持续时间
    private float randomMoveTimer = 0f;

    private Vector3 randomDirection;

    // 边界变量
    public float minX = -50f;
    public float maxX = 50f;
    public float minY = 0f;
    public float maxY = 2f;
    public float minZ = -50f;
    public float maxZ = 50f;

    // 重置位置（灵活设置）
    public Vector3 resetPosition;

    // 教程功能
    public bool tuto = false;
    public bool moveAllowed = false;

    private Rigidbody rb;
    private bool isPaused = false;

    // Shake variables
    private bool isShaking = false;          // Indicates if the ghost is currently shaking
    private float shakeMagnitude = 0.1f;     // Magnitude of the shake effect
    private Vector3 originalPosition;        // Original position of the ghost

    // Light variables
    private Light ghostLight;                // Reference to the ghost's light component

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 初始化重置位置为幽灵当前的起始位置
        resetPosition = transform.position;

        // Get the Light component on the ghost
        ghostLight = GetComponentInChildren<Light>();

        // Initially turn off the light
        if (ghostLight != null)
        {
            ghostLight.enabled = false;
        }
    }

    void Update()
    {
        // If the ghost is shaking or paused, skip other behaviors
        if (isShaking || isPaused) return;

        Debug.Log($"Ghost {gameObject.name}: Position = {transform.position}");

        // Check if the ghost is out of bounds
        if (IsOutOfBounds())
        {
            Debug.Log($"Ghost {gameObject.name}: Out of bounds! Returning to origin.");
            ReturnToOrigin();
        }
        else
        {
            // Calculate distance between the ghost and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (tuto)
            {
                if (moveAllowed)
                {
                    Debug.Log($"Ghost {gameObject.name}: Tutorial mode - chasing player.");
                    ChasePlayer();
                }
            }
            else
            {
                if (distanceToPlayer <= chaseRange)
                {
                    Debug.Log($"Ghost {gameObject.name}: Chasing player.");
                    ChasePlayer();
                }
                else
                {
                    Debug.Log($"Ghost {gameObject.name}: Random movement.");
                    RandomMovement();
                }
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Check if there's an obstacle ahead
        if (IsObstacleAhead())
        {
            Debug.Log($"Ghost {gameObject.name}: Obstacle ahead, avoiding.");
            direction += AvoidObstacle();
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        rb.velocity = transform.forward * moveSpeed;
    }

    void RandomMovement()
    {
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f)
        {
            randomMoveTimer = randomMoveDuration;

            float randomAngle = Random.Range(45f, 135f);
            float sign = Random.value < 0.5f ? -1f : 1f;
            randomAngle *= sign;

            randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * transform.forward;

            // 确保随机方向不是零向量
            if (randomDirection == Vector3.zero)
            {
                randomDirection = transform.forward;
            }

            Debug.Log($"Ghost {gameObject.name}: New random direction generated = {randomDirection}");
        }

        Vector3 moveDirection = randomDirection;

        if (IsObstacleAhead())
        {
            Debug.Log($"Ghost {gameObject.name}: Obstacle ahead, adjusting direction.");
            moveDirection += AvoidObstacle();
        }

        // 确保方向有效
        if (moveDirection == Vector3.zero)
        {
            moveDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        rb.velocity = transform.forward * moveSpeed;

        Debug.Log($"Ghost {gameObject.name}: Moving with velocity = {rb.velocity}");
    }

    bool IsObstacleAhead()
    {
        RaycastHit hit;
        float detectionDistance = 1f;

        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            return true;
        }
        return false;
    }

    Vector3 AvoidObstacle()
    {
        float avoidAngle = 90f;
        float sign = Random.value < 0.5f ? -1f : 1f;
        Vector3 avoidDirection = Quaternion.Euler(0f, avoidAngle * sign, 0f) * transform.forward;
        return avoidDirection.normalized;
    }

    bool IsOutOfBounds()
    {
        Vector3 pos = transform.position;
        return pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY || pos.z < minZ || pos.z > maxZ;
    }

    void ReturnToOrigin()
    {
        rb.velocity = Vector3.zero;
        transform.position = resetPosition;
        randomMoveTimer = 0f;

        Debug.Log($"Ghost {gameObject.name}: Returned to reset position = {resetPosition}");
    }

    public void Stun(float duration)
    {
        StartCoroutine(ShakeAndPause(duration));
    }

    IEnumerator ShakeAndPause(float duration)
    {
        isPaused = true;
        originalPosition = transform.position;

        // Stop movement
        rb.velocity = Vector3.zero;

        // Turn on the light
        if (ghostLight != null)
        {
            ghostLight.enabled = true;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetZ = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
        isPaused = false;

        if (ghostLight != null)
        {
            ghostLight.enabled = false;
        }

        Debug.Log($"Ghost {gameObject.name}: Stun finished.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (tuto)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameObject tc = GameObject.Find("TutoController");
                if (tc)
                {
                    TutoController tcs = tc.GetComponent<TutoController>();
                    tcs.trigger4();
                }
            }
        }
    }
}
