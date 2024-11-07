using UnityEngine;
using System.Collections;

public class SetUVLight : MonoBehaviour
{
    public Material revealMaterial;        // 需要更新的材质
    public Light myLight;                  // 要控制的灯光
    public float batteryUsagePerToggle = 20f; // 每次开启灯光消耗的电量
    public float autoOffTime = 10f;        // 灯光自动关闭的时间（秒）
    private float currentBattery = 100f;   // 当前电量（可以从其他地方传递进来）

    private bool isLightOn = false;        // 初始状态为关闭
    private Coroutine autoOffCoroutine;    // 记录当前自动关闭的协程

    void Start()
    {
        if (revealMaterial == null || myLight == null)
        {
            Debug.LogError("Reveal material or light reference is missing!");
            enabled = false;  // 如果缺少依赖，禁用脚本
            return;
        }

        myLight.enabled = false;  // 游戏开始时将灯光关闭
    }

    void Update()
    {
        // 检查玩家是否按下控制按钮并且电量充足
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLight();
        }

        // 仅当灯光开启时更新材质属性
        if (isLightOn)
        {
            UpdateLightProperties();
        }
    }

    void ToggleLight()
    {
        if (!isLightOn && currentBattery >= batteryUsagePerToggle)
        {
            // 开启灯光
            isLightOn = true;
            myLight.enabled = true;

            // 每次开启灯光时减少固定的电量
            currentBattery -= batteryUsagePerToggle;
            currentBattery = Mathf.Clamp(currentBattery, 0, 100); // 确保电量不会小于0
            Debug.Log("UV Light - 当前电量: " + currentBattery);

            // 启动或重置自动关闭的协程
            if (autoOffCoroutine != null)
            {
                StopCoroutine(autoOffCoroutine);
            }
            autoOffCoroutine = StartCoroutine(AutoTurnOff());
        }
        else if (isLightOn)
        {
            // 如果灯光已开启，再次按下 "F" 键时关闭灯光
            TurnOffLight();
        }
    }

    IEnumerator AutoTurnOff()
    {
        // 等待指定的自动关闭时间
        yield return new WaitForSeconds(autoOffTime);

        // 关闭灯光
        TurnOffLight();
    }

    void TurnOffLight()
    {
        // 关闭灯光并更新状态
        isLightOn = false;
        myLight.enabled = false;

        // 停止协程
        if (autoOffCoroutine != null)
        {
            StopCoroutine(autoOffCoroutine);
            autoOffCoroutine = null;
        }

        Debug.Log("UV Light - 已自动关闭");
    }

    private void UpdateLightProperties()
    {
        // 设置材质的灯光相关属性
        revealMaterial.SetVector("_LightPosition", myLight.transform.position);
        revealMaterial.SetVector("_LightDirection", -myLight.transform.forward);
        revealMaterial.SetFloat("_LightAngle", myLight.spotAngle);
    }
}
