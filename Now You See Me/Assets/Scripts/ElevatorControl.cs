using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public Animator doorAnimator1;  // Animator controlling the elevator door
    public Transform player1;       // Player object
    public float interactionDistance1 = 3f;  // Interaction distance for using the elevator
    // public float autoCloseDelay = 5f;  // Time in seconds before the door automatically closes

    private bool isDoorOpen = false;  // Tracks the current state of the elevator door
     void Start()
    {
        // Force the door to start in the closed state when the game begins
        doorAnimator1.Play("ElevatorDoorClose", 0, 0f);  // Set the Animator to play the closed state immediately
        isDoorOpen = false;  // Ensure the door is marked as closed
    }
    void Update()
    {
        // Calculate the distance between the player and the elevator
        float distance = Vector3.Distance(player1.position, transform.position);

        // If the player is within interaction range
        if (distance < interactionDistance1)
        {
            // Check if the player presses the F key
            if (Input.GetKeyDown(KeyCode.F))
            {
                // If the door is currently closed, open the door and start a timer to close it automatically
                if (!isDoorOpen && doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("ElevatorDoorClose"))
                {
                    OpenDoor();
                }
                // If the door is currently open, close it manually
                else if (isDoorOpen && doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("ElevatorDoorOpen"))
                {
                    CloseDoor();
                }
            }
        }
    }

    void OpenDoor()
    {
        doorAnimator1.SetTrigger("Open");  // Trigger the open animation
        isDoorOpen = true;  // Mark the door as open
        // Invoke("CloseDoor", autoCloseDelay);  // Automatically close the door after a delay
    }

    void CloseDoor()
    {
        doorAnimator1.SetTrigger("Close");  // Trigger the close animation
        isDoorOpen = false;  // Mark the door as closed
    }
}
