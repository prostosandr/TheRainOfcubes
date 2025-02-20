using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _startPosition;
    [SerializeField] private CubeObjectPool _pool;
    [SerializeField] private float _spawnInterval;

    private float _nextSpawnTime;

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            Spawn();
            _nextSpawnTime = Time.time + _spawnInterval;
        }
    }

    private void Spawn()
    {
        GameObject cube = _pool.TryGetPooledObject();

        if (cube != null)
        {
            _startPosition.TryGetComponent(out Collider startCollider);

            if (startCollider != null)
            {
                cube.transform.position = GetSpawnPosition(startCollider);
                cube.SetActive(true);
            }
        }
    }

    private Vector3 GetSpawnPosition(Collider collider)
    {
        Bounds bound = collider.bounds;

        return new Vector3(
            Random.Range(bound.min.x, bound.max.x),
            Random.Range(bound.min.y, bound.max.y),
            Random.Range(bound.min.z, bound.max.z)
            );
    }
}
