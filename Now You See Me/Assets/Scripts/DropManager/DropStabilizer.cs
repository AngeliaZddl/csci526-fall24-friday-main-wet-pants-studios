using UnityEngine;

public class DropStabilizer : MonoBehaviour
{
    private bool hasLanded = false;

    // 初始化方法，用于状态重置
    public void Initialize()
    {
        hasLanded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 打印碰撞对象的名称，帮助调试
        Debug.Log($"{gameObject.name} collided with: {collision.gameObject.name}");

        // 检查是否碰撞到地板（确保地板的 Tag 是 "Ground"）
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 确保只处理第一次碰撞
            if (!hasLanded)
            {
                hasLanded = true;

                // 停止物品的物理运动
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;        // 停止移动
                    rb.angularVelocity = Vector3.zero; // 停止旋转
                    rb.isKinematic = true;            // 冻结物理
                }

                // 打印日志，确认物品已稳定在地面上
                Debug.Log($"{gameObject.name} has landed on the ground and stopped moving.");
            }
        }
        else
        {
            // 如果碰撞的不是地板，也打印日志，帮助排查问题
            Debug.LogWarning($"{gameObject.name} collided with a non-ground object: {collision.gameObject.name}");
        }
    }
}
