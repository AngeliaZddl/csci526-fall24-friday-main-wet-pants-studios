using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger1 : MonoBehaviour
{
    public TutoController tc;
    public GameObject g;
    public GameObject msg;

    private bool activated = false;
    private GameObject player;
    private Vector3 orig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            //Debug.Log(Vector3.Dot(orig, player.transform.forward));
            if(Vector3.Dot(orig, player.transform.forward) < -0.5)
            {
                activated = false;
                tc.trigger1Turned();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tc.trigger1();
            g.SetActive(true);
            msg.SetActive(true);
            player = other.gameObject;
            orig = other.gameObject.transform.forward;
            activated = true;
        }
    }
}
