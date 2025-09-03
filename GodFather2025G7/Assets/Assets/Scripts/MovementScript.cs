using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public bool IsSpeedNormalized = false;
    [SerializeField] float _playerSpeed = 50f;

    [SerializeField] float _dashPower = 50f;
    [SerializeField] float _dashDuration = 1f;
    [SerializeField] float _coolDownDash = 2;
    [Space(50),SerializeField] float TimeToCatch = 1;


    private bool _isDashing = false;
    private bool _canDash = true;
    private bool _canCatch = true;

    BoxCollider2D Catcher;


    public Vector3 _lastDir = Vector3.right;

    LineRenderer _linerenderer;

    lancer _lancer;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _linerenderer = GetComponent<LineRenderer>();
        _lancer = GetComponent<lancer>();
        Catcher = GetComponentInChildren<BoxCollider2D>();
        Catcher.gameObject.SetActive(false);
    }
    private void Update()
    {
        _linerenderer.SetPositions(new Vector3[2] { transform.position + Vector3.forward, transform.position + _lastDir * 2 });

    }
    public void OnMovement(InputAction.CallbackContext c)
    {
        if (!c.performed) return;
        if (_isDashing) return;

        Vector2 dir = c.ReadValue<Vector2>();
        _lastDir = dir == Vector2.zero ? _lastDir : dir.normalized;

        if (IsSpeedNormalized) dir = dir.normalized;


        _rb.linearVelocity = (new Vector3(dir.x, dir.y) *_playerSpeed);
    }

    public void OnShoot(InputAction.CallbackContext c)
    {
        if (!_lancer) return;
        if (!c.performed) return;
        if (_isDashing) return;


        if(_lancer._chapInstantiate)
            _lancer.lancerchap(_lastDir);
        else if(_canCatch)
        {
            StartCoroutine(CatchTime());
        }
    }

    IEnumerator CatchTime()
    {
        _canCatch = false;
        Catcher.gameObject.SetActive(true);
        yield return new WaitForSeconds(TimeToCatch);
        Catcher.gameObject.SetActive(false);
        _canCatch = true;
    }
    public void OnDash(InputAction.CallbackContext c)
    {
        if (!c.performed) return;
        if (_isDashing) return;
        if (!_canDash) return;

        _isDashing = true;
        _canDash = false;
        StartCoroutine(DoDash());   
    }

    IEnumerator DoDash()
    {
        float timeSinceStart = 0;
        while (timeSinceStart <= _dashDuration)
        {
            timeSinceStart += Time.deltaTime;

            transform.position += _lastDir * (_dashPower * Time.deltaTime);

            yield return null;
        }

        _isDashing = false;
        yield return new WaitForSeconds(_coolDownDash);
        print("HAYEEEEEEEEEE");
        _canDash = true;
    }

    public void OnStart(InputAction.CallbackContext c)
    {
        if (c.performed)
        {
            PlayerManager p = FindAnyObjectByType<PlayerManager>();
            p.StartGame();
        }
    }
}
