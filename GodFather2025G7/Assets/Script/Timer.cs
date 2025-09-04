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
    [SerializeField] private Camera _camera;
    private float _currentTime;
    private bool _isReduceWall = false;
    private bool _isReduceCamera = false;

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
                if ($"{Mathf.FloorToInt((_timeLimit - _currentTime) / 60):00}:{Mathf.FloorToInt((_timeLimit - _currentTime) % 60):00}" == "01:00")
                {
                    _isReduceWall = true;
                    _isReduceCamera = true;
                    print("Reduce Wall");
                }
            }
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        if (_isReduceWall)
        {
            _wall.transform.localScale = new Vector3(_wall.transform.localScale.x - (0.017f * deltaTime), _wall.transform.localScale.y - (0.017f * deltaTime), _wall.transform.localScale.z);
            if (_wall.transform.localScale.y <= 0.5f)
            {
                _isReduceWall = false;
            }
        }
        if (_isReduceCamera)
        {
            _camera.fieldOfView -= 0.95f * deltaTime;
            if (_camera.fieldOfView <= 30f)
            {
                _isReduceCamera = false;
            }
        }
    }
}
