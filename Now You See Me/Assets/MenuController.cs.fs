using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // 菜单面板
    private bool isMenuActive = false;

    void Start()
    {
        // 初始时隐藏菜单
        menuPanel.SetActive(false);

        // 隐藏鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 检测 M 键的按下
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);

        if (isMenuActive)
        {
            // 显示鼠标光标
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // 暂停游戏（可选）
            Time.timeScale = 0f;
        }
        else
        {
            // 隐藏鼠标光标
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // 恢复游戏（可选）
            Time.timeScale = 1f;
        }
    }

    // 以下是按钮的功能方法，需要在按钮的 OnClick 事件中绑定

    public void LoadLevel0()
    {
        // 恢复时间
        Time.timeScale = 1f;
        // 加载第0关卡
        SceneManager.LoadScene("Level0");
    }

    public void LoadLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        // 重新加载当前关卡
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        // 退出游戏
        Application.Quit();

        // 在编辑器模式下停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void CloseMenu()
    {
        ToggleMenu();
    }
}