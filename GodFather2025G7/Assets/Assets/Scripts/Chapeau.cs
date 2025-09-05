using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chapeau : MonoBehaviour
{

    private CircleCollider2D _circleCollider;
    private Rigidbody2D _rb;
    public lancer lancer_player;

    public bool _hasFallen = true;

    public GameObject _hit;

    private GameObject _player;
    public GameObject Player { get => _player; set => _player = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _hasFallen = true;
    }
    private void OnEnable()
    {
        _hasFallen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Vector3.zero, transform.position) > 10) transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Catch"))
        {
            pickUpChap(collision);
        }
    }
    void pickUpChap(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("catchs")) return;
        _hasFallen = false;
        collision.transform.parent.GetComponent<lancer>()._canShoot = true;
        collision.transform.parent.GetComponent<lancer>()._chapInstantiate = gameObject;
        _rb.linearVelocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        collision.gameObject.SetActive(false);
        _player = null;
        print(collision.name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_rb.linearVelocity.magnitude > 1) OnContact(collision);
            else pickUpChap(collision.collider);
        }
    }

    void OnContact(Collision2D collision)
    {
        if (!_hasFallen)
        {
            var mov = Player.GetComponent<MovementScript>();
            if (collision.gameObject.GetComponent<vie>().takeDamage(1))
            {
                if (Compteur.instance.controllers.ContainsKey(mov.PlayerID))
                {
                    Compteur.instance.controllers[mov.PlayerID]++;
                }
                else
                {
                    Compteur.instance.controllers.Add(mov.PlayerID, 1);
                }
            }
            var a = Instantiate(_hit, transform.position, Quaternion.identity);
            a.transform.position += new Vector3(0, 0, -1);
            a.GetComponent<ParticleSystem>().Play();

            _hasFallen = true;
            _rb.linearDamping = 10;
        }
    }
}
