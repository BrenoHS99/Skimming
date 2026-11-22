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
    public bool canPress;

    [Header("Connected NPC")]
    public GameObject npc;
    private NPCDialogue npcDialogueScript;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        npcDialogueScript = npc.GetComponent<NPCDialogue>();
    }

    private void Update()
    {
        if(Camera.main != null)
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
        }

        if(active && canPress && Input.GetKeyDown(KeyCode.E))
        {
            canPress = false;
            StartCoroutine(npcDialogueScript.StartDialogue());
        }

        if(Camera.main != null)
        {
            if ((Camera.main.transform.position - transform.position).magnitude <= visibleDistance)
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
        else
        {
            renderer.enabled = false;
        }
    }
}
