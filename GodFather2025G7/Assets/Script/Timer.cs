using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 240f;
    [SerializeField] private EventManager _eventManager;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _wall;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<GameObject> _wallProtection = new List<GameObject>();
    [SerializeField] private List<Vector3> _spawnPointBallons = new List<Vector3>();
    [SerializeField] private GameObject _goodBallonPrefab;
    [SerializeField] private GameObject _badBallonPrefab;
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
                SpawnBallon();
                if ($"{Mathf.FloorToInt((_timeLimit - _currentTime) / 60):00}:{Mathf.FloorToInt((_timeLimit - _currentTime) % 60):00}" == "01:00")
                {
                    _isReduceWall = true;
                    _isReduceCamera = true;
                    print("Reduce Wall");
                }
            }
            if (_currentTime % 20 == 0)
            {
                _eventManager.StartWheel();
            }
        }
    }

    void SpawnBallon()
    {
        int randomNbx = Random.Range((int)_spawnPointBallons[0].x, (int)_spawnPointBallons[1].x);
        int randomNby = Random.Range((int)_spawnPointBallons[0].y, (int)_spawnPointBallons[1].y);
        Vector3 spawnPosition = new Vector3(randomNbx, randomNby, 0);
        if (Random.Range(0, 2) == 0)
            Instantiate(_goodBallonPrefab, spawnPosition, Quaternion.identity);
        else
        Instantiate(_badBallonPrefab, spawnPosition, Quaternion.identity);
    
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        if (_isReduceWall)
        {
            _wall.transform.localScale = new Vector3(_wall.transform.localScale.x - (0.017f * deltaTime), _wall.transform.localScale.y - (0.017f * deltaTime), _wall.transform.localScale.z);
            for (int i = 0; i < _wallProtection.Count; i++)
            {
                _wallProtection[i].transform.localScale = new Vector3(_wallProtection[i].transform.localScale.x - (0.03f * deltaTime), _wallProtection[i].transform.localScale.y - (0.03f * deltaTime), _wallProtection[i].transform.localScale.z);
            }
            if (_wall.transform.localScale.y <= 0.5f)
            {
                _isReduceWall = false;
            }
        }
        if (_isReduceCamera)
        {
            _camera.orthographicSize -= 0.085f * deltaTime;
            if (_camera.orthographicSize <= 2.4f)
            {
                _isReduceCamera = false;
            }
        }
    }
}
