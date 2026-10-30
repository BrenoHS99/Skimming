using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    public bool canTP = false;
    public Transform playerCam;
    public LayerMask groundLayer;
    public float distance;

    private PlayerDash plrDashScript;

    void Start()
    {
        plrDashScript = GetComponent<PlayerDash>();
    }

    void Update()
    {
        if (canTP && plrDashScript.canDash)
        {
            RaycastHit hit;
            bool r = Physics.Raycast(playerCam.position, playerCam.forward, out hit, distance, groundLayer);

            if(Input.GetMouseButtonDown(1))
            {
                plrDashScript.canDash = false;
                if (r)
                {
                    transform.position = hit.point;
                }
                else
                {
                    transform.position += playerCam.forward * distance;
                }
            }
        }
    }
}
