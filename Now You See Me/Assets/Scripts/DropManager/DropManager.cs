using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject[] dropItems;       // 存放可能掉落的物品Prefab
    public float dropChance = 0.5f;      // 掉落几率 (0.5表示50%)
    public GameObject[] ghostObjects;   // 引用所有 Ghost 对象
    public LayerMask groundLayer;       // 地面层，用于碰撞检测

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

            // 确保生成位置完全在地面上方
            GameObject droppedItem = Instantiate(itemToDrop, position, Quaternion.identity);
            Collider droppedCollider = droppedItem.GetComponent<Collider>();
            if (droppedCollider != null)
            {
                Vector3 spawnPosition = position + Vector3.up * droppedCollider.bounds.extents.y * 2;
                droppedItem.transform.position = spawnPosition;
            }

            // 忽略小球与所有 Ghost 的碰撞
            if (droppedCollider != null)
            {
                foreach (GameObject ghost in ghostObjects)
                {
                    Collider ghostCollider = ghost.GetComponent<Collider>();
                    if (ghostCollider != null)
                    {
                        Physics.IgnoreCollision(droppedCollider, ghostCollider);
                        Debug.Log($"Ignoring collision between {droppedCollider.name} and {ghostCollider.name}");
                    }
                }
            }

            // 为掉落物品添加刚体并设置属性
            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // 高精度碰撞检测
                rb.useGravity = true; // 启用重力
                rb.isKinematic = false; // 启用物理模拟
                rb.mass = 1f; // 增加质量，避免高速度穿透

                Vector3 randomForce = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(1f, 3f),
                    Random.Range(-1f, 1f)
                );
                Vector3 randomTorque = new Vector3(
                    Random.Range(-5f, 5f),
                    Random.Range(-5f, 5f),
                    Random.Range(-5f, 5f)
                );

                rb.AddForce(randomForce, ForceMode.Impulse);
                rb.AddTorque(randomTorque, ForceMode.Impulse);
            }

            // 添加地面检测组件
            DropCollisionDetector detector = droppedItem.AddComponent<DropCollisionDetector>();
            detector.Initialize(rb, groundLayer);
        }
    }
}

// 自定义地面检测器
public class DropCollisionDetector : MonoBehaviour
{
    private Rigidbody rb;
    private LayerMask groundLayer;

    public void Initialize(Rigidbody rigidbody, LayerMask layer)
    {
        rb = rigidbody;
        groundLayer = layer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // 确保物体位置完全停在地面上
            Vector3 position = transform.position;
            position.y = collision.contacts[0].point.y + GetComponent<Collider>().bounds.extents.y;
            transform.position = position;

            // 停止物理运动
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // 禁用物理模拟
            }

            Debug.Log($"Object {gameObject.name} has landed on the ground and stopped.");
        }
    }
}
