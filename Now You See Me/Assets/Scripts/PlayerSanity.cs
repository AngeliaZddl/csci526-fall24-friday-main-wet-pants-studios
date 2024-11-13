using UnityEngine;
using TMPro;

public class PlayerSanity : MonoBehaviour
{
    public float sanityDecline = 1.0f;
    public float additionalDecline = 0.0f;
    public float playerSanity = 100.0f;
    public float maxSanity = 100.0f;
    public TMPro.TextMeshProUGUI textUI;
    public SanityBarController sanityBarController;

    public int deathCount = 0;  // Add a death counter

    // Start is called before the first frame update
    void Start()
    {
        sanityBarController.SetMaxHealth(maxSanity);
    }

    public void decreaseSanity()
    {
        playerSanity -= (sanityDecline + additionalDecline) * Time.deltaTime;

        if (playerSanity <= 0)
        {
            // Player dies
            playerSanity = 0;
            deathCount++;  // Increment death count
            HandleDeath();  // Call the method to handle player death
        }
    }

    public void directlyLoseSanity()
    {
        playerSanity -= 10;

        if (playerSanity <= 0)
        {
            // Player dies
            playerSanity = 0;
            deathCount++;  // Increment death count
            HandleDeath();  // Call the method to handle player death
        }
    }

    private void HandleDeath()
    {
        // Handle the death event (e.g., trigger respawn, show death screen, etc.)
        Debug.Log("Player has died! Death count: " + deathCount);
        // Optionally reset sanity or trigger game over logic
    }

    // Update is called once per frame
    void Update()
    {
        sanityBarController.SetHealth(playerSanity);
        textUI.text = "Sanity: " + ((int)playerSanity).ToString();

        if (additionalDecline > 0.0f)
        {
            textUI.color = Color.red;
        }
        else
        {
            textUI.color = Color.white;
        }
    }
}
