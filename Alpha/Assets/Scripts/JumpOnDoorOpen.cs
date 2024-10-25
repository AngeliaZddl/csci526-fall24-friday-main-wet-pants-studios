using UnityEngine;

public class JumpOnDoorOpen : MonoBehaviour
{
    public bool doorOpened = false;   // 用于检测门是否已打开
    public GameObject jumpObject;
    public bool jumpObjectStatus = false;
    private float delayBeforeAppear = 1f;  // 延迟时间，单位：秒

    // Start is called before the first frame update
    void Start()
    {
        jumpObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        // 如果门打开且物体还没有跳出
        if (doorOpened)
        {
            Invoke("ActivateJumpObject", delayBeforeAppear);
            doorOpened = false;  // 防止重复跳出
            jumpObjectStatus = true;
        }
    }

    // 让物体向前冲
    void ActivateJumpObject()
    {
        jumpObject.SetActive(true);
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p)
        {
            PlayerSanity ps = p.GetComponent<PlayerSanity>();
            ps.playerSanity -= 10;
        }
    }

    // 这个方法可以被外部调用，设置门打开状态
    public void OnDoorOpened()
    {
        doorOpened = true;
    }
    public bool jumpObjectAppeared() {
        return jumpObjectStatus;
    }
}