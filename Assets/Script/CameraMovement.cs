using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

public class CameraMovement : MonoBehaviour
{
    private const float SMOOTH_TIME = 0.1f;
    public bool LockX, LockY, LockZ;
    public float offsetZ = -3.0f;
    public float offsetY = 1.0f;
    public bool useSmoothing = true;
    public Transform target; // วัตถุเป้าหมายที่จะติดตาม เช่น ตัวละครผู้เล่น
    private Transform thisTransform;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        // เก็บค่าของ transform ของกล้อง
        thisTransform = transform;

        // กำหนดค่าเริ่มต้นให้กับ velocity
        velocity = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        Vector3 newPos = new Vector3();
        
        if (useSmoothing)
        {
            // ติดตามผู้เล่นอย่างราบรื่น
            newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
            newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y + offsetY, ref velocity.y, SMOOTH_TIME);
            newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offsetZ, ref velocity.z, SMOOTH_TIME);
        }
        else
        {
            // ติดตามผู้เล่นโดยตรงโดยไม่ต้องทำให้ราบรื่น
            newPos.x = target.position.x;
            newPos.y = target.position.y + offsetY;
            newPos.z = target.position.z + offsetZ;
        }

        if (LockX)
        {
            newPos.x = thisTransform.position.x;
        }

        if (LockY)
        {
            newPos.y = thisTransform.position.y;
        }

        if (LockZ)
        {
            newPos.z = thisTransform.position.z;
        }

        // อัปเดตตำแหน่งของกล้องด้วยการใช้ Slerp เพื่อการเคลื่อนที่ที่นุ่มนวล
        transform.position = Vector3.Lerp(thisTransform.position, newPos, 10f * Time.deltaTime);
    }
}
