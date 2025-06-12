using UnityEngine;
using UnityEngine.Video;

public class videoPlayerScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool reverse = false;

    private void Start()
    {
        videoPlayer.isLooping = false;
        videoPlayer.Play();
    }

    private void Update()
    {
        if (!videoPlayer.isPlaying && !reverse)
        {
            reverse = true;
            videoPlayer.playbackSpeed = -1f;
            videoPlayer.Play();
        }else if(!videoPlayer.isPlaying && reverse)
        {
            reverse = false;
            videoPlayer.playbackSpeed = 1f;
            videoPlayer.Play();
        }


    }
}
