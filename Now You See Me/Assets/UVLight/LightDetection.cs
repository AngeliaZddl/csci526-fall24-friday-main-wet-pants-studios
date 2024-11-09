using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    public Light detectionLight;
    public Material revealMaterial;
    private Material originalMaterial;
    private Renderer objRenderer;

    public GameObject handler;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;  // save object's original material 
    }

    void Update()
    {
        if (IsLitByLight())
        {
            Debug.Log("Object " + gameObject.name + " is being lit by the light.");
            if (objRenderer.material != revealMaterial)
            {
                objRenderer.material = revealMaterial;  // change to new material
                handler.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Object " + gameObject.name + " is outside the light cone.");
            if (objRenderer.material != originalMaterial)
            {
                objRenderer.material = originalMaterial;  // back to original
                handler.SetActive(false);

            }
        }
    }


    bool IsLitByLight()
    {
        Vector3 objectWorldPosition = transform.position;
        Vector3 lightWorldPosition = detectionLight.transform.position;
        Vector3 directionToLight = objectWorldPosition - lightWorldPosition;

        // �������͹�Դ֮�����С���룬����ǳ�����ʱ���ʧ��
        float distanceToLight = directionToLight.magnitude;
        float minDistance = 1f;  // ����Ը�����Ҫ���������С����
        if (distanceToLight < minDistance)
        {
            return true;  // ��Դ������ǳ�����ֱ����Ϊ���屻����
        }

        // �����飬ȷ�������ڹ�Դ�ķ�Χ��
        if (distanceToLight > detectionLight.range)
        {
            return false;  // ����������䷶Χ
        }

        // �Ƕȼ��
        float angleToLight = Vector3.Angle(detectionLight.transform.forward, directionToLight);
        if (angleToLight < detectionLight.spotAngle / 2)
        {
            // ���߼�⣬ȷ������ʵ�����䵽����
            Ray ray = new Ray(lightWorldPosition + detectionLight.transform.forward * 0.1f, directionToLight); // �ӹ�Դǰ����ƫ�Ʒ�������
            if (Physics.Raycast(ray, out RaycastHit hit, detectionLight.range))
            {
                if (hit.collider.gameObject == gameObject)  // ȷ������������Ŀ������
                {
                    return true;
                }
            }
        }

        return false;
    }





    //bool IsLitByLight()
    //{
    //    Vector3 objectWorldPosition = transform.position;  
    //    Vector3 lightWorldPosition = detectionLight.transform.position;  
    //    Vector3 directionToLight = lightWorldPosition - objectWorldPosition;

    //    float angleToLight = Vector3.Angle(detectionLight.transform.forward, -directionToLight);

    //    if (angleToLight < detectionLight.spotAngle / 2)
    //    {
    //        return true; 
    //    }
    //    else
    //    {
    //        return false; 
    //    }
    //}





}