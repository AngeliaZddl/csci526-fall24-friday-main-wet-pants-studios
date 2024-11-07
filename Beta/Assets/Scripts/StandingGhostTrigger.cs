using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingGhostTrigger : MonoBehaviour
{
    public GameObject standingGhost;
    public GameObject ghost;
    public GameObject lights;
    public GameObject textUI;
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
                standingGhost.SetActive(false);
                ghost.SetActive(true);
                lights.SetActive(false);
                textUI.SetActive(true);
                happened = true;
            }
        }
    }
}
