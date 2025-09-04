using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 240f;
    [SerializeField] private EventManager _eventManager;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _wall;
    private float _currentTime;

    void Start()
    {
        UpdateTimerText();
        StartCoroutine(TimerGame());
        _gameOverPanel.SetActive(false);
    }

    void UpdateTimerText()
    {
        _timerText.text = $"{Mathf.FloorToInt((_timeLimit - _currentTime) / 60):00}:{Mathf.FloorToInt((_timeLimit - _currentTime) % 60):00}";
        if (_currentTime >= _timeLimit)
        {
            _timerText.text = "00:00";
        }
    }

    void EndGame()
    {
        print("Game Over");
        _gameOverPanel.SetActive(true);
    }


    private IEnumerator TimerGame()
    {
        while (_currentTime < _timeLimit)
        {
            yield return new WaitForSeconds(1f);
            _currentTime += 1f;
            UpdateTimerText();
            if ($"{Mathf.FloorToInt((_timeLimit - _currentTime) / 60):00}:{Mathf.FloorToInt((_timeLimit - _currentTime) % 60):00}" == "00:00")
            {
                EndGame();
            }
            else if ((Mathf.FloorToInt((_timeLimit - _currentTime) % 60) == 0) || (Mathf.FloorToInt((_timeLimit - _currentTime) % 60) == 30))
            {
                _eventManager.StartWheel();
                if ($"{Mathf.FloorToInt((_timeLimit - _currentTime) / 60):00}:{Mathf.FloorToInt((_timeLimit - _currentTime) % 60):00}" == "00:30")
                {
                    ReduceWall();
                }
            }
        }
    }
    
    void ReduceWall()
    {
        print("Wall is moving");
        _wall.GetComponent<Animation>().Play();
    }
}
