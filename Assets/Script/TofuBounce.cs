using UnityEngine;

public class TofuBounce : MonoBehaviour
{
    [Header("Idle Settings")]
    public float idleSpeed = 2f;
    public float idleAmount = 0.05f;

    [Header("Walking Settings")]
    public float walkSpeed = 15f;
    public float walkAmount = 0.15f;

    [Header("Jumping/Airborne")]
    public float airAmount = 0.02f; // ตอนอยู่บนฟ้าให้เด้งน้อยๆ จะเด้งทำไมนนนักหนา

    private Vector3 originalScale;
    private Rigidbody parentRb;

    void Start()
    {
        originalScale = transform.localScale;
        parentRb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if (parentRb == null) return;

        float speed = idleSpeed;
        float amount = idleAmount;

        Vector3 horizontalVelocity = new Vector3(parentRb.linearVelocity.x, 0, parentRb.linearVelocity.z);
        float moveSpeed = horizontalVelocity.magnitude;

        bool isMoving = moveSpeed > 0.1f;
        bool isInAir = Mathf.Abs(parentRb.linearVelocity.y) > 0.1f;

        if (isInAir)
        {
            speed = idleSpeed;
            amount = airAmount; //ถ้าลอยอยู่บนนนฟ้้า
        }
        else if (isMoving)
        {
            speed = walkSpeed;
            amount = walkAmount;
        }

        float bounce = Mathf.Sin(Time.time * speed) * amount;

        
        transform.localScale = new Vector3(
            originalScale.x * (1f + bounce),
            originalScale.y * (1f - bounce),
            originalScale.z * (1f + bounce)
        );
    }
}
