using UnityEngine;

public class TxtBubbleBoxScript : MonoBehaviour
{
    public string text;
    public float textDelay;
	public bool singleTimeTrigger;
    private bool alreadyTriggered = false;

    public GameObject TxtBubbleSystem;
    private TextBubble txtBubbleScript;

    void Start()
    {
        txtBubbleScript = TxtBubbleSystem.GetComponent<TextBubble>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (singleTimeTrigger)
        {
            if (!alreadyTriggered)
            {
                alreadyTriggered = true;
                StartCoroutine(txtBubbleScript.CreateTextBubble(text, textDelay));
            }
        }
        else
        {
			StartCoroutine(txtBubbleScript.CreateTextBubble(text, textDelay));
		}
	}
}
