using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostSanity : MonoBehaviour
{
    // 引用要调用的脚本
    public PlayerSanity playerSanity;

    // 当带有 "player" 标签的对象停留在触发器内时调用
    private void OnTriggerStay(Collider other)
    {

        //If player stay in the ghost's trigger, sanity lose
        if (other.CompareTag("Player"))
        {
            // 调用另一个脚本中的函数
            playerSanity.decreaseSanity();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检查是否是带有 "player" 标签的对象
        if (collision.gameObject.CompareTag("Player"))
        {
            // 打印消息
            playerSanity.directlyLoseSanity();
        }
    }



}
