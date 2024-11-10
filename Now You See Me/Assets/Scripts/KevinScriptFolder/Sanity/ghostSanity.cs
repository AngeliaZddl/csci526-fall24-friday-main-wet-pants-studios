using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostSanity : MonoBehaviour
{
 
    public PlayerSanity playerSanity;
    public KevinCameraShake kevinCameraShake;
    public GameObject jumpScareObject;
    public GhostBack ghostBackScript; // ���� GhostBack �ű�




    // ������ "player" ��ǩ�Ķ���ͣ���ڴ�������ʱ����
    private void OnTriggerStay(Collider other)
    {

        //If player stay in the ghost's trigger, sanity lose
        if (other.CompareTag("Ghost"))
        {

            // ������һ���ű��еĺ���
            playerSanity.decreaseSanity();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ��Ǵ��� "player" ��ǩ�Ķ���
        if (collision.gameObject.CompareTag("Ghost"))
        {
            
            playerSanity.directlyLoseSanity();
            //��ҡ��֮ǰ��ghost����ԭλ�� �����apiҪ���ľ�������Ū����
            ghostBackScript.ReturnToOriginalPosition();


            StartCoroutine(ShakeAndWait());



        }
    }

    private IEnumerator ShakeAndWait()
    {
        // �����������
        kevinCameraShake.StartShake();
        jumpScareObject.SetActive(true);


        // �ȴ� 3 ��
        yield return new WaitForSeconds(3);
        kevinCameraShake.StopShake();


        jumpScareObject.SetActive(false);

    }


}
