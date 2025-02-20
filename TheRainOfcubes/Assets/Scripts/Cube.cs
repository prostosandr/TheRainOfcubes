using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))] 
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;
    
    private bool _hasCollided;
    private float _lifeTime;

    public Renderer Renderer => GetComponent<Renderer>();
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();

    private void OnEnable()
    {
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_hasCollided == false)
        {
            _hasCollided = true;
            _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);

            ChangeColor(Color.red);
            Invoke(nameof(Deactivate), _lifeTime);
        } 
    }

    private void ChangeColor(Color color)
    {
        Renderer.material.color = color;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void Init()
    {
        _hasCollided = false;
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;

        ChangeColor(Color.white);
    }
}
