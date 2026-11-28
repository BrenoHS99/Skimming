using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public enum SpecialDialogueFeature
    {
        none,
        umbrellaUnlock,
        tpUnlock,
        desir,
        fantasyEnd,
        ruido
    }

    public SpecialDialogueFeature dialogueFeature;

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

                switch (dialogueFeature)
                {
                    case SpecialDialogueFeature.none:
                        break;
                    case SpecialDialogueFeature.umbrellaUnlock:
                        Gliding glideScript = playerObj.GetComponent<Gliding>();
                        glideScript.glideEnabled = true;
                        break;
                    case SpecialDialogueFeature.tpUnlock:
                        PlayerTP tpScript = playerObj.GetComponent<PlayerTP>();
                        tpScript.TPenabled = true;
                        break;
                    case SpecialDialogueFeature.fantasyEnd:
                        SceneManager.LoadScene(7);
                        break;
                    default:
                        break;
                }

                if (dialogueFeature != SpecialDialogueFeature.fantasyEnd)
                {
                    foreach (string text in dialogueLines)
                    {
                        StartCoroutine(npcBubbleScript.CreateTextBubble(dialogueName, text, dialogueSprite, textDelay, dialogueSfx));
                        yield return new WaitForSeconds(2f);
                    }

                    if (dialogueFeature == SpecialDialogueFeature.desir)
                    {
                        SceneManager.LoadScene(4);
                    }

                    if(dialogueFeature == SpecialDialogueFeature.ruido)
                    {
                        SceneManager.LoadScene(12);
                    }
                }
            }
        }
        else
        {
            switch (dialogueFeature)
            {
                case SpecialDialogueFeature.none:
                    break;
                case SpecialDialogueFeature.umbrellaUnlock:
                    Gliding glideScript = playerObj.GetComponent<Gliding>();
                    glideScript.glideEnabled = true;
                    break;
                case SpecialDialogueFeature.tpUnlock:
                    PlayerTP tpScript = playerObj.GetComponent<PlayerTP>();
                    tpScript.TPenabled = true;
                    break;
                case SpecialDialogueFeature.fantasyEnd:
                    SceneManager.LoadScene(7);
                    break;
                default:
                    break;
            }

            if(dialogueFeature != SpecialDialogueFeature.fantasyEnd)
            {
                foreach (string text in dialogueLines)
                {
                    StartCoroutine(npcBubbleScript.CreateTextBubble(dialogueName, text, dialogueSprite, textDelay, dialogueSfx));
                    yield return new WaitForSeconds(2f);
                }

                if (dialogueFeature == SpecialDialogueFeature.desir)
                {
                    SceneManager.LoadScene(4);
                }
            }
        }
        proxScript.canPress = true;

        NPCCamera.SetActive(false);
        playerObj.SetActive(true);
        playerCamObj.SetActive(true);
    }
}
