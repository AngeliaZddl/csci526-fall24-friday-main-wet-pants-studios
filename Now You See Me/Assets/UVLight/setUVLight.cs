using System.Collections.Generic;  // ���ʹ�� List����Ҫ������������ռ�
using UnityEngine;

public class SetUVLight : MonoBehaviour
{
    public List<Material> revealMaterials;  // �б���ʽ�� Material
    public Light myLight;                   // �ֵ�Ͳ��Դ

    void Update()
    {
        // �����б����������ݴ��ݸ�ÿ������
        foreach (Material material in revealMaterials)
        {
            if (material != null)
            {
                material.SetVector("_LightPosition", myLight.transform.position);
                material.SetVector("_LightDirection", -myLight.transform.forward);
                material.SetFloat("_LightAngle", myLight.spotAngle);
                material.SetInteger("_LightEnabled", myLight.enabled ? 1 : 0);
            }
        }
    }
}
