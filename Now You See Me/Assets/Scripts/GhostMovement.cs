using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour, IGhostController
{
    public Transform player;          // Player's Transform, to be assigned in the Inspector
    public float chaseRange = 10f;    // Ghost's chasing range
    public float moveSpeed = 5f;      // Movement speed
    public float rotationSpeed = 5f;  // Rotation speed

    public float randomMoveDuration = 2f; // Duration for random movement
    private float randomMoveTimer = 0f;

    private Vector3 randomDirection;

    // Boundary variables
    public float minX = -50f;
    public float maxX = 50f;
    public float minY = 0f;
    public float maxY = 2f;
    public float minZ = -50f;
    public float maxZ = 50f;

    private Vector3 resetPosition; // Reset position (automatically initialized)

    // Tutorial-related functionality
    public bool tuto = false;
    public bool moveAllowed = false;

    private Rigidbody rb;
    private bool isPaused = false;

    // Shake-related variables
    private bool isShaking = false;          // Indicates if the ghost is shaking
    private float shakeMagnitude = 0.1f;     // Magnitude of the shake effect
    private Vector3 originalPosition;        // Original position of the ghost

    // Light-related variables
    private Light ghostLight;                // Reference to the ghost's light component

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Automatically initialize reset position to the ghost's starting position
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
        // Skip behaviors if the ghost is shaking or paused
        if (isShaking || isPaused) return;

        // Check if the ghost is out of bounds
        if (IsOutOfBounds())
        {
            ReturnToOrigin();
        }
        else
        {
            // Calculate distance between the ghost and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (tuto)
            {
                //Debug.Log("in tuto");
                if (moveAllowed)
                {
                    //Debug.Log("ima move");
                    ChasePlayer();
                }
                else
                {
                    rb.velocity = transform.forward * 0f;
                }
            }
            else
            {
                if (distanceToPlayer <= chaseRange)
                {
                    ChasePlayer();
                }
                else
                {
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
            //Debug.Log("Obstacle Ahead");
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

            // Ensure the random direction is not a zero vector
            if (randomDirection == Vector3.zero)
            {
                randomDirection = transform.forward;
            }
        }

        Vector3 moveDirection = randomDirection;

        if (IsObstacleAhead())
        {
            moveDirection += AvoidObstacle();
        }

        // Ensure the movement direction is valid
        if (moveDirection == Vector3.zero)
        {
            moveDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        rb.velocity = transform.forward * moveSpeed;
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
