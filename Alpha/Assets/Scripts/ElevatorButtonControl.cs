using UnityEngine;
using TMPro;

public class ElevatorButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Animator for controlling the elevator door
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Interaction distance between the player and the button
    // public float autoCloseDelay = 5f;  // Time in seconds before the door automatically closes

    private bool isDoorOpen = false;  // Tracks the current state of the elevator door

    private InverntoryManager inverntoryManager;

    public TMP_Text statusKey;


    void Start()
    {
        inverntoryManager = GameObject.Find("Inventory").GetComponent<InverntoryManager>();
       
    }

    void Update()
    {
        // Calculate the distance between the player and the button
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if the player is within interaction range
        if (distance < interactionDistance)
        {
            // Get the PlayerInventory component from the player
            //PlayerInventory inventory = player.GetComponent<PlayerInventory>();

            // Check if the player has the key
            if (inverntoryManager.itemCheck("key", 1))
            {
                // If the player has the key, they can press F to open or close the door
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!isDoorOpen)
                    {
                        // Open the door and start a timer to close it automatically
                        OpenDoor();
                    }
                    else
                    {
                        // Close the door
                        CloseDoor();
                    }
                }
            }
            else
            {
                Debug.Log("You need a key to open the elevator.");
            }
        }
    }

    void OpenDoor()
    {
        doorAnimator.SetTrigger("Open");  // Open the door
        isDoorOpen = true;

        // Automatically close the door after a delay
        // Invoke("CloseDoor", autoCloseDelay);
    }

    void CloseDoor()
    {
        CancelInvoke("CloseDoor");  // Cancel any pending auto-close if it's manually closed
        doorAnimator.SetTrigger("Close");  // Close the door
        isDoorOpen = false;
    }

    // Optional: Display a message when the player is near the button
    private void OnGUI()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        //PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        if (distance < interactionDistance)
        {
            if (inverntoryManager.itemCheck("key", 1))
            {
                statusKey.text = "Press F to use";
                statusKey.enabled = true;


                //GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 20), "Press F to use");
            }
            else
            {
                statusKey.text = "You need a key";
                statusKey.enabled = true;
                //GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 20), "You need a key");
            }
        }
        else
        {
            statusKey.enabled = false;
        }
    }
}
