using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostSanity : MonoBehaviour
{
 
    public PlayerSanity playerSanity;
    public KevinCameraShake kevinCameraShake;
    public GameObject jumpScareObject;
    public GhostBack ghostBackScript; // 引用 GhostBack 脚本




    // 当带有 "player" 标签的对象停留在触发器内时调用
    private void OnTriggerStay(Collider other)
    {

        //If player stay in the ghost's trigger, sanity lose
        if (other.CompareTag("Ghost"))
        {

            // 调用另一个脚本中的函数
            playerSanity.decreaseSanity();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检查是否是带有 "player" 标签的对象
        if (collision.gameObject.CompareTag("Ghost"))
        {
            
            playerSanity.directlyLoseSanity();
            //在摇摆之前让ghost返回原位吧 如果有api要做的就在这里弄好了
            ghostBackScript.ReturnToOriginalPosition();


            StartCoroutine(ShakeAndWait());



        }
    }

    private IEnumerator ShakeAndWait()
    {
        // 调用相机抖动
        kevinCameraShake.StartShake();
        jumpScareObject.SetActive(true);


        // 等待 3 秒
        yield return new WaitForSeconds(3);
        kevinCameraShake.StopShake();


        jumpScareObject.SetActive(false);

    }


}
