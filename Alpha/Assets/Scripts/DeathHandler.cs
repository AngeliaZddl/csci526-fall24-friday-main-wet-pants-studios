using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // 用于场景管理

public class DeathHandler : MonoBehaviour
{
    public GameObject deathPanel;      // 死亡通知面板
    public GameObject jumpObject;      // 跳出的物体
    public float delayBeforeDeath = 2f;  // 跳出物体后延迟时间，单位：秒
    public string mainMenuSceneName = "MainMenu";  // 主菜单场景的名称

    private JumpOnDoorOpen jumpObjectstatus;  // 检测 jumpObject 是否出现

    void Start() 
    {
        deathPanel.SetActive(false);  // 确保一开始死亡面板不可见
    }

    void Update()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        PlayerSanity ps = p.GetComponent<PlayerSanity>();
        
        // 如果 playerSanity 变为 0，启动死亡处理
        if (ps.playerSanity <= 0 && !deathPanel.activeInHierarchy)  // 防止重复执行
        {
            deathPanel.SetActive(true);  // 显示死亡通知面板
            StartCoroutine(HandleDeath());  // 启动协程处理返回主菜单
        }
    }

    // 协程处理死亡后的流程
    private IEnumerator HandleDeath()
    {
        // 延迟 delayBeforeDeath 秒后执行返回主菜单操作
        yield return new WaitForSeconds(delayBeforeDeath);

        // 返回主菜单
        ReturnToMainMenu();
    }

    // 返回主菜单
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);  // 加载主菜单场景
    }
}