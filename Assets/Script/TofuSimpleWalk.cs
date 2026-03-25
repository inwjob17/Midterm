using UnityEngine;

public class TofuSimpleWalk : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float rotationSpeed = 720f;

    [Header("ค่าการเด้ง (ปรับได้ตามชอบ)")]
    public float bounceFrequency = 15f; 
    public float bounceAmplitude = 0.15f; 

    [Header("ลาก Model ลูกมาใส่ที่นี่นะคะ")]
    public Transform modelTransform;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null) cameraTransform = Camera.main.transform;

        if (modelTransform == null)
        {
            if (transform.childCount > 0) modelTransform = transform.GetChild(0);
            else modelTransform = transform;
        }

        
        originalScale = modelTransform.localScale;
    }

    void Update()
    {
        if (modelTransform == null) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
          
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
            modelTransform.rotation = Quaternion.RotateTowards(modelTransform.rotation, Quaternion.LookRotation(moveDir), rotationSpeed * Time.deltaTime);

           
            float bounceEffect = Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;

           
            modelTransform.localScale = new Vector3(
                originalScale.x + (bounceEffect * originalScale.x),
                originalScale.y - (bounceEffect * originalScale.y), 
                originalScale.z + (bounceEffect * originalScale.z)  
            );

          
            void FixedUpdate()
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                Vector3 inputDir = new Vector3(h, 0, v).normalized;

                if (inputDir.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                   
                    rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            
            modelTransform.localScale = Vector3.Lerp(modelTransform.localScale, originalScale, Time.deltaTime * 10f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * 6f, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        
        return Physics.Raycast(transform.position, Vector3.down, 2f);
    }
}