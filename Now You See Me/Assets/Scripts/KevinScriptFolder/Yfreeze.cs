using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yfreeze : MonoBehaviour
{
    private float fixedY; // 固定的 Y 坐标

    void Start()
    {
        // 记录玩家初始的 Y 坐标
        fixedY = transform.position.y;
    }

    void Update()
    {
        // 每帧固定玩家的 Y 坐标
        Vector3 position = transform.position;
        position.y = fixedY; // 锁定 Y 坐标
        transform.position = position;
    }
}
