using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanRestorePIck : MonoBehaviour
{
    public float radius = 2.5f; // 检测半径
    private Transform playerTransform; // 玩家的位置引用

    private void Start()
    {
        // 找到玩家对象，并获取其 Transform
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
        // 如果玩家存在
        if (playerTransform != null)
        {
            // 计算与玩家的距离
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // 如果距离小于半径，则触发逻辑
            if (distanceToPlayer <= radius)
            {
                // 获取玩家上的 PlayerSanity 脚本
                PlayerSanity playerSanity = playerTransform.GetComponent<PlayerSanity>();
                if (playerSanity != null)
                {
                    // 调用玩家的 restoreSanity 方法
                    playerSanity.restoreSanity();
                    Debug.Log("Sanity restored by SanRestorePick!");

                    // 销毁当前物体
                    Destroy(gameObject);
                }
            }
        }
    }
}
