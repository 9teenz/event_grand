using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float walkSpeed = 4.4f;
    public float sprintSpeed = 9f;
    public float gravity = -9.81f; // Гравитация только вниз

    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0f, 2f, -4f);
    public float cameraSensitivity = 2f;
    public float cameraSmoothSpeed = 10f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private CharacterController controller;
    private Animator animator;
    private float cameraPitch = 0f;
    private Vector3 verticalVelocity; // Только для гравитации

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        Move();
        ApplyGravity(); // ← добавлено
        CameraFollowAndLook();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(x, 0, z).normalized;
        float inputMagnitude = inputDir.magnitude;

        animator.SetFloat("Speed", inputMagnitude * (Input.GetKey(KeyCode.LeftShift) ? 2f : 1f));

        if (inputMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f; // Притягиваем к земле
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    void CameraFollowAndLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -30f, 60f);

        cameraTransform.rotation = Quaternion.Euler(cameraPitch, cameraTransform.eulerAngles.y + mouseX, 0f);

        Vector3 desiredPosition = transform.position + cameraTransform.rotation * cameraOffset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, Time.deltaTime * cameraSmoothSpeed);
    }
}
