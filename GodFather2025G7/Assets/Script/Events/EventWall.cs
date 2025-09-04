using System.Collections;
using UnityEngine;

public class EventWall : ParentEvent
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private float _timeEnable = 5f;
    void DoEvent()
    {
        _wall.SetActive(true);
        StartCoroutine(WaitAndDisable(_timeEnable));
    }

    IEnumerator WaitAndDisable(float time)
    {
        yield return new WaitForSeconds(time);
        _wall.SetActive(false);
    }
}
