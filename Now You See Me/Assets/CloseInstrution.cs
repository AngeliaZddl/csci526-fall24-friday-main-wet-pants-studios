using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseInstruction : MonoBehaviour
{
    public GameObject instruction; // Instruction 面板
    public GameObject instrucOnScreen; // 浮动按钮
    // public GameObject closeButton; // 关闭按钮

    private bool isInstructionOpen = false;

    void Start()
    {
        // 初始化：显示浮动按钮，隐藏说明面板
        if (SceneManager.GetActiveScene().name == "Level0") {
            instrucOnScreen.SetActive(false);
            instruction.SetActive(true);
            isInstructionOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            instrucOnScreen.SetActive(true);
            instruction.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        // 检测 I 键的按下
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInstructionPanel();
        }
    }

    public void ToggleInstructionPanel()
    {
        isInstructionOpen = !isInstructionOpen;

        // 切换面板状态
        instruction.SetActive(isInstructionOpen);
        instrucOnScreen.SetActive(!isInstructionOpen);

        // 暂停或恢复游戏
        Time.timeScale = isInstructionOpen ? 0f : 1f;

        // 控制鼠标光标的显示和锁定状态
        Cursor.visible = isInstructionOpen;
        Cursor.lockState = isInstructionOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // 供关闭按钮调用的方法
    public void CloseInstructionPanel()
    {
        isInstructionOpen = false;

        instruction.SetActive(false);
        instrucOnScreen.SetActive(true);

        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}