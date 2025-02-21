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
        int firstElement = 0;
        Cube cube = null;

        foreach (Cube obj in _pool)
        {
            if (obj.gameObject.activeSelf == false)
            {
                cube = obj;
                break;
            }
        }

        if (cube == null)
            cube = _pool[Random.Range(firstElement, _poolSize)];

        return cube;
    }
}