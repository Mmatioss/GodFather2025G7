using System.Collections;
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


    private Vector3 _lastDir = Vector3.right;
    void Start()
    {

    }

    public void OnMovement(InputAction.CallbackContext c)
    {
        if (_isDashing) return;
        if (!c.performed) return;


        Vector2 dir = c.ReadValue<Vector2>();
        if (IsSpeedNormalized) dir = dir.normalized;

        _lastDir = dir == Vector2.zero ? _lastDir : dir.normalized;

        print(dir + " // " + _playerSpeed * Time.deltaTime);

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

        _isDashing = true;
        StartCoroutine(DoDash());
    }

    IEnumerator DoDash()
    {
        float timeSinceStart = 0;
        while (timeSinceStart <= _dashDuration)
        {
            print("isDashin'");
            timeSinceStart += Time.deltaTime;

            transform.position += _lastDir * (_dashPower * Time.deltaTime);

            yield return null;
        }

        _isDashing = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + _lastDir);
    }

}
