using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 0.5f; // ����ǿ��
    public float shakeFrequency = 40.0f; // ����Ƶ��
    private Vector3 originalPosition;

    private PlayerSanity playerSanityScript;  // ���� PlayerSanity �ű�

    void Start()
    {
        originalPosition = transform.localPosition;

        // ��ȡ PlayerSanity �ű�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerSanityScript = player.GetComponent<PlayerSanity>();
        }
        else
        {
            Debug.LogError("Player GameObject not found or does not have PlayerSanity component.");
        }
    }

    void Update()
    {
        // ��� playerSanity �Ƿ�С�� 20
        if (playerSanityScript != null && playerSanityScript.playerSanity < 20)
        {
            // һֱ���ֶ���״̬
            Shake();
        }
        else
        {
            // �ָ�ԭλ�ã���� sanity ���ڵ��� 20
            transform.localPosition = originalPosition;
        }
    }

    void Shake()
    {
        // ʹ�� Perlin �����������ƫ��
        float shakeAmountX = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0) - 0.5f) * shakeIntensity;
        float shakeAmountY = (Mathf.PerlinNoise(0, Time.time * shakeFrequency) - 0.5f) * shakeIntensity;

        // Ӧ�ö���ƫ�Ƶ�����ͷλ��
        transform.localPosition = originalPosition + new Vector3(shakeAmountX, shakeAmountY, 0);
    }
}
