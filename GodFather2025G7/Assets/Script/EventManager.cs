using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private float _timeWheelSpin = 10f;
    [SerializeField] private float _wheelSpeed = 30f;
    [SerializeField] private float _wheelShowTime = 3f;
    [SerializeField] private GameObject _eventUiMask;
    [SerializeField] private GameObject _eventContainer;
    [SerializeField] private List<EventData> _gameObjectEvents;
    [SerializeField] private List<GameObject> _UIEventsTxt = new List<GameObject>();
    private List<EventData> _eventData = new List<EventData>();

    private bool _tryStop = false;
    private enum TimerEventType
    {
        Start,
        Show
    }
    [System.Serializable]
    private class EventData
    {
        public string name;
        public int probability;
        public GameObject eventObject;
        public string message;
    }
    void Start()
    {
        SetRandomListEvent();
        _eventUiMask.SetActive(false);
    }

    void Update()
    {
        if (_tryStop) // Try to stop the wheel
        {
            if (_UIEventsTxt[2].transform.position.y - _eventUiMask.transform.position.y > 0f && _UIEventsTxt[2].transform.position.y - _eventUiMask.transform.position.y < 320f)
            {
                _eventContainer.GetComponent<Rigidbody2D>().linearVelocityY = 0f;
                _tryStop = false;
                CallEvent(SearchEventByName(_UIEventsTxt[2].GetComponentInChildren<TMPro.TextMeshProUGUI>().text));
                StartCoroutine(TimerEvent(_wheelShowTime, TimerEventType.Show));
            }
            else
            {
                print(_UIEventsTxt[2].transform.position.y - _eventUiMask.transform.position.y);
            }
        }

        if (_UIEventsTxt[4].transform.position.y - _eventUiMask.transform.position.y < -300f) // Recycle the text objects
        {
            EventData randomEvent = GetRandomEvent();
            _UIEventsTxt[4].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = randomEvent.name;
            _UIEventsTxt.Insert(0, _UIEventsTxt[4]);
            _UIEventsTxt[0].transform.localPosition = new Vector3(
                _UIEventsTxt[0].transform.localPosition.x,
                _UIEventsTxt[0].transform.localPosition.y + 600f,
                _UIEventsTxt[0].transform.localPosition.z
            );
            _UIEventsTxt.RemoveAt(5);
        }
    }

    public void StartWheel()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        _eventUiMask.SetActive(true);
        _eventContainer.GetComponent<Rigidbody2D>().linearVelocityY += -_wheelSpeed;
        StartCoroutine(TimerEvent(_timeWheelSpin, TimerEventType.Start));
    }

    void CallEvent(EventData eventData)
    {
        if (eventData.message != "")
        {
            eventData.eventObject.GetComponent<ParentEvent>().SendMessage("DoEvent", eventData.message);
            return;
        }
        else
        {
            eventData.eventObject.GetComponent<ParentEvent>().SendMessage("DoEvent");
        }
    }

    EventData GetRandomEvent()
    {
        int index = Random.Range(0, _eventData.Count);
        return _eventData[index];
    }

    EventData SearchEventByName(string name)
    {
        foreach (EventData eventData in _gameObjectEvents)
        {
            if (eventData.name == name)
            {
                return eventData;
            }
        }
        return null;
    }

    void SetRandomListEvent()
    {
        foreach (EventData eventData in _gameObjectEvents)
        {
            for (int i = 0; i < eventData.probability; i++)
            {
                _eventData.Add(eventData);
            }
        }
    }

    private IEnumerator TimerEvent(float time, TimerEventType type)
    {
        yield return new WaitForSeconds(time);
        switch (type)
        {
            case TimerEventType.Start:
                _tryStop = true;
                break;
            case TimerEventType.Show:
                _eventUiMask.SetActive(false);
                break;
        }
    }
}
