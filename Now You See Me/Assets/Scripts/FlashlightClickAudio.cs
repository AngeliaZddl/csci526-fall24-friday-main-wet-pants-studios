using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightClickAudio : MonoBehaviour
{
    public AudioClip toggleSound;     // �ֵ�Ͳ������Ч
    private AudioSource audioSource;  // ��Ƶ���

    void Start()
    {
        // ��ʼ����Ƶ���
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = toggleSound;
        audioSource.playOnAwake = false;  // ��������ʱ������Ƶ
    }

    void Update()
    {
        // ��ⰴ�� F ��
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayToggleSound();
        }
    }

    // ������Ч
    void PlayToggleSound()
    {
        if (audioSource != null && toggleSound != null)
        {
            audioSource.Play();  // ������Ƶ�ļ�
        }
    }
}
