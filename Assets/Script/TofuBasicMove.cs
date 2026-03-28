using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TofuBasicMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 150f;
    public float jumpForce = 8f;


    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        rb.linearDamping = 1f;
    }

    void Update()
    {
       
        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotateInput * rotationSpeed * Time.deltaTime, 0);


        float moveInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * moveInput * moveSpeed;

   
        moveDirection.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDirection;

    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

          
            isGrounded = false;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}


