using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    // 单例实例
    public static MainMenuController Instance;

    // 定义你想要加载的场景
    public string gameScene = "SampleScene";

    // 启动游戏并加载游戏场景
    public void StartGame()
    {
        Debug.Log("Loading SampleScene...");
        SceneManager.LoadScene(gameScene);  // 加载 SampleScene
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // 如果在编辑器中，停止播放
        #else
            Application.Quit();  // 在构建后退出游戏
        #endif
    }
}