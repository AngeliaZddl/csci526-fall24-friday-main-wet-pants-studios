using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorItemPickup : MonoBehaviour
{

    public GameObject sensoritem;
    public GameObject playerSensor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sensoritem.SetActive(false);
            playerSensor.SetActive(true);
        }
    }
}
