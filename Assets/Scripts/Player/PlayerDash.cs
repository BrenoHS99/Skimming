using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody rb;
    public Transform plrOrientation;

    private bool canDash = true;

    private PlayerController plrController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        plrController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash) // left ctrl to dash
        {
            float xzMultiplier = 2f;
            canDash = false;
            plrController.BreakSpeedLimit();
            StartCoroutine(SetDashing());
            Vector3 dashDirection = plrOrientation.forward;
            dashDirection.Normalize();
            dashDirection *= 15f;
            dashDirection = new Vector3(dashDirection.x * xzMultiplier, dashDirection.y, dashDirection.z * xzMultiplier);
            rb.linearVelocity = dashDirection;
        }

        if (plrController.onGround) // resets the dash condition
        {
            canDash = true;
        }
    }
    IEnumerator SetDashing() // activates the dash boolean for X seconds
    {
        plrController.dashing = true;
        yield return new WaitForSeconds(0.5f); // X seconds
        plrController.dashing = false;
        plrController.limitSpeed = true;
    }
}
