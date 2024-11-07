using UnityEngine;

public class DoorControllerRight : MonoBehaviour
{
    public Animator doorAnimator1;
    public Transform player1;
    public float interactionDistance1 = 3f;
    private bool doorOpened = false;
    public JumpOnDoorOpen jumpObjectScript;
    // private bool animatorEnabled = false; // 用来跟踪 Animator 是否启用

    void Start()
    {
        //doorAnimator1.enabled = false;  // 游戏开始时禁用 Animator
    }
   


    void Update()
    {
        float distance = Vector3.Distance(player1.position, transform.position);

        if (distance < interactionDistance1)
        {
            // if (!animatorEnabled) 
            // {
            //     doorAnimator1.enabled = true;  // 玩家靠近时启用 Animator
            //     animatorEnabled = true;
            // }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("DoorCloseRight"))
                {
                    doorAnimator1.SetTrigger("Open");
                    
                    if (jumpObjectScript != null)
                    {
                        jumpObjectScript.OnDoorOpened();
                        jumpObjectScript.Update();  // 调用 jumpObjectScript 的方法
                    }
                }
                else if (doorAnimator1.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenRight"))
                {
                    doorAnimator1.SetTrigger("Close");
                }
            }
        }
    }
}