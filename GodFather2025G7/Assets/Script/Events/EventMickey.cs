using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EventMickey : ParentEvent
{
    [SerializeField] private GameObject _mickey;
    [SerializeField] private float _timeEnable = 5f;
    [SerializeField] private float _speed = 2f;
    private bool _isActive = false;
    private Vector3 _mickeySpawnTransform;

    void Start()
    {
        _mickeySpawnTransform = _mickey.transform.position;
    }

    void DoEvent()
    {
        _mickey.SetActive(true);
        _isActive = true;
        StartCoroutine(WaitAndDisable(_timeEnable));
        StartCoroutine(PlayAudioWithDelay(1.3f));
    }

    void Update()
    {
        if (_isActive)
        {
            _mickey.transform.position = new Vector3(_mickey.transform.position.x + (_speed * Time.deltaTime), _mickey.transform.position.y, _mickey.transform.position.z);
            _mickey.transform.Rotate(0f, 0f, -360f * Time.deltaTime);
        }
    }

    IEnumerator WaitAndDisable(float time)
    {
        yield return new WaitForSeconds(time);
        _mickey.transform.position = _mickeySpawnTransform;
        _mickey.SetActive(false);
        _isActive = false;
    }

    IEnumerator PlayAudioWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        print("Play Sound");
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
