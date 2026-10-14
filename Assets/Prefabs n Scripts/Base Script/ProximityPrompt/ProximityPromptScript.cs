using Unity.VisualScripting;
using UnityEngine;

public class ProximityPromptScript : MonoBehaviour
{
    private SpriteRenderer renderer;

    public enum ProximityPromptOrientation
    {
        LookAtCamera,
        TransformForward
    }

    [Header("Proximity Prompt Configurations")]
    public ProximityPromptOrientation ppOrientation;
    public float visibleDistance;

    [Header("Attributes")]
    public bool active;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (ppOrientation)
        {
            case ProximityPromptOrientation.LookAtCamera:
                transform.LookAt(Camera.main.transform.position);
                break;
            case ProximityPromptOrientation.TransformForward:
                transform.forward = Camera.main.transform.forward;
                break;
        }

        if((Camera.main.transform.position - transform.position).magnitude <= visibleDistance)
        {
            renderer.enabled = true;
            active = true;
        }
        else
        {
            renderer.enabled = false;
            active = false;
        }
    }
}
