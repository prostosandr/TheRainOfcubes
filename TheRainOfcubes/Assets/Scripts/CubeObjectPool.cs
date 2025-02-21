using System.Collections.Generic;
using UnityEngine;

public class CubeObjectPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolSize;

    private List<Cube> _pool;

    private void Start()
    {
        _pool = new List<Cube>();

        for (int i = 0; i < _poolSize; i++)
        {
            Cube cube = Instantiate(_prefab);
            cube.gameObject.SetActive(false);

            _pool.Add(cube);
        }
    }

    public Cube GetPooledObject()
    {
        foreach (Cube cube in _pool)
        {
            if (cube.gameObject.activeSelf == false)
                return cube;
        }

        return null;
    }
}