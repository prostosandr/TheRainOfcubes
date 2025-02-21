using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubePainter _cubePainter;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private Coroutine _coroutine;
    private Rigidbody _rigidbody;
    private bool _hasCollided;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            ChangeParameters();

            _coroutine = StartCoroutine(CountDown());
        }
    }

    private IEnumerator CountDown()
    {
        var wait = new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));

        while (enabled)
        {
            yield return wait;

            Deactivate();
        }
    }

    private void ChangeParameters()
    {
        _hasCollided = true;

        _cubePainter.ChangeColor();
    }

    private void Deactivate()
    {
        StopCoroutine(_coroutine);
        ResetParameters();

        gameObject.SetActive(false);
    }

    private void ResetParameters()
    {
        _cubePainter.ResetColor();

        _hasCollided = false;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}