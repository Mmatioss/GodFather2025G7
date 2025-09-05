using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _shakeAmount = 0.1f;
    private float shake = 0f;
    public float Shake { get { return shake; } set { shake = value; } }
    private float decreaseFactor = 1.0f;

    void Update()
    {
        if (shake > 0)
        {
            _camera.transform.localPosition = new Vector3(Random.Range(-1f, 1f) * _shakeAmount, Random.Range(-1f, 1f) * _shakeAmount, _camera.transform.localPosition.z);
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
        }
    }
}
