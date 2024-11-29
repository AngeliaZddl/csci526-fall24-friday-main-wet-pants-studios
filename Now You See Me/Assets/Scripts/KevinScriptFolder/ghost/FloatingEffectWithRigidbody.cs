using UnityEngine;

public class FloatingEffectWithRigidbody : MonoBehaviour
{
    public float amplitude = 0.5f; // ���¸����ķ���
    public float frequency = 1f; // ���¸�����Ƶ��

    private Vector3 startPosition;

    void Start()
    {
        // �����ʼλ��
        startPosition = transform.position;
    }

    void Update()
    {
        // �������Һ����������¸�����λ��
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
