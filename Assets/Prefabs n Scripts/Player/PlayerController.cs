using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float speed;
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    public float playerHeight;
    public LayerMask ground;
    public bool onGround;
    public float groundDrag;

    public float jumpForce;
    public float airMultiplier;

    private bool running = false;

    public GameObject cameraObject;
    private Camera cameraComp;

    private Rigidbody rb;

    public bool limitSpeed = true;
    public bool dashing = false;

    public GameObject checkpoint;
    public float currentCheckpointSequence;

    private GameObject voidStart;

    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraComp = cameraObject.GetComponent<Camera>();
        startPos = transform.position;
        voidStart = GameObject.FindGameObjectWithTag("Void");
    }

    // Update is called once per frame
    void Update()
    {
        // ground checking
        RaycastHit hit;
        onGround = Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.03f, ground);

        if (onGround && hit.collider.gameObject.GetComponent<MovingPlatforms>())
        {
            MovingPlatforms MPlatformHit = hit.collider.gameObject.GetComponent<MovingPlatforms>();
            if (MPlatformHit.moving == false)
            {
                StartCoroutine(MPlatformHit.StartMovement());
            }
        }

        if (onGround)
        {
            rb.linearDamping = groundDrag;
            limitSpeed = true;
            dashing = false;
        }
        else
        {
            rb.linearDamping = 0;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal"); // ad <> movement
        verticalInput = Input.GetAxisRaw("Vertical"); // ws ^v movement

        // limiting velocity
        if (dashing)
        {
            limitSpeed = false;
        }
        Vector3 vel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (vel.magnitude > speed && limitSpeed)
        {
            Vector3 velLimit = vel.normalized * speed;
            rb.linearVelocity = new Vector3(velLimit.x, rb.linearVelocity.y, velLimit.z);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Defeat();
        }

        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 1f * jumpForce, rb.linearVelocity.z); // jumping velocity
        }

        if (Input.GetKey(KeyCode.LeftShift) && !dashing) // left shift to run
        {
            running = true;
        }
        else
        {
            running = false;
        }
        if (running) // running condition
        {
            speed = runSpeed;
            groundDrag = 2f;
            if (!dashing)
            {
                cameraComp.fieldOfView = Mathf.Lerp(cameraComp.fieldOfView, 110f, 0.2f);
            }
        }
        else
        {
            speed = walkSpeed;
            groundDrag = 5f;
            if (!dashing)
            {
                cameraComp.fieldOfView = Mathf.Lerp(cameraComp.fieldOfView, 90f, 0.1f);
            }
        }

        if (transform.position.y < voidStart.transform.position.y)
        {
            Defeat();
        }
    }

    public void BreakSpeedLimit() // function that breaks the speed limit
    {
        limitSpeed = false;
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (!dashing)
        {
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

    public void Defeat()
    {
        if (checkpoint != null)
        {
            transform.position = checkpoint.transform.position;
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            transform.position = startPos;
            rb.linearVelocity = Vector3.zero;
        }
    }

    public void SetCheckpoint(GameObject checkpointObj, float checkpointSequence)
    {
        checkpoint = checkpointObj;
        currentCheckpointSequence = checkpointSequence;

        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpointInstance in checkpoints)
        {
            CheckpointScript checkpointScript = checkpointInstance.GetComponent<CheckpointScript>();
            if (checkpointScript.checkpointSequence < currentCheckpointSequence)
            {
                Renderer checkpointInstanceRender = checkpointInstance.GetComponent<Renderer>();
                checkpointInstanceRender.material.color = Color.blue;
            }
        }
    }
}