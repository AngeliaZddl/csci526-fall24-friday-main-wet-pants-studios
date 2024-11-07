using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setUVLight : MonoBehaviour
{

    public Material revealMaterial;
    public Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        revealMaterial.SetVector("_LightPosition", myLight.transform.position);
        revealMaterial.SetVector("_LightDirection", -myLight.transform.forward);
        revealMaterial.SetFloat("_LightAngle", myLight.spotAngle);
    }
}
