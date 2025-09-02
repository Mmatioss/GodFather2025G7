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
    private bool _canDash = false;



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


        transform.GetChild(0).position = transform.position + _lastDir;
        float theta = Mathf.Tan(_lastDir.y / _lastDir.x);

        print(theta * Mathf.Rad2Deg);
        transform.GetChild(0).LookAt(transform.position);
        transform.GetChild(0).localEulerAngles = new Vector3(transform.GetChild(0).localEulerAngles.x,90, transform.GetChild(0).localEulerAngles.z);



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
        if (_isDashing && _canDash) return;

        _isDashing = true;
        _canDash = false;
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
        yield return new WaitForSeconds(_coolDownDash);
        _canDash = true;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + _lastDir);
    }


}
