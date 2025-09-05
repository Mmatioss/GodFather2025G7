using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FrenesiInverse : ParentEvent
{
    public float reverseFrenesie = 7f;

    public ScriptableRendererFeature rendererFeature;

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
        rendererFeature.SetActive(true);

        yield return new WaitForSeconds(reverseFrenesie);

        var players = FindObjectsByType<MovementScript>(FindObjectsSortMode.None);

        foreach (MovementScript p in players)
        {
            p.reverseFrenesie = false;
        }
        rendererFeature.SetActive(false);

    }

}
