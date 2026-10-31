using System;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    private PlayerController plrController;
    private Rigidbody rb;

    public float startGlidingFall = -0.3f;
    public float fallSpeed = 0.05f;

    private float glidingFall = -0.3f;

    public bool canGlide = true;
    public bool inAirCurrent = false;

    public bool gliding = false;

    public GameObject dianaHands;
    private Animator handsAnimator;

    private void Start()
    {
        plrController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        handsAnimator = dianaHands.GetComponent<Animator>();
    }

    private void Update()
    {
        if (rb.linearVelocity.y > 0)
        {
            glidingFall = startGlidingFall;
        }

        if (Input.GetKeyDown(KeyCode.Space) && plrController.onGroundForGliding == false && inAirCurrent == false && canGlide)
        {
            handsAnimator.SetTrigger("startedGliding");
        }

        if (Input.GetKey(KeyCode.Space) && plrController.onGroundForGliding == false && inAirCurrent == false && canGlide)
        {
            gliding = true;
            Vector3 glidingVelocity = new Vector3(
                rb.linearVelocity.x,
                Mathf.Max(rb.linearVelocity.y, glidingFall),
                rb.linearVelocity.z
                );
            rb.linearVelocity = glidingVelocity;
            glidingFall -= fallSpeed;
        }
        else
        {
            gliding = false;
        }
        handsAnimator.SetBool("gliding", gliding);
    }
}
