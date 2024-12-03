using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject[] dropItems;  // 存放可能掉落的物品Prefab
    public float dropChance = 0.5f; // 掉落几率 (0.5表示50%)

    public void DropItem(Vector3 position)
    {
        if (dropItems.Length == 0)
        {
            Debug.LogWarning("No items assigned for dropping!");
            return;
        }

        if (Random.value <= dropChance)
        {
            // 随机选择一个物品 Prefab
            GameObject itemToDrop = dropItems[Random.Range(0, dropItems.Length)];

            // 确保生成位置在地面上方，避免嵌入地面
            Vector3 spawnPosition = position + Vector3.up * 0.5f; // 上方1单位高度
            GameObject droppedItem = Instantiate(itemToDrop, spawnPosition, Quaternion.identity);

            // 添加随机力，模拟自然掉落效果
            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomForce = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(1f, 2f),
                    Random.Range(-1f, 1f)
                );
                rb.AddForce(randomForce, ForceMode.Impulse);
            }

            // 为掉落物品添加碰撞稳定处理
            DropStabilizer stabilizer = droppedItem.AddComponent<DropStabilizer>();
            stabilizer.Initialize();
        }
    }
}
