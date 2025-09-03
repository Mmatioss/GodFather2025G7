using System.Collections;
using UnityEngine;


public class EventIrl : ParentEvent
{
    [SerializeField] private GameObject _irlObject;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    public void DoEvent(string txt)
    {
        Debug.Log("Event Irl");
        SetText(txt);
        StartCoroutine(ActiveGO());
    }

    void SetText(string txt)
    {
        _text.text = txt;
    }

    IEnumerator ActiveGO()
    {
        _irlObject.SetActive(true);
        yield return new WaitForSeconds(8f);
        _irlObject.SetActive(false);
    }
}
