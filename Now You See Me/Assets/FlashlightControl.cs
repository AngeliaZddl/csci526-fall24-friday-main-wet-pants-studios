using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashlightControl : MonoBehaviour
{
    public Light flashlight;               // 手电筒的 Light 组件
    public Slider batterySlider;           // 电量显示的 UI Slider
    public float maxBattery = 100f;        // 电量最大值
    public float batteryUsagePerToggle = 20f; // 每次开启手电筒消耗的电量
    public float autoOffTime = 10f;        // 手电筒自动关闭的时间（秒）

    private float currentBattery;          // 当前电量
    private bool isLightOn = false;        // 手电筒状态
    private Coroutine autoOffCoroutine;    // 记录自动关闭的协程

    void Start()
    {
        currentBattery = maxBattery;            // 初始化电量为最大值
        batterySlider.maxValue = maxBattery;    // 设置 UI Slider 的最大值
        batterySlider.value = currentBattery;   // 设置初始电量显示
        flashlight.enabled = false;             // 游戏开始时关闭手电筒
    }

    void Update()
    {
        // 按下 "F" 键来切换手电筒状态，且只有在电量不为0时才可以开启
        if (Input.GetKeyDown(KeyCode.F) && currentBattery > 0)
        {
            ToggleFlashlight();
        }

        // 更新电量条的显示
        batterySlider.value = currentBattery;
    }

    void ToggleFlashlight()
    {
        if (!isLightOn && currentBattery >= batteryUsagePerToggle)
        {
            // 开启手电筒
            isLightOn = true;
            flashlight.enabled = true;

            // 每次开启手电筒时减少固定的电量
            currentBattery -= batteryUsagePerToggle;
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery); // 确保电量不会小于0
            Debug.Log("当前电量: " + currentBattery);

            // 启动自动关闭协程
            if (autoOffCoroutine != null)
            {
                StopCoroutine(autoOffCoroutine);
            }
            autoOffCoroutine = StartCoroutine(AutoTurnOff());
        }
        else if (isLightOn)
        {
            // 如果手电筒已开启，再次按下时关闭手电筒
            TurnOffFlashlight();
        }
    }

    IEnumerator AutoTurnOff()
    {
        // 等待指定的自动关闭时间
        yield return new WaitForSeconds(autoOffTime);

        // 关闭手电筒
        TurnOffFlashlight();
    }

    void TurnOffFlashlight()
    {
        isLightOn = false;
        flashlight.enabled = false;
        Debug.Log("手电筒已自动关闭");
    }
}
