using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanBorder : MonoBehaviour
{
    private Image bloodBorderImage; // 自动获取当前对象上的 Image 组件
    public PlayerSanity playerSanityScript; // 引用 PlayerSanity 脚本

    private float maxAlpha = 1f; // 最大不透明度
    private float minAlpha = 0f; // 最小透明度

    void Start()
    {
        // 自动获取挂载在当前 GameObject 上的 Image 组件
        bloodBorderImage = GetComponent<Image>();

        // 确保引用了 PlayerSanity 脚本
        if (playerSanityScript == null)
        {
            Debug.LogWarning("PlayerSanity 脚本未被设置，请在 Inspector 中引用");
        }
    }

    void Update()
    {
        if (playerSanityScript != null)
        {
            // 获取当前的 sanity 值
            float currentSanity = playerSanityScript.playerSanity;

            // 根据 sanity 值从 100 到 0 计算 Alpha 值
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (100f - currentSanity) / 100f);

            // 设置 Image 的透明度
            SetImageAlpha(alpha);
        }
    }

    // 设置 Image 的透明度
    private void SetImageAlpha(float alpha)
    {
        if (bloodBorderImage != null)
        {
            Color currentColor = bloodBorderImage.color;
            currentColor.a = alpha;
            bloodBorderImage.color = currentColor;
        }
    }
}
