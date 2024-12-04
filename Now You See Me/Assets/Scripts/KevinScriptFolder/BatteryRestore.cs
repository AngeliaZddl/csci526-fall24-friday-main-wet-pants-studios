using UnityEngine;

public class BatteryRestore : MonoBehaviour
{
    public float destroyRadius = 2f;        // 销毁半径
    public float restoreAmount = 25f;      // 恢复的电量值
    private Transform playerTransform;     // 玩家 Transform
    private UVLightMechanics uvLightMechanics; // UVLightMechanics 脚本引用

    private void Start()
    {
        // 获取玩家 Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No Player found in the scene! Make sure the Player is tagged as 'Player'.");
        }

        // 获取 UVLightMechanics 脚本
        uvLightMechanics = FindObjectOfType<UVLightMechanics>();
        if (uvLightMechanics == null)
        {
            Debug.LogError("UVLightMechanics script not found in the scene!");
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= destroyRadius)
            {
                // 恢复电量
                if (uvLightMechanics != null && uvLightMechanics.batteryBar != null)
                {
                    uvLightMechanics.batteryBar.value += restoreAmount;

                    // 确保电量不超过最大值
                    if (uvLightMechanics.batteryBar.value > uvLightMechanics.batteryBar.maxValue)
                    {
                        uvLightMechanics.batteryBar.value = uvLightMechanics.batteryBar.maxValue;
                    }
                }

                // 销毁电池对象
                Destroy(gameObject);
                Debug.Log("Battery restored and object destroyed.");
            }
        }
    }
}
