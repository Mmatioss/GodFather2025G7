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
    private bool _isDashing = false;
    private bool _canDash = true;



    private Vector3 _lastDir = Vector3.right;

    LineRenderer _linerenderer;
    void Start()
    {
        _linerenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (_isDashing) _linerenderer.SetPositions(new Vector3[2] { transform.position, transform.position + _lastDir * 2 });
    }
    public void OnMovement(InputAction.CallbackContext c)
    {
        if (!c.performed) return;
        if (_isDashing) return;

        Vector2 dir = c.ReadValue<Vector2>();
        _lastDir = dir == Vector2.zero ? _lastDir : dir.normalized;

        if (IsSpeedNormalized) dir = dir.normalized;

        _linerenderer.SetPositions(new Vector3[2] { transform.position + Vector3.forward, transform.position + _lastDir * 2 });

        transform.position += new Vector3(dir.x, dir.y) * (_playerSpeed * Time.deltaTime);
    }

    public void OnShoot(InputAction.CallbackContext c)
    {
        if (!c.performed) return;
        if (_isDashing) return;

        print("SHHHHHHHOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOT");
    }

    public void OnDash(InputAction.CallbackContext c)
    {
        if (!c.performed) return;
        if (_isDashing) return;
        if (!_canDash) return;
        print("4");

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
}
