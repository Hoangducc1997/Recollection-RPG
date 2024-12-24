using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathBarFixedRotate : MonoBehaviour
{
    private Quaternion originalRotation;
    private Transform parentBoss; // Transform của boss

    private void Start()
    {
        // Lưu lại rotation ban đầu của Canvas
        originalRotation = transform.rotation;
        parentBoss = transform.parent; // Lấy boss là cha của Canvas
    }

    private void LateUpdate()
    {
        // Giữ nguyên rotation của Canvas
        transform.rotation = originalRotation;

        // Đảo hướng của Canvas nếu boss bị flip (scale.x < 0)
        Vector3 localScale = transform.localScale;
        localScale.x = parentBoss.localScale.x < 0 ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
}
