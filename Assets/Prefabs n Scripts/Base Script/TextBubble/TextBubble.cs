using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
	public AudioClip dialogueClip;
	private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator CreateTextBubble(string mainText, float textDelay)
    {
		GameObject TextBubbleOBJ = GameObject.Find("TextBubbleGOBJ");
		GameObject canvasObj = GameObject.FindWithTag("Canvas");

		GameObject TxtBubbleInst = Instantiate(TextBubbleOBJ, canvasObj.transform);
		TxtBubbleInst.transform.localPosition = new Vector3(-603.5f, -343.9f, 0);
		GameObject TextFromBubble = TxtBubbleInst.transform.Find("Text").gameObject;
        TextMeshProUGUI txtMeshBubble = TextFromBubble.GetComponent<TextMeshProUGUI>();
        txtMeshBubble.text = "";
		foreach (char c in mainText)
        {
            txtMeshBubble.text = txtMeshBubble.text + c;
            audioSource.PlayOneShot(dialogueClip);
			yield return new WaitForSeconds(textDelay);
		}
		Destroy(TxtBubbleInst, 2f);
    }
}
