using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SenceHalfDeathRecord : MonoBehaviour
{
    public static int SenceHalfCounter = 0; // ʹ�þ�̬������¼��������
    private bool SenceHalfChecker = false; // ��ֹ�ظ���¼����

    public PlayerSanity playerSanityScript; // ���� PlayerSanity �ű�

    void Update()
    {
        // ȷ�������� PlayerSanity �ű�
        if (playerSanityScript != null)
        {
            if (playerSanityScript.playerSanity <= 0)
            {
                // �����û�м�¼������������������
                if (!SenceHalfChecker)
                {
                    SenceHalfCounter++;
                    Debug.Log("DeathCountTimes: " + SenceHalfCounter);
                    SenceHalfChecker = true; // ���Ϊ�Ѽ�¼
                }
            }
            else
            {
                // �� sanity �ָ�Ϊ����ʱ���������
                SenceHalfChecker = false;
            }
        }
    }

    public int GetDeathCount()
    {
        return SenceHalfCounter;
    }


}
