using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    public Light detectionLight;
    public Material revealMaterial;
    private Material originalMaterial;
    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;  // save object's original material 
    }

    void Update()
    {
        if (IsLitByLight())
        {
            Debug.Log("Object " + gameObject.name + " is being lit by the light.");
            if (objRenderer.material != revealMaterial)
            {
                objRenderer.material = revealMaterial;  // change to new material
            }
        }
        else
        {
            Debug.Log("Object " + gameObject.name + " is outside the light cone.");
            if (objRenderer.material != originalMaterial)
            {
                objRenderer.material = originalMaterial;  // back to original
            }
        }
    }

    bool IsLitByLight()
    {
        Vector3 objectWorldPosition = transform.position;  
        Vector3 lightWorldPosition = detectionLight.transform.position;  
        Vector3 directionToLight = lightWorldPosition - objectWorldPosition;

        float angleToLight = Vector3.Angle(detectionLight.transform.forward, -directionToLight);

        if (angleToLight < detectionLight.spotAngle / 2)
        {
            return true; 
        }
        else
        {
            return false; 
        }
    }
}