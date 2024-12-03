using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghostSanity : MonoBehaviour
{
    public PlayerSanity playerSanity;
    public KevinCameraShake kevinCameraShake;
    public GameObject jumpScareObject;
    public GameObject colorBar; // 需要更改颜色的目标对象
    public AudioClip jumpScareAudioClip; // 音频剪辑
    private AudioSource audioSource; // 音频播放器

    private void Awake()
    {
        // 在当前对象中添加 AudioSource 组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 禁止自动播放
    }

    private void setSanBarToRedColor()
    {
        colorBar.GetComponent<Image>().color = Color.red;
    }

    private void setSanBarToGreenColor()
    {
        colorBar.GetComponent<Image>().color = Color.green;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            GhostController ghostController = other.GetComponent<GhostController>();

            if (ghostController != null && ghostController.getShakeStatus())
            {
                setSanBarToGreenColor();
                Debug.Log("Ghost is shaking. No sanity loss.");
                return;
            }

            setSanBarToRedColor();
            playerSanity.decreaseSanity();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            setSanBarToGreenColor();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            GhostController collidedGhostController = collision.gameObject.GetComponent<GhostController>();
            GhostBack ghostBackScript = collision.gameObject.GetComponent<GhostBack>();

            if (collidedGhostController.getShakeStatus())
            {
                Debug.Log("Ghost is shaking. Losing sanity");
                return;
            }

            setSanBarToRedColor();
            playerSanity.directlyLoseSanity();

            if (ghostBackScript != null)
            {
                ghostBackScript.ReturnToOriginalPosition();
            }

            StartCoroutine(ShakeAndWait());
        }
    }

    private IEnumerator ShakeAndWait()
    {
        // 调用相机抖动
        kevinCameraShake.StartShake();
        jumpScareObject.SetActive(true);

        // 播放音频
        if (jumpScareAudioClip != null)
        {
            audioSource.clip = jumpScareAudioClip;
            audioSource.Play();
        }

        // 等待 1 秒
        yield return new WaitForSeconds(1);

        kevinCameraShake.StopShake();
        jumpScareObject.SetActive(false);
        setSanBarToGreenColor();
    }
}
