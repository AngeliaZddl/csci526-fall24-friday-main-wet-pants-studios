using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDismiss : MonoBehaviour
{
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayDismiss(1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DelayDismiss(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        self.SetActive(false);
    }
}
