using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 10f;

    public Transform cam; // 👈 เพิ่มกล้อง

    Rigidbody rb;

    InputAction moveInput;
    InputAction jumpInput;
    
    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
        grounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void Update()
    {
        Vector2 input = moveInput.ReadValue<Vector2>();

        // 👇 เอาทิศจากกล้อง
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // 👇 สร้างทิศทางการเดินตามกล้อง
        Vector3 move = camForward * input.y + camRight * input.x;

        // 👇 เคลื่อนที่
        transform.position += move * speed * Time.deltaTime;

        // 👇 หันตัวตามทิศที่เดิน
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // 👇 กระโดด (เหมือนเดิม)
        if (jumpInput.triggered && grounded)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            grounded = false;
        }
    }
}