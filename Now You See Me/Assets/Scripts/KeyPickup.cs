using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // When the player collides with the key
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.PickUpKey();  // The player picks up the key
                Destroy(gameObject);    // Destroy the key object
            }
        }
    }
}
