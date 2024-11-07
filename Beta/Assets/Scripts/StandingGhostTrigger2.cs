using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingGhostTrigger2 : MonoBehaviour
{
    public GameObject lights;
    private bool happened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!happened)
            {
                lights.SetActive(true);
                happened = true;
            }
        }
    }
}
