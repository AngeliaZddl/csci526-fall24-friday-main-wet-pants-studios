using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanBorder : MonoBehaviour
{
    private Image bloodBorderImage; // �Զ���ȡ��ǰ�����ϵ� Image ���
    public PlayerSanity playerSanityScript; // ���� PlayerSanity �ű�

    private float maxAlpha = 1f; // ���͸����
    private float minAlpha = 0f; // ��С͸����

    void Start()
    {
        // �Զ���ȡ�����ڵ�ǰ GameObject �ϵ� Image ���
        bloodBorderImage = GetComponent<Image>();

        // ȷ�������� PlayerSanity �ű�
        if (playerSanityScript == null)
        {
            Debug.LogWarning("PlayerSanity �ű�δ�����ã����� Inspector ������");
        }
    }

    void Update()
    {
        if (playerSanityScript != null)
        {
            // ��ȡ��ǰ�� sanity ֵ
            float currentSanity = playerSanityScript.playerSanity;

            // ��� sanity С�ڵ��� 50����������͸����
            if (currentSanity <= 50)
            {
                // ���� sanity ֵ���� Alpha ֵ��ԽСԽ�ӽ� maxAlpha��
                float alpha = Mathf.Lerp(maxAlpha, minAlpha, currentSanity / 50f);

                // ���� Image ��͸����
                SetImageAlpha(alpha);
            }
            else
            {
                // �� sanity ���� 50 ʱ����͸���Ȼָ�Ϊ minAlpha
                SetImageAlpha(minAlpha);
            }
        }
    }

    // ���� Image ��͸����
    private void SetImageAlpha(float alpha)
    {
        if (bloodBorderImage != null)
        {
            Color currentColor = bloodBorderImage.color;
            currentColor.a = alpha;
            bloodBorderImage.color = currentColor;
        }
    }
}
