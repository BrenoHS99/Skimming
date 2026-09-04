using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public float jumpHeight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z);

            PlayerDash plrDash = other.gameObject.GetComponent<PlayerDash>();
            plrDash.canDash = true;
        }
    }
}
