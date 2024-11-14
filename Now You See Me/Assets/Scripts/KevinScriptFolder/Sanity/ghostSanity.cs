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

    //public GhostBack ghostBackScript; // ���� GhostBack �ű�

    private void setSanBarToRedColor()
    {
        colorBar.GetComponent<Image>().color = Color.red;
    }

    private void setSanBarToGreenColor()
    {
        colorBar.GetComponent<Image>().color = Color.green;
    }


    // ������ "player" ��ǩ�Ķ���ͣ���ڴ�������ʱ����
    private void OnTriggerStay(Collider other)
    {

        //If player stay in the ghost's trigger, sanity lose
        if (other.CompareTag("Ghost"))
        {
            setSanBarToRedColor();
            // ������һ���ű��еĺ���
            playerSanity.decreaseSanity();

        }
    }

    // ������ "Ghost" ��ǩ�Ķ����뿪������ʱ����
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            setSanBarToGreenColor();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ��Ǵ��� "player" ��ǩ�Ķ���
        if (collision.gameObject.CompareTag("Ghost"))
        {
            setSanBarToRedColor();
            playerSanity.directlyLoseSanity();
            //��ҡ��֮ǰ��ghost����ԭλ�� �����apiҪ���ľ�������Ū����
            //ghostBackScript.ReturnToOriginalPosition();
            // ��ȡ��ײ����� GhostBack �ű��������� ReturnToOriginalPosition
            GhostBack ghostBackScript = collision.gameObject.GetComponent<GhostBack>();
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


        // �ȴ� 1 ��
        yield return new WaitForSeconds(1);
        kevinCameraShake.StopShake();


        jumpScareObject.SetActive(false);
        setSanBarToGreenColor();

    }


}
