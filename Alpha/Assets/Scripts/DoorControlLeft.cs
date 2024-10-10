using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;  // �ο��ŵ� Animator
    public Transform player;       // �ο���ҵ� Transform
    public float interactionDistance = 3f; // ������ŵĽ�������

    void Update()
    {
        // ���������ŵľ���
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < interactionDistance)  // �����ҿ�����
        {
            if (Input.GetKeyDown(KeyCode.F)) // ���� F ��
            {
                // ��鵱ǰ����״̬
                if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorCloseLeft"))
                {
                    doorAnimator.SetTrigger("Open");  // ����
                }
                else if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenLeft"))
                {
                    doorAnimator.SetTrigger("Close"); // ����
                }
            }
        }
    }
}
