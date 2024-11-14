using UnityEngine;
using TMPro;
using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Threading.Tasks;

public class ElevatorCloseButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Animator for controlling the elevator door
    public string playerTag = "Player"; // Tag for the Player
    public string ghostTag = "Ghost";  // Tag for the Ghost
    public float interactionDistance = 3f;  // Interaction distance between the player and the button
    public GameObject victoryCanvas;  // Reference to the victory UI Canvas
    public TMP_Text statusKey;     // Reference to the status text UI
    public LayerMask obstacleLayerMask; // Used to specify which objects are considered obstacles
    public float messageDisplayDuration = 3f;  // Duration to display the message

    private float time2clearlevel;  // Timer for level clear time
    private float distance;  // Stores the distance to avoid redundant calculations
    private float totalDistanceToGhost = 0f;  // Total accumulated distance to ghost
    private int frameCount = 0;  // Frame counter to calculate average distance
    private Coroutine hideMessageCoroutine;  // Reference to the coroutine used to hide the message

    private PlayerMovement playerMovement;  // Reference to PlayerMovement for distance data

    private GameObject player; // Reference to the Player object
    private GameObject ghost;  // Reference to the Ghost object

    private PlayerSanity playerSanity;  // Reference to PlayerSanity script

    async void Start()
    {
        await UnityServices.InitializeAsync();  // 等待 Analytics 服务初始化完成

        // 启动数据收集（假设玩家已同意数据收集）
        AnalyticsService.Instance.StartDataCollection();

        time2clearlevel = 0.0f;  // Start timer for level clear event
        victoryCanvas.SetActive(false);  // Ensure the victoryCanvas is initially hidden

        // Find the Player and Ghost by their Tags
        player = GameObject.FindGameObjectWithTag(playerTag);
        ghost = GameObject.FindGameObjectWithTag(ghostTag);

        // Get the PlayerMovement and PlayerSanity components
        playerMovement = player.GetComponent<PlayerMovement>();
        playerSanity = player.GetComponent<PlayerSanity>();
    }

    void Update()
    {
        time2clearlevel += Time.deltaTime;  // Increment timer for time2clearlevel event

        // Calculate distance between player and ghost
        float distanceToGhost = Vector3.Distance(player.transform.position, ghost.transform.position);
        totalDistanceToGhost += distanceToGhost;
        frameCount++;

        // Use Raycast to check if there is an obstacle between the player and the button
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            UpdateStatusText("Press F to close door");

            if (Input.GetKeyDown(KeyCode.F))
            {
                CloseDoorAndTriggerVictory();
            }

            if (hideMessageCoroutine != null)
            {
                StopCoroutine(hideMessageCoroutine);
                hideMessageCoroutine = null;
            }
        }
        else
        {
            if (hideMessageCoroutine == null && statusKey.enabled)
            {
                hideMessageCoroutine = StartCoroutine(HideStatusMessageAfterDelay(messageDisplayDuration));
            }
        }
    }

    bool IsObstacleBetweenPlayerAndButton()
    {
        Vector3 directionToButton = (transform.position - player.transform.position).normalized;
        float distanceToButton = Vector3.Distance(player.transform.position, transform.position);

        return Physics.Raycast(player.transform.position, directionToButton, distanceToButton, obstacleLayerMask);
    }

    void CloseDoorAndTriggerVictory()
    {
        doorAnimator.SetTrigger("Close");
        UpdateStatusText("Door is closing...");
        ShowVictoryUI();

        // Calculate average distance to the Ghost
        float averageDistanceToGhost = totalDistanceToGhost / frameCount;

        // Send analytics event with time to clear, average distance to ghost, and death count
        levelClear lc = new levelClear
        {
            time2clear = time2clearlevel,
            totalDistanceMoved = playerMovement.totalDistanceMoved,
            averageDistanceToGhost = averageDistanceToGhost,
            deathCount = playerSanity.deathCount  // Send the death count as part of the analytics
        };
        AnalyticsService.Instance.RecordEvent(lc);
        Debug.Log("Event sent: time2clear = " + time2clearlevel + ", totalDistanceMoved = " + playerMovement.totalDistanceMoved + ", averageDistanceToGhost = " + averageDistanceToGhost + ", deathCount = " + playerSanity.deathCount);

        Invoke("QuitGame", 2f);
    }

    void ShowVictoryUI()
    {
        victoryCanvas.SetActive(true);
    }

    void QuitGame()
    {
        Debug.Log("Quitting the game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void UpdateStatusText(string message)
    {
        if (statusKey != null)
        {
            statusKey.text = message;
            statusKey.enabled = true;
        }
    }

    IEnumerator HideStatusMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (statusKey != null)
        {
            statusKey.enabled = false;
        }

        hideMessageCoroutine = null;
    }
}

public class levelClear : Unity.Services.Analytics.Event
{
    public levelClear() : base("levelClear")
    {
    }

    public float time2clear { set { SetParameter("time2clear", value); } }
    public float totalDistanceMoved { set { SetParameter("totalDistanceMoved", value); } }
    public float averageDistanceToGhost { set { SetParameter("averageDistanceToGhost", value); } }
    public int deathCount { set { SetParameter("deathCount", value); } }  // Add deathCount parameter
}
