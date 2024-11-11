using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGhostMovement : MonoBehaviour
{

    public bool moveAllowed = false;
    public GameObject p;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveAllowed)
        {
            transform.position += (p.transform.position - transform.position) * moveSpeed * Time.deltaTime;
        }
    }
}
