using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPick : MonoBehaviour
{
    public GameObject batteryObject; 
    public float activationRadius = 2.5f; // active radius
    private Transform playerTransform; // ��� Transform

    private void Start()
    {
        // ������Ҷ��󲢻�ȡ�� Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No Player found in the scene! Make sure the Player is tagged as 'Player'.");
        }
    }

    private void Update()
    {
        // �������Ƿ����
        if (playerTransform != null)
        {
            // ��������뵱ǰ����ľ���
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // �������С�ڰ뾶
            if (distanceToPlayer <= activationRadius)
            {
                // ����Ŀ�����
                if (batteryObject != null)
                {
                    batteryObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("No object assigned to activate.");
                }

                // ���ٵ�ǰ����
                Destroy(gameObject);
            }
        }
    }
}
