using UnityEngine;
using System.Collections;

public class KevinCameraShake : MonoBehaviour
{
    public float shakeMagnitude = 0.2f; // ��������
    public bool isShaking = false; // ��������Ƿ񶶶��Ŀ���

    private Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        // ��� isShaking Ϊ true�����������
        if (isShaking)
        {
            Vector3 shakePos = originalPos + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = shakePos;
        }
        else
        {
            // �ָ�ԭʼλ��
            transform.localPosition = originalPos;
        }
    }

    public void StartShake()
    {
        isShaking = true; // ��������
    }

    public void StopShake()
    {
        isShaking = false; // ֹͣ����
    }
}
