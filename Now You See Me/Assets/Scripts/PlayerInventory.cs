using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;  // Whether the player has the key

    // Simulate the key pickup function (you can implement this in other ways, such as triggering it when the player collides with the key)
    public void PickUpKey()
    {
        hasKey = true;
        Debug.Log("Player picked up the key!");
    }
}
