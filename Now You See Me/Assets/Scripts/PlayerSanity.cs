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

    }

    public void directlyLoseSanity()
    {
        playerSanity -= 10;

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
