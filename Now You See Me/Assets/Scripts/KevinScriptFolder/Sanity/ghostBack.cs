using UnityEngine;

public class GhostBack : MonoBehaviour
{
    private Vector3 originalPosition; // 记录初始位置

    void Start()
    {
        // 在开始时记录物体的初始位置
        originalPosition = transform.position;
    }

    // 将物体移回初始位置的公开方法
    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
    }
}
