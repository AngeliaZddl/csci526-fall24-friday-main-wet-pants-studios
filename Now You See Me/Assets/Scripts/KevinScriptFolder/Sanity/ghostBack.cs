using UnityEngine;

public class GhostBack : MonoBehaviour
{
    private Vector3 originalPosition; // ��¼��ʼλ��

    void Start()
    {
        // �ڿ�ʼʱ��¼����ĳ�ʼλ��
        originalPosition = transform.position;
    }

    // �������ƻس�ʼλ�õĹ�������
    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
    }
}
