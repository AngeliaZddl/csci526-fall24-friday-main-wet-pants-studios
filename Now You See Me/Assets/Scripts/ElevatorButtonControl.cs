using UnityEngine;
using TMPro;

public class ElevatorButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Controls the elevator door animation
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Interaction distance for the player and button
    private bool isDoorOpen = false;  // Track whether the door is open
    public TMP_Text statusText;  // UI text prompt for interaction
    public LayerMask obstacleLayerMask;  // Layer mask to detect obstacles

    void Start()
    {
        statusText.enabled = false;  // Hide the status text initially
    }

    void Update()
    {
        // Calculate the distance between the player and the button
        float distance = Vector3.Distance(player.position, transform.position);

        // If the door is not open, the player is within interaction distance, and there’s no obstacle, show the prompt
        if (!isDoorOpen && distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            statusText.text = "Press E to open the door";  // Set interaction text
            statusText.enabled = true;  // Enable the prompt

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();  // Open the door when F is pressed
            }
        }
        else
        {
            statusText.enabled = false;  // Hide the prompt when out of range or if the door is open
        }
    }
    

    // Checks for obstacles between the player and the button
    bool IsObstacleBetweenPlayerAndButton()
    {
        Vector3 directionToButton = (transform.position - player.position).normalized;
        float distanceToButton = Vector3.Distance(player.position, transform.position);

        if (Physics.Raycast(player.position, directionToButton, distanceToButton, obstacleLayerMask))
        {
            return true;  // Obstacle detected
        }

        return false;  // No obstacles, interaction allowed
    }

    // Opens the elevator door
    public void OpenDoor()
    {
        if (!isDoorOpen)
        {
            doorAnimator.SetTrigger("Open");
            isDoorOpen = true;  // Mark the door as open to prevent reactivation
            statusText.enabled = false;  // Hide the prompt after the door opens
        }
    }
}
