using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Collider _startPosition;
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _spawnInterval;

    private void Start()
    {
        StartCoroutine(SpawnTime());
    }

    private IEnumerator SpawnTime()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            if (_cubePool.NumberOfCubes < _cubePool.PoolCapacity)
                Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        Cube cube = _cubePool.GetCube();
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