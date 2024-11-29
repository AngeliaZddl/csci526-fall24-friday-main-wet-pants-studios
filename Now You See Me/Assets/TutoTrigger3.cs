using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger3 : MonoBehaviour
{
    public GameObject g2;
    public GameObject g3;
    public GameObject tmp;
    public TutoController tc;
    public GameObject csc;

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
            g2.SetActive(false);
            //tmp.SetActive(true);
            g3.SetActive(true);
            GhostController gc = g3.GetComponent<GhostController>();
            cutsceneController c = csc.GetComponent<cutsceneController>();
            c.show();
            gc.tuto = true;
            tc.trigger3();
        }
    }
}
