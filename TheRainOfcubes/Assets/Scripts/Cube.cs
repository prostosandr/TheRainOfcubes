using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private Coroutine _coroutine;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _hasCollided;

    public event Action<Renderer, Color> Collided;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private IEnumerator CountDown()
    {
        var wait = new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        while (enabled)
        {
            yield return wait;

            Deactivate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            ChangeParameters();

            _coroutine = StartCoroutine(CountDown());
        }
    }

    private void ChangeParameters()
    {
        _hasCollided = true;

        ChangeColor(Color.red);
    }

    private void Deactivate()
    {
        StopCoroutine(_coroutine);
        ResetParameters();

        gameObject.SetActive(false);
    }

    private void ResetParameters()
    {
        ChangeColor(Color.white);

        _hasCollided = false;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void ChangeColor(Color color)
    {
        Collided?.Invoke(_renderer, color);
    }
}