using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    public GameObject deathPanel;         // 死亡通知面板
    public float delayBeforeDeath = 0.02f;   // 跳出物体后延迟时间
    // public string mainMenuSceneName = "MainMenu";  // 主菜单场景的名称

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
        yield return new WaitForSeconds(delayBeforeDeath);  // 延迟

        // 确保卸载完成后再加载主菜单
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}