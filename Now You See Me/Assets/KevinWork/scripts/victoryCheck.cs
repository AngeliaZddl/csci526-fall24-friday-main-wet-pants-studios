using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class victoryCheck : MonoBehaviour
{

    public GameObject victoryCanvas; // �����͸�Ϊ GameObject

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            victoryCanvas.SetActive(true); // 



            Invoke("NextLevel", 1f);
        }
    }

    void NextLevel()
    {
        //Debug.Log("Quitting the game...");
        // #if UNITY_EDITOR
        //         UnityEditor.EditorApplication.isPlaying = false;
        // #else
        //         Application.Quit();
        // #endif
        SceneManager.LoadScene("Level1");
    }
}