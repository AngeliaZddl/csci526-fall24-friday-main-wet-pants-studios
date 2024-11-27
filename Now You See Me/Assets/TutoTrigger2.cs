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
        Debug.Log("t2");
        if(other.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("its ghost");
            //g1.SetActive(false);
            //g2.SetActive(true);
            GhostController gc1 = other.gameObject.GetComponent<GhostController>();
            gc1.moveAllowed = false;
            tc.trigger2();
        }


    }
}
