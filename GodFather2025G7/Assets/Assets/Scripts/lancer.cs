using UnityEngine;
using System.Collections;

public class lancer : MonoBehaviour
{
    public GameObject ChapPrefab;

    public GameObject _chapInstantiate;

    public float LancerMax; //Puissance max du lancer
    public float ForceDistance;
    public bool _canShoot = false;
    public Collision2D _collision;

    public MovementScript _mov;

    private void Start()
    {
        _mov = GetComponent<MovementScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _canShoot = true;
    }

    public void lancerchap(Vector2 dir)
    {
        if (_canShoot)
        {
            float spawnOffset = 1.5f;
            Vector3 spawnPosition = transform.position + (Vector3)(dir * spawnOffset);

            Rigidbody2D rock =_chapInstantiate.GetComponent<Rigidbody2D>();
            rock.constraints = RigidbodyConstraints2D.None;
            rock.linearDamping = 0f;
            rock.AddForce(dir * ForceDistance, ForceMode2D.Impulse);
            _canShoot = false;
            _chapInstantiate = null;
        }
    }
    private void Update()
    {
        if (_chapInstantiate) _chapInstantiate.transform.position = transform.position + _mov._lastDir;
    }
}
