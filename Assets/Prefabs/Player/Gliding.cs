using System;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    private PlayerController plrController;
    private Rigidbody rb;

    public float startGlidingFall = -0.3f;
    public float fallSpeed = 0.05f;

    private float glidingFall = -0.3f;

    private void Start()
    {
        plrController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.linearVelocity.y > 0)
        {
            glidingFall = startGlidingFall;
        }
        if (Input.GetKey(KeyCode.Space) && plrController.onGround == false)
        {
            Vector3 glidingVelocity = new Vector3(
                rb.linearVelocity.x,
                Mathf.Max(rb.linearVelocity.y, glidingFall),
                rb.linearVelocity.z
                );
            rb.linearVelocity = glidingVelocity;
            glidingFall -= fallSpeed;
        }
    }
}
