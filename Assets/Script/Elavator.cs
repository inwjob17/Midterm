using UnityEngine;

public class Elavator : MonoBehaviour
{
    [Header("Platform Settings")]
    public float movementDistance = 3.0f; // ระยะทางที่เคลื่อนที่ไป-กลับ
    public float speed = 1.0f;           // ความเร็ว ยิ่งน้อยยิ่งช้า

    [Header("Optional: Rotation")]
    public float rotationSpeed = 0f;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Update()
    {
        // คูณด้วยระยะทางและความเร็ว
        float offset = Mathf.Sin(Time.time * speed) * movementDistance;

        transform.position = _startPosition + new Vector3(0, offset, 0);


        if (rotationSpeed != 0)
        {
            // คำนวณความเร็วในการเคลื่อนที่ เพื่อหาทิศทาง
            float velocity = Mathf.Cos(Time.time * speed) * speed * movementDistance;


            float rotOffset = Mathf.Clamp(velocity, -1, 1) * rotationSpeed;
            transform.rotation = _startRotation * Quaternion.Euler(0, rotOffset, 0);
        }
    }

    // ตัวละครติดไปกับไม้เวลาเหยียบ


    private void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบว่าเป็นผู้เล่น
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.transform.SetParent(transform);
        }
    }

    // Exit Collision
    private void OnCollisionExit(Collision collision)
    {
        // ตรวจว่าเป็นผู้เล่น
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
