using UnityEngine;
using System.Collections;

public class KevinCameraShake : MonoBehaviour
{
    public float shakeMagnitude = 0.2f; // 抖动幅度
    public bool isShaking = false; // 控制相机是否抖动的开关

    private Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        // 如果 isShaking 为 true，则持续抖动
        if (isShaking)
        {
            Vector3 shakePos = originalPos + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = shakePos;
        }
        else
        {
            // 恢复原始位置
            transform.localPosition = originalPos;
        }
    }

    public void StartShake()
    {
        isShaking = true; // 启动抖动
    }

    public void StopShake()
    {
        isShaking = false; // 停止抖动
    }
}
