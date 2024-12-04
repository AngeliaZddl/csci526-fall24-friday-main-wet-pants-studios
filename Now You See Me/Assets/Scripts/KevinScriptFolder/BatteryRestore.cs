using UnityEngine;

public class BatteryRestore : MonoBehaviour
{
    public float destroyRadius = 2f; // ����뾶
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

            // �������С�ڵ��� destroyRadius����������
            if (distanceToPlayer <= destroyRadius)
            {
                Destroy(gameObject);
                Debug.Log("BatteryRestore object destroyed due to proximity.");
            }
        }
    }
}
