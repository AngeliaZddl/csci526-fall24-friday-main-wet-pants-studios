using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperAudio : MonoBehaviour
{
    public GameObject player;  // ��Ҷ���
    public List<GameObject> ghosts;  // ��������б�

    public AudioClip proximityClip;  // ��Ƶ�ļ�
    private AudioSource audioSource; // ��Ƶ���

    public float maxVolume = 1.0f;  // �������
    public float maxDistance = 10.0f;  // �����Ч���루�����˾�����Ƶ����Ϊ0��
    public float volumeCurveFactor = 2.0f;  // �����仯��ָ������

    void Start()
    {
        // ��Ӳ���ʼ�� AudioSource ���
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = proximityClip;
        audioSource.loop = true;  // ����Ϊѭ������
        audioSource.volume = 0;   // ��ʼ����Ϊ 0
        audioSource.playOnAwake = false;  // ��������ʱ����

        // ȷ����ƵΪ3D��Ƶ
        audioSource.spatialBlend = 1.0f;  // 1��ʾ��ȫ3DЧ��
    }

    void Update()
    {
        if (player == null || ghosts == null || ghosts.Count == 0) return;

        // �ҵ���������������֮��ľ���
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

        // ����������������Ч��Χ��
        if (minDistance <= maxDistance)
        {
            // ʹ��ָ�����߼�����������������к������仯��ǿ�ҳ̶�
            float normalizedDistance = minDistance / maxDistance;
            float volume = maxVolume * Mathf.Pow(1 - normalizedDistance, volumeCurveFactor);
            audioSource.volume = volume;

            // �����Ƶδ���ţ���ʼ����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // ������Χʱֹͣ��Ƶ
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
