using UnityEngine;

public class DefeatBoxScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            PlayerController plrScript = player.GetComponent<PlayerController>();

            plrScript.Defeat();
        }
    }
}
