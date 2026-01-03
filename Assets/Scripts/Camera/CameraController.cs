using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference lookInput;
    [SerializeField] InputActionReference zoomInput;
    [SerializeField] InputActionReference cameraActivationInput;


    [Header("Settings")]
    [SerializeField] LayerMask groundMask;
    
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform mark3d;
    
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    [SerializeField] float limitMinAxeX, limitMaxAxeX;
    [SerializeField] float limitMinZoom, limitMaxZoom;

    [SerializeField] float smoothMovement, smoothRotation, smoothZoom;

    Vector2 movementDirection;
    Vector2 rotationDirection;

    float zoomLevel = 30;
    Vector3 targetPoint;
    Vector3 targetRotation;

    bool isOrbitActive;

    private void Start()
    {
        mark3d.parent = null;

        targetPoint = transform.position;
        targetRotation = transform.localEulerAngles;

        cameraTransform = transform.GetChild(0);
        cameraTransform.localPosition = Vector3.forward * -zoomLevel;

        moveInput.action.performed += OnMove;
        lookInput.action.performed += OnLook;
        zoomInput.action.performed += OnZoom;
        cameraActivationInput.action.performed += OnCameraActivationInput;
    }

    private void OnEnable()
    {
        moveInput.action.Enable();
        lookInput.action.Enable();
        zoomInput.action.Enable();
        cameraActivationInput.action.Enable();
    }

    private void OnDisable()
    {
        moveInput.action.Disable();
        lookInput.action.Disable();
        zoomInput.action.Disable();
        cameraActivationInput.action.Disable();
    }

    private void OnDestroy()
    {
        moveInput.action.performed -= OnMove;
        lookInput.action.performed -= OnLook;
        zoomInput.action.performed -= OnZoom;
        cameraActivationInput.action.performed -= OnCameraActivationInput;
    }

    private void FixedUpdate()
    {
        Movement(Time.fixedDeltaTime);
        Rotation(Time.fixedDeltaTime);
        Zoom(Time.fixedDeltaTime);
    }

    //------------------------------------------------------------------------

    private void Movement(float delta)
    {
        //calcula a posição
        if (movementDirection.sqrMagnitude > 0.1f)
        {
            Vector3 transformed = transform.TransformVector(
                new Vector3(movementDirection.x, 0, movementDirection.y)).normalized;

            float speed = (movementSpeed + (zoomLevel * 0.2f));
            float multiplier = speed * delta;

            targetPoint.x += transformed.x * multiplier;
            targetPoint.z += transformed.z * multiplier;
        }

        Ray ray = new Ray(transform.position + (Vector3.up * 100f), Vector3.down);

        targetPoint.y = Physics.Raycast(
            ray: ray,
            hitInfo: out RaycastHit hit,
            maxDistance: Mathf.Infinity,
            layerMask: groundMask)
            ? hit.point.y
            : 0f;

        //Aplica a posição
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPoint, 
            smoothMovement * delta);

        mark3d.position = targetPoint;
    }

    private void Rotation(float delta)
    {
        //Calcula a rotação
        if (isOrbitActive)
        {
            if (rotationDirection.sqrMagnitude > 0.1f)
            {
                targetRotation.x += rotationDirection.y * rotationSpeed * delta;
                targetRotation.y += rotationDirection.x * rotationSpeed * delta;

                targetRotation.x = Mathf.Clamp(targetRotation.x, limitMinAxeX, limitMaxAxeX);
            }
        }


        //Aplica animação e rotação
        Quaternion targetRotion = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(targetRotation),
                smoothRotation * delta);

        transform.rotation = targetRotion;
    }

    private void Zoom(float delta)
    {
        cameraTransform.localPosition = Vector3.Lerp(
            cameraTransform.localPosition,
            Vector3.forward * -zoomLevel,
            smoothZoom * delta);
    }

    private void OnCameraActivationInput(InputAction.CallbackContext context)
    {
        isOrbitActive = context.ReadValue<float>() == 1;
    }

    private void OnZoom(InputAction.CallbackContext context) {
        zoomLevel -= context.ReadValue<Vector2>().y;
        zoomLevel = Mathf.Clamp(zoomLevel,limitMinZoom,limitMaxZoom);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>().normalized;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        rotationDirection = context.ReadValue<Vector2>(); 
    }
}
