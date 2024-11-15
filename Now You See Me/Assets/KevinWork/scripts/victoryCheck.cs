using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

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
            victoryCanvas.SetActive(true); // ��ʾʤ���� Canvas

            Invoke("Nextlevel", 1f);
        }
    }

    void Nextlevel()
    {
        //Debug.Log("Quitting the game...");
// #if UNITY_EDITOR
//                 UnityEditor.EditorApplication.isPlaying = false;
// #else
//         Application.Quit();
// #endif

        if(SceneManager.GetActiveScene().name == "Level0") {
            SceneManager.LoadScene("Level0.5");
        } else if (SceneManager.GetActiveScene().name == "Level0.5") {
            SceneManager.LoadScene("Level1");
        } else {
            Application.Quit();
        }
    }
}