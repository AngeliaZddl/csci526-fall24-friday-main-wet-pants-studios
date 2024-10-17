using UnityEngine;

public class DoorControllerRight : MonoBehaviour
{
    //public Animator doorAnimator1;
    //public Transform player1;
    //public float interactionDistance1 = 3f;
    //private bool doorOpened = false;
    //public JumpOnDoorOpen jumpObjectScript;
    //// private bool animatorEnabled = false; // 用来跟踪 Animator 是否启用


    public Transform player1;                  // 玩家的位置
    public float rotationSpeed = 180f;         // 旋转的速度
    public GameObject pivot;                   // 门的旋转枢轴点
    public JumpOnDoorOpen jumpObjectScript;    // 用于在门打开时触发额外动作

    private bool doorOpened = false;           // 记录门是否已经打开
    private float targetAngle = 0f;            // 目标旋转角度
    private float currentAngle = 0f;           // 当前旋转角度
    private bool playerInTrigger = false;      // 玩家是否在触发区域内

    void Start()
    {
        // 初始化 currentAngle 和 targetAngle，确保与门的初始状态一致
        currentAngle = pivot.transform.localEulerAngles.y;  // 当前的旋转角度
        targetAngle = 0f;  // 初始状态为门关闭时
    }

    void Update()
    {
        // 只有玩家在触发区域内，按住 F 键才能切换门的开关状态
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // 切换目标角度，无论门是否正在旋转
            if (doorOpened)
            {
                targetAngle = 0f;  // 关闭门
            }
            else
            {
                targetAngle = -90f;  // 向右开门
                if (jumpObjectScript != null)
                {
                    jumpObjectScript.OnDoorOpened();  // 触发 jumpObjectScript 的开门逻辑
                    jumpObjectScript.Update();  // 调用 jumpObjectScript 的方法
                }
            }

            doorOpened = !doorOpened;  // 切换门的状态
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
            playerInTrigger = true;  // 玩家进入触发区域
        }
    }

    // 当玩家离开触发区域时，禁止开关门
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;  // 玩家离开触发区域
        }
    }

    //void Start()
    //{
    //    //doorAnimator1.enabled = false;  // 游戏开始时禁用 Animator
    //}



    //void Update()
    //{
    //    float distance = Vector3.Distance(player1.position, transform.position);

    //    if (distance < interactionDistance1)
    //    {
    //        // if (!animatorEnabled) 
    //        // {
    //        //     doorAnimator1.enabled = true;  // 玩家靠近时启用 Animator
    //        //     animatorEnabled = true;
    //        // }

    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("DoorCloseRight"))
    //            {
    //                doorAnimator1.SetTrigger("Open");

    //                if (jumpObjectScript != null)
    //                {
    //                    jumpObjectScript.OnDoorOpened();
    //                    jumpObjectScript.Update();  // 调用 jumpObjectScript 的方法
    //                }
    //            }
    //            else if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenRight"))
    //            {
    //                doorAnimator1.SetTrigger("Close");
    //            }
    //        }
    //    }
    //}
}