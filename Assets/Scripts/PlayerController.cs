using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    public float playerHeight;
    public LayerMask ground;
    private bool onGround;
    public float groundDrag;

    public float jumpForce;
    public float airMultiplier;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // ground checking
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.15f, ground);

        if (onGround)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal"); // ad <> movement
        verticalInput = Input.GetAxisRaw("Vertical"); // ws ^v movement

        // limiting velocity
        Vector3 vel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if(vel.magnitude > speed)
        {
            Vector3 velLimit = vel.normalized * speed;
            rb.linearVelocity = new Vector3(velLimit.x, rb.linearVelocity.y, velLimit.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 1f * jumpForce, rb.linearVelocity.z); // jumping velocity
        }
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (onGround)
        {
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force); // vel on ground
        }
        else
        {
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force); // vel on air
        }
    }
}
