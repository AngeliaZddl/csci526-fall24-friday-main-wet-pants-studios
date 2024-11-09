using UnityEngine;

public class FlashlightItemUVLight : MonoBehaviour
{
    public Material revealMaterial;    // 手电筒光照下的材质
    public Light myLight;              // 手电筒的光源

    private float originalIntensity;   // 手电筒的原始亮度
    private float originalSpotAngle;   // 手电筒的原始照射角度
    private float originalRange;       // 手电筒的原始照射范围

    void Start()
    {
        // 记录手电筒的原始亮度、角度和范围
        originalIntensity = myLight.intensity;
        originalSpotAngle = myLight.spotAngle;
        originalRange = myLight.range;
    }

    void Update()
    {
        // 更新手电筒光照的方向和位置，用于动态效果
        revealMaterial.SetVector("_LightPosition", myLight.transform.position);
        revealMaterial.SetVector("_LightDirection", -myLight.transform.forward);
        revealMaterial.SetFloat("_LightAngle", myLight.spotAngle);
    }
}
