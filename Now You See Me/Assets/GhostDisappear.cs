using System.Collections;
using UnityEngine;

public class GhostDisappear : MonoBehaviour
{
    public Light flashlight;                // 手电筒的 Light 组件
    public float disappearDuration = 3f;    // 幽灵消失的持续时间
    public bool disappearIndefinitely = false; // 是否永久消失（教程关卡）

    private Renderer ghostRenderer;         // 控制幽灵的可见性
    private Collider ghostCollider;         // 控制幽灵的碰撞
    private bool isDisappeared = false;     // 跟踪幽灵是否已消失

    void Start()
    {
        ghostRenderer = GetComponent<Renderer>();
        ghostCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // 检查幽灵是否被手电筒光照到
        if (flashlight.enabled && !isDisappeared && IsIlluminatedByFlashlight())
        {
            StartCoroutine(DisappearTemporarily());
        }
    }

    private bool IsIlluminatedByFlashlight()
    {
        Vector3 directionToGhost = transform.position - flashlight.transform.position;
        float angleToGhost = Vector3.Angle(flashlight.transform.forward, directionToGhost);

        // 检查幽灵是否在手电筒光锥内
        if (angleToGhost < flashlight.spotAngle / 2 && directionToGhost.magnitude < flashlight.range)
        {
            Ray ray = new Ray(flashlight.transform.position, directionToGhost);
            if (Physics.Raycast(ray, out RaycastHit hit, flashlight.range))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator DisappearTemporarily()
    {
        isDisappeared = true;
        ghostRenderer.enabled = false;    // 隐藏幽灵
        ghostCollider.enabled = false;    // 禁用幽灵的碰撞

        // 如果设置为永久消失，协程在此结束，不再恢复
        if (disappearIndefinitely)
        {
            yield break;
        }

        yield return new WaitForSeconds(disappearDuration); // 等待消失的时间

        // 恢复幽灵
        ghostRenderer.enabled = true;
        ghostCollider.enabled = true;
        isDisappeared = false;
    }
}
