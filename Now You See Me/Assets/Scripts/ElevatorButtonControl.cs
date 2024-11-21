using UnityEngine;

public class ElevatorButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Controls the elevator door animation
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Interaction distance for the player and button
    private bool isDoorOpen = false;  // Track whether the door is open
    public LayerMask obstacleLayerMask;  // Layer mask to detect obstacles

    void Update()
    {
        // Calculate the distance between the player and the button
        float distance = Vector3.Distance(player.position, transform.position);

        // If the door is not open, the player is within interaction distance, and there’s no obstacle, allow interaction
        if (!isDoorOpen && distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();  // Open the door when E is pressed
            }
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
        }
    }
}
