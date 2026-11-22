using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string dialogueName;
    public string[] dialogueLines;
    public Sprite dialogueSprite;
    public float textDelay;
    public AudioClip dialogueSfx;


    public bool singleTimeTrigger;
    private bool alreadyTriggered = false;

    public GameObject NpcBubbleSystem;
    private NpcBubbleScript npcBubbleScript;

    public GameObject proxObj;
    private ProximityPromptScript proxScript;

    private GameObject playerObj;
    private GameObject playerCamObj;

    public GameObject NPCCamera;

    void Start()
    {
        npcBubbleScript = NpcBubbleSystem.GetComponent<NpcBubbleScript>();
        proxScript = proxObj.GetComponent<ProximityPromptScript>();

        playerObj = GameObject.FindWithTag("Player");
        playerCamObj = GameObject.FindWithTag("MainCamera");
    }

    public IEnumerator StartDialogue()
    {
        NPCCamera.SetActive(true);
        playerObj.SetActive(false);
        playerCamObj.SetActive(false);
        if (singleTimeTrigger)
        {
            if (!alreadyTriggered)
            {
                alreadyTriggered = true;
                foreach(string text in dialogueLines)
                {
                    StartCoroutine(npcBubbleScript.CreateTextBubble(dialogueName, text, dialogueSprite, textDelay, dialogueSfx));
                    yield return new WaitForSeconds(2f);
                }
            }
        }
        else
        {
            foreach (string text in dialogueLines)
            {
                StartCoroutine(npcBubbleScript.CreateTextBubble(dialogueName, text, dialogueSprite, textDelay, dialogueSfx));
                yield return new WaitForSeconds(2f);
            }
        }
        proxScript.canPress = true;

        NPCCamera.SetActive(false);
        playerObj.SetActive(true);
        playerCamObj.SetActive(true);
    }
}
