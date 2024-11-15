using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SenceHalfDeathRecord : MonoBehaviour
{
    public static int SenceHalfCounter = 0; // 使用静态变量记录死亡次数
    private bool SenceHalfChecker = false; // 防止重复记录死亡

    public PlayerSanity playerSanityScript; // 引用 PlayerSanity 脚本

    void Update()
    {
        // 确保引用了 PlayerSanity 脚本
        if (playerSanityScript != null)
        {
            if (playerSanityScript.playerSanity <= 0)
            {
                // 如果还没有记录死亡，增加死亡计数
                if (!SenceHalfChecker)
                {
                    SenceHalfCounter++;
                    Debug.Log("DeathCountTimes: " + SenceHalfCounter);
                    SenceHalfChecker = true; // 标记为已记录
                }
            }
            else
            {
                // 当 sanity 恢复为正数时，解除限制
                SenceHalfChecker = false;
            }
        }
    }

    public int GetDeathCount()
    {
        return SenceHalfCounter;
    }


}
