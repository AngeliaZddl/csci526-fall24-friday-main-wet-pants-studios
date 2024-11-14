using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger2 : MonoBehaviour
{

    public GameObject g1;
    public GameObject g2;
    public TutoController tc;


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
        if(other.gameObject.CompareTag("Player"))
        {
            g1.SetActive(false);
            g2.SetActive(true);
            GhostController gc2 = g2.GetComponent<GhostController>();
            gc2.tuto = true;
            tc.trigger2();
        }


    }
}
