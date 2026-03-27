using UnityEngine;

public class TofuBounce : MonoBehaviour
{
    [Header("(Idle)")]
    public float idleSpeed = 2f;
    public float idleAmount = 0.05f;

    [Header("(Walking)")]
    public float walkSpeed = 15f;
    public float walkAmount = 0.15f;

    private Vector3 originalScale;
    private Rigidbody parentRb;

    void Start()
    {
        originalScale = transform.localScale;

        parentRb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        float speed = idleSpeed;
        float amount = idleAmount;


        if (parentRb != null && parentRb.linearVelocity.magnitude > 0.1f)
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

