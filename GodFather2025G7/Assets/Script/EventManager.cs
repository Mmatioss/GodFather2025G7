using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private float _timeBetweenEvents = 30f;
    [SerializeField] private GameObject _eventUiMask;
    [SerializeField] private GameObject _eventContainer;
    [SerializeField] private List<GameObject> _UIEventsTxt = new List<GameObject>();
    private bool _tryStop = false;
    private enum GameEvent
    {
        Ice,
        Bomb,
        Fire
    }
    void Start()
    {
        StartWheel();
    }

    void StartWheel()
    {
        _eventContainer.GetComponent<Rigidbody2D>().linearVelocityY += -20f;
    }
    void StopWheel()
    {
        while (_tryStop)
        {
            if (_UIEventsTxt[2].transform.position.y - _eventUiMask.transform.position.y > -0.2f && _UIEventsTxt[2].transform.position.y - _eventUiMask.transform.position.y < 0.2f)
            {
                _eventContainer.GetComponent<Rigidbody2D>().linearVelocityY = 0f;
                _tryStop = false;
            }
        }
    }

    void Update()
    {
        if (_UIEventsTxt[4].transform.position.y - _eventUiMask.transform.position.y < -7f)
        {
            GameEvent randomEvent = (GameEvent)Random.Range(0, System.Enum.GetValues(typeof(GameEvent)).Length);
            _UIEventsTxt[4].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = randomEvent.ToString();
            _UIEventsTxt.Insert(0, _UIEventsTxt[4]);
            _UIEventsTxt[0].transform.localPosition = new Vector3(
                _UIEventsTxt[0].transform.localPosition.x,
                _UIEventsTxt[0].transform.localPosition.y + 600f,
                _UIEventsTxt[0].transform.localPosition.z
            );
            _UIEventsTxt.RemoveAt(5);
        }
    }

    void CallEvent(GameEvent randomEvent)
    {
        switch (randomEvent)
        {
            case GameEvent.Ice:
                Debug.Log("Ice Event Triggered");
                break;
            case GameEvent.Bomb:
                Debug.Log("Bomb Event Triggered");
                break;
            case GameEvent.Fire:
                Debug.Log("Fire Event Triggered");
                break;
        }
    }

    private IEnumerator TimerEvent(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
        }
    }
}
