using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("KeyBind")]
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    public bool grounded;

    [Header("Sprint Variables")]
    public float runningSpeed;
    public bool isRunning;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    private Rigidbody rb;

    float originalCameraFOV;
    public float runningCameraFOV = 90;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalCameraFOV = Camera.main.fieldOfView;
    }

    private void Awake()
    {
        readyToJump = true;
        isRunning = false;
    }

    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, groundLayer);

        StoreInput();
        SpeedControl();

        //handle drag
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0.1f;
        }
        ManageCameraFOV();
    }

    private void StoreInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        isRunning = Input.GetKey(sprintKey) && (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0);
    }

    private void ManageCameraFOV()
    {
        if (isRunning)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, runningCameraFOV, Time.deltaTime * 5);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, originalCameraFOV, Time.deltaTime * 5);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            if (isRunning)
            {
                rb.AddForce(moveDirection.normalized * runningSpeed * 3.5f, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * walkSpeed * 3.5f, ForceMode.Force);
            }
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (isRunning && flatVel.magnitude > runningSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * runningSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
        else if (!isRunning && flatVel.magnitude > walkSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * walkSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //Debug.LogWarning("SALTANDO!");
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
