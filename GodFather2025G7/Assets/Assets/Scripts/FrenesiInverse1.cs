using System.Collections;
using UnityEngine;

public class FrenesiInverse : ParentEvent
{
    public float reverseFrenesie = 7f;
    public void DoEvent()
    {
        var players = FindObjectsByType<MovementScript>(FindObjectsSortMode.None);

        foreach(MovementScript p in players)
        {
            p.reverseFrenesie = true;
        }


        StartCoroutine(endEvent());

    }

    IEnumerator endEvent()
    {
        yield return new WaitForSeconds(reverseFrenesie);

        var players = FindObjectsByType<MovementScript>(FindObjectsSortMode.None);

        foreach (MovementScript p in players)
        {
            p.reverseFrenesie = false;
        }
    }

}
