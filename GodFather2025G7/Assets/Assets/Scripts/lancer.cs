using UnityEngine;
using System.Collections;

public class lancer : MonoBehaviour
{
    public GameObject ChapPrefab;
    public float LancerMax; //Puissance max du lancer
    public float ForceDistance;
    public bool _CanLancer = false;
    public Collision2D _collision;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _CanLancer = true;
    }

    public void lancerchap(Vector2 dir)
    {
        if (_CanLancer)
        {
            float spawnOffset = 1.5f;
            Vector3 spawnPosition = transform.position + (Vector3)(dir * spawnOffset);

            Rigidbody2D rock = Instantiate(ChapPrefab, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

            rock.AddForce(dir * ForceDistance, ForceMode2D.Impulse);
            _CanLancer = false;
        }
    }

}
