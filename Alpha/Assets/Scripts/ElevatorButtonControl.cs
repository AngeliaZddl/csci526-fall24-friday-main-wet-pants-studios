using UnityEngine;
using TMPro;

public class ElevatorButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // 用于控制电梯门的动画
    public Transform player;       // 玩家对象
    public float interactionDistance = 3f;  // 玩家与按钮的交互距离
    private bool isDoorOpen = false;  // 电梯门是否已经打开
    public TMP_Text statusText;  // UI文本提示
    public GameObject passwordPanel;  // 密码输入面板UI
    public LayerMask obstacleLayerMask;  // 用于检测是否有障碍物
    public Puzzle puzzleScript;  // 引用 Puzzle 脚本

    private bool isInteracting = false;  // 是否处于交互状态

    void Start()
    {
        passwordPanel.SetActive(false);  // 隐藏密码面板
        statusText.enabled = false;  // 隐藏状态提示

        // 启动时锁定并隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 计算玩家与按钮的距离
        float distance = Vector3.Distance(player.position, transform.position);

        // 如果电梯门未打开且玩家在交互距离内且没有障碍物，则允许打开UI
        if (!isDoorOpen && distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            if (!isInteracting)
            {
                statusText.text = "Press E to enter password";
                statusText.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenPasswordPanel();  // 显示密码面板
                }
            }
        }
        else
        {
            statusText.enabled = false;  // 当超出距离时隐藏提示
        }

        // 检测玩家是否按下 Enter 键来提交密码
        if (passwordPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            puzzleScript.Submit();  // 调用 Puzzle 的 Submit 方法来验证密码
        }
    }

    // 打开密码输入面板
    private void OpenPasswordPanel()
    {
        if (!isDoorOpen)  // Only open the panel if the door is not opened yet
        {
            isInteracting = true;  // 标记为交互状态
            passwordPanel.SetActive(true);  // 显示密码面板
            statusText.enabled = false;  // 隐藏提示

            // 解锁鼠标并显示
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // 关闭密码面板并重置交互状态
    public void ClosePasswordPanel()
    {
        isInteracting = false;  // 重置交互状态
        passwordPanel.SetActive(false);  // 隐藏密码面板

        // 锁定并隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 检查玩家与按钮之间是否有障碍物
    bool IsObstacleBetweenPlayerAndButton()
    {
        Vector3 directionToButton = (transform.position - player.position).normalized;
        float distanceToButton = Vector3.Distance(player.position, transform.position);

        if (Physics.Raycast(player.position, directionToButton, distanceToButton, obstacleLayerMask))
        {
            return true;  // 检测到障碍物
        }

        return false;  // 无障碍物，允许交互
    }

    // 打开电梯门
    public void OpenDoor()
    {
        if (!isDoorOpen)
        {
            doorAnimator.SetTrigger("Open");
            isDoorOpen = true;
            ClosePasswordPanel();  // 关闭密码面板
        }
    }
}
