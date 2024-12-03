using UnityEngine;

public class BackgroundMusicLoop : MonoBehaviour
{
    public AudioClip backgroundMusic; // ����������Ƶ����
    private AudioSource audioSource;  // ��Ƶ������

    void Start()
    {
        // ��ʼ�� AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic; // ���ñ�������
        audioSource.loop = true; // ����Ϊѭ������
        audioSource.volume = 0.05f; // ��������
        audioSource.Play(); // ��ʼ��������
    }
}
