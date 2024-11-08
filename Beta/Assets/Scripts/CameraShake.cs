using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 0.5f; // 抖动强度
    public float shakeFrequency = 40.0f; // 抖动频率
    private Vector3 originalPosition;

    private PlayerSanity playerSanityScript;  // 引用 PlayerSanity 脚本

    void Start()
    {
        originalPosition = transform.localPosition;

        // 获取 PlayerSanity 脚本
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerSanityScript = player.GetComponent<PlayerSanity>();
        }
        else
        {
            Debug.LogError("Player GameObject not found or does not have PlayerSanity component.");
        }
    }

    void Update()
    {
        // 检查 playerSanity 是否小于 20
        if (playerSanityScript != null && playerSanityScript.playerSanity < 20)
        {
            // 一直保持抖动状态
            Shake();
        }
        else
        {
            // 恢复原位置，如果 sanity 大于等于 20
            transform.localPosition = originalPosition;
        }
    }

    void Shake()
    {
        // 使用 Perlin 噪声生成随机偏移
        float shakeAmountX = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0) - 0.5f) * shakeIntensity;
        float shakeAmountY = (Mathf.PerlinNoise(0, Time.time * shakeFrequency) - 0.5f) * shakeIntensity;

        // 应用抖动偏移到摄像头位置
        transform.localPosition = originalPosition + new Vector3(shakeAmountX, shakeAmountY, 0);
    }
}
