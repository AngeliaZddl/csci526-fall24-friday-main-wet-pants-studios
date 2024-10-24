using UnityEngine;
using TMPro;

public class ElevatorButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Animator for controlling the elevator door
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Interaction distance between the player and the button
    private bool isDoorOpen = false;  // Tracks the current state of the elevator door
    private InverntoryManager inverntoryManager;
    public TMP_Text statusKey;  // Reference to the status text UI
    public LayerMask obstacleLayerMask; // Used to specify which objects are considered obstacles
    public GameObject puzzle;

    void Start()
    {
        // Initialize the InventoryManager reference
        inverntoryManager = GameObject.Find("Inventory").GetComponent<InverntoryManager>();
    }

    void Update()
    {
        // Calculate the distance between the player and the button
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if the player is within interaction range and there are no obstacles
        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            // Check if the player has the key
            if (inverntoryManager.itemCheck("key", 1))
            {
                // If the player has the key, they can press F to open the door
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!isDoorOpen)
                    {
                        OpenDoor();
                    }
                }
            }
            else
            {
                Debug.Log("You need a key to open the elevator.");
            }
        }
    }

    // Use Raycast to check if there are any obstacles between the player and the button
    bool IsObstacleBetweenPlayerAndButton()
    {
        // Direction from the player to the button
        Vector3 directionToButton = (transform.position - player.position).normalized;

        // Distance between the player and the button
        float distanceToButton = Vector3.Distance(player.position, transform.position);

        // Perform Raycast to determine if there are any obstacles between the player and the button
        if (Physics.Raycast(player.position, directionToButton, distanceToButton, obstacleLayerMask))
        {
            //Debug.Log("Obstacle detected between player and button");
            return true;  // Obstacle detected
        }

        return false;  // No obstacles, interaction is allowed
    }

    void OpenDoor()
    {
        // Trigger the open door animation
        doorAnimator.SetTrigger("Open");
        isDoorOpen = true;
    }

    // Display a message on the screen
    private void OnGUI()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            if (inverntoryManager.itemCheck("key", 1))
            {
                statusKey.text = "Press F to use";
                statusKey.enabled = true;
            }
            else
            {
                statusKey.text = "You need a key";
                statusKey.enabled = true;
            }
        }
        else
        {
            statusKey.enabled = false;
        }
    }
}
