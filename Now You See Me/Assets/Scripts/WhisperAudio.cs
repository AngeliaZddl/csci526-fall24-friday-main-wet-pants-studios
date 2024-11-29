using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperAudio : MonoBehaviour
{
    public GameObject player;  // 玩家对象
    public List<GameObject> ghosts;  // 幽灵对象列表

    public AudioClip proximityClip;  // 音频文件
    private AudioSource audioSource; // 音频组件

    public float maxVolume = 1.0f;  // 最大音量
    public float maxDistance = 10.0f;  // 最大有效距离（超过此距离音频音量为0）
    public float volumeCurveFactor = 2.0f;  // 音量变化的指数因子

    void Start()
    {
        // 添加并初始化 AudioSource 组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = proximityClip;
        audioSource.loop = true;  // 设置为循环播放
        audioSource.volume = 0;   // 初始音量为 0
        audioSource.playOnAwake = false;  // 不在启动时播放

        // 确保音频为3D音频
        audioSource.spatialBlend = 1.0f;  // 1表示完全3D效果
    }

    void Update()
    {
        if (player == null || ghosts == null || ghosts.Count == 0) return;

        // 找到最近的幽灵与玩家之间的距离
        float minDistance = float.MaxValue;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null)
            {
                float distance = Vector3.Distance(player.transform.position, ghost.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }

        // 如果最近的幽灵在有效范围内
        if (minDistance <= maxDistance)
        {
            // 使用指数曲线计算音量，调整距离感和音量变化的强烈程度
            float normalizedDistance = minDistance / maxDistance;
            float volume = maxVolume * Mathf.Pow(1 - normalizedDistance, volumeCurveFactor);
            audioSource.volume = volume;

            // 如果音频未播放，开始播放
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // 超出范围时停止音频
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
