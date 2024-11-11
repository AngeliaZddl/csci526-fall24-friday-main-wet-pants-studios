using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVLightMechanics : MonoBehaviour
{
    public Light myLight;                   // Flashlight light source
    public List<GameObject> ghosts;         // List of ghosts
    public Slider batteryBar;               // Battery bar UI slider
    public float flashDuration = 0.1f;      // Duration of the flash effect
    public float boostedIntensity = 20f;    // Intensity during flash effect
    public float boostedSpotAngle = 60f;    // Spotlight angle during flash effect
    public float boostedRange = 20f;        // Range during flash effect
    public float stopDuration = 3f;         // Duration for ghost to be stunned
    public float batteryDrainAmount = 25f;  // Amount of battery drained per flash
    public float maxBattery = 100f;         // Maximum battery capacity

    private float originalIntensity;
    private float originalSpotAngle;
    private float originalRange;
    private Color originalLightColor;

    void Start()
    {
        // Record original flashlight settings
        originalIntensity = myLight.intensity;
        originalSpotAngle = myLight.spotAngle;
        originalRange = myLight.range;
        originalLightColor = myLight.color;

        // Initialize battery capacity
        if (batteryBar != null)
        {
            batteryBar.maxValue = maxBattery;
            batteryBar.value = maxBattery;
        }
    }

    void Update()
    {
        // Trigger flash effect and ghost stun if battery is above 0 and F key is pressed
        if (Input.GetKeyDown(KeyCode.F) && batteryBar.value > 0)
        {
            StartCoroutine(CameraFlashEffect());
            StunGhostsInLightRange();
            DrainBattery();
        }
    }

    IEnumerator CameraFlashEffect()
    {
        // Apply boosted settings for the flash duration
        myLight.intensity = boostedIntensity;
        myLight.spotAngle = boostedSpotAngle;
        myLight.range = boostedRange;
        myLight.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        // Restore original settings
        myLight.intensity = originalIntensity;
        myLight.spotAngle = originalSpotAngle;
        myLight.range = originalRange;
        myLight.color = originalLightColor;
    }

    void StunGhostsInLightRange()
    {
        foreach (GameObject ghost in ghosts)
        {
            if (IsGhostInLightRange(ghost))
            {
                // Check if the ghost has an IGhostController component and call Stun if it does
                IGhostController ghostController = ghost.GetComponent<IGhostController>();
                if (ghostController != null)
                {
                    ghostController.Stun(stopDuration); // Stun the ghost for stopDuration seconds
                }
            }
        }
    }

    bool IsGhostInLightRange(GameObject ghost)
    {
        Vector3 toGhost = ghost.transform.position - myLight.transform.position;
        float angleToGhost = Vector3.Angle(myLight.transform.forward, toGhost);
        return angleToGhost < myLight.spotAngle / 2 && toGhost.magnitude < myLight.range;
    }

    void DrainBattery()
    {
        batteryBar.value -= batteryDrainAmount;
        if (batteryBar.value < 0)
        {
            batteryBar.value = 0;
        }
    }
}
