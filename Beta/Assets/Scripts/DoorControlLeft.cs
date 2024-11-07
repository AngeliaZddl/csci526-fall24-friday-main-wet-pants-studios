using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;  // 参考门的 Animator
    public Transform player;       // 参考玩家的 Transform
    public float interactionDistance = 3f; // 玩家与门的交互距离
    // private bool animatorEnabled = false; // 跟踪 Animator 是否已启用

    void Start()
    {
       // doorAnimator.enabled = false;  // 游戏开始时禁用 Animator
    }

    void Update()
    {
        // 检测玩家与门的距离
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < interactionDistance)  // 如果玩家靠近门
        {
            // if (!animatorEnabled) 
            // {
            //     doorAnimator.enabled = true;  // 启用 Animator
            //     animatorEnabled = true;
            // }

            if (Input.GetKeyDown(KeyCode.F)) // 按下 F 键
            {
                // 检查当前动画状态
                if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorCloseLeft"))
                {
                    doorAnimator.SetTrigger("Open");  // 开门
                }
                else if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenLeft"))
                {
                    doorAnimator.SetTrigger("Close"); // 关门
                }
            }
        }
    }
}