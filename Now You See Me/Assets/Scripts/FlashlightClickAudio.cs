using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightClickAudio : MonoBehaviour
{
    public AudioClip toggleSound;     // 手电筒开关音效
    private AudioSource audioSource;  // 音频组件

    void Start()
    {
        // 初始化音频组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = toggleSound;
        audioSource.playOnAwake = false;  // 避免启动时播放音频
    }

    void Update()
    {
        // 检测按下 F 键
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayToggleSound();
        }
    }

    // 播放音效
    void PlayToggleSound()
    {
        if (audioSource != null && toggleSound != null)
        {
            audioSource.Play();  // 播放音频文件
        }
    }
}
