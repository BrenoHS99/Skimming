using System.Collections;
using UnityEngine;

public class DashCrystal : MonoBehaviour
{
    private MeshRenderer render;

    public Material dashCrystalOnMaterial;
    public Material dashCrystalOffMaterial;

    public bool canActivate = true;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canActivate)
        {
            PlayerDash dashScript = other.gameObject.GetComponent<PlayerDash>();
            if(dashScript.canDash == false)
            {
                canActivate = false;
                dashScript.canDash = true;
                render.material = dashCrystalOffMaterial;
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);
        render.material = dashCrystalOnMaterial;
        canActivate = true;
    }
}
