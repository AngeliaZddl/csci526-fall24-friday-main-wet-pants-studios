using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseInstruction : MonoBehaviour
{
    public GameObject instruction; // Instruction 面板

    [SerializeField]
    private GameObject menuPenal;
    void Start()
    {
        // 初始化：显示浮动按钮，隐藏说明面板
        instruction.SetActive(false);
        
    }

    void Update()
    {
        
    }

    

    // 供关闭按钮调用的方法
    public void CloseInstructionPanel()
    {
        instruction.SetActive(false);
        menuPenal.SetActive(true);
    }
}