using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TofuController : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpForce = 8f;

    private Rigidbody rb;
    private Transform model;

    private Vector3 originalScale = Vector3.one;
    private Vector3 targetScale = Vector3.one;
    private Vector3 currentScale = Vector3.one;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 1f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (transform.childCount > 0) model = transform.GetChild(0);
        else model = transform;

        originalScale = model.localScale;
        currentScale = originalScale;
        targetScale = originalScale;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Camera.main == null) return;

        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 dir = (forward * v + right * h).normalized;

        if (dir.magnitude > 0.1f)
        {
            Vector3 moveVel = dir * moveSpeed;
            moveVel.y = rb.linearVelocity.y;
            rb.linearVelocity = moveVel;

            Quaternion lookRot = Quaternion.LookRotation(dir);
            model.rotation = Quaternion.Slerp(model.rotation, lookRot, Time.fixedDeltaTime * 10f);

            
            float wave = Mathf.Sin(Time.time * 15f) * 0.15f;
            targetScale = new Vector3(
                originalScale.x * (1f + wave),
                originalScale.y * (1f - wave),
                originalScale.z * (1f + wave)
            );
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            targetScale = originalScale;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 0.8f))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           
            currentScale = new Vector3(originalScale.x * 0.6f, originalScale.y * 1f, originalScale.z * 0.6f);
        }

        currentScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * 15f);
        if (model != null) model.localScale = currentScale;
    }
}