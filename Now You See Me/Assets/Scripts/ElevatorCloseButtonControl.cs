using UnityEngine;
using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ElevatorCloseButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Animator for controlling the elevator door
    public string playerTag = "Player"; // Tag for the Player
    public string ghostTag = "Ghost";  // Tag for the Ghost
    public float interactionDistance = 3f;  // Interaction distance between the player and the button
    public GameObject victoryCanvas;  // Reference to the victory UI Canvas
    public LayerMask obstacleLayerMask; // Used to specify which objects are considered obstacles

    private float time2clearlevel;  // Timer for level clear time
    private float totalDistanceToGhost = 0f;  // Total accumulated distance to ghost
    private int frameCount = 0;  // Frame counter to calculate average distance

    private PlayerMovement playerMovement;  // Reference to PlayerMovement for distance data

    private GameObject player; // Reference to the Player object
    private GameObject ghost;  // Reference to the Ghost object

    private PlayerSanity playerSanity;  // Reference to PlayerSanity script
    public SenceOneDeathRecord senceOneDeathRecord;
    public bool winLastLevel = false;

    async void Start()
    {
        await UnityServices.InitializeAsync();  // Wait for Analytics service to initialize

        // Start data collection for analytics
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
        float distanceToGhost = 0;

        if (ghost != null)
        {
            distanceToGhost = Vector3.Distance(player.transform.position, ghost.transform.position);
        }
        totalDistanceToGhost += distanceToGhost;

        frameCount++;

        // Use Raycast to check if there is an obstacle between the player and the button
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseDoorAndTriggerVictory();
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
        ShowVictoryUI();

        // Calculate average distance to the Ghost
        float averageDistanceToGhost = totalDistanceToGhost / frameCount;

        // Send analytics event with time to clear, average distance to ghost, and death count
        levelClear lc = new levelClear
        {
            time2clear = time2clearlevel,
            totalDistanceMoved = playerMovement.totalDistanceMoved,
            averageDistanceToGhost = averageDistanceToGhost,
            deathCount = senceOneDeathRecord.GetDeathCount() // Send the death count as part of the analytics
        };
        AnalyticsService.Instance.RecordEvent(lc);
        Debug.Log("Event sent: time2clear = " + time2clearlevel + ", totalDistanceMoved = " + playerMovement.totalDistanceMoved + ", averageDistanceToGhost = " + averageDistanceToGhost + ", deathCount = " + senceOneDeathRecord.GetDeathCount());

        Invoke("Nextlevel", 2f);
    }

    void ShowVictoryUI()
    {
        victoryCanvas.SetActive(true);
    }

    void Nextlevel()
    {
        if (SceneManager.GetActiveScene().name == "Level0")
        {
            SceneManager.LoadScene("Level0.5");
        }
        else if (SceneManager.GetActiveScene().name == "Level0.5")
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            
        }
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
