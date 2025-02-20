using System.Collections.Generic;
using UnityEngine;

public class CubeObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;

    private List<GameObject> _pool;

    private void Start()
    {
        _pool = new List<GameObject>();

        for(int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);

            _pool.Add(obj);
        }
    }

    public GameObject TryGetPooledObject()
    {
        foreach(GameObject obj in _pool)
        {
            if (obj.activeSelf == false)
                return obj;
        }

        return null;
    }
}