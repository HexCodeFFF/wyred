using UnityEngine;

public class TitleShake : MonoBehaviour
{
    public float shakeSize = 5f;
    public float shakeDuration = 0.05f;
    private Vector3 _lastPos;
    private Vector3 _startPos;
    private Vector3 _target;
    private float _timer;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startPos = _transform.localPosition;
        _lastPos = _startPos;
        RandomTarget();
    }

    private void Update()
    {
        _transform.localPosition = Vector3.Lerp(_lastPos, _target, _timer / shakeDuration);

        _timer += Time.deltaTime;
        if (_timer > shakeDuration)
        {
            _timer = 0;
            _lastPos = _target;
            RandomTarget();
        }
    }

    private void RandomTarget()
    {
        _target = new Vector3(Random.Range(-shakeSize, shakeSize), Random.Range(-shakeSize, shakeSize),
            _startPos.z); // + _startPos;
    }
}