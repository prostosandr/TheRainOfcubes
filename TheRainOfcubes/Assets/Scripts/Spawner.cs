using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Collider _startPosition;
    [SerializeField] private CubeObjectPool _pool;
    [SerializeField] private float _spawnInterval;

    private void Start()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        Cube cube = _pool.GetPooledObject();
        cube.transform.position = GetSpawnPosition();
        cube.gameObject.SetActive(true);

    }

    private Vector3 GetSpawnPosition()
    {
        Bounds bound = _startPosition.bounds;

        return new Vector3(
            Random.Range(bound.min.x, bound.max.x),
            Random.Range(bound.min.y, bound.max.y),
            Random.Range(bound.min.z, bound.max.z)
            );
    }
}