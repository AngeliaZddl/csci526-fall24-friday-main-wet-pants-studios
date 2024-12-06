using UnityEngine;
using UnityEngine.SceneManagement; // 导入SceneManagement以加载场景

public class InstructionController : MonoBehaviour
{   
    void Start()
    {
        // 显示并解锁鼠标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackToPreviousScene()
    {   
        SceneManager.LoadScene("MainMenu");
    }
}

