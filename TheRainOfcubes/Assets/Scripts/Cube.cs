using System;
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

    public event Action<Cube> Deactivated;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            Init();

            _coroutine = StartCoroutine(Lifespan());
        }
    }

    private IEnumerator Lifespan()
    {
        var wait = new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        yield return wait;

        Deactivate();
    }

    private void Init()
    {
        _hasCollided = true;

        _cubePainter.ChangeColor();
    }

    private void Deactivate()
    {
        StopCoroutine(_coroutine);
        ResetParameters();

        Deactivated?.Invoke(this);
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