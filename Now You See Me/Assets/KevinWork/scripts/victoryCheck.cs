using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics; // 引入 Unity Analytics

public class victoryCheck : MonoBehaviour
{
    public GameObject victoryCanvas; // 胜利 UI Canvas

    private float startTime; // 记录关卡开始时间

    private void Start()
    {
        // 在关卡开始时记录时间
        startTime = Time.time;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // 检查是否是玩家进入触发区域
        if (collision.gameObject.tag == "Player")
        {
            // 计算通关所用时间
            float completionTime = Time.time - startTime;

                { "levelName", "Level 1" },         // 当前关卡名称（根据需要修改）
                { "completionTime", completionTime } // 通关时间（单位：秒）
            });

            Debug.Log("Level complete! Time taken: " + completionTime + " seconds");

            // 显示胜利 Canvas
            victoryCanvas.SetActive(true);

            // 延迟退出游戏
            Invoke("QuitGame", 1f);
        }
    }

    void NextLevel()
    {
        // 停止游戏
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 在编辑器中停止游戏
#else
        Application.Quit(); // 在构建版本中退出应用
#endif
    }
}
