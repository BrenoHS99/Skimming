using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    private GameObject player;
    private PlayerDash plrDashScript;
    private PlayerTP plrTpScript;

    // crosshair
    public GameObject dashCrosshair;
    public GameObject tpCrosshair;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        plrDashScript = player.GetComponent<PlayerDash>();
        plrTpScript = player.GetComponent<PlayerTP>();
    }

    private void Update()
    {
        dashCrosshair.SetActive(plrDashScript.canDash);

        if (plrTpScript.TPenabled)
        {
            tpCrosshair.SetActive(plrTpScript.canTP);
        }
        else
        {
            tpCrosshair.SetActive(false);
        }
    }
}
