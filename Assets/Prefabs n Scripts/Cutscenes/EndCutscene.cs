using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndCutscene : MonoBehaviour
{
    public int sceneToLoad = 0;

    private VideoPlayer videoPlayer;

    private bool startedVideo;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (videoPlayer.isPlaying && !startedVideo)
        {
            startedVideo = true;
        }
        if (!videoPlayer.isPlaying && startedVideo)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
