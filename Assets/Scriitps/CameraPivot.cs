using UnityEngine;

public class CameraPivot : MonoBehaviour
{
   [Header("Mouse Settings")]
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    public float verticalClampMin = -40f;
    public float verticalClampMax = 70f;

    [Header("Camera Distance (Radius)")]
    public float cameraDistance = 4f;
    public float minDistance = 1f;
    public float maxDistance = 10f;
    public float zoomSpeed = 2f;
    public float zoomSmoothSpeed = 8f;

    [Header("Camera Offset")]
    public Vector3 pivotOffset = new Vector3(0.5f, 1.5f, 0f);

    private float _pitch = 0f;
    private float _yaw = 0f;
    private float _currentDistance;
    public Transform _cameraTransform;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        _yaw = transform.parent.eulerAngles.y;
        _currentDistance = cameraDistance;

        //_cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMouseRotation();
        HandleZoom();
        //PositionCamera();
    }

    void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        _yaw += mouseX;
        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, verticalClampMin, verticalClampMax);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            cameraDistance -= scroll * zoomSpeed;
            cameraDistance = Mathf.Clamp(cameraDistance, minDistance, maxDistance);
        }

        _currentDistance = Mathf.Lerp(_currentDistance, cameraDistance, Time.deltaTime * zoomSmoothSpeed);
    }

    void PositionCamera()
    {
        if (_cameraTransform == null) return;

        Vector3 pivotWorldPos = transform.position + transform.TransformDirection(pivotOffset);
        Vector3 cameraOffset = transform.rotation * new Vector3(0f, 0f, -_currentDistance);

        _cameraTransform.position = pivotWorldPos + cameraOffset;
        _cameraTransform.rotation = transform.rotation;
    }

    public float GetYaw() => _yaw;
    public float GetCurrentDistance() => _currentDistance;
}
