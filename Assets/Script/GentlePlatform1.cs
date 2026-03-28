using UnityEngine;

public class GentlePlatform1 : MonoBehaviour
{
    [Header("Platform Settings")]
    public float movementDistance = 3.0f; // ���зҧ�������͹����-��Ѻ
    public float speed = 1.0f;           // �������� ��觹�����觪��

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
        // �ٳ�������зҧ��Ф�������
        float offset = Mathf.Sin((Time.time * speed) - 1.57f) * movementDistance;

        // ������ѡ���ҧ���������Ӥѭ
        transform.position = _startPosition + new Vector3(offset, 0, 0);


        if (rotationSpeed != 0)
        {
            // �ӹǳ��������㹡������͹��� �����ҷ�ȷҧ
            float velocity = Mathf.Cos(Time.time * speed) * speed * movementDistance;

          
            float rotOffset = Mathf.Clamp(velocity, -1, 1) * rotationSpeed;
            transform.rotation = _startRotation * Quaternion.Euler(0, rotOffset, 0);
        }
    }

    // ����ФõԴ仡Ѻ�����������º

   
    private void OnCollisionEnter(Collision collision)
    {
        // ��Ǩ�ͺ����繼�����
        if (collision.gameObject.CompareTag("Player"))
        {
            
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // Exit Collision
    private void OnCollisionExit(Collision collision)
    {
        // ��Ǩ����繼�����
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
