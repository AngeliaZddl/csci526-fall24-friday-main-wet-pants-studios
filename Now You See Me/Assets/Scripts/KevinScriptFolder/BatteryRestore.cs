using UnityEngine;

public class BatteryRestore : MonoBehaviour
{
    public float destroyRadius = 2f; // 距离半径
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

            // 如果距离小于等于 destroyRadius，销毁物体
            if (distanceToPlayer <= destroyRadius)
            {
                Destroy(gameObject);
                Debug.Log("BatteryRestore object destroyed due to proximity.");
            }
        }
    }
}
