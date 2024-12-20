using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    // 单例实例
    public static MainMenuController Instance;

    // 定义你想要加载的场景
    public string gameScene1 = "Level0";
    public string gameScene2 = "Level0.5";
    public string gameScene3 = "Level1";
    
    void Start() {
        Time.timeScale = 1f;

        // 显示并解锁鼠标光标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // 启动游戏并加载游戏场景
    public void Totu()
    {
        Debug.Log("Loading SampleScene...");
        SceneManager.LoadScene(gameScene1);  // 加载 SampleScene
    }

    public void Level1()
        {
            Debug.Log("Loading SampleScene...");
            SceneManager.LoadScene(gameScene2);  // 加载 SampleScene
        }

    public void Level2()
    {
        Debug.Log("Loading SampleScene...");
        SceneManager.LoadScene(gameScene3);  // 加载 SampleScene
    }

    public void Instruction()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 隐藏并锁定鼠标光标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("Instruction");
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