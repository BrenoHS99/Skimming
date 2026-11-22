using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NpcBubbleScript : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator CreateTextBubble(string name, string mainText, Sprite dialogueSprite, float textDelay, AudioClip dialogueSfx)
    {
        GameObject TextBubbleOBJ = GameObject.Find("TextBubbleGOBJ");
        GameObject canvasObj = GameObject.FindWithTag("Canvas");

        GameObject TxtBubbleInst = Instantiate(TextBubbleOBJ, canvasObj.transform);
        TxtBubbleInst.transform.localPosition = new Vector3(-249, -303.9f, 0);

        GameObject TextFromBubble = TxtBubbleInst.transform.Find("Text").gameObject;
        GameObject NameFromBubble = TxtBubbleInst.transform.Find("Name").gameObject;
        GameObject SpriteFromBubble = TxtBubbleInst.transform.Find("Sprite").gameObject;

        TextMeshProUGUI txtMeshBubble = TextFromBubble.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nameMeshBubble = NameFromBubble.GetComponent<TextMeshProUGUI>();
        Image imageBubble = SpriteFromBubble.GetComponent<Image>();

        txtMeshBubble.text = "";
        nameMeshBubble.text = name;
        imageBubble.sprite = dialogueSprite;

        foreach (char c in mainText)
        {
            txtMeshBubble.text = txtMeshBubble.text + c;
            audioSource.PlayOneShot(dialogueSfx);
            yield return new WaitForSeconds(textDelay);
        }

        Destroy(TxtBubbleInst, 2f);
    }
}
