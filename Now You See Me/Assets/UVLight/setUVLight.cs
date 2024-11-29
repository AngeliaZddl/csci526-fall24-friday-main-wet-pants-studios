using System.Collections.Generic;  // 如果使用 List，需要引用这个命名空间
using UnityEngine;

public class SetUVLight : MonoBehaviour
{
    public List<Material> revealMaterials;  // 列表形式的 Material
    public Light myLight;                   // 手电筒光源

    void Update()
    {
        // 遍历列表，将光照数据传递给每个材质
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
