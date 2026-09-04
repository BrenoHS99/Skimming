using System.Collections;
using UnityEngine;

public class TutorialGCtrl : MonoBehaviour
{
    public GameObject txtBubbleSystem;
    private TextBubble txtBubbleScript;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        txtBubbleScript = txtBubbleSystem.GetComponent<TextBubble>();
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
       yield return new WaitForSeconds(1f);
       StartCoroutine(txtBubbleScript.CreateTextBubble("Mova o mouse para mexer a câmera.", 0.05f));
       yield return new WaitForSeconds(4f);
	   StartCoroutine(txtBubbleScript.CreateTextBubble("WASD para andar.", 0.05f));
	}
}
