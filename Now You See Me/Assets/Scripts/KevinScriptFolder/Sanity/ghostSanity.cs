using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostSanity : MonoBehaviour
{
    // ����Ҫ���õĽű�
    public PlayerSanity playerSanity;

    // ������ "player" ��ǩ�Ķ���ͣ���ڴ�������ʱ����
    private void OnTriggerStay(Collider other)
    {

        //If player stay in the ghost's trigger, sanity lose
        if (other.CompareTag("Player"))
        {
            // ������һ���ű��еĺ���
            playerSanity.decreaseSanity();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ��Ǵ��� "player" ��ǩ�Ķ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ӡ��Ϣ
            playerSanity.directlyLoseSanity();
        }
    }



}
