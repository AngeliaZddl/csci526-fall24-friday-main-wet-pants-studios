using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ElevatorButtonLevel0 : MonoBehaviour
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
        // Start timer for level clear event
        time2clearlevel = 0.0f;

        // Ensure the victoryCanvas is initially hidden
        victoryCanvas.SetActive(false);
    }

    void Update()
    {
        // Increment timer
        time2clearlevel += Time.deltaTime;

        // Calculate the distance between the player and the button
        distance = Vector3.Distance(player.position, transform.position);

        // Check if there is an obstacle between the player and the button
        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            // Display the prompt message
            UpdateStatusText("Press E to close door");

            // If the player presses the E key, close the door and trigger victory logic
            if (Input.GetKeyDown(KeyCode.E))
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

    // Check if there is an obstacle between the player and the button
    bool IsObstacleBetweenPlayerAndButton()
    {
        // Direction from the player to the button
        Vector3 directionToButton = (transform.position - player.position).normalized;

        // Distance between the player and the button
        float distanceToButton = Vector3.Distance(player.position, transform.position);

        // Perform Raycast to detect if there is any object blocking between the player and the button
        return Physics.Raycast(player.position, directionToButton, distanceToButton, obstacleLayerMask);
    }

    void CloseDoorAndTriggerVictory()
    {
        // Trigger the door close animation
        doorAnimator.SetTrigger("Close");

        // Update the status text
        UpdateStatusText("Door is closing...");

        // Show the victory UI
        ShowVictoryUI();

        // Quit the game after a short delay (2 seconds)
        Invoke("Nextlevel", 2f);
    }

    // Function to show the victory UI
    void ShowVictoryUI()
    {
        victoryCanvas.SetActive(true);
    }

    // Function to quit the game
    void Nextlevel()
    {
        //Debug.Log("Quitting the game...");
// #if UNITY_EDITOR
//                 UnityEditor.EditorApplication.isPlaying = false;
// #else
//         Application.Quit();
// #endif

        if(SceneManager.GetActiveScene().name == "Level0") {
            SceneManager.LoadScene("Level0.5");
        } else if (SceneManager.GetActiveScene().name == "Level0.5") {
            SceneManager.LoadScene("Level1");
        } else {
            Application.Quit();
        }
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
            UpdateStatusText("Press E to close door");
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
