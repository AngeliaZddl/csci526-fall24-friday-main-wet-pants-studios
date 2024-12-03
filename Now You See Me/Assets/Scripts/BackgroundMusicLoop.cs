using UnityEngine;

public class BackgroundMusicLoop : MonoBehaviour
{
    public AudioClip backgroundMusic; // 背景音乐音频剪辑
    private AudioSource audioSource;  // 音频播放器

    void Start()
    {
        // 初始化 AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic; // 设置背景音乐
        audioSource.loop = true; // 设置为循环播放
        audioSource.volume = 0.05f; // 设置音量
        audioSource.Play(); // 开始播放音乐
    }
}
