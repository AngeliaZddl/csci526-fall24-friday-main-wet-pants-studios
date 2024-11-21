using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ElevatorButtonLevel0 : MonoBehaviour
{
    public Animator doorAnimator;  // Animator for controlling the elevator door
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Interaction distance between the player and the button
    public GameObject victoryCanvas;  // Reference to the victory UI Canvas
    public LayerMask obstacleLayerMask; // Used to specify which objects are considered obstacles

    private float distance;  // Stores the distance to avoid redundant calculations
    private float time2clearlevel;  // Timer for level clear event

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

        // Check if there is no obstacle and the player presses the E key
        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseDoorAndTriggerVictory();
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

        // Show the victory UI
        ShowVictoryUI();

        // Proceed to the next level after a short delay
        Invoke("Nextlevel", 2f);
    }

    // Function to show the victory UI
    void ShowVictoryUI()
    {
        victoryCanvas.SetActive(true);
    }

    // Function to proceed to the next level or quit the game
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
            Application.Quit();
        }
    }
}
