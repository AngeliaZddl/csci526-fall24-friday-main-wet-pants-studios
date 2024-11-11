using UnityEngine;

public class SetUVLight : MonoBehaviour
{
    public Material revealMaterial;         // Material to reveal under flashlight illumination
    public Light myLight;                   // Flashlight light source

    void Update()
    {
        // Pass flashlight's position, direction, and enabled state to the shader
        revealMaterial.SetVector("_LightPosition", myLight.transform.position);
        revealMaterial.SetVector("_LightDirection", -myLight.transform.forward);
        revealMaterial.SetFloat("_LightAngle", myLight.spotAngle);
        revealMaterial.SetInteger("_LightEnabled", myLight.enabled ? 1 : 0);
    }
}
