using UnityEngine;

public class FloatingEffectWithRigidbody : MonoBehaviour
{
    public float amplitude = 0.5f; // 上下浮动的幅度
    public float frequency = 1f; // 上下浮动的频率

    private Vector3 startPosition;

    void Start()
    {
        // 保存初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 基于正弦函数计算上下浮动的位置
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
