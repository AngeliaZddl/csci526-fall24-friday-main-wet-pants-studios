using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghostSanity : MonoBehaviour
{
    public PlayerSanity playerSanity;
    public KevinCameraShake kevinCameraShake;
    public GameObject jumpScareObject;
    public GameObject colorBar; // ��Ҫ������ɫ��Ŀ�����
    public AudioClip jumpScareAudioClip; // ��Ƶ����
    private AudioSource audioSource; // ��Ƶ������

    private void Awake()
    {
        // �ڵ�ǰ��������� AudioSource ���
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // ��ֹ�Զ�����
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
        // �����������
        kevinCameraShake.StartShake();
        jumpScareObject.SetActive(true);

        // ������Ƶ
        if (jumpScareAudioClip != null)
        {
            audioSource.clip = jumpScareAudioClip;
            audioSource.Play();
        }

        // �ȴ� 1 ��
        yield return new WaitForSeconds(1);

        kevinCameraShake.StopShake();
        jumpScareObject.SetActive(false);
        setSanBarToGreenColor();
    }
}
