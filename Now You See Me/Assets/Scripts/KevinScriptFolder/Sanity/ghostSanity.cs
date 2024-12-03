using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghostSanity : MonoBehaviour
{
 
    public PlayerSanity playerSanity;
    public KevinCameraShake kevinCameraShake;
    public GameObject jumpScareObject;
    public GameObject colorBar; // 需要更改颜色的目标对象

    //public GhostBack ghostBackScript; // 引用 GhostBack 脚本




    private void setSanBarToRedColor()
    {
        colorBar.GetComponent<Image>().color = Color.red;
    }

    private void setSanBarToGreenColor()
    {
        colorBar.GetComponent<Image>().color = Color.green;
    }


    // 当带有 "player" 标签的对象停留在触发器内时调用
    private void OnTriggerStay(Collider other)
    {


        //If player stay in the ghost's trigger, sanity lose
        if (other.CompareTag("Ghost"))
        {

            // 获取 Ghost 上的 GhostController 脚本
            GhostController ghostController = other.GetComponent<GhostController>();

            // 如果 Ghost 存在并且正在 shaking，则跳过掉血逻辑
            if (ghostController != null && ghostController.getShakeStatus())
            {
                setSanBarToGreenColor();
                Debug.Log("Ghost is shaking. No sanity loss.");
                return; // 跳过后续逻辑
            }



            setSanBarToRedColor();
            // 调用另一个脚本中的函数
            playerSanity.decreaseSanity();

        }
    }

    // 当带有 "Ghost" 标签的对象离开触发器时调用
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            setSanBarToGreenColor();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        // 检查是否是带有 "player" 标签的对象
        if (collision.gameObject.CompareTag("Ghost"))
        {
            GhostController collidedGhostController = collision.gameObject.GetComponent<GhostController>();

            GhostBack ghostBackScript = collision.gameObject.GetComponent<GhostBack>();

            // 如果 Ghost 正在 shaking
            if (collidedGhostController.getShakeStatus())
            {
                Debug.Log("Ghost is shaking. Losing sanity");


                // 让 Ghost 返回原位
                //if (ghostBackScript != null)
                //{
                //    ghostBackScript.ReturnToOriginalPosition();
                //}

                return; // 跳过后续逻辑
            }

            setSanBarToRedColor();
            playerSanity.directlyLoseSanity();
            //在摇摆之前让ghost返回原位吧 如果有api要做的就在这里弄好了
            //ghostBackScript.ReturnToOriginalPosition();
            // 获取碰撞对象的 GhostBack 脚本，并调用 ReturnToOriginalPosition

            if (ghostBackScript != null)
            {
                ghostBackScript.ReturnToOriginalPosition();
            }


            StartCoroutine(ShakeAndWait());

        }
    }


    private IEnumerator ShakeAndWait()
    {
        // 调用相机抖动
        kevinCameraShake.StartShake();
        jumpScareObject.SetActive(true);


        // 等待 1 秒
        yield return new WaitForSeconds(1);
        kevinCameraShake.StopShake();


        jumpScareObject.SetActive(false);
        setSanBarToGreenColor();

    }


}
