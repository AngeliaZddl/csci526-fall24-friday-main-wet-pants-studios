using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
{
    float targetAspect = 16.0f / 9.0f; // Desired aspect ratio
    float windowAspect = (float)Screen.width / (float)Screen.height;
    float scaleHeight = windowAspect / targetAspect;

    Camera camera = GetComponent<Camera>();

    if (scaleHeight < 1.0f)
    {
        camera.orthographicSize = camera.orthographicSize / scaleHeight;
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
