using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanRestorePIck : MonoBehaviour
{
    public float radius = 2.5f; // ���뾶
    private Transform playerTransform; // ��ҵ�λ������

    private void Start()
    {
        // �ҵ���Ҷ��󣬲���ȡ�� Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No Player found in the scene!");
        }
    }

    private void Update()
    {
        // �����Ҵ���
        if (playerTransform != null)
        {
            // ��������ҵľ���
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // �������С�ڰ뾶���򴥷��߼�
            if (distanceToPlayer <= radius)
            {
                // ��ȡ����ϵ� PlayerSanity �ű�
                PlayerSanity playerSanity = playerTransform.GetComponent<PlayerSanity>();
                if (playerSanity != null)
                {
                    // ������ҵ� restoreSanity ����
                    playerSanity.restoreSanity();
                    Debug.Log("Sanity restored by SanRestorePick!");

                    // ���ٵ�ǰ����
                    Destroy(gameObject);
                }
            }
        }
    }
}
