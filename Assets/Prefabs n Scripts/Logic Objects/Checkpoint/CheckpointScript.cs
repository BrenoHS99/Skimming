using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public bool alreadyTouched = false;
    public float checkpointSequence; // the sequence of checkpoints in the level

    Renderer checkpointRender;

    public GameObject bookModel;
    private Animator bookAnimator;

    private void Start()
    {
        checkpointRender = gameObject.GetComponent<Renderer>();
        checkpointRender.material.color = Color.red;

        bookAnimator = bookModel.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (checkpointSequence > playerController.currentCheckpointSequence)
            {
                // sets the checkpoint when touched if this checkpoint position is higher than previous touched by player
                alreadyTouched = true;
                bookAnimator.SetBool("claimed", true);
                playerController.SetCheckpoint(this.gameObject, checkpointSequence);

                checkpointRender.material.color = Color.blue;
            }
        }
    }
}
