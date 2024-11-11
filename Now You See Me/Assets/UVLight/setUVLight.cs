using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setUVLight : MonoBehaviour
{
    public Material revealMaterial;         // Material to reveal under flashlight illumination
    public Light myLight;                   // Flashlight light source
    public List<GameObject> ghosts;         // List of ghosts
    public Slider batteryBar;               // Battery bar UI slider
    public float flashDuration = 0.1f;      // Duration of the flash effect
    public float boostedIntensity = 20f;    // Intensity during flash effect
    public float boostedSpotAngle = 60f;    // Spotlight angle during flash effect
    public float boostedRange = 20f;        // Range during flash effect
    public float stopDuration = 3f;         // Duration for ghost to be stopped
    public float batteryDrainAmount = 25f;  // Amount of battery drained per flash
    public float maxBattery = 100f;         // Maximum battery capacity

    private float originalIntensity;        // Original intensity of the flashlight
    private float originalSpotAngle;        // Original spotlight angle
    private float originalRange;            // Original range of the flashlight
    private Color originalLightColor;       // Original color of the flashlight

    void Start()
    {
        // Record the original intensity, angle, range, and color of the flashlight
        originalIntensity = myLight.intensity;
        originalSpotAngle = myLight.spotAngle;
        originalRange = myLight.range;
        originalLightColor = myLight.color;

        // Initialize battery capacity to maximum and update the battery bar UI
        if (batteryBar != null)
        {
            batteryBar.maxValue = maxBattery;
            batteryBar.value = maxBattery;
        }

        // The flashlight's base light is always on
        myLight.enabled = true;
    }

    void Update()
    {
        // Update flashlight's position and direction for dynamic effects
        revealMaterial.SetVector("_LightPosition", myLight.transform.position);
        revealMaterial.SetVector("_LightDirection", -myLight.transform.forward);
        revealMaterial.SetFloat("_LightAngle", myLight.spotAngle);
        revealMaterial.SetInteger("_LightEnabled", 1); // Flashlight is always on

        // Check if F key is pressed and battery level is above 0 to trigger flash effect and ghost stop
        if (Input.GetKeyDown(KeyCode.F) && batteryBar.value > 0)
        {
            StartCoroutine(CameraFlashEffect()); // Perform camera flash effect
            StopGhostsInLightRange(); // Stop ghosts within light range
            DrainBattery(); // Reduce battery level
        }
    }

    IEnumerator CameraFlashEffect()
    {
        // Set flashlight intensity, angle, range, and color for flash effect
        myLight.intensity = boostedIntensity;
        myLight.spotAngle = boostedSpotAngle;
        myLight.range = boostedRange;
        myLight.color = Color.white;

        // Wait briefly to simulate flash duration
        yield return new WaitForSeconds(flashDuration);

        // Restore original flashlight intensity, angle, range, and color
        myLight.intensity = originalIntensity;
        myLight.spotAngle = originalSpotAngle;
        myLight.range = originalRange;
        myLight.color = originalLightColor;
    }

    void StopGhostsInLightRange()
    {
        // Iterate through ghosts and pause those within light range
        foreach (GameObject ghost in ghosts)
        {
            if (IsGhostInLightRange(ghost))
            {
                StartCoroutine(PauseGhostMovement(ghost));
            }
        }
    }

    IEnumerator PauseGhostMovement(GameObject ghost)
    {
        // Get the ghost's movement component (RandomBouncingMovement)
        var movement = ghost.GetComponent<RandomBouncingMovement>();
        if (movement != null)
        {
            movement.isPaused = true; // Pause ghost movement
            yield return new WaitForSeconds(stopDuration); // Pause ghost for stopDuration seconds
            movement.isPaused = false; // Resume ghost movement
        }
    }

    bool IsGhostInLightRange(GameObject ghost)
    {
        // Check if ghost is within flashlight range
        Vector3 toGhost = ghost.transform.position - myLight.transform.position;
        float angleToGhost = Vector3.Angle(myLight.transform.forward, toGhost);

        // Determine if ghost is within flashlight angle and distance range
        return angleToGhost < myLight.spotAngle / 2 && toGhost.magnitude < myLight.range;
    }

    void DrainBattery()
    {
        // Reduce battery level each time flashlight is used
        batteryBar.value -= batteryDrainAmount;

        // Ensure battery level does not fall below 0
        if (batteryBar.value < 0)
        {
            batteryBar.value = 0;
        }
    }
}
