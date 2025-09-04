using System.Collections;
using UnityEngine;

public class RockController : MonoBehaviour
{

    private CircleCollider2D _circleCollider;
    private Rigidbody2D _rb;
    public lancer lancer_player;


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
            collision.transform.parent.GetComponent<lancer>()._CanLancer = true;
            Destroy(gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Mathf.Abs(_rb.linearVelocityX) > 10)
            {
                _rb.linearVelocityX /= Mathf.Abs(_rb.linearVelocityX);
                _rb.linearVelocityX *= 10;
            }
            Destroy(gameObject);
        }

    }
}
