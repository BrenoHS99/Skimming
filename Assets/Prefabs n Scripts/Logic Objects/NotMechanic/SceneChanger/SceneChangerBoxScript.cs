using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerBoxScript : MonoBehaviour
{
    public int sceneId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneId);
        }
    }
}
