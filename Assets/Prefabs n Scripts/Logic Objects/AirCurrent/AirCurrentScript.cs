using UnityEngine;

public class AirCurrentScript : MonoBehaviour
{
    private Transform topPos;
    public float forceDirectionMultiplier = 1f;

    private void Start()
    {
        topPos = transform.Find("Top");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 forceDirection = topPos.position - transform.position;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.linearVelocity += (forceDirection) * forceDirectionMultiplier;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gliding glidingScript = other.gameObject.GetComponent<Gliding>();
            glidingScript.inAirCurrent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gliding glidingScript = other.gameObject.GetComponent<Gliding>();
            glidingScript.inAirCurrent = false;
        }
    }
}
