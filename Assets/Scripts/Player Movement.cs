using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;
    public Transform playerCamera;
    public float playerSpeed = 12f;

    public float mouseSensitivity = 100f;
    public bool lockCursor = true;

    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    float xRotation = 0f;
    float yVelocity = 0f;

    void Start()
    {
        if (playerController == null)
            playerController = GetComponent<CharacterController>();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (playerCamera != null)
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        Vector3 horizontalVelocity = moveDirection * playerSpeed;

        if (playerController.isGrounded && yVelocity < 0f)
            yVelocity = -2f;

        if (Input.GetButtonDown("Jump") && playerController.isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 finalVelocity = horizontalVelocity + Vector3.up * yVelocity;
        playerController.Move(finalVelocity * Time.deltaTime);
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
