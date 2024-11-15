using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceOneDeathRecord : MonoBehaviour
{
    public static int SenseOneCounter = 0; // ʹ�þ�̬������¼��������
    private bool SenseOneDeathCheck = false; // ��ֹ�ظ���¼����

    public PlayerSanity playerSanityScript; // ���� PlayerSanity �ű�

    void Update()
    {
        // ȷ�������� PlayerSanity �ű�
        if (playerSanityScript != null)
        {
            if (playerSanityScript.playerSanity <= 0)
            {
                // �����û�м�¼������������������
                if (!SenseOneDeathCheck)
                {
                    SenseOneCounter++;
                    Debug.Log("DeathCountTimes: " + SenseOneCounter);
                    SenseOneDeathCheck = true; // ���Ϊ�Ѽ�¼
                }
            }
            else
            {
                // �� sanity �ָ�Ϊ����ʱ���������
                SenseOneDeathCheck = false;
            }
        }
    }

    public int GetDeathCount()
    {
        return SenseOneCounter;
    }
}
