using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFoundSample : MonoBehaviour
{
    private InverntoryManager inverntoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inverntoryManager = GameObject.Find("Inventory").GetComponent<InverntoryManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (inverntoryManager.itemCheck("moon", 7))
            {
                Debug.Log("Enough");
            }
            else
            {
                Debug.Log("Not Enough");
            }

        }
    }



}


