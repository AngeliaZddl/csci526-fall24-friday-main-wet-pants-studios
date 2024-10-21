using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenColor : MonoBehaviour
{
    public Material screenMat;
    private GameObject targetGhost;

    public float yellowDistance = 20.0f;
    public float redDistance = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Find the closest "Ghost" object
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        targetGhost = FindClosestGhost(ghosts);
        changeScreenColorByDistance(targetGhost);

        // Find player for sanity control
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (targetGhost != null)
        {
            float distance = Vector3.Distance(transform.position, targetGhost.transform.position);
            if (p)
            {
                PlayerSanity ps = p.GetComponent<PlayerSanity>();
                if (distance <= redDistance)
                {
                    ps.additionalDecline = 1.0f;

                }
                else
                {
                    ps.additionalDecline = 0.0f;
                }
            }
        }
    }

    GameObject FindClosestGhost(GameObject[] ghosts)
    {
        GameObject closestGhost = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject ghost in ghosts)
        {
            float distance = Vector3.Distance(transform.position, ghost.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestGhost = ghost;
            }
        }

        return closestGhost;
    }

    void changeScreenColorByDistance(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance < redDistance)
        {
            screenMat.SetColor("_EmissionColor", Color.red);
        }
        else if(distance < yellowDistance)
        {
            screenMat.SetColor("_EmissionColor", Color.yellow);
        }
        else
        {
            screenMat.SetColor("_EmissionColor", Color.green);
        }
    }
}
