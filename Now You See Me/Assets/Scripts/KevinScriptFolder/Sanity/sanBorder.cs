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

            // 如果 sanity 小于等于 50，则逐渐增加透明度
            if (currentSanity <= 50)
            {
                // 根据 sanity 值计算 Alpha 值（越小越接近 maxAlpha）
                float alpha = Mathf.Lerp(maxAlpha, minAlpha, currentSanity / 50f);

                // 设置 Image 的透明度
                SetImageAlpha(alpha);
            }
            else
            {
                // 当 sanity 大于 50 时，将透明度恢复为 minAlpha
                SetImageAlpha(minAlpha);
            }
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
