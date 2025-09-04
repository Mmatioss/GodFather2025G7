using UnityEngine;
using UnityEngine.Video;

public class EventSqueezie : ParentEvent
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject _squeezieObject;

    void Start()
    {
        _squeezieObject.SetActive(false);
        _videoPlayer.loopPointReached += OnVideoEnd;
    }
    public void DoEvent()
    {
        Debug.Log("Event Squeezie");
        _squeezieObject.SetActive(true);
        _videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        _squeezieObject.SetActive(false);
    }
}
