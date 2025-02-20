using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private Coroutine _coroutine;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _hasCollided;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ResetParameters();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(_hasCollided == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            SetParameters();

            _coroutine = StartCoroutine(CountDown());
        }
    }

    private void SetParameters()
    {
        _hasCollided = true;

        ChangeColor(Color.red);
    } 

    private void Deactivate()
    {
        StopCoroutine(_coroutine);

        gameObject.SetActive(false);
    }

    private void ResetParameters()
    {
        _hasCollided = false;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;

        ChangeColor(Color.white);
    }

    private void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }
}