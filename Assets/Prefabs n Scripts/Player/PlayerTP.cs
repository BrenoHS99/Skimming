using UnityEngine;
using System.Collections;

public class PlayerTP : MonoBehaviour
{
    public bool TPenabled = false;
    public bool canTP = false;
    public Transform playerCam;
    public LayerMask groundLayer;
    public float distance;

    private PlayerController plrController;
    private Rigidbody rb;

    void Start()
    {
        plrController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (TPenabled && plrController.onGround == false && canTP)
        {
            RaycastHit hit;
            bool r = Physics.Raycast(playerCam.position, playerCam.forward, out hit, distance, groundLayer);

            if(Input.GetMouseButtonDown(1))
            {
                canTP = false;
                StartCoroutine(TPFov());
                if (r)
                {
                    rb.position = hit.point;
                }
                else
                {
                    rb.position += playerCam.forward * distance;
                }
            }
        }

        if (plrController.onGround)
        {
            canTP = true;
        }
    }
    IEnumerator TPFov() // the field of view changing when dashing
    {
        Camera.main.fieldOfView = 170f;
        yield return new WaitForSeconds(0.1f);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90f, 0.02f);
    }
}
