
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TofuSimpleMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;     
    public float rotationSpeed = 100f; 
    public float jumpForce = 10f;

    [Header("Animation")]
    public float bounceSpeed = 10f;
    public float bounceAmount = 0.15f;

    private Rigidbody rb;
    private Transform model;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearDamping = 1f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        model = (transform.childCount > 0) ? transform.GetChild(0) : transform;
        originalScale = model.localScale;
    }

    void Update()
    {
       
        float rotationInput = Input.GetAxis("Horizontal"); 
        transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);

       
        float moveInput = Input.GetAxis("Vertical"); 

       
        Vector3 moveDir = transform.forward * moveInput * moveSpeed;

      
        moveDir.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDir;

       

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            ApplyBounce(bounceSpeed, bounceAmount);
        }
        else
        {
            ApplyBounce(2f, 0.03f);
        }

        // การกระโดด
        if (Input.GetKeyDown(KeyCode.Space))
        {
    
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ApplyBounce(float speed, float amount)
    {
        float wave = Mathf.Sin(Time.time * speed) * amount;
        model.localScale = new Vector3(
            originalScale.x * (1f + wave),
            originalScale.y * (1f - wave),
            originalScale.z * (1f + wave)
        );
    }
}