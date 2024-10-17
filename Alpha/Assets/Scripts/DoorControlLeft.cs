using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;  // 参考门的 Animator
    public Transform player;       // 参考玩家的 Transform
    public float interactionDistance = 3f; // 玩家与门的交互距离
                                           // private bool animatorEnabled = false; // 跟踪 Animator 是否已启用

    public float rotationSpeed = 180f;    // 每秒旋转的角度数
    public GameObject pivot;              // 门的 pivot 对象，门作为其子对象
    private bool doorOpened = false;      // 记录门是否已经打开
    private float targetAngle = 0f;       // 目标旋转角度
    private float currentAngle = 0f;      // 当前旋转角度
    private bool playerInTrigger = false; // 玩家是否在触发区域内

    void Start()
    {
        currentAngle = pivot.transform.localEulerAngles.y;  // 初始化当前角度
    }

    void Update()
    {
        // 只有玩家在触发区域内，按下 F 键才能切换门的开关状态
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // 切换目标角度，无论门是否正在旋转
            if (doorOpened)
            {
                targetAngle = 0f; // 关闭门
            }
            else
            {
                targetAngle = -90f; // 开门
            }

            doorOpened = !doorOpened; // 切换门的状态
        }

        // 平滑旋转门，实时更新目标角度
        if (Mathf.Abs(currentAngle - targetAngle) > 0.1f)
        {
            // 计算当前角度，LerpAngle 实现平滑旋转
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed / 90f);

            // 更新 pivot 的旋转，带动门旋转
            pivot.transform.localEulerAngles = new Vector3(0f, currentAngle, 0f);
        }
    }

    // 当玩家进入触发区域时，允许开关门
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 假设玩家的 tag 是 "Player"
        {
            playerInTrigger = true; // 玩家进入触发区域
        }
    }

    // 当玩家离开触发区域时，禁止开关门
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; // 玩家离开触发区域
        }
    }

    //void Update()
    //{


    // kevin comment this part as unuseful

    // if (!animatorEnabled) 
    // {
    //     doorAnimator.enabled = true;  // 启用 Animator
    //     animatorEnabled = true;
    // }

    //if (Input.GetKeyDown(KeyCode.F)) // 按下 F 键
    //{


    //// 检查当前动画状态
    //if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorCloseLeft"))
    //{
    //    doorAnimator.SetTrigger("Open");  // 开门
    //}
    //else if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenLeft"))
    //{
    //    doorAnimator.SetTrigger("Close"); // 关门
    //}

    //}
    // kevin comment this part as unuseful
    //}
    //}
}