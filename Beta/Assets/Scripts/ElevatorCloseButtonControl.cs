using UnityEngine;
using TMPro;
using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class ElevatorCloseButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Animator for controlling the elevator door
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Interaction distance between the player and the button
    public GameObject victoryCanvas;  // Reference to the victory UI Canvas
    public TMP_Text statusKey;     // Reference to the status text UI
    public LayerMask obstacleLayerMask; // Used to specify which objects are considered obstacles

    public float messageDisplayDuration = 3f;  // Duration to display the message
    private float distance;  // Stores the distance to avoid redundant calculations
    private Coroutine hideMessageCoroutine;  // Reference to the coroutine used to hide the message

    private float time2clearlevel;

    void Start()
    {
        // Analytics service start
        UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        // start timer for level2clear event
        time2clearlevel = 0.0f;

        // Ensure the victoryCanvas is initially hidden
        victoryCanvas.SetActive(false);
    }

    void Update()
    {
        // increment timer
        time2clearlevel += Time.deltaTime;

        // Calculate the distance between the player and the button
        distance = Vector3.Distance(player.position, transform.position);

        // Use Raycast to check if there is an obstacle between the player and the button
        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            // Display the prompt message
            UpdateStatusText("Press F to close door");

            // If the player presses the C key, close the door and trigger victory logic
            if (Input.GetKeyDown(KeyCode.F))
            {
                CloseDoorAndTriggerVictory();
            }

            // If the message hide coroutine is running, stop it
            if (hideMessageCoroutine != null)
            {
                StopCoroutine(hideMessageCoroutine);
                hideMessageCoroutine = null;
            }
        }
        else
        {
            // If the player leaves the interaction range, start a coroutine to delay hiding the message
            if (hideMessageCoroutine == null && statusKey.enabled)
            {
                hideMessageCoroutine = StartCoroutine(HideStatusMessageAfterDelay(messageDisplayDuration));
            }
        }
    }

    // Use Raycast to check if there is an obstacle between the player and the button
    bool IsObstacleBetweenPlayerAndButton()
    {
        // Direction from the player to the button
        Vector3 directionToButton = (transform.position - player.position).normalized;

        // Distance between the player and the button
        float distanceToButton = Vector3.Distance(player.position, transform.position);

        // Perform Raycast to detect if there is any object blocking between the player and the button
        if (Physics.Raycast(player.position, directionToButton, distanceToButton, obstacleLayerMask))
        {
            //Debug.Log("Obstacle detected between player and button");
            return true;  // Obstacle detected
        }

        return false;  // No obstacle detected
    }

    void CloseDoorAndTriggerVictory()
    {
        // Trigger the door close animation
        doorAnimator.SetTrigger("Close");

        // Update the status text
        UpdateStatusText("Door is closing...");

        // Show the victory UI
        ShowVictoryUI();

        levelClear lc = new levelClear
        {
            time2clear = time2clearlevel
        };
        AnalyticsService.Instance.RecordEvent(lc);
        Debug.Log("Event sent: time2clear = " + time2clearlevel);

        // Quit the game after a short delay (2 seconds)
        Invoke("QuitGame", 2f);
    }

    // Function to show the victory UI
    void ShowVictoryUI()
    {
        victoryCanvas.SetActive(true);
    }

    // Function to quit the game
    void QuitGame()
    {
        Debug.Log("Quitting the game...");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in the Unity Editor
        #else
            Application.Quit();  // Quit the application in a build
        #endif
    }

    // Optional: Update the status text for better flexibility
    void UpdateStatusText(string message)
    {
        if (statusKey != null)
        {
            statusKey.text = message;
            statusKey.enabled = true;
        }
    }

    // Coroutine to hide the message after a delay
    IEnumerator HideStatusMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (statusKey != null)
        {
            statusKey.enabled = false;  // Hide the status message
        }

        hideMessageCoroutine = null;  // Reset the coroutine reference
    }

    // Display the message on screen
    private void OnGUI()
    {
        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            UpdateStatusText("Press F to close door");
        }
        else
        {
            if (statusKey != null && hideMessageCoroutine == null)
            {
                hideMessageCoroutine = StartCoroutine(HideStatusMessageAfterDelay(messageDisplayDuration));
            }
        }
    }
}

public class levelClear : Unity.Services.Analytics.Event
{
    public levelClear() : base("levelClear")
    {
    }

    public float time2clear { set { SetParameter("time2clear", value); } }
}