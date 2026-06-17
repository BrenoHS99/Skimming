using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody rb;
    public Transform plrOrientation;

    private bool canDash = true;

    public GameObject cameraObject;
    private Camera cameraComp;

    private PlayerController plrController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        plrController = GetComponent<PlayerController>();
        cameraComp = cameraObject.GetComponent<Camera>();
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
            StartCoroutine(DashFov());
            Vector3 dashDirection = plrOrientation.forward;
            dashDirection.Normalize();
            dashDirection *= 50f;
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
        yield return new WaitForSeconds(0.1f); // X seconds
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.2f, rb.linearVelocity.z);
        plrController.dashing = false;
        plrController.limitSpeed = true;
    }
    IEnumerator DashFov() // the field of view changing when dashing
    {
        cameraComp.fieldOfView = 120f;
        yield return new WaitForSeconds(0.3f);
        cameraComp.fieldOfView = Mathf.Lerp(cameraComp.fieldOfView, 90f, 0.01f);
    }
}
