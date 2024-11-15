using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // 菜单的 Panel 对象
    public GameObject menuOnScreen;
    [HideInInspector]
    public bool isMenuActive = false;

    void Start()
    {
        // 开始时隐藏菜单
        menuPanel.SetActive(false);

        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuOnScreen.SetActive(true);
    }

    void Update()
    {
        // 检测 M 键的按下
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMenuActive = !isMenuActive;
            menuPanel.SetActive(isMenuActive);
            menuOnScreen.SetActive(false);

            // 暂停或恢复游戏
            Time.timeScale = isMenuActive ? 0f : 1f;

            // 控制鼠标光标的显示和锁定状态
            if (isMenuActive)
            {
                ShowMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void ShowMenu()
    {
        isMenuActive = true;
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        // 暂停游戏时间
        Time.timeScale = 0f;

        // 显示并解锁鼠标光标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    // 加载 Level0 场景
    public void LoadLevel0()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("Level0");
    }

    // 加载 Level1 场景
    public void LoadLevel1()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("Level0.5");
    }

    public void LoadLevel2()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("Level1");
    }

    // 重启当前场景
    public void RestartLevel()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 退出游戏
    public void QuitGame()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 如果需要，在退出游戏前处理鼠标光标状态
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // 关闭菜单
    public void CloseMenu()
    {
        isMenuActive = false;
        menuPanel.SetActive(false);
        menuOnScreen.SetActive(true);

        // 恢复游戏
        Time.timeScale = 1f;

        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}