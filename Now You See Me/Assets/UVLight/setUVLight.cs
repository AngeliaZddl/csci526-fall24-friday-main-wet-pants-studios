using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setUVLight : MonoBehaviour
{
    public Material revealMaterial;         // 手电筒光照下的材质
    public Light myLight;                   // 手电筒的光源
    public List<GameObject> ghosts;         // 幽灵列表
    public Slider batteryBar;               // 电池条 UI 滑动条
    public float flashDuration = 0.1f;      // 闪光的持续时间
    public float boostedIntensity = 20f;    // 闪光时的亮度强度
    public float boostedSpotAngle = 60f;    // 闪光时的光照角度
    public float boostedRange = 20f;        // 闪光时的光照范围
    public float stopDuration = 3f;         // 幽灵暂停的时间长度
    public float batteryDrainAmount = 25f;  // 每次闪光减少的电池量
    public float maxBattery = 100f;         // 最大电池容量

    private float originalIntensity;        // 手电筒的原始亮度
    private float originalSpotAngle;        // 手电筒的原始照射角度
    private float originalRange;            // 手电筒的原始照射范围
    private Color originalLightColor;       // 手电筒的原始颜色

    void Start()
    {
        // 记录手电筒的原始亮度、角度、范围和颜色
        originalIntensity = myLight.intensity;
        originalSpotAngle = myLight.spotAngle;
        originalRange = myLight.range;
        originalLightColor = myLight.color;

        // 初始化电池容量为最大值，并更新电池条 UI
        if (batteryBar != null)
        {
            batteryBar.maxValue = maxBattery;
            batteryBar.value = maxBattery;
        }
    }

    void Update()
    {
        // 更新手电筒光照的方向和位置，用于动态效果
        revealMaterial.SetVector("_LightPosition", myLight.transform.position);
        revealMaterial.SetVector("_LightDirection", -myLight.transform.forward);
        revealMaterial.SetFloat("_LightAngle", myLight.spotAngle);

        // 检测按下 F 键
        if (Input.GetKeyDown(KeyCode.F) && batteryBar.value > 0)
        {
            StartCoroutine(CameraFlashEffect()); // 执行相机闪光效果
            StopGhostsInLightRange(); // 暂停光照范围内的幽灵
            DrainBattery(); // 减少电池容量
        }

        // 当电池电量为 0 时关闭手电筒
        if (batteryBar.value <= 0)
        {
            myLight.enabled = false;
        }
        else
        {
            myLight.enabled = true;
        }
    }

    IEnumerator CameraFlashEffect()
    {
        // 设置手电筒亮度、角度、范围和颜色为闪光效果
        myLight.intensity = boostedIntensity;
        myLight.spotAngle = boostedSpotAngle;
        myLight.range = boostedRange;
        myLight.color = Color.white;

        // 短暂等待，模拟闪光的持续时间
        yield return new WaitForSeconds(flashDuration);

        // 恢复手电筒的原始亮度、角度、范围和颜色
        myLight.intensity = originalIntensity;
        myLight.spotAngle = originalSpotAngle;
        myLight.range = originalRange;
        myLight.color = originalLightColor;
    }

    void StopGhostsInLightRange()
    {
        // 遍历幽灵列表，找到在光照范围内的幽灵并暂停它们
        foreach (GameObject ghost in ghosts)
        {
            if (IsGhostInLightRange(ghost))
            {
                StartCoroutine(PauseGhostMovement(ghost));
            }
        }
    }

    IEnumerator PauseGhostMovement(GameObject ghost)
    {
        // 获取幽灵的移动组件 (RandomBouncingMovement)
        var movement = ghost.GetComponent<RandomBouncingMovement>();
        if (movement != null)
        {
            movement.isPaused = true; // 暂停幽灵的移动
            yield return new WaitForSeconds(stopDuration); // 让幽灵暂停 stopDuration 秒
            movement.isPaused = false; // 恢复幽灵的移动
        }
    }

    bool IsGhostInLightRange(GameObject ghost)
    {
        // 检查幽灵是否在手电筒光照范围内
        Vector3 toGhost = ghost.transform.position - myLight.transform.position;
        float angleToGhost = Vector3.Angle(myLight.transform.forward, toGhost);

        // 判断幽灵是否在光照角度和距离范围内
        return angleToGhost < myLight.spotAngle / 2 && toGhost.magnitude < myLight.range;
    }

    void DrainBattery()
    {
        // 每次使用时减少电池容量
        batteryBar.value -= batteryDrainAmount;

        // 确保电池容量不会小于 0
        if (batteryBar.value < 0)
        {
            batteryBar.value = 0;
        }
    }
}
