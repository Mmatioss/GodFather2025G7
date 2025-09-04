using System.Collections;
using UnityEngine;

public class Chapeau : MonoBehaviour
{

    private CircleCollider2D _circleCollider;
    private Rigidbody2D _rb;
    public lancer lancer_player;

    public bool _hasFallen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

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
        _hasFallen = false;
        collision.transform.parent.GetComponent<lancer>()._canShoot = true;
        collision.transform.parent.GetComponent<lancer>()._chapInstantiate = gameObject;
        _rb.linearVelocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        collision.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_rb.linearVelocity.magnitude > 1) OnContact();
            else pickUpChap(collision.collider);
        }
    }

    void OnContact()
    {
        if (!_hasFallen)
        {
            _hasFallen = true;
            _rb.linearDamping = 10;
        }
    }
}
