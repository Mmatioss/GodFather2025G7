using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilleChapeu : ParentEvent
{
    public float time = 7f;

    public int nbDeChapeu = 23;

    public GameObject Chapeau;

    List<GameObject> instantiated = new();
    
    public void DoEvent()
    {
        instantiated.Clear();

        for (int i = 0; i < nbDeChapeu; i++)
        {
            Vector3 a = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5));
            instantiated.Add(Instantiate(Chapeau, a, Quaternion.identity));
        }

        StartCoroutine(endEvent());

    }

    IEnumerator endEvent()
    {
        yield return new WaitForSeconds(time);
        for(int i = nbDeChapeu-1; i > -1; i--)
        {
            Destroy(instantiated[i], Random.Range(0,1));
        }


    }

}
