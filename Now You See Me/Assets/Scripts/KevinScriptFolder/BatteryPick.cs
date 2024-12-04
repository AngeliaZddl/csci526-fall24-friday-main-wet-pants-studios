using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPick : MonoBehaviour
{
    public GameObject batteryObject; 
    public float activationRadius = 2.5f; // active radius
    private Transform playerTransform; // 玩家 Transform

    private void Start()
    {
        // 查找玩家对象并获取其 Transform
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
        // 检查玩家是否存在
        if (playerTransform != null)
        {
            // 计算玩家与当前物体的距离
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // 如果距离小于半径
            if (distanceToPlayer <= activationRadius)
            {
                // 激活目标对象
                if (batteryObject != null)
                {
                    batteryObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("No object assigned to activate.");
                }

                // 销毁当前物体
                Destroy(gameObject);
            }
        }
    }
}
