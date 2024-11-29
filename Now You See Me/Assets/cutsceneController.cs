using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneController : MonoBehaviour
{

    public GameObject topBar;
    public GameObject bottomBar;

    public float expandSize;
    public float expandSpeed;

    private bool expand = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(expand)
        {
            if (topBar.GetComponent<RectTransform>().sizeDelta.y < expandSize)
            {
                Vector2 origTop = topBar.GetComponent<RectTransform>().sizeDelta;
                Vector2 origBottom = bottomBar.GetComponent<RectTransform>().sizeDelta;
                origTop.y += expandSpeed;
                origBottom.y += expandSpeed;

                topBar.GetComponent<RectTransform>().sizeDelta= origTop;
                bottomBar.GetComponent<RectTransform>().sizeDelta=origBottom;
            }
        }
        else
        {
            if(topBar.GetComponent<RectTransform>().sizeDelta.y > 100)
            {
                Vector2 origTop = topBar.GetComponent<RectTransform>().sizeDelta;
                Vector2 origBottom = bottomBar.GetComponent<RectTransform>().sizeDelta;
                origTop.y -= expandSpeed;
                origBottom.y -= expandSpeed;
                topBar.GetComponent<RectTransform>().sizeDelta = origTop;
                bottomBar.GetComponent<RectTransform>().sizeDelta = origBottom;
            }
        }
    }

    public void show()
    {
        expand = true;
    }

    public void hide()
    {
        expand = false;
    }
}
